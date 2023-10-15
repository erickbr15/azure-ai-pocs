using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class SentimentAnalysisBatchModel
{
    public IList<TextDocumentInput> TextDocuments { get; set; } = new List<TextDocumentInput>();
    public AnalyzeSentimentOptions Options { get; set; } = default!;
}