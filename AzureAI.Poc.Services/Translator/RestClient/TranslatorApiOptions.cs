namespace AzureAI.Poc.Services.Api.Translator.RestClient;

public sealed class TranslatorApiOptions
{
    public string LanguagesRoute { get; set; } = default!;
    public string TranslateRoute { get; set; } = default!;
    public string DetectRoute { get; set; } = default!;
    public string TransliterateRoute { get; set; } = default!;
    public string ApiVersion { get; set; } = default!;
}
