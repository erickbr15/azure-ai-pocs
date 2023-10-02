using AzureAI.Poc.Services.Api.Translator.Model;

namespace AzureAI.Poc.Services.Api.Translator.Contracts;

public interface ITranslatorRestClient
{
    Task<string> GetLanguagesAsync(CancellationToken cancellationToken);
    Task<string> DetectAsync(IEnumerable<DetectRequest> request, CancellationToken cancellationToken);
    Task<string> TranslateAsync(TranslateRequest request, CancellationToken cancellationToken);
    Task<string> TransliterateAsync(TransliterateRequest request, CancellationToken cancellationToken);
}
