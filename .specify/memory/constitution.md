# RSS Feed Reader Spec Constitution
<!--
SYNC IMPACT REPORT
Version change: none -> 1.0.0
Modified principles:
- [PRINCIPLE_1_NAME] -> Security & Data Handling (NON-NEGOTIABLE)
- [PRINCIPLE_2_NAME] -> Maintainability & Simplicity
- [PRINCIPLE_3_NAME] -> Testability & Quality
- [PRINCIPLE_4_NAME] -> Observability & Error Handling
- [PRINCIPLE_5_NAME] -> Versioning, Reviews & Release Discipline
Added sections:
- Security Requirements
- Development Workflow & Quality Gates
Removed sections:
- None
Templates requiring updates:
- .specify/templates/plan-template.md ✅ updated
- .specify/templates/spec-template.md ✅ updated
- .specify/templates/tasks-template.md ✅ updated
Follow-up TODOs:
- None
-->

## Core Principles

### Security & Data Handling (NON-NEGOTIABLE)
- MVP constraint: The MVP MUST NOT perform outbound network requests. Subscriptions are stored in-memory only; no HTTP clients or feed parsing code may be enabled in the MVP branch.
- Extended-MVP: Any feature that adds network fetching (manual refresh or background) MUST include: a threat model, HTTP timeouts and cancellation, URL validation, content-size limits, HTML sanitization (e.g., HtmlSanitizer), and explicit review of CORS and origin policies.
- Never render untrusted HTML as raw markup. Sanitize feed content before any display. Log and surface benign error messages; never expose stack traces to the UI.

### Maintainability & Simplicity
- Keep a small, clear project layout (separate `backend` / `frontend` when used). Implement single-responsibility services and small, testable modules.
- BEFORE implementing UI features: remove Blazor template demo pages (`Home.razor`, `Counter.razor`, `Weather.razor`) and verify routing to ensure no ambiguous routes.
- Prefer explicit structure over clever abstractions. Defer persistence and background processing until Extended-MVP.

### Testability & Quality
- Core subscription functionality MUST have unit tests (add + list operations) before merging feature work that modifies subscription logic.
- Integration tests are REQUIRED for any API endpoints added for feed operations. Tests are a gating requirement for PRs that change behavior.
- Use a consistent test runner for the platform (e.g., xUnit for .NET) and include instructions in the feature `spec.md` and `plan.md`.

### Observability & Error Handling
- Use structured logging for important events (add subscription, fetch attempt, fetch failure). Include enough context to reproduce issues locally.
- Surface clear, user-friendly error messages in the UI (e.g., "Failed to load feed"). Implement a simple health or readiness endpoint for the backend when feasible.

### Versioning, Reviews & Release Discipline
- Use semantic versioning (MAJOR.MINOR.PATCH). Bump rules: MAJOR for breaking governance or principle removals; MINOR for adding principles or mandatory sections; PATCH for wording/clarity fixes.
- All changes to core behavior must go through PR with at least one approver and passing CI. PRs that amend the constitution require explicit changelogs and a version bump.

## Security Requirements
- MVP: No network calls. The app runs locally and stores subscriptions in memory only.
- Extended-MVP: Feed fetching MUST be manual (explicit user action) and follow hardened rules: HTTP client timeouts (e.g., 10s), size limits, sanitization of HTML, and safe parsing via `System.ServiceModel.Syndication` or similar. Rate-limit and fail gracefully.
- CORS: Backend must allow only configured frontend origins. Document required ports and origins in the feature plan.

## Development Workflow & Quality Gates
- Phase gates require constitution compliance: security plan for network features, Blazor cleanup verification, and required tests for subscription logic.
- Branching: follow feature branch naming `[###-feature-name]`. Use PR templates that reference the constitution and checklist.
- CI: PRs must run unit and integration tests and report status. Merge when CI green and reviewer approval obtained.

## Governance
- The constitution is the source of truth for project practices. Amendments MUST be proposed via a documented PR that includes: rationale, migration steps, templates impacted, and a proposed version bump.
- Approval: Constitution amendments require one maintainer approval and passing CI. For governance-impacting changes (MAJOR), require two approvers.
- Versioning: Follow semantic versioning. The maintainer merging the PR MUST update the `Version` line and `Last Amended` date.

**Version**: 1.0.0 | **Ratified**: 2026-05-18 | **Last Amended**: 2026-05-18
