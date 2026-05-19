# Quickstart: Run Add Subscriptions MVP (Local)

This quickstart documents the minimal steps to run the MVP locally once the `backend` and `frontend` projects are created.

Prerequisites
- .NET SDK (pin exact version in `global.json` — recommended: .NET 8.x)
- `dotnet` CLI available on PATH

Default ports
- Backend API: `http://localhost:5151`
- Frontend UI: `http://localhost:5213`

Backend (expected commands after project scaffold)

```powershell
dotnet build backend/RSSFeedReader.Api/RSSFeedReader.Api.csproj
dotnet run --project backend/RSSFeedReader.Api --urls "http://localhost:5151"
```

Frontend (expected commands after project scaffold)

```powershell
dotnet run --project frontend/RSSFeedReader.UI --urls "http://localhost:5213"
```

Configuration notes
- Ensure `frontend/wwwroot/appsettings.json` contains the correct backend API base URL, for example:

```json
{
  "ApiBaseUrl": "http://localhost:5151/api/"
}
```

Before implementing or running the UI: remove Blazor demo pages (`Home.razor`, `Counter.razor`, `Weather.razor`) and verify routing to avoid ambiguous routes.
