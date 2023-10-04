using AzureAI.Poc.Services.Api.Tests.Fixtures;
using AzureAI.Poc.Services.Api.Translator.Contracts;
using AzureAI.Poc.Services.Api.Translator.Model;
using Microsoft.Extensions.DependencyInjection;

namespace AzureAI.Poc.Services.Api.Tests.Translator;

public class TranslatorRestApiTests : IClassFixture<HostInitializationFixture>
{
    private readonly HostInitializationFixture _hostInitializer;
    private readonly ITranslatorRestClient _translatorRestClient;

    public TranslatorRestApiTests(HostInitializationFixture hostInitializer)
    {
        _hostInitializer = hostInitializer ?? throw new ArgumentNullException(nameof(hostInitializer));
        _translatorRestClient = _hostInitializer.Host.Services.GetService<ITranslatorRestClient>()!;
    }

    /// <summary>
    ///     List the AI Azure Translator available languages
    /// </summary>
    [Fact]
    public async Task Translator_Get_Languages()
    {
        var response = await _translatorRestClient.GetLanguagesAsync(CancellationToken.None);
        Assert.NotNull(response);
    }

    /// <summary>
    ///     Translate a single input
    /// </summary>
    [Fact]
    public async Task Translator_Translate_Single_Input()
    {
        var request = new TranslateRequest
        {
            FromLanguage = "en",
            ToLanguages = new List<string> {"es-MX"},
            IncludeAlignment = true,
            IncludeSentenceLength = true
        };

        request.Text.Add("Hello!, what's your name?");
        
        var response = await _translatorRestClient.TranslateAsync(request, CancellationToken.None);
        Assert.NotNull(response);
    }

    /// <summary>
    ///     Translate a single input with language autodetection
    /// </summary>
    [Fact]
    public async Task Translator_Translate_Single_Input_With_Language_Autodetection()
    {
        var request = new TranslateRequest
        {
            ToLanguages = new List<string> {"es-MX" },
            IncludeAlignment = true,
            IncludeSentenceLength = true
        };

        request.Text.Add("Hello!, what's your name?");
        var response = await _translatorRestClient.TranslateAsync(request, CancellationToken.None);

        Assert.NotNull(response);
    }

    /// <summary>
    ///     Translate a single input with transliteration
    /// </summary>
    [Fact]
    public async Task Translator_Translate_With_Transliteration()
    {
        var request = new TranslateRequest
        {
            ToLanguages = new List<string> { "es-MX" },
            ToScript = "Latn",
            IncludeAlignment = true,
            IncludeSentenceLength = true
        };

        request.Text.Add("Hello!, what's your name?");
        var response = await _translatorRestClient.TranslateAsync(request, CancellationToken.None);

        Assert.NotNull(response);
    }

    /// <summary>
    ///     Translate multiple pieces of text
    /// </summary>
    [Fact]
    public async Task Translator_Translate_Multiple_Pieces_Of_Text()
    {
        var request = new TranslateRequest
        {
            FromLanguage = "en",
            ToLanguages = new List<string> { "es-MX" },
            IncludeAlignment = true,
            IncludeSentenceLength = true
        };

        request.Text.Add("Hello!, what's your name?");
        request.Text.Add("I am fine, thank you.");

        var response = await _translatorRestClient.TranslateAsync(request, CancellationToken.None);

        Assert.NotNull(response);
    }

    /// <summary>
    ///     Translate to multiple languages
    /// </summary>
    [Fact]
    public async Task Translator_Translate_To_Multiple_Languages()
    {
        var request = new TranslateRequest
        {
            FromLanguage = "en",
            ToLanguages = new List<string> { "zh-Hans", "es-MX" },
            IncludeAlignment = true,
            IncludeSentenceLength = true
        };

        request.Text.Add("Hello!, what's your name?");
        request.Text.Add("I am fine, thank you.");

        var response = await _translatorRestClient.TranslateAsync(request, CancellationToken.None);

        Assert.NotNull(response);
    }

    /// <summary>
    ///     Translate with markup and decide what is translated
    /// </summary>
    [Fact]
    public async Task Translator_Translate_With_Markup_And_Decide_WhatIs_Translated()
    {
        var request = new TranslateRequest
        {
            FromLanguage = "en",
            ToLanguages = new List<string> { "zh-Hans", "es-MX" },
            IncludeAlignment = true,
            IncludeSentenceLength = true,
            TextType = "html"
        };

        request.Text.Add("<div class=\"notranslate\">This will not be translated.</div><div>This will be translated.</div>");

        var response = await _translatorRestClient.TranslateAsync(request, CancellationToken.None);

        Assert.NotNull(response);
    }
}