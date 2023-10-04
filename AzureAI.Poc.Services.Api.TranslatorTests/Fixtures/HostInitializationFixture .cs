using Azure.Identity;
using AzureAI.Poc.Services.Api.Common;
using AzureAI.Poc.Services.Api.Common.Contracts;
using AzureAI.Poc.Services.Api.Translator;
using AzureAI.Poc.Services.Api.Translator.Contracts;
using AzureAI.Poc.Services.Api.Translator.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AzureAI.Poc.Services.Api.Tests.Fixtures;

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
            .ConfigureLogging((hostContext, loggingBuilder) =>
            {
                loggingBuilder.AddSerilog(dispose: true);
            })
            .ConfigureServices((hostContext, services) =>
            {                
                services.AddOptions<AiServicesOptions>().BindConfiguration("AiServices");
                services.AddOptions<CognitiveServiceOptions>().BindConfiguration("CognitiveService");
                services.AddSingleton<IHttpProxy, HttpProxy>();
                services.AddOptions<TranslatorApiOptions>().BindConfiguration("Translator:Global");
                services.AddSingleton<ITranslatorApiRouteBuilder, TranslatorGlobalApiRouteBuilder>();
                services.AddSingleton<ITranslatorRestClient, TranslatorRestClient>();
                services.AddSingleton<ITranslatorService, TranslatorService>();
            });

        Host = hostBuilder.Build();        
    }
    
    public void Dispose()
    {
        Host.StopAsync().Wait();
        Host.Dispose();
    }
}