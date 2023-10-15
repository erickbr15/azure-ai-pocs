using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class AnalyzeActionsModel
{
    public IList<TextDocumentInput> TextDocuments { get; set; } = new List<TextDocumentInput>();
    public TextAnalyticsActions Actions { get; set; } = default!;
    public AnalyzeActionsOptions Options { get; set; } = default!;
}
