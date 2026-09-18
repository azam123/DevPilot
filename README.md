# 🚀 DevPilot

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Minimal%20APIs-512BD4)](https://learn.microsoft.com/aspnet/core/)
[![NuGet](https://img.shields.io/badge/NuGet-DevPilot.Core-004880?logo=nuget)](https://www.nuget.org/)
[![License](https://img.shields.io/badge/License-GPL--3.0-blue)](LICENSE)
[![Build](https://img.shields.io/badge/Build-.NET%208-success)](#development)

DevPilot is a modular **.NET 8 developer productivity toolkit** exposing practical utilities through documented REST APIs, with reusable functionality available through the `DevPilot.Core` NuGet package.

## ✨ Features

- JSON validation and formatting
- Base64 encoding and decoding
- SHA256 and SHA512 hashing
- GUID and UTC timestamp generation
- Recursive JSON → C# model generation
- Recursive XML → C# model generation
- C# property declarations → JSON/XML templates
- Separate controllers based on single responsibility
- Swagger/OpenAPI documentation
- Reusable `DevPilot.Core` library

## 🏗️ Architecture

```mermaid
flowchart TD
    U[User / Client] --> API[ASP.NET Core API]
    API --> C[Focused Controllers]
    C --> S[Application Services]
    S --> CORE[DevPilot.Core Utilities]
    S --> CONV[Model Conversion Service]
    CORE --> OUT[Validated Response]
    CONV --> OUT
```

## ⚡ Quick Start — Web API

### Prerequisites

- .NET 8 SDK
- Git
- Optional: Visual Studio 2022, VS Code, or Rider

### Installation

```bash
git clone https://github.com/azam123/DevPilot.git
cd DevPilot
dotnet restore
dotnet build
dotnet run
```

Open the Swagger UI address printed in the terminal, typically:

```text
https://localhost:<port>/swagger
```

In Swagger, select an endpoint → **Try it out** → provide input → **Execute**.

## 📦 NuGet Package

The reusable library project is located at `src/DevPilot.Core`.

### Build the package locally

```bash
dotnet restore src/DevPilot.Core/DevPilot.Core.csproj
dotnet build src/DevPilot.Core/DevPilot.Core.csproj -c Release
dotnet pack src/DevPilot.Core/DevPilot.Core.csproj -c Release -o ./artifacts
```

### Use the package in another .NET project

```bash
dotnet add package DevPilot.Core --version 0.1.0
```

Example:

```csharp
using DevPilot.Core;

var encoded = DeveloperUtilities.ToBase64("Hello DevPilot");
var hash = DeveloperUtilities.Sha256("Hello DevPilot");
Console.WriteLine(encoded);
Console.WriteLine(hash);
```

### Publishing

The workflow `.github/workflows/nuget-publish.yml` builds the package and publishes it to NuGet.org when a version tag is pushed. Configure the repository secret `NUGET_API_KEY` before publishing.

```bash
git tag v0.1.0
git push origin v0.1.0
```

> Package publication requires a valid NuGet.org account, API key, and successful GitHub Actions execution. A workflow configuration alone does not mean the package has already been published.

## 🔌 API Reference

### Model conversion request

```json
{
  "input": "{\"customer\":{\"name\":\"Azam\"}}",
  "root": "CustomerResponse"
}
```

| Method | Endpoint | Purpose |
|---|---|---|
| POST | `/api/json-to-csharp` | Generate nested C# models from JSON |
| POST | `/api/xml-to-csharp` | Generate C# models from XML |
| POST | `/api/csharp-to-json` | Generate a JSON template from C# properties |
| POST | `/api/csharp-to-xml` | Generate an XML template from C# properties |
| POST | `/api/json/format` | Validate and format JSON |
| POST | `/api/base64` | Encode or decode Base64 |
| POST | `/api/hash` | Generate SHA256 or SHA512 |
| GET | `/api/utility/guid` | Generate a GUID |
| GET | `/api/utility/timestamp` | Get UTC and Unix timestamps |

## 🧱 Project Structure

```text
DevPilot/
├── Api/                       # Focused API controllers
├── Services/                  # Application services
├── src/
│   └── DevPilot.Core/         # Reusable NuGet library
│       ├── DevPilot.Core.csproj
│       └── DeveloperUtilities.cs
├── wwwroot/                   # Frontend assets
├── DevPilot.csproj            # Web API project
├── Program.cs
└── README.md
```

## 🧪 Development

```bash
dotnet format
dotnet build
dotnet test
```

Add tests for new utilities and endpoints before merging. Prefer small services, explicit contracts, cancellation support, input validation, and predictable error responses.

## 🗺️ Improvement Roadmap

- [ ] Add automated unit and integration tests
- [ ] Add global exception handling and ProblemDetails
- [ ] Add request size limits and rate limiting
- [ ] Add Roslyn-based C# parsing for safer model conversion
- [ ] Add structured logging and health checks
- [ ] Expand reusable NuGet APIs
- [ ] Add versioned API documentation

## 📄 License

Licensed under the GNU General Public License v3.0. See [LICENSE](LICENSE).
