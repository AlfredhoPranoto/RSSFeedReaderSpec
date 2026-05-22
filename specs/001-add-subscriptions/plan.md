# Implementation Plan: Add Subscriptions (MVP)

**Branch**: `main-feat/RSS-subscriptions` | **Date**: 2026-05-19 | **Spec**: specs/001-add-subscriptions/spec.md
**Input**: Feature specification from `specs/001-add-subscriptions/spec.md`

## Summary

Implement the MVP subscription-management slice: a backend API exposing add/list endpoints backed by in-memory storage, and a Blazor WebAssembly frontend providing a simple page to paste a feed URL and view the subscription list. No network fetching, parsing, or persistent storage are included in the MVP.

## Technical Context

**Language/Version**: C# / .NET 8 (recommended) — NEEDS CLARIFICATION: pin exact SDK version for CI images
**Primary Dependencies**: ASP.NET Core Web API, Blazor WebAssembly (MVP)
**Storage**: In-memory List<Subscription> (MVP)
**Testing**: xUnit for backend unit tests; bUnit for Blazor component tests (recommendation; confirm)
**Target Platform**: Cross-platform (Windows/macOS/Linux) and browser (WASM) for the frontend
**Project Type**: Web application (backend + frontend)
**Performance Goals**: N/A for MVP (small, interactive demo)
**Constraints**: No network calls in MVP; immediate UI update on add; data lost on application stop
**Scale/Scope**: Single-user local demo, small codebase

## Constitution Check
*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Security**: PASS — MVP explicitly avoids network operations (no feed fetching/parsing).
- **Maintainability** (Blazor cleanup): NEEDS ACTION — Blazor demo pages (`Home.razor`, `Counter.razor`, `Weather.razor`) must be removed and routing verified before UI implementation. See task `T0031` in tasks template (Phase 1 Foundational).
- **Testing**: PASS (unit tests for add/list are required and will be included in tasks).
- **CI & PR**: PASS (PRs must run tests and have reviewer approval per constitution).

Actions required before implementing UI or merging plan:
- Remove Blazor demo pages and verify routing (Phase 1 Foundational task). 
- Pin and document the .NET SDK version to use in CI (minor gating step).

## Project Structure

### Documentation (this feature)

```text
specs/001-add-subscriptions/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── contracts/
│   └── api-subscriptions.md
└── tasks.md    # created later by /speckit.tasks
```

### Source Code (recommended layout)

```text
backend/RSSFeedReader.Api/        # ASP.NET Core Web API (add/list endpoints)
frontend/RSSFeedReader.UI/        # Blazor WebAssembly app (subscriptions page)
tests/                            # Unit/integration tests
```

**Structure Decision**: Use a two-project web application layout (`backend` + `frontend`) to keep responsibilities separate and enable later sharing of DTOs if desired.

## Complexity Tracking

No constitution violations require an exception; maintainability gate requires action (cleanup) but is a straightforward foundational task.

## Phase 0: Outline & Research (next steps)

1. Confirm .NET SDK minor version to pin in project/CI (recommend: .NET 8; confirm)
2. Confirm test runner/tools (xUnit, bUnit) and CI configuration
3. Verify Blazor demo page cleanup (owner/action)
4. Confirm port choices and CORS settings (defaults: backend `http://localhost:5151`, frontend `http://localhost:5213` per TechStack)

## Phase 1: Design & Contracts (deliverables)

- `data-model.md` — Subscription entity and validation rules
- `contracts/api-subscriptions.md` — API contract for add/list endpoints
- `quickstart.md` — local run instructions and port/CORS verification steps

## Outputs created by this command

- `specs/001-add-subscriptions/plan.md` (this file)
- `specs/001-add-subscriptions/research.md`
- `specs/001-add-subscriptions/data-model.md`
- `specs/001-add-subscriptions/quickstart.md`
- `specs/001-add-subscriptions/contracts/api-subscriptions.md`

