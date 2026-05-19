# Research: Add Subscriptions (MVP)

Decision: Target runtime and tool choices
- Decision: Use C# with .NET 8 (recommended). Rationale: current supported runtime with modern Blazor + ASP.NET features and good tooling/IDE support. Alternatives: .NET 7 (older LTS), .NET 6 (wider installed base). Recommendation favors .NET 8 unless CI/tooling constraints require otherwise.

Decision: Ports and local config
- Decision: Use the port conventions from the TechStack: backend `http://localhost:5151`, frontend `http://localhost:5213`. Rationale: aligns with documented tech notes and simplifies CORS configuration. Alternative: pick different ports if collisions occur; ensure `frontend/wwwroot/appsettings.json` matches backend port.

Decision: Testing
- Decision: Use xUnit for backend unit tests and bUnit for Blazor component tests. Rationale: common .NET testing ecosystem and good integration with CI. Alternatives: NUnit, MSTest; xUnit recommended for new .NET projects.

Decision: Contracts and API shape
- Decision: Provide a minimal API surface in MVP: `GET /api/subscriptions` and `POST /api/subscriptions` (body: `{ "url": "..." }`). Rationale: satisfies UI interactions without fetching feeds.

Security & Governance notes
- MVP: No network operations. Any Extended-MVP that enables fetching MUST include a short threat model, timeouts, content-size limits, and sanitization.

Unresolved items / Actionables
- Pin .NET SDK: confirm exact SDK minor version to pin in `global.json` and CI images (RECOMMEND: 8.0.x). Owner: NEEDS CLARIFICATION.
- Blazor cleanup: remove demo pages (`Home.razor`, `Counter.razor`, `Weather.razor`) and verify routing before implementing the UI. Owner: NEEDS CLARIFICATION — implement as Phase 1 Foundational task.

Decision Summary
- Chosen stack and conventions allow a minimal, testable MVP to be delivered quickly while preserving clear upgrade paths for Extended-MVP features.
