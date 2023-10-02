namespace AzureAI.Poc.Services.Api.Translator.Model;

public class DetectedLanguage
{
    public string Language { get; set; } = default!;
    public float Score { get; set; }
}
