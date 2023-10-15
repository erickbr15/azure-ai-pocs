using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class PiiRecognitionBatchModel
{
    public IList<TextDocumentInput> TextDocuments { get; set; } = new List<TextDocumentInput>();
    public RecognizePiiEntitiesOptions Options { get; set; } = default!;
}
