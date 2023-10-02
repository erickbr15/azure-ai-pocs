using AzureAI.Poc.Services.Api.Translator.Contracts;
using AzureAI.Poc.Services.Api.Translator.Model;
using Newtonsoft.Json;

namespace AzureAI.Poc.Services.Api.Translator;

public class TranslatorService : ITranslatorService
{
    private readonly ITranslatorRestClient _translatorRestClient;

    public TranslatorService(ITranslatorRestClient translatorRestClient)
    {        
        _translatorRestClient = translatorRestClient ?? throw new ArgumentNullException(nameof(translatorRestClient));
    }

    public async Task<string> GetLanguagesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _translatorRestClient.GetLanguagesAsync(cancellationToken);
        return result;
    }   

    public async Task<DetectResult[]?> DetectAsync(IEnumerable<DetectRequest> request, CancellationToken cancellationToken = default)
    {
        var detectResult = await _translatorRestClient.DetectAsync(request, cancellationToken);
        var result = JsonConvert.DeserializeObject < DetectResult[]>(detectResult);

        return result;
    }

    public async Task<TranslationResult[]?> TranslateAsync(TranslateRequest request, CancellationToken cancellationToken = default)
    {
        var translateResult = await _translatorRestClient.TranslateAsync(request, cancellationToken);
        var result = JsonConvert.DeserializeObject<TranslationResult[]>(translateResult);

        return result;
    }

    public async Task<TransliterationResult[]?> TransliterateAsync(TransliterateRequest request, CancellationToken cancellationToken = default)
    {
        var transliterateResult = await _translatorRestClient.TransliterateAsync(request, cancellationToken);
        var result = JsonConvert.DeserializeObject<TransliterationResult[]>(transliterateResult);
        
        return result;
    }
}
