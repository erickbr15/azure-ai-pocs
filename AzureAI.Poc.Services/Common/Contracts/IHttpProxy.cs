namespace AzureAI.Poc.Services.Api.Common.Contracts;

public interface IHttpProxy
{
    Task<HttpResponseMessage> GetAsync(Uri uri, IDictionary<string, string> headers, CancellationToken cancellationToken);
    Task<HttpResponseMessage> PostAsync(Uri uri, IDictionary<string, string> headers, HttpContent? content, CancellationToken cancellationToken);
}
