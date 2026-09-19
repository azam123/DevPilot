# DevPilot Architecture

DevPilot follows a small, modular ASP.NET Core architecture. Controllers focus on HTTP concerns, services own conversion logic, and shared infrastructure handles validation and unexpected failures.

```mermaid
flowchart TD
    Client[Developer / API Client] --> Swagger[Swagger / HTTP Client]
    Swagger --> Controllers[API Controllers]
    Controllers --> Validator[ConversionInputValidator]
    Validator --> Services[Application Services]
    Services --> Core[Reusable DevPilot.Core]
    Controllers --> Health[Health Endpoint]
    App[ASP.NET Core Pipeline] --> Handler[GlobalExceptionHandler]
    Handler --> Problem[ProblemDetails]
    Services --> Response[JSON / XML / C# Response]
```

## Conversion request flow

```mermaid
sequenceDiagram
    participant C as Client
    participant A as Controller
    participant V as Validator
    participant S as Conversion Service

    C->>A: POST conversion request
    A->>V: Validate input/root
    alt Invalid request
        V-->>A: ArgumentException
        A-->>C: 400 error
    else Valid request
        A->>S: Convert
        S-->>A: Generated model
        A-->>C: 200 response
    end
```

## Design principles

- Keep controllers thin and services reusable.
- Validate untrusted input before expensive parsing.
- Return predictable HTTP errors.
- Never expose internal exception details in production.
- Keep examples runnable with the REST Client extension or similar HTTP tooling.
