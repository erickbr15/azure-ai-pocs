using Azure;
using Azure.AI.TextAnalytics;
using AzureAI.Poc.Services.Common;
using AzureAI.Poc.Services.Sdk.Language.Contracts;
using AzureAI.Poc.Services.Sdk.Language.Model;
using Microsoft.Extensions.Options;

namespace AzureAI.Poc.Services.Sdk.Language;

public class TextAnalyticsSdkClient : ITextAnalyticsSdkClient
{        
    private readonly TextAnalyticsClient _textAnalyticsClient;

    public TextAnalyticsSdkClient(IOptions<CognitiveServiceOptions> cognitiveServicesOptions)
    {
        var options = cognitiveServicesOptions.Value ?? throw new ArgumentNullException(nameof(cognitiveServicesOptions));

        var textAnalyticsUri = new Uri(options.Endpoint);
        var azureKeyCredentials = new AzureKeyCredential(options.ApiKey);

        _textAnalyticsClient = new TextAnalyticsClient(textAnalyticsUri, azureKeyCredentials);
    }

    public async Task<PiiEntityCollection> RecognizePiiEntitiesAsync(PiiRecognitionModel model, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.RecognizePiiEntitiesAsync(model.Document, model.Language, model.Options, cancellationToken);
        return result.Value;        
    }

    public async Task<RecognizePiiEntitiesResultCollection> RecognizePiiEntitiesBatchAsync(PiiRecognitionBatchModel model, CancellationToken cancellationToken)
    {        
        var batchResult = await _textAnalyticsClient.RecognizePiiEntitiesBatchAsync(model.TextDocuments, model.Options, cancellationToken);
        return batchResult.Value;
    }

    public async Task<CategorizedEntityCollection> RecognizeEntitiesAsync(string document, string language, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.RecognizeEntitiesAsync(document, language, cancellationToken);
        return result.Value;
    }

    public async Task<RecognizeEntitiesResultCollection> RecognizeEntitiesBatchAsync(EntitiesRecognitionBatchModel model, CancellationToken cancellationToken)
    {
        var batchResult = await _textAnalyticsClient.RecognizeEntitiesBatchAsync(model.TextDocuments, model.Options, cancellationToken);
        return batchResult.Value;
    }

    public async Task<DocumentSentiment> AnalyzeSentimentAsync(SentimentAnalysisModel model, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.AnalyzeSentimentAsync(model.Document, model.Language, model.Options, cancellationToken);
        return result.Value;
    }

    public async Task<AnalyzeSentimentResultCollection> AnalyzeSentimentBatchAsync(SentimentAnalysisBatchModel model, CancellationToken cancellationToken)
    {
        var batchResult = await _textAnalyticsClient.AnalyzeSentimentBatchAsync(model.TextDocuments, model.Options, cancellationToken);
        return batchResult.Value;
    }

    public async Task<DetectedLanguage> DetectLanguageAsync(string document, string countryHint, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.DetectLanguageAsync(document, countryHint, cancellationToken);
        return result.Value;
    }

    public async Task<DetectLanguageResultCollection> DetectLanguageBatchAsync(LanguageDetectionBatchModel model, CancellationToken cancellationToken)
    {
        var batchResult = await _textAnalyticsClient.DetectLanguageBatchAsync(model.DetectLanguageInputs, model.Options, cancellationToken);
        return batchResult.Value;
    }

    public async Task<KeyPhraseCollection> ExtractKeyPhrasesAsync(string document, string language, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.ExtractKeyPhrasesAsync(document, language, cancellationToken);
        return result.Value;
    }

    public async Task<ExtractKeyPhrasesResultCollection> ExtractKeyPhrasesBatchAsync(KeyPhraseExtractionBatchModel model, CancellationToken cancellationToken)
    {
        var batchResult = await _textAnalyticsClient.ExtractKeyPhrasesBatchAsync(model.TextDocuments, model.Options, cancellationToken);
        return batchResult.Value;
    }

    public async Task<LinkedEntityCollection> RecognizeLinkedEntitiesAsync(string document, string language, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.RecognizeLinkedEntitiesAsync(document, language, cancellationToken);
        return result.Value;
    }

    public async Task<RecognizeLinkedEntitiesResultCollection> RecognizeLinkedEntitiesBatchAsync(LinkedEntitiesRecognitionBatchModel model, CancellationToken cancellationToken)
    {
        var batchResult = await _textAnalyticsClient.RecognizeLinkedEntitiesBatchAsync(model.TextDocuments, model.Options, cancellationToken);
        return batchResult.Value;
    }   

    public async Task<ExtractiveSummarizeOperation> ExtractSummarizeAsync(Azure.WaitUntil waitUntil, ExtractiveSummarizeModel model, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.ExtractiveSummarizeAsync(waitUntil, model.TextDocuments, model.Options, cancellationToken);
        return result;
    }

    public async Task<AnalyzeHealthcareEntitiesOperation> AnalyzeHealthcareEntitiesAsync(Azure.WaitUntil waitUntil, AnalyzeHealthcareEntitiesModel model, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.AnalyzeHealthcareEntitiesAsync(waitUntil, model.TextDocuments, model.Options, cancellationToken);
        return result;
    }

    public async Task<AnalyzeActionsOperation> AnalyzeActionsAsync(Azure.WaitUntil waitUntil, AnalyzeActionsModel model, CancellationToken cancellationToken)
    {
        var result = await _textAnalyticsClient.AnalyzeActionsAsync(waitUntil, model.TextDocuments, model.Actions, model.Options, cancellationToken);
        return result;
    }
}
