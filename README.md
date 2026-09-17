# DevPilot 🚀

DevPilot is a .NET 8 ASP.NET Core Web API providing developer productivity utilities through focused, documented REST endpoints.

## Features

- JSON validation and formatting
- Base64 encoding and decoding
- SHA256 and SHA512 hashing
- GUID and UTC timestamp generation
- JSON → C# model generation with nested objects and collections
- XML → C# model generation with nested elements and repeated nodes
- C# property declarations → JSON template
- C# property declarations → XML template
- Swagger/OpenAPI documentation
- Separate controllers aligned with the Single Responsibility Principle

## Prerequisites

- .NET 8 SDK
- Git

## Run Locally

```bash
git clone https://github.com/azam123/DevPilot.git
cd DevPilot
dotnet restore
dotnet build
dotnet run
```

Open the Swagger UI URL shown in the terminal, usually:

```text
https://localhost:xxxx/swagger
```

Expand an endpoint, select **Try it out**, enter the request body, and select **Execute**.

## Model Conversion APIs

All conversion endpoints accept:

```json
{
  "input": "...JSON, XML, or C# text...",
  "root": "RootModel"
}
```

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/json-to-csharp` | Generate C# classes recursively from nested JSON |
| POST | `/api/xml-to-csharp` | Generate C# classes from nested XML |
| POST | `/api/csharp-to-json` | Generate a JSON template from C# property declarations |
| POST | `/api/csharp-to-xml` | Generate an XML template from C# property declarations |

### JSON → C# example

```json
{
  "input": "{\"customer\":{\"name\":\"Azam\",\"address\":{\"city\":\"Hyderabad\"}},\"orders\":[{\"id\":1}]}",
  "root": "CustomerResponse"
}
```

### XML → C# example

```json
{
  "input": "<Customer><Name>Azam</Name><Address><City>Hyderabad</City></Address><Order><Id>1</Id></Order><Order><Id>2</Id></Order></Customer>",
  "root": "Customer"
}
```

### C# → JSON / XML example

```json
{
  "input": "public string Name { get; set; } public int Age { get; set; } public bool Active { get; set; }",
  "root": "Person"
}
```

> Note: C# → JSON/XML currently generates templates from property declarations and default values. It does not execute arbitrary C# code or deserialize a runtime object.

## Other APIs

| Method | Endpoint | Purpose |
|---|---|---|
| POST | `/api/json/format` | Validate and format JSON |
| POST | `/api/base64` | Encode or decode Base64 |
| POST | `/api/hash` | Generate SHA256 or SHA512 hash |
| GET | `/api/utility/guid` | Generate a GUID |
| GET | `/api/utility/timestamp` | Get UTC and Unix timestamps |

## Project Structure

```text
DevPilot/
├── Api/
│   ├── JsonController.cs
│   ├── Base64Controller.cs
│   ├── HashController.cs
│   ├── UtilityController.cs
│   └── ModelConversionControllers.cs
├── Services/
│   └── ModelConversionService.cs
├── DevPilot.csproj
├── Program.cs
└── README.md
```

## Design Principles

- Single Responsibility Principle
- Separation of controllers and services
- Recursive processing for nested JSON/XML structures
- Swagger annotations for discoverability
- Meaningful HTTP responses and input validation

## License

Licensed under the GNU General Public License v3.0. See `LICENSE`.
