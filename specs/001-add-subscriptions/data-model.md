# Data Model: Add Subscriptions (MVP)

## Entities

### Subscription
- **Description**: Represents a user-saved feed URL in the MVP subscription list.
- **Fields**:
  - `id` (string): Opaque identifier (GUID recommended) — primary key for in-memory storage.
  - `url` (string): The feed URL as entered by the user. MVP accepts any non-empty string.
  - `addedAt` (string / datetime): UTC timestamp when the subscription was added (optional but recommended for UI sorting).

## Validation rules (MVP)
- `url` MUST be a non-empty string. No further URL/feed validation is required for the MVP.
- For Extended-MVP: validate URL scheme (http/https), enforce maximum length, and consider normalization and deduplication rules.

## State transitions
- `New` -> `Added` (user submits URL) — no deletion or persistence in MVP.
