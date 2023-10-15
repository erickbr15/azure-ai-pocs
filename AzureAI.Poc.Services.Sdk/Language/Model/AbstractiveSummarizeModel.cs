using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class AbstractiveSummarizeModel
{
    public IList<TextDocumentInput> TextDocuments { get; set; } = new List<TextDocumentInput>();
    public AbstractiveSummarizeOptions Options { get; set; } = default!;
}
