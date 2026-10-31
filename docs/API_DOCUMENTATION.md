# API Documentation

**Status: design only. No endpoints are implemented yet (Phases 4–5).**

Base URL (development): `https://localhost:7162/api`
OpenAPI document (development): `https://localhost:7162/openapi/v1.json`
Swagger UI will be enabled in Phase 4.

---

## Conventions

- REST-style JSON over HTTPS.
- DTOs only — EF Core entities are never returned directly.
- Consistent status codes: `200`, `201`, `204`, `400`, `401`, `403`, `404`, `409`, `422`, `429`, `500`.
- Validation errors use ASP.NET Core `ValidationProblemDetails`.
- Other errors use a consistent `ProblemDetails` shape; **no** database exception text is
  exposed to clients.
- Collections are paginated (`page`, `pageSize`) and return a wrapper with `items`,
  `page`, `pageSize`, `totalCount`.
- Public endpoints only return published content.
- Every `/api/admin/*` endpoint requires an authenticated administrator.
- State-changing requests require CSRF protection.
- Cancellation tokens are accepted by asynchronous actions.

### Authentication model (planned)

- ASP.NET Core Identity password hashing.
- Cookie authentication, HttpOnly + Secure + SameSite.
- No tokens in `localStorage`.
- Login and inquiry endpoints are rate limited.
- Preferred deployment topology: same site, or Next.js server-side proxy/BFF so cookies
  are first-party.

---

## Public endpoints

| Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/profile` | Public profile |
| GET | `/api/projects` | Published projects (paged, filter by category/technology, search) |
| GET | `/api/projects/{slug}` | Project details with images and technologies |
| GET | `/api/skills` | Skills grouped/filtered by category |
| GET | `/api/experience` | Work experience |
| GET | `/api/education` | Education |
| GET | `/api/certifications` | Certifications |
| GET | `/api/blogs` | Published posts (paged, filter by category/tag, search) |
| GET | `/api/blogs/{slug}` | Blog details |
| GET | `/api/social-links` | Visible social links |
| POST | `/api/inquiries` | Submit contact inquiry |

`POST /api/inquiries` behavior: validate → spam protection/rate limit → persist → respond.
Email notification is optional and must never be required for the inquiry to be stored.

## Authentication endpoints

| Method | Endpoint | Purpose |
|---|---|---|
| POST | `/api/admin/login` | Authenticate administrator (sets HttpOnly cookie) |
| POST | `/api/admin/logout` | Sign out |
| GET | `/api/admin/me` | Current authentication status |

## Admin endpoints (all protected)

| Resource | Endpoints |
|---|---|
| Projects | `GET/POST /api/admin/projects`, `GET/PUT/DELETE /api/admin/projects/{id}` |
| Skills | `GET/POST /api/admin/skills`, `PUT/DELETE /api/admin/skills/{id}` |
| Profile | `GET/PUT /api/admin/profile` |
| Experience | `GET/POST /api/admin/experience`, `PUT/DELETE /api/admin/experience/{id}` |
| Education | `GET/POST /api/admin/education`, `PUT/DELETE /api/admin/education/{id}` |
| Certifications | `GET/POST /api/admin/certifications`, `PUT/DELETE /api/admin/certifications/{id}` |
| Blog posts | `GET/POST /api/admin/blogs`, `GET/PUT/DELETE /api/admin/blogs/{id}` |
| Blog categories | `GET/POST /api/admin/blog-categories`, `PUT/DELETE /api/admin/blog-categories/{id}` |
| Blog tags | `GET/POST /api/admin/blog-tags`, `PUT/DELETE /api/admin/blog-tags/{id}` |
| Inquiries | `GET /api/admin/inquiries`, `GET/DELETE /api/admin/inquiries/{id}`, `PATCH .../read-state` |
| Social links | `GET/POST /api/admin/social-links`, `PUT/DELETE /api/admin/social-links/{id}` |
| Site settings | `GET/PUT /api/admin/site-settings` |
| Uploads | `POST /api/admin/uploads`, `DELETE /api/admin/uploads/{id}` |
| Dashboard | `GET /api/admin/dashboard` |

## Upload rules (planned)

Allowed: images (`jpg`, `jpeg`, `png`, `webp`), documents (`pdf`) for CV/certificates.
The server validates extension **and** content, limits size, generates safe filenames,
stores files outside the source tree, and never allows executable uploads.

## Implementation status

| Phase | Area | Status |
|---|---|---|
| 4 | Public endpoints | not implemented |
| 5 | Authentication / admin protection | not implemented |
| 5–7 | Admin CRUD | not implemented |
| 8 | Uploads | not implemented |

This table is updated as endpoints are implemented and tested.
