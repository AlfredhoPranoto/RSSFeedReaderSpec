---
description: "Tasks for Add Subscriptions (MVP)"
---

# Tasks: Add Subscriptions (MVP)

**Input**: Design documents from `specs/001-add-subscriptions/` (plan.md, spec.md, data-model.md, contracts/)

## Phase 1: Setup (Shared Infrastructure)

- [x] T001 Create project skeleton: add `RSSFeedReader.sln`, `backend/RSSFeedReader.Api/`, `frontend/RSSFeedReader.UI/`
- [x] T002 Initialize backend project in `backend/RSSFeedReader.Api/` (ASP.NET Core Web API template)
- [x] T003 Initialize frontend project in `frontend/RSSFeedReader.UI/` (Blazor WebAssembly template)
- [x] T004 [P] Add repo tooling: create `global.json` (pin .NET SDK), add `.editorconfig` and basic CI workflow placeholder in `.github/workflows/ci.yml`
- [x] T005 Remove Blazor demo pages: delete `frontend/RSSFeedReader.UI/Pages/Home.razor`, `frontend/RSSFeedReader.UI/Pages/Counter.razor`, `frontend/RSSFeedReader.UI/Pages/Weather.razor` and verify only intended pages use `@page "/"`
- [x] T006 Verify port and routing configuration: update `frontend/RSSFeedReader.UI/wwwroot/appsettings.json`, `backend/RSSFeedReader.Api/Properties/launchSettings.json`, and confirm backend CORS settings in `backend/RSSFeedReader.Api/Program.cs`

---

## Phase 2: Foundational (Blocking prerequisites)

- [x] T007 [P] Create `backend/RSSFeedReader.Api/Models/Subscription.cs` (fields: `id`, `url`, `addedAt`)
- [x] T008 [P] Implement in-memory store `backend/RSSFeedReader.Api/Services/SubscriptionStore.cs` (add/list operations)
- [x] T009 [P] Register `SubscriptionStore` in DI and configure CORS in `backend/RSSFeedReader.Api/Program.cs`
- [x] T010 Implement API controller `backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs` with:
    - `GET /api/subscriptions` → returns list per contract
    - `POST /api/subscriptions` → accepts `{ "url": "..." }`, returns created subscription (201)
- [x] T011 Create backend unit tests in `backend/tests/Unit/SubscriptionStoreTests.cs` (xUnit): tests for add and list behavior

---

## Phase 3: User Story 1 - Add a feed subscription (Priority: P1) 🎯 MVP

**Goal**: Allow a user to paste a feed URL and add it to the subscription list; UI updates immediately.

**Independent Test**: Run the app locally, add a URL via the UI, and verify the new subscription appears in the list without page reload.

- [x] T012 [P] [US1] Add frontend DTO `frontend/RSSFeedReader.UI/Models/SubscriptionDto.cs`
- [x] T013 [P] [US1] Implement frontend service `frontend/RSSFeedReader.UI/Services/SubscriptionApiService.cs` to call backend endpoints
- [x] T014 [US1] Create `frontend/RSSFeedReader.UI/Pages/Subscriptions.razor` (input field, Add button, list display)
- [x] T015 [US1] Wire UI to service and ensure immediate state update in `Subscriptions.razor`
- [X] T016 [US1] Add component tests `frontend/tests/SubscriptionsComponentTests.cs` (bUnit) verifying add behavior and list update
- [X] T017 [US1] Add integration test `tests/integration/AddSubscriptionFlowTests.cs` (POST -> GET verification) — optional if CI supports headless execution

---

## Phase N: Polish & Cross-Cutting Concerns

- [X] T018 [P] Update documentation: `specs/001-add-subscriptions/quickstart.md` and top-level `README.md` with run steps
- [X] T019 [P] Security check: verify no HttpClient-based feed fetching code exists in MVP branches (scan `**/*HttpClient*` or search for `System.Net.Http` usage)
- [X] T020 [P] Run formatters/linters and ensure CI passes (configure `dotnet format` in CI)

---

## Dependencies & Execution Order

- **Phase 1** (Setup) must be completed before **Phase 2** (Foundational).
- **Phase 2** must be completed before **Phase 3** (User Story implementation).
- Within **Phase 2**, `T007` (model) and `T008` (store) should be implemented before `T010` (controller).
- Tests (`T011`, `T016`, `T017`) should be written as early as possible and run in CI.

## Parallel Opportunities

- `T004`, `T007`, `T008`, and `T011` can run in parallel by different contributors.
- Frontend tasks (`T012`-`T015`) can proceed in parallel with backend tasks after the foundational store/controller endpoints exist.

## Implementation Strategy

- MVP-first: deliver `T001`-`T015` to provide the minimal working demo (Add + List). Stop and validate the independent test for US1 before proceeding to polish tasks.

## Total tasks

- Total: 20 tasks
- Tasks for US1: 6 (T012–T017)
