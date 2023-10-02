namespace AzureAI.Poc.Services.Api.Translator.Model;

public class TransliterateRequest
{
    public string Text { get; set; } = default!;
    public string? ToScript { get; set; }
    public string? FromScript { get; set; }
    public string? Language { get; set; }  
}