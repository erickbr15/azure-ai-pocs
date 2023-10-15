using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class EntitiesRecognitionBatchModel
{
    public IList<TextDocumentInput> TextDocuments { get; set; } = new List<TextDocumentInput>();
    public TextAnalyticsRequestOptions Options { get; set; } = default!;
}
