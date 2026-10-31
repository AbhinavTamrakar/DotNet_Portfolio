# Personal Portfolio

A professional, responsive personal portfolio website with a secure admin dashboard.

The public site presents the owner's profile, projects, skills, experience, education,
certifications and blog. The admin dashboard allows the owner to manage all of that
content without editing source code.

---

## Technology stack

| Layer | Technology | Version |
|---|---|---|
| Frontend | Next.js (App Router), TypeScript | 16.4.3 / 5.x |
| Styling | Tailwind CSS | 4.x |
| Backend | ASP.NET Core Web API (C#) | .NET 10 (`net10.0`) |
| Database | MySQL Server | 9.7.1 |
| ORM | Entity Framework Core | 10.0.12 |
| MySQL provider | Oracle `MySql.EntityFrameworkCore` | 10.0.12 |
| DB driver | MySQL Connector/NET (`MySql.Data`) | 26.10.0 |
| API docs | ASP.NET Core OpenAPI (Swagger UI added later) | 10.0.14 |
| Version control | Git + GitHub | — |

### Version alignment notes

- EF Core packages (`Microsoft.EntityFrameworkCore*`, `Microsoft.AspNetCore.Identity.EntityFrameworkCore`)
  and the `dotnet-ef` tool are pinned to **10.0.12** to match the Oracle MySQL provider exactly.
- `Microsoft.AspNetCore.OpenApi` stays on **10.0.14** because it ships with the installed
  ASP.NET Core 10.0.14 shared framework; it is not an EF Core package.
- Do **not** add Pomelo, a second MySQL EF provider, or mix EF Core 9/10 packages.
- MySQL is the only database for this project. PostgreSQL is not used.

---

## Repository structure

```text
personal-portfolio/
├── frontend/                     # Next.js App Router + TypeScript + Tailwind CSS
│   ├── app/
│   ├── public/
│   ├── .env.local.example
│   └── package.json
├── backend/                      # ASP.NET Core API
│   ├── Portfolio.Api/            # Startup, controllers, middleware
│   ├── Portfolio.Application/    # Use cases / services / DTO contracts
│   ├── Portfolio.Domain/         # Entities and domain rules
│   ├── Portfolio.Infrastructure/ # EF Core, Identity, external services
│   └── Portfolio.sln
├── docs/                         # Project documentation
├── dotnet-tools.json             # Local .NET tool manifest (dotnet-ef 10.0.12)
├── .gitattributes
├── .gitignore
└── README.md
```

Project source and project data live on `E:`. Tool caches (npm, NuGet) remain on `C:`.

---

## Current status

**Phase 2 — Project initialization: complete.**
**Phase 3 — Backend foundation: in progress (migration generated, pending database apply).**

- [x] Repository initialized on `main`
- [x] Next.js frontend scaffolded and production build verified
- [x] ASP.NET Core solution with four projects and project references
- [x] Local `dotnet-ef` tool manifest
- [x] Root `.gitignore`, `.gitattributes`, README and project documentation
- [x] EF Core 10.0.12 + Oracle MySQL provider + Identity packages
- [x] Domain entities, `PortfolioDbContext`, entity configurations, UTC audit timestamps
- [x] ASP.NET Core Identity wired to EF Core; secure admin seeder
- [x] `InitialCreate` migration generated and MySQL DDL reviewed
- [ ] Apply the migration to `personal_portfolio` (needs the `portfolio_app` secret)

No application features (public pages, APIs, authentication endpoints) are implemented yet.
See [docs/PROJECT_PLAN.md](docs/PROJECT_PLAN.md) for the phase tracker.

---

## Prerequisites

Verified local environment:

| Tool | Version |
|---|---|
| Node.js | 24.19.0 |
| npm | 11.17.0 |
| Git | 2.55.0 |
| .NET SDK | 10.0.114 |
| MySQL Server | 9.7.1 (service `MySQL97`) |
| MySQL Workbench | 8.0.47 |
| VS Code | 1.116.0 |

PowerShell execution policy blocks `npm.ps1` on this machine, so use **`npm.cmd`** and
**`npx.cmd`** (see [docs/SETUP.md](docs/SETUP.md)).

---

## Quick start

### Backend

```powershell
# One-time local secrets (never committed)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<connection-string>" --project backend/Portfolio.Api
dotnet user-secrets set "Admin:Email" "<admin-email>" --project backend/Portfolio.Api
dotnet user-secrets set "Admin:Password" "<strong-admin-password>" --project backend/Portfolio.Api

# Apply the generated migration once the database and account exist
dotnet dotnet-ef database update --project backend/Portfolio.Infrastructure --startup-project backend/Portfolio.Api

dotnet run --project backend/Portfolio.Api
```

The administrator is seeded at startup only when `Admin:Email` and `Admin:Password` are set;
no credentials are hardcoded.

- HTTPS: `https://localhost:7162`
- HTTP: `http://localhost:5213`
- OpenAPI document (development): `https://localhost:7162/openapi/v1.json`

### Frontend

```powershell
cd frontend
Copy-Item .env.local.example .env.local
npm.cmd install     # already installed during scaffolding
npm.cmd run dev
```

- App: `http://localhost:3000`

---

## Documentation

| Document | Purpose |
|---|---|
| [docs/PROJECT_PLAN.md](docs/PROJECT_PLAN.md) | Phase tracker and scope |
| [docs/SETUP.md](docs/SETUP.md) | Local environment and database setup |
| [docs/DATABASE_SCHEMA.md](docs/DATABASE_SCHEMA.md) | Planned schema and conventions |
| [docs/API_DOCUMENTATION.md](docs/API_DOCUMENTATION.md) | Planned REST API surface |

---

## Security rules for this repository

- Never commit database passwords, API keys, email credentials or auth secrets.
- Use .NET user-secrets or environment variables for local development.
- Admin authentication uses HttpOnly cookies — never `localStorage`.
- Every admin endpoint must be authenticated and CSRF-protected.
- Uploaded files are validated, renamed and stored outside the application source tree.
- Do not push to GitHub without explicit owner approval.
