using AzureAI.Poc.Services.Api.Translator.Model;

namespace AzureAI.Poc.Services.Api.Translator.Contracts;

public interface ITranslatorApiRouteBuilder
{
    string BuildLanguagesRoute();
    string BuildDetectRoute();
    string BuildTranslateRoute(TranslateRequest request);    
    string BuildTransliterateRoute(TransliterateRequest request);
}