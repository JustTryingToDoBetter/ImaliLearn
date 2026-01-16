# ImaliLearn

## Problem
ImaliLearn is the backend foundation for a learning platform that needs secure user management and an API surface for educational data. The current goal is to establish the infrastructure—authentication, persistence, and routing—so feature work can grow safely as requirements evolve.

## Approach
Your technical reasoning:
- Architecture decisions: separated API, Application, Domain, and Infrastructure projects to keep concerns isolated and ready for growth.
- Algorithms used: JWT bearer token validation and Identity role seeding for baseline security and authorization.
- Trade-offs considered: keeping domain/application layers minimal for now while investing in API and infrastructure setup.

## Implementation Highlights
- Key modules explained: `Program.cs` wires dependency injection, EF Core, Identity, and JWT; health and weather forecast controllers provide sample endpoints.
- Why certain tools/frameworks were chosen: ASP.NET Core for API hosting, EF Core with Npgsql for PostgreSQL, and Swagger for API exploration in development.

## Results
What works, metrics, screenshots, sample output.
- `GET /api/health` returns a simple uptime payload, e.g. `{ "status": "Healthy", "timestamp": "2026-01-16T12:39:56Z" }`.
- Swagger UI is enabled in development for quick endpoint discovery.
- A sample weather forecast endpoint returns randomized forecast data.

## What I’d Improve Next
Shows maturity and engineering judgment.
- Flesh out domain entities and application services for courses, lessons, and enrollments.
- Add authentication endpoints (register/login) and validation around JWT issuance.
- Add persistence repositories and tests for the service layer.
- Introduce structured logging/monitoring and CI coverage for the API.
