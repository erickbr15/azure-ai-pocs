using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class PiiRecognitionModel
{
    public string Document { get; set; } = default!;
    public string Language { get; set; } = default!;
    public RecognizePiiEntitiesOptions Options { get; set; } = default!;
}
