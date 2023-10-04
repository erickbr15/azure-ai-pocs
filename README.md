# AI Azure POCs

## Project Description
The "AI Azure POCs" project aims to provide C# implementation examples for utilizing Azure AI REST API services and Azure SDKs when applicable.

## Features
- C# implementation examples for Azure AI services.
- Usage of Azure AI Services' REST APIs.
- Integration with Azure SDKs when appropriate.

## Prerequisites
Before you begin, it is desirable that you meet the following requirements:
- You will need a Microsoft Azure subscription
- You will need Visual Studio (Community, Professional, or Enterprise) Version 17.6.5 

## Project components view
![image](https://github.com/erickbr15/azure-ai-pocs/assets/72543531/377eaf63-6e96-4fc1-9b0e-0f0e48cff5ef)


## Project Setup
Follow these steps to set up and clone the project in your local environment:

1. Clone the repository
2. Open the solution ```azure-ai-pocs.sln```
3. Restore the nuget packages for the solution
4. Build the solution
5. Provision the Azure Resources using the ARM template ```https://github.com/erickbr15/azure-ai-pocs/tree/main/arm-template```
6. Create a secret named CognitiveServiceApiKey with the value of one of the AI Services API Keys
7. Set up the app configuration settings following the next table:
    | Key | Value | Type |
    |--------------|--------------|--------------|
    | AIServices:Translator:GlobalEndpoint | https://api.cognitive.microsofttranslator.com/ | Key-Value |
    | CognitiveService:ApiKey | {CognitiveServiceApiKey-Key-Vault-Uri} | Key-Vault reference |
    | CognitiveService:Endpoint | {Your-AI-Service-Endpoint} | Key-Value |
    | CognitiveService:Location | {Your-Azure-Resources-Location} | Key-Value |
    | Translator:Global:ApiVersion | 3.0 | Key-Value |
    | Translator:Global:DetectRoute | detect | Key-Value |
    | Translator:Global:LanguagesRoute | languages | Key-Value |
    | Translator:Global:TranslateRoute | translate | Key-Value |
    | Translator:Global:TransliterateRoute | transliterate | Key-Value |

9. Ensure that the Azure RBAC is enabled in the app configuration resource to use a system-assigned managed identity to read the Azure Key Vault.
10. Configure the app config connection string in the xUnit Integration Test project.

    ```
    {
      "ConfigConnectionString": "Endpoint={your-appconfig-endpoint};Id={your-appconfig-id};Secret={your-appconfig-secret}"
    }
    ```

## Azure Translator AI Service Examples

** These examples were built following the API REST spec for version 3.0. Please see https://learn.microsoft.com/en-us/azure/ai-services/translator/reference/v3-0-reference **

#### This project contains examples of How-To:
- List supported languages by the Microsoft AI Translator Service
- Translate text from/to multiple languages supporting transliteration
- Detect the language
- Transliterate from/to different scripts

** For a complete list of the applications please visit the Microsoft Translator Documentation (https://learn.microsoft.com/en-us/azure/ai-services/translator/) **

#### This project contains examples of How-To:
- Invoke the Microsoft Translator REST API v3.0 to deserialize the responses to string, so you can use this code to implement your own deserialization model, or take advantage of JSON objects. See the class: AzureAI.Poc.Services/Translator/RestClient/TranslatorRestClient.cs
- Invoke the Microsoft Translator REST API v3.0 to deserialize the responses using strong types defined in the project. You can use this approach to create more complex solutions where you need a full typed response for the translation result. See the class: AzureAI.Poc.Services/Translator/TranslatorService.cs

### References
- Microsoft Translator documentation: https://learn.microsoft.com/en-us/azure/ai-services/translator/
- Microsoft Learn Training for AI Translation services: https://learn.microsoft.com/en-us/training/modules/translate-text-with-translator-service/

