# Quickstart: Run Add Subscriptions MVP (Local)

## Prerequisites
- .NET 8 SDK — version pinned to `8.0.421` via `global.json`
- `dotnet` CLI on PATH (Windows: `C:\Program Files\dotnet\dotnet.exe`)

## Default ports
| Service | URL |
|---------|-----|
| Backend API | `http://localhost:5151` |
| Frontend UI | `http://localhost:5213` |

## Start the backend

```bash
dotnet run --project backend/RSSFeedReader.Api --urls "http://localhost:5151"
```

Swagger UI available at: `http://localhost:5151/swagger`

## Start the frontend

```bash
dotnet run --project frontend/RSSFeedReader.UI --urls "http://localhost:5213"
```

Then open `http://localhost:5213/subscriptions`.

## Run all tests

```bash
# Unit tests (backend)
dotnet test backend/tests/Unit/RSSFeedReader.Api.Tests.csproj

# Component tests (bUnit, frontend)
dotnet test frontend/tests/RSSFeedReader.UI.Tests.csproj

# Integration tests
dotnet test tests/integration/RSSFeedReader.Integration.Tests.csproj

# Full solution
dotnet test RSSFeedReader.sln
```

## Configuration

`frontend/RSSFeedReader.UI/wwwroot/appsettings.json` sets the backend base URL:

```json
{
  "ApiBaseUrl": "http://localhost:5151/api/"
}
```

CORS is configured in `backend/RSSFeedReader.Api/Program.cs` to allow `http://localhost:5213`.

## Notes
- Data is in-memory and lost when the backend restarts (MVP scope).
- No HTTPS required for local development; use the `http` profile.

