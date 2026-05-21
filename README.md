GitHub Spec Kit commands will use stakeholder documentation to help generate the constitution.md, spec.md, and plan.md files.

# RSS Feed Reader

A minimal web application to manage RSS feed subscriptions.
Backend: ASP.NET Core Web API (.NET 8) | Frontend: Blazor WebAssembly (.NET 8)

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)

## Run locally

Open **two terminals** from the repository root:

**Terminal 1 — Backend API** (port 5151):
```bash
dotnet run --project backend/RSSFeedReader.Api --urls "http://localhost:5151"
```

**Terminal 2 — Frontend UI** (port 5213):
```bash
dotnet run --project frontend/RSSFeedReader.UI --urls "http://localhost:5213"
```

Open `http://localhost:5213/subscriptions` in your browser.

> On Windows with Git Bash, replace `dotnet` with the full path:
> `"/c/Program Files/dotnet/dotnet"`

## Run tests

```bash
# Backend unit tests
dotnet test backend/tests/Unit/RSSFeedReader.Api.Tests.csproj

# Frontend component tests (bUnit)
dotnet test frontend/tests/RSSFeedReader.UI.Tests.csproj

# Integration tests
dotnet test tests/integration/RSSFeedReader.Integration.Tests.csproj

# All tests
dotnet test RSSFeedReader.sln
```

## Project structure

```
backend/RSSFeedReader.Api/        # ASP.NET Core Web API
  Controllers/                   # SubscriptionsController (GET + POST /api/subscriptions)
  Models/                        # Subscription entity
  Services/                      # SubscriptionStore (in-memory)
backend/tests/Unit/               # xUnit backend unit tests

frontend/RSSFeedReader.UI/        # Blazor WebAssembly app
  Pages/Subscriptions.razor      # Add + list subscriptions page
  Services/SubscriptionApiService.cs
  Models/SubscriptionDto.cs
frontend/tests/                   # bUnit component tests

tests/integration/                # ASP.NET Core integration tests (WebApplicationFactory)

specs/001-add-subscriptions/      # Feature specification & design docs
```

