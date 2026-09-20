# DevPilot Architecture

DevPilot uses a small, modular ASP.NET Core architecture. Controllers focus on HTTP concerns, services own conversion logic, and the reusable `DevPilot.Core` package contains dependency-free utilities.

```mermaid
flowchart TD
    Client[Developer / API Client] --> Swagger[Swagger / HTTP Client]
    Swagger --> Controllers[Focused API Controllers]
    Controllers --> Validation[Request Validation]
    Validation --> Services[Application Services]
    Services --> Core[DevPilot.Core]
    Controllers --> Health[Health Endpoint]
    App[ASP.NET Core Pipeline] --> Handler[Global Exception Handler]
    Handler --> Problem[ProblemDetails]
    Services --> Response[Validated JSON Response]
```

## Utility request flow

```mermaid
sequenceDiagram
    participant C as Client
    participant A as Controller
    participant V as Validation
    participant S as Core Utility

    C->>A: POST /api/encoding/url/encode
    A->>V: Validate request body
    alt Missing value
        V-->>A: Invalid request
        A-->>C: 400 Bad Request
    else Valid value
        A->>S: Encode URL component
        S-->>A: Encoded string
        A-->>C: 200 OK
    end
```

## Conversion request flow

```mermaid
sequenceDiagram
    participant C as Client
    participant A as Controller
    participant V as ConversionInputValidator
    participant S as ModelConversionService

    C->>A: POST conversion request
    A->>V: Validate input/root
    alt Invalid request
        V-->>A: Validation failure
        A-->>C: 400 error
    else Valid request
        A->>S: Convert
        S-->>A: Generated model
        A-->>C: 200 response
    end
```

## Design principles

- Keep controllers thin and focused on HTTP concerns.
- Keep reusable, dependency-free behavior in `DevPilot.Core`.
- Validate untrusted input before expensive parsing.
- Return predictable HTTP errors.
- Never expose internal exception details in production.
- Keep examples runnable with the REST Client extension or similar HTTP tooling.
- Add tests for reusable behavior and keep CI responsible for build/test/package validation.
