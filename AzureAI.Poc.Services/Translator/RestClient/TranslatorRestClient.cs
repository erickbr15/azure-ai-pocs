using AzureAI.Poc.Services.Api.Common;
using AzureAI.Poc.Services.Api.Common.Contracts;
using AzureAI.Poc.Services.Api.Translator.Contracts;
using AzureAI.Poc.Services.Api.Translator.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace AzureAI.Poc.Services.Api.Translator.RestClient;

public class TranslatorRestClient : ITranslatorRestClient
{
    private readonly AiServicesOptions _aiServicesOptions;
    private readonly CognitiveServiceOptions _cognitiveServiceOptions;
    private readonly ITranslatorApiRouteBuilder _routeBuilder;
    private readonly IHttpProxy _httpProxy;

    public TranslatorRestClient(IOptions<AiServicesOptions> aiServicesOptions, 
        IOptions<CognitiveServiceOptions> cognitiveServiceOptions, 
        ITranslatorApiRouteBuilder translatorApiRoute, 
        IHttpProxy httpProxy)
    {
        _aiServicesOptions = aiServicesOptions?.Value ?? throw new ArgumentNullException(nameof(aiServicesOptions));
        _cognitiveServiceOptions = cognitiveServiceOptions?.Value ?? throw new ArgumentNullException(nameof(cognitiveServiceOptions));
        _routeBuilder = translatorApiRoute ?? throw new ArgumentNullException(nameof(translatorApiRoute));
        _httpProxy = httpProxy ?? throw new ArgumentNullException(nameof(httpProxy));
    }

    public async Task<string> GetLanguagesAsync(CancellationToken cancellationToken)
    {
        var route = _routeBuilder.BuildLanguagesRoute();
        var response = await GetAsync(route, cancellationToken);

        return response;
    }

    public async Task<string> DetectAsync(IEnumerable<DetectRequest> request, CancellationToken cancellationToken)
    {
        var route = _routeBuilder.BuildDetectRoute();
        var body = request.Select(r => new TranslatorRequestBody { Text = r.Text }).ToArray();
                
        var response = await PostAsync(route, body, cancellationToken);
        
        return response;
    }

    public async Task<string> TranslateAsync(TranslateRequest request, CancellationToken cancellationToken)
    {
        var route = _routeBuilder.BuildTranslateRoute(request);
        var body = request.Text.Select(t => new TranslatorRequestBody { Text = t }).ToArray();

        var response = await PostAsync(route, body, cancellationToken);

        return response;
    }

    public async Task<string> TransliterateAsync(TransliterateRequest request, CancellationToken cancellationToken)
    {
        var route = _routeBuilder.BuildTransliterateRoute(request);
        var body = new TranslatorRequestBody[] { new TranslatorRequestBody { Text = request.Text } };

        var response = await PostAsync(route, body, cancellationToken);

        return response;
    }

    private async Task<string> GetAsync(string route, CancellationToken cancellationToken) 
    {
        var uri = new Uri($"{_aiServicesOptions.Translator.GlobalEndpoint}{route}");

        var headers = new Dictionary<string, string>
        {
            { "Ocp-Apim-Subscription-Key", _cognitiveServiceOptions.ApiKey },
            { "Ocp-Apim-Subscription-Region", _cognitiveServiceOptions.Location }
        };

        var result = await _httpProxy.GetAsync(uri, headers, cancellationToken);        
        var resultText = await result.Content.ReadAsStringAsync(cancellationToken);

        return resultText;
    }

    private async Task<string> PostAsync(string route, object body, CancellationToken cancellationToken)
    {        
        var uri = new Uri($"{_aiServicesOptions.Translator.GlobalEndpoint}{route}");
        var bodyContent = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

        var headers = new Dictionary<string, string>
        {
            { "Ocp-Apim-Subscription-Key", _cognitiveServiceOptions.ApiKey },
            { "Ocp-Apim-Subscription-Region", _cognitiveServiceOptions.Location }
        };

        var response = await _httpProxy.PostAsync(uri, headers, bodyContent, cancellationToken);
        var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

        return responseText;
    }
}
