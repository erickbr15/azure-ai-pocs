namespace AzureAI.Poc.Services.Api.Translator.Model;

public class Translation
{
    public string Text { get; set; } = default!;
    public TextResult Transliteration { get; set; } = default!;
    public string To { get; set; } = default!;
    public Alignment Alignment { get; set; } = default!;
    public SentenceLength SentLen { get; set; } = default!;
}
