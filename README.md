# DevPilot 🚀

DevPilot is a .NET 8 ASP.NET Core Web API that provides practical developer productivity utilities through focused, documented REST endpoints.

## Features

- JSON validation and formatting
- Base64 encoding and decoding
- SHA256 and SHA512 hashing
- GUID generation
- UTC timestamp generation
- Swagger/OpenAPI documentation
- Separate controllers aligned with the Single Responsibility Principle

## Technology Stack

- .NET 8
- ASP.NET Core Web API
- C#
- Swashbuckle.AspNetCore

## Project Structure

```text
DevPilot/
├── Api/
│   ├── JsonController.cs
│   ├── Base64Controller.cs
│   ├── HashController.cs
│   └── UtilityController.cs
├── wwwroot/
├── DevPilot.csproj
├── Program.cs
├── README.md
└── LICENSE
```

## How to Use

### 1. Prerequisites

Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) and Git.

### 2. Clone the repository

```bash
git clone https://github.com/azam123/DevPilot.git
cd DevPilot
```

### 3. Restore dependencies and build

```bash
dotnet restore
dotnet build
```

### 4. Run the API

```bash
dotnet run
```

Use the HTTP or HTTPS URL shown in the terminal.

### 5. Open Swagger UI

Navigate to:

```text
https://localhost:xxxx/swagger
```

In Swagger UI, expand an endpoint, click **Try it out**, enter the request body if required, and click **Execute**.

## API Reference

| Method | Endpoint | Purpose |
|---|---|---|
| POST | `/api/json/format` | Validate and format JSON |
| POST | `/api/base64` | Encode or decode Base64 |
| POST | `/api/hash` | Generate SHA256 or SHA512 hash |
| GET | `/api/utility/guid` | Generate a new GUID |
| GET | `/api/utility/timestamp` | Get current UTC and Unix timestamp |

## API Examples

### Format JSON

`POST /api/json/format`

```json
{"name":"Azam","role":"Architect"}
```

### Encode Base64

`POST /api/base64`

```json
{"value":"Hello DevPilot","decode":false}
```

### Decode Base64

```json
{"value":"SGVsbG8gRGV2UGlsb3Q=","decode":true}
```

### Generate a SHA256 hash

`POST /api/hash`

```json
{"value":"Hello DevPilot","algorithm":"SHA256"}
```

### Generate a GUID

`GET /api/utility/guid`

### Get the current UTC timestamp

`GET /api/utility/timestamp`

## Design Principles

- Single Responsibility Principle: each controller owns one utility area.
- Clear API boundaries and predictable routes.
- Swagger annotations for endpoint discoverability.
- Input validation and meaningful HTTP responses.
- Small components that can be extended independently.

## Notes

The API currently does not require authentication. Avoid sending secrets or sensitive production data to test endpoints.

## License

Licensed under the GNU General Public License v3.0. See `LICENSE`.
