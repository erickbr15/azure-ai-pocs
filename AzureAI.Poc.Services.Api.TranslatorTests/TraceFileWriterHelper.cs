
namespace AzureAI.Poc.Services.Api.Tests;

public static class TraceFileWriterHelper
{
    public static void Write(Guid testingSessionId, string fileName, string trace)
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{testingSessionId}-{fileName}.json");
        File.AppendAllText(path, trace);
    }
}
