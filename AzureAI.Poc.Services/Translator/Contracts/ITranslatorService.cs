using AzureAI.Poc.Services.Api.Translator.Model;

namespace AzureAI.Poc.Services.Api.Translator.Contracts;

public interface ITranslatorService
{
    Task<string> GetLanguagesAsync(CancellationToken cancellationToken = default);
    Task<DetectResult[]?> DetectAsync(IEnumerable<DetectRequest> request, CancellationToken cancellationToken = default);
    Task<TranslationResult[]?> TranslateAsync(TranslateRequest request, CancellationToken cancellationToken = default);
    Task<TransliterationResult[]?> TransliterateAsync(TransliterateRequest request, CancellationToken cancellationToken = default);
}
