namespace AzureAI.Poc.Services.Api.Translator.Model;

public class AltTranslations
{
    public string Language { get; set; } = default!;
    public float Score { get; set; }
    public bool IsTranslationSupported { get; set; }
    public bool IsTransliterationSupported { get; set; }
}
