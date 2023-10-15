namespace AzureAI.Poc.Services.Common;

public class CognitiveServiceOptions
{
    public string ApiKey { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string Endpoint { get; set; } = default!;
}