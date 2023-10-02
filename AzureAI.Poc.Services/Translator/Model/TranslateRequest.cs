namespace AzureAI.Poc.Services.Api.Translator.Model;

public class TranslateRequest
{
    public IList<string> Text { get; set; } = new List<string>();

    /// <summary>
    /// Specifies the language of the input text. Find which languages are available to translate from by looking up supported languages using the translation scope. 
    /// If the from parameter isn't specified, automatic language detection is applied to determine the source language.
    /// </summary>
    public string? FromLanguage { get; set; }

    /// <summary>
    /// Specifies the language of the output text. The target language must be one of the supported languages included in the translation scope.
    /// </summary>
    public IList<string> ToLanguages { get; set; } = new List<string>();

    /// <summary>
    /// Specifies whether to include alignment projection from source text to translated text. Possible values are: true or false (default).
    /// </summary>
    public bool? IncludeAlignment { get; set; }

    /// <summary>
    /// Specifies whether to include sentence boundaries for the input text and the translated text. Possible values are: true or false (default).
    /// </summary>
    public bool? IncludeSentenceLength { get; set; }

    /// <summary>
    /// Specifies the script of the input text.
    /// </summary>
    public string? FromScript { get; set; }

    /// <summary>
    /// Specifies the script of the translated text.
    /// </summary>
    public string? ToScript { get; set; }

    /// <summary>
    /// Defines whether the text being translated is plain text or HTML text. Any HTML needs to be a well-formed, complete element. Possible values are: plain (default) or html.
    /// </summary>
    public string? TextType { get; set; }

}