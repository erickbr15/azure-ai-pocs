using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class SentimentAnalysisModel
{
    public string Document { get; set; } = default!;
    public string Language { get; set; } = default!;
    public AnalyzeSentimentOptions Options { get; set; } = default!;
}
