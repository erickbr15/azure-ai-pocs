# AI Azure POCs

## Project Description
The "AI Azure POCs" project aims to provide C# implementation examples for utilizing Azure AI REST API services and Azure SDKs when applicable.

## Features
- C# implementation examples for Azure AI services.
- Usage of Azure AI Services' REST APIs.
- Integration with Azure SDKs when appropriate.

## Prerequisites
Before you begin, it is desirable that you meet the following requirements:
- You have a Microsoft Azure subscription
- You have a Visual Studio (Community, Professional, or Enterprise) Version 17.6.5

## Project components view
![image](https://github.com/erickbr15/azure-ai-pocs/assets/72543531/9cfbf9a8-724a-4841-a8e3-66249d07b0b0)

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

## Azure Translator AI Service

You will find specific examples of Azure's Translator AI service within this project. This service allows efficient translation of text and documents from one language to another.
