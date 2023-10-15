using Azure.AI.TextAnalytics;
using AzureAI.Poc.Services.Sdk.Language.Contracts;
using AzureAI.Poc.Services.Sdk.Language.Model;
using AzureAI.Poc.Services.Sdk.Tests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace AzureAI.Poc.Services.Sdk.Tests.Language;

public class TextAnalyticsSdkTests : IClassFixture<HostInitializationFixture>
{
    private readonly HostInitializationFixture _hostInitialization;
    private readonly ITextAnalyticsSdkClient _textAnalyticsSdkClient;

    public TextAnalyticsSdkTests(HostInitializationFixture fixture)
    {
        _hostInitialization = fixture ?? throw new ArgumentNullException(nameof(fixture));
        _textAnalyticsSdkClient = _hostInitialization.Host.Services.GetRequiredService<ITextAnalyticsSdkClient>()!;
    }

    [Fact]
    public async Task RecognizePiiEntitiesAsync()
    {
        var model = new PiiRecognitionModel
        {
            Document = "Parker Doe has repaid all of their loans as of 2020-04-25. Their SSN is 859-98-0987. To contact them,"
                        + " use their phone number 800-102-1100. They are originally from Brazil and have document ID number"
                        + " 998.214.865-68.",
            Language = "en",
            Options = new RecognizePiiEntitiesOptions { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.RecognizePiiEntitiesAsync(model, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task RecognizePiiEntitiesBatchAsync()
    {
        string documentA = "Parker Doe has repaid all of their loans as of 2020-04-25. Their SSN is 859-98-0987. To contact them,"
                            + " use their phone number 800-102-1100. They are originally from Brazil and have document ID number"
                            + " 998.214.865-68.";

        string documentB = "Yesterday, Dan Doe was asking where they could find the ABA number. I explained that it is the first"
                            + " 9 digits in the lower left hand corner of their personal check. After looking at their account"
                            + " they confirmed the number was 111000025.";

        string documentC = string.Empty;

        var model = new PiiRecognitionBatchModel
        {
            TextDocuments = new List<TextDocumentInput>
            {
                new TextDocumentInput(Guid.NewGuid().ToString(), documentA),
                new TextDocumentInput(Guid.NewGuid().ToString(), documentB),
                new TextDocumentInput(Guid.NewGuid().ToString(), documentC)
            },
            Options = new RecognizePiiEntitiesOptions { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.RecognizePiiEntitiesBatchAsync(model, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task RecognizeEntitiesAsync()
    {
          var document = "Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, to develop and sell BASIC"
                        + " interpreters for the Altair 8800. During his career at Microsoft, Gates held the positions of"
                        + " chairman, chief executive officer, president and chief software architect, while also being the"
                        + " largest individual shareholder until May 2014.";

        var result = await _textAnalyticsSdkClient.RecognizeEntitiesAsync(document, "en", CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task RecognizeEntitiesBatchAsync()
    {
        string documentA = "Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, to develop and sell BASIC"
                            + " interpreters for the Altair 8800. During his career at Microsoft, Gates held the positions of"
                            + " chairman, chief executive officer, president and chief software architect, while also being the"
                            + " largest individual shareholder until May 2014.";

        string documentB = "Yesterday, Dan Doe was asking where they could find the ABA number. I explained that it is the first"
                            + " 9 digits in the lower left hand corner of their personal check. After looking at their account"
                            + " they confirmed the number was 111000025.";
        

        var model = new EntitiesRecognitionBatchModel
        {
            TextDocuments = new List<TextDocumentInput>
            {
                new TextDocumentInput(Guid.NewGuid().ToString(), documentA),
                new TextDocumentInput(Guid.NewGuid().ToString(), documentB),                
            },
            Options = new TextAnalyticsRequestOptions { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.RecognizeEntitiesBatchAsync(model, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task AnalyzeSentimentAsync()
    {
        var model = new SentimentAnalysisModel
        {
            Document = "The hotel was dark and unclean. I like microsoft.",
            Language = "en",
            Options = new AnalyzeSentimentOptions { IncludeStatistics = true, IncludeOpinionMining = true}
        };
        
        var result = await _textAnalyticsSdkClient.AnalyzeSentimentAsync(model, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task AnalyzeSentimentBatchAsync()
    {
        string documentA = "The hotel was dark and unclean. I like microsoft.";
        string documentB = "The restaurant had amazing gnocchi! The waiters were excellent.";

        var model = new SentimentAnalysisBatchModel
        {
            TextDocuments = new List<TextDocumentInput>
            {
                new TextDocumentInput(Guid.NewGuid().ToString(), documentA),
                new TextDocumentInput(Guid.NewGuid().ToString(), documentB),
            },
            Options = new AnalyzeSentimentOptions { IncludeStatistics = true, IncludeOpinionMining = true }
        };

        var result = await _textAnalyticsSdkClient.AnalyzeSentimentBatchAsync(model, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task DetectLanguageAsync()
    {
        var document = "Ce document est rédigé en Français.";

        var result = await _textAnalyticsSdkClient.DetectLanguageAsync(document, "fr", CancellationToken.None);

        Assert.True(!string.IsNullOrEmpty(result.Iso6391Name));
    }

    [Fact]
    public async Task DetectLanguageBatchAsync()
    {
        string documentA = "Ce document est rédigé en Français.";
        string documentB = "Este documento está escrito en español.";

        var model = new LanguageDetectionBatchModel
        {
            DetectLanguageInputs = new List<DetectLanguageInput>
            {
                new DetectLanguageInput(Guid.NewGuid().ToString(), documentA),
                new DetectLanguageInput(Guid.NewGuid().ToString(), documentB),
            },
            Options = new TextAnalyticsRequestOptions { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.DetectLanguageBatchAsync(model, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task ExtractKeyPhrasesAsync()
    {
        var document = "My cat might need to see a veterinarian.";

        var result = await _textAnalyticsSdkClient.ExtractKeyPhrasesAsync(document, "en", CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task ExtractKeyPhrasesBatchAsync()
    {
        string documentA = "My cat might need to see a veterinarian.";
        string documentB = "The restaurant had amazing gnocchi! The waiters were excellent.";

        var model = new KeyPhraseExtractionBatchModel
        {
            TextDocuments = new List<TextDocumentInput>
            {
                new TextDocumentInput(Guid.NewGuid().ToString(), documentA),
                new TextDocumentInput(Guid.NewGuid().ToString(), documentB),
            },
            Options = new TextAnalyticsRequestOptions { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.ExtractKeyPhrasesBatchAsync(model, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task RecognizeLinkedEntitiesAsync()
    {
        var document = "Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, to develop and sell BASIC"
                        + " interpreters for the Altair 8800. During his career at Microsoft, Gates held the positions of"
                        + " chairman, chief executive officer, president and chief software architect, while also being the"
                        + " largest individual shareholder until May 2014.";

        var result = await _textAnalyticsSdkClient.RecognizeLinkedEntitiesAsync(document, "en", CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task RecognizeLinkedEntitiesBatchAsync()
    {
        string documentA = "Microsoft was founded by Bill Gates and Paul Allen on April 4, 1975, to develop and sell BASIC"
                            + " interpreters for the Altair 8800. During his career at Microsoft, Gates held the positions of"
                            + " chairman, chief executive officer, president and chief software architect, while also being the"
                            + " largest individual shareholder until May 2014.";

        string documentB = "Yesterday, Dan Doe was asking where they could find the ABA number. I explained that it is the first"
                            + " 9 digits in the lower left hand corner of their personal check. After looking at their account"
                            + " they confirmed the number was 111000025.";

        var model = new LinkedEntitiesRecognitionBatchModel
        {
            TextDocuments = new List<TextDocumentInput>
            {
                new TextDocumentInput(Guid.NewGuid().ToString(), documentA),
                new TextDocumentInput(Guid.NewGuid().ToString(), documentB),
            },
            Options = new TextAnalyticsRequestOptions { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.RecognizeLinkedEntitiesBatchAsync(model, CancellationToken.None);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task ExtractSummarizeAsync()
    {
        string document = TestingResources.LongText;

        var model = new ExtractiveSummarizeModel
        {
            TextDocuments = new List<TextDocumentInput>
            {
                new TextDocumentInput(Guid.NewGuid().ToString(), document){ Language = "es" }
            },
            Options = new ExtractiveSummarizeOptions { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.ExtractSummarizeAsync(Azure.WaitUntil.Completed, model, CancellationToken.None);
        var summary = new StringBuilder();

        if (result.HasValue)
        {
            var asyncPageableResult = result.GetValuesAsync(cancellationToken: CancellationToken.None);
            await foreach(var pages in asyncPageableResult)
            {
                foreach (var page in pages)
                {
                    summary.AppendLine(JsonConvert.SerializeObject(page));
                }
            }

        }

        Assert.NotNull(result);
    }   

    [Fact]
    public async Task AnalyzeHealthcareEntitiesAsync()
    {
        var documentA = TestingResources.HealthTextA;

        var model = new AnalyzeHealthcareEntitiesModel
        {
            TextDocuments = new List<TextDocumentInput>
            {
                new TextDocumentInput(Guid.NewGuid().ToString(), documentA){Language = "en"}
            },
            Options = new AnalyzeHealthcareEntitiesOptions { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.AnalyzeHealthcareEntitiesAsync(Azure.WaitUntil.Completed, model, CancellationToken.None);
        var summary = new StringBuilder();

        if (result.HasValue)
        {
            var asyncPageableResult = result.GetValuesAsync(cancellationToken: CancellationToken.None);
            await foreach (var pages in asyncPageableResult)
            {
                foreach (var page in pages)
                {
                    summary.AppendLine(JsonConvert.SerializeObject(page));
                }
            }

        }

        Assert.NotNull(result);
    }

    [Fact]
    public async Task AnalyzeActionsAsync()
    {
        string documentA =
        "We love this trail and make the trip every year. The views are breathtaking and well worth the hike!"
        + " Yesterday was foggy though, so we missed the spectacular views. We tried again today and it was"
        + " amazing. Everyone in my family liked the trail although it was too challenging for the less"
        + " athletic among us.";

        string documentB =
            "Last week we stayed at Hotel Foo to celebrate our anniversary. The staff knew about our anniversary"
            + " so they helped me organize a little surprise for my partner. The room was clean and with the"
            + " decoration I requested. It was perfect!";

        var model = new AnalyzeActionsModel
        {
            TextDocuments = new List<TextDocumentInput>
            {
                new TextDocumentInput(Guid.NewGuid().ToString(), documentA){Language = "en"},
                new TextDocumentInput(Guid.NewGuid().ToString(), documentB){Language = "en"}
            },
            Actions = new()
            {
                ExtractKeyPhrasesActions = new List<ExtractKeyPhrasesAction>() { new ExtractKeyPhrasesAction() { ActionName = "ExtractKeyPhrasesSample" } },
                RecognizeEntitiesActions = new List<RecognizeEntitiesAction>() { new RecognizeEntitiesAction() { ActionName = "RecognizeEntitiesSample" } },
                DisplayName = "AnalyzeOperationSample"
            },
            Options = new() { IncludeStatistics = true }
        };

        var result = await _textAnalyticsSdkClient.AnalyzeActionsAsync(Azure.WaitUntil.Completed, model, CancellationToken.None);

        var summary = new StringBuilder();

        if (result.HasValue)
        {
            var asyncPageableResult = result.GetValuesAsync(cancellationToken: CancellationToken.None);
            await foreach (var analyzeResult in asyncPageableResult)
            {
                analyzeResult.ExtractKeyPhrasesResults.ToList().ForEach(x => summary.AppendLine(JsonConvert.SerializeObject(x)));
                analyzeResult.RecognizeEntitiesResults.ToList().ForEach(x => summary.AppendLine(JsonConvert.SerializeObject(x)));
            }
        }

        Assert.NotNull(result);
    }
}
