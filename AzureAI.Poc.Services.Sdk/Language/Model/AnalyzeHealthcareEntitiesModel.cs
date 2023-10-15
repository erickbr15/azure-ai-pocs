using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class AnalyzeHealthcareEntitiesModel
{
    public IList<TextDocumentInput> TextDocuments { get; set; } = new List<TextDocumentInput>();
    public AnalyzeHealthcareEntitiesOptions Options { get; set; } = default!;
}
