namespace AzureAI.Poc.Services.Api.Translator.Model;

public class TranslationResult
{
    public DetectedLanguage DetectedLanguage { get; set; } = default!;
    public TextResult SourceText { get; set; } = default!;
    public Translation[] Translations { get; set; } = default!;
}
