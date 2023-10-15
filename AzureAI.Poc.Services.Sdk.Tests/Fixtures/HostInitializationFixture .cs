using Azure.Identity;
using AzureAI.Poc.Services.Common;
using AzureAI.Poc.Services.Sdk.Language;
using AzureAI.Poc.Services.Sdk.Language.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AzureAI.Poc.Services.Sdk.Tests.Fixtures;

public sealed class HostInitializationFixture : IDisposable
{
    public IHost Host { get; private set; }

    public HostInitializationFixture()
    {
        var hostBuilder = new HostBuilder()
            .ConfigureAppConfiguration((hostContext, configBuilder) =>
            {
                configBuilder.AddJsonFile("appsettings.local.json", optional: true);

                var config = configBuilder.Build();
                var configConnectionString = config["ConfigConnectionString"];

                configBuilder.AddAzureAppConfiguration(options =>
                {
                    options.Connect(configConnectionString).Select(KeyFilter.Any);
                    options.ConfigureKeyVault(options =>
                        options.SetCredential(new DefaultAzureCredential())
                    );
                });
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddOptions<CognitiveServiceOptions>().BindConfiguration("CognitiveService");
                services.AddSingleton<ITextAnalyticsSdkClient, TextAnalyticsSdkClient>();
            });

        this.Host = hostBuilder.Build();
        this.Host.StartAsync().Wait();
    }

    public void Dispose()
    {
        Host.StopAsync().Wait();
        Host.Dispose();
    }
}
