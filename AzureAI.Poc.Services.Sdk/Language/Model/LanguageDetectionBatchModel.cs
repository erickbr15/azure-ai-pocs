using Azure.AI.TextAnalytics;

namespace AzureAI.Poc.Services.Sdk.Language.Model;

public class LanguageDetectionBatchModel
{
    public IList<DetectLanguageInput> DetectLanguageInputs { get; set; } = new List<DetectLanguageInput>();
    public TextAnalyticsRequestOptions Options { get; set; } = default!;
}
