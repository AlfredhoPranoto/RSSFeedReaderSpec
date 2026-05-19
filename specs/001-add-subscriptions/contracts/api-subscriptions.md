# API Contract: Subscriptions

## Endpoints

### GET /api/subscriptions
- Description: Return the current list of subscriptions stored in memory.
- Response 200 (application/json):

```json
[ { "id": "<string>", "url": "<string>", "addedAt": "<ISO-8601>" }, ... ]
```

### POST /api/subscriptions
- Description: Add a new subscription. MVP accepts any non-empty URL string; no validation against feedness is performed.
- Request (application/json):

```json
{ "url": "https://example.com/feed" }
```

- Response 201 (application/json): created subscription object

```json
{ "id": "<string>", "url": "https://example.com/feed", "addedAt": "2026-05-19T12:00:00Z" }
```

### Notes
- CORS: Backend must allow the configured frontend origin (default `http://localhost:5213`) during local development.
- Errors: For MVP, minimal error handling is acceptable; return `400` for empty request bodies.
