using Azure.Identity;
using OpenAI.Chat;
using Azure.AI.OpenAI;
using System;
using System.Buffers;
using System.ClientModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;


// See https://aka.ms/new-console-template for more information
Console.WriteLine("Alo, I am OA Bot Coder, I can help you to write code");

// Set environment variables
string keyFromEnvironment = Environment.GetEnvironmentVariable("AZURE_OPENAI_API_KEY") ?? throw new InvalidOperationException("Environment variable 'AZURE_OPENAI_API_KEY' not found.");
string endpoint = Environment.GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT") ?? throw new InvalidOperationException("Environment variable 'AZURE_OPENAI_ENDPOINT' not found.");
string deploymentId = Environment.GetEnvironmentVariable("AZURE_OPENAI_DEPLOYMENT_ID") ?? throw new InvalidOperationException("Environment variable 'AZURE_OPENAI_DEPLOYMENT_ID' not found.");

AzureOpenAIClient azureClient = new(
    new Uri(endpoint),
    new ApiKeyCredential(keyFromEnvironment));
ChatClient chatClient = azureClient.GetChatClient(deploymentId);

while (true)
{
    Console.Write("You: ");
    string userInput = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userInput))
    {
        break;
    }

    ChatCompletion completion = chatClient.CompleteChat(
        new List<ChatMessage>
        {
            new SystemChatMessage("You are a C# professional developer and expert writing code based on Bouncy Castle cryptographic API. You always suggest code base on BouncyCastle.Cryptography package version 2.5.1 or above. Always include in your code suggestions these 7 namespaces System, System.Text, Org.BouncyCastle.Crypto, Org.BouncyCastle.Crypto.Digests, Org.BouncyCastle.Crypto.Macs, Org.BouncyCastle.Security and Org.BouncyCastle.Crypto.Parameters"),
            new UserChatMessage(userInput)
        });

    Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");
    Console.WriteLine();
}

