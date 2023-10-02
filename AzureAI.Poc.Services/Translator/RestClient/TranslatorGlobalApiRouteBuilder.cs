using AzureAI.Poc.Services.Api.Translator.Contracts;
using AzureAI.Poc.Services.Api.Translator.Model;
using Microsoft.Extensions.Options;
using System.Text;

namespace AzureAI.Poc.Services.Api.Translator.RestClient;

public class TranslatorGlobalApiRouteBuilder : ITranslatorApiRouteBuilder
{
    private readonly TranslatorApiOptions _options;

    public TranslatorGlobalApiRouteBuilder(IOptions<TranslatorApiOptions> options)
    {
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }
        _options = options.Value;
    }

    public string BuildLanguagesRoute()
    {
        var queryString = $"?api-version={_options.ApiVersion}";
        return $"{_options.LanguagesRoute}{queryString}";
    }

    public string BuildTranslateRoute(TranslateRequest request)
    {
        var queryString = new StringBuilder();

        queryString.Append($"?api-version={_options.ApiVersion}");

        if (!string.IsNullOrWhiteSpace(request.FromLanguage))
        {
            queryString.Append($"&from={request.FromLanguage}");
        }
        
        queryString.Append($"&to={string.Join("&to=", request.ToLanguages)}");
        
        if(!string.IsNullOrWhiteSpace(request.TextType))
        {
            queryString.Append($"&textType={request.TextType}");
        }

        if(!string.IsNullOrWhiteSpace(request.FromScript))
        {
            queryString.Append($"&fromScript={request.FromScript}");
        }

        if (!string.IsNullOrWhiteSpace(request.ToScript))
        {
            queryString.Append($"&toScript={request.ToScript}");
        }

        if(request.IncludeAlignment ?? false)
        {
            queryString.Append($"&includeAlignment=true");
        }
        
        if (request.IncludeSentenceLength ?? false)
        {
            queryString.Append($"&includeSentenceLength=true");
        }

        return $"{_options.TranslateRoute}{queryString}";
    }

    public string BuildDetectRoute()
    {
        var queryString = $"?api-version={_options.ApiVersion}";
        return $"{_options.DetectRoute}{queryString}";
    }

    public string BuildTransliterateRoute(TransliterateRequest request)
    {
        var queryString = $"?api-version={_options.ApiVersion}&language={request.Language}&fromScript={request.FromScript}&toScript={request.ToScript}";
        return $"{_options.TransliterateRoute}{queryString}";
    }
}
