# JoshuaProjectClient

A .NET Standard 2.1 client library for the [Joshua Project API](https://api.joshuaproject.org), providing easy access to data about unreached people groups and languages around the world.

## Overview

The Joshua Project tracks unreached and unengaged people groups globally. This client library provides a simple, type-safe way to access their API data in your .NET applications.

## Installation

Install via NuGet Package Manager:

```bash
dotnet add package JoshuaProjectClient
```

Or add directly to your `.csproj` file:

```xml
<PackageReference Include="JoshuaProjectClient" Version="0.1.2" />
```

## Usage

### Getting Started

First, obtain an API key from [Joshua Project](https://api.joshuaproject.org). Then initialize the client:

```csharp
using JoshuaProjectClient;

var client = new Client("your-api-key-here");
```

### Retrieving Languages

Get all languages:

```csharp
var languages = client.GetAllLanguages();
foreach (var language in languages)
{
    Console.WriteLine($"{language.Language} ({language.ROL3})");
}
```

Get a specific language by ISO 639-3 code:

```csharp
var english = client.GetLanguage("eng");
Console.WriteLine($"Speakers: {english.WorldSpeakers}");
Console.WriteLine($"Bible Status: {english.BibleStatus}");
```

### Retrieving People Groups

Get all people groups:

```csharp
var peopleGroups = client.GetAllPeopleGroups();
foreach (var group in peopleGroups)
{
    Console.WriteLine($"{group.PeopNameInCountry} in {group.Ctry}");
}
```

Get a specific people group by ID:

```csharp
var peopleGroup = client.GetPeopleGroup("12345");
Console.WriteLine($"Population: {peopleGroup.Population}");
Console.WriteLine($"Primary Religion: {peopleGroup.PrimaryReligion}");
```

### Error Handling

The client throws specific exceptions for common scenarios:

```csharp
try
{
    var language = client.GetLanguage("xyz");
}
catch (KeyNotFoundException ex)
{
    Console.WriteLine("Language not found");
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine("Invalid API key");
}
```

## Building from Source

### Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download) or later
- Git

### Clone and Build

```bash
git clone https://github.com/WycliffeAssociates/JoshuaProjectClient.git
cd JoshuaProjectClient
dotnet restore
dotnet build
```

Build in Release configuration:

```bash
dotnet build --configuration Release
```

## Configuration

### API Key

The client requires a valid Joshua Project API key. You can request one at [https://api.joshuaproject.org](https://api.joshuaproject.org).

**Best Practices:**
- Store your API key in environment variables or secure configuration
- Never commit API keys to source control
- Use user secrets for local development

Example using environment variables:

```csharp
var apiKey = Environment.GetEnvironmentVariable("JOSHUA_PROJECT_API_KEY");
var client = new Client(apiKey);
```

Example using configuration (ASP.NET Core):

```csharp
public class JoshuaProjectService
{
    private readonly Client _client;
    
    public JoshuaProjectService(IConfiguration configuration)
    {
        var apiKey = configuration["JoshuaProject:ApiKey"];
        _client = new Client(apiKey);
    }
}
```

## Technical Details

### Target Framework

- .NET Standard 2.1

### Dependencies

- `System.Text.Json` 8.0.5 - High-performance JSON serialization

### API Coverage

The client currently supports:

- **Languages**: Retrieve all languages or specific languages by ISO 639-3 code
- **People Groups**: Retrieve all people groups or specific groups by ID

### Models

The library includes strongly-typed models for all API responses:

- `JPLanguage` - Language information including speakers, Bible translation status, and resources
- `JPPeopleGroup` - People group details including population, religion, location, and progress indicators
- `JPProfile` - Profile text for people groups
- `JPResource` - Available resources for languages and people groups

### HTTP Client Management

The library uses a single `HttpClient` instance per `Client` object to prevent socket exhaustion and improve performance. The client is created once in the constructor and reused for all API calls.

### JSON Deserialization

The library uses `System.Text.Json` with case-insensitive property matching for robust deserialization of API responses.

## License

This project is licensed under the terms found in the [LICENSE](LICENSE) file.

## Related Links

- [Joshua Project Website](https://joshuaproject.net)
- [Joshua Project API Documentation](https://api.joshuaproject.org)
- [NuGet Package](https://www.nuget.org/packages/JoshuaProjectClient)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Authors

- rbnswartz
- rbnswartz_wa
