# DevPilot 🚀

A clean ASP.NET Core .NET 8 developer productivity toolkit exposing practical utilities through documented REST APIs.

## Features

- JSON formatting
- Base64 encoding and decoding
- SHA256 and SHA512 hashing
- GUID generation
- UTC timestamp generation
- Swagger/OpenAPI documentation
- Separate controllers following the Single Responsibility Principle

## Tech Stack

- .NET 8
- ASP.NET Core Web API
- C#
- Swagger / OpenAPI via Swashbuckle

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
└── Program.cs
```

## How to Use

### 1. Prerequisites

Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

### 2. Clone the repository

```bash
git clone https://github.com/azam123/DevPilot.git
cd DevPilot
```

### 3. Restore and run

```bash
dotnet restore
dotnet run
```

### 4. Open Swagger

Open the Swagger URL shown in the terminal, usually:

```text
https://localhost:xxxx/swagger
```

Select an endpoint, click **Try it out**, provide the request body, and click **Execute**.

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

### Generate Hash

`POST /api/hash`

```json
{"value":"Hello DevPilot","algorithm":"SHA256"}
```

### Generate GUID

`GET /api/utility/guid`

### Get UTC Timestamp

`GET /api/utility/timestamp`

## Design Principles

- Single Responsibility Principle
- Clear API boundaries
- Small, independently maintainable controllers
- Swagger documentation for discoverability
- Input validation and meaningful HTTP responses

## License

Licensed under the GNU General Public License v3.0. See `LICENSE`.
