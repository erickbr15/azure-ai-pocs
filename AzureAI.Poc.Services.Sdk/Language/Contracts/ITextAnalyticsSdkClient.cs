using Azure.AI.TextAnalytics;
using AzureAI.Poc.Services.Sdk.Language.Model;

namespace AzureAI.Poc.Services.Sdk.Language.Contracts;

public interface ITextAnalyticsSdkClient
{
    Task<PiiEntityCollection> RecognizePiiEntitiesAsync(PiiRecognitionModel model, CancellationToken cancellationToken);
    Task<RecognizePiiEntitiesResultCollection> RecognizePiiEntitiesBatchAsync(PiiRecognitionBatchModel model, CancellationToken cancellationToken);
    Task<CategorizedEntityCollection> RecognizeEntitiesAsync(string document, string language, CancellationToken cancellationToken);
    Task<RecognizeEntitiesResultCollection> RecognizeEntitiesBatchAsync(EntitiesRecognitionBatchModel model, CancellationToken cancellationToken);
    Task<DocumentSentiment> AnalyzeSentimentAsync(SentimentAnalysisModel model, CancellationToken cancellationToken);
    Task<AnalyzeSentimentResultCollection> AnalyzeSentimentBatchAsync(SentimentAnalysisBatchModel model, CancellationToken cancellationToken);
    Task<DetectedLanguage> DetectLanguageAsync(string document, string countryHint, CancellationToken cancellationToken);
    Task<DetectLanguageResultCollection> DetectLanguageBatchAsync(LanguageDetectionBatchModel model, CancellationToken cancellationToken);
    Task<KeyPhraseCollection> ExtractKeyPhrasesAsync(string document, string language, CancellationToken cancellationToken);
    Task<ExtractKeyPhrasesResultCollection> ExtractKeyPhrasesBatchAsync(KeyPhraseExtractionBatchModel model, CancellationToken cancellationToken);
    Task<LinkedEntityCollection> RecognizeLinkedEntitiesAsync(string document, string language, CancellationToken cancellationToken);
    Task<RecognizeLinkedEntitiesResultCollection> RecognizeLinkedEntitiesBatchAsync(LinkedEntitiesRecognitionBatchModel model, CancellationToken cancellationToken);
    Task<ExtractiveSummarizeOperation> ExtractSummarizeAsync(Azure.WaitUntil waitUntil, ExtractiveSummarizeModel model, CancellationToken cancellationToken);
    Task<AnalyzeHealthcareEntitiesOperation> AnalyzeHealthcareEntitiesAsync(Azure.WaitUntil waitUntil, AnalyzeHealthcareEntitiesModel model, CancellationToken cancellationToken);
    Task<AnalyzeActionsOperation> AnalyzeActionsAsync(Azure.WaitUntil waitUntil, AnalyzeActionsModel model, CancellationToken cancellationToken);

}
