# Project Plan

This document tracks implementation phases. It is updated as work progresses.

**Last updated:** 2026-10-31 (Phase 2 complete)

---

## Fixed technical decisions

| Decision | Value |
|---|---|
| Backend framework | ASP.NET Core Web API, .NET 10 (`net10.0`) |
| Frontend framework | Next.js 16 (App Router), TypeScript |
| Styling | Tailwind CSS 4 |
| Database | MySQL Server 9.7.1 (single instance, service `MySQL97`) |
| EF Core | 10.0.12 (pinned) |
| MySQL provider | Oracle `MySql.EntityFrameworkCore` 10.0.12 |
| MySQL driver | `MySql.Data` 26.10.0 (transitive) |
| EF CLI | `dotnet-ef` 10.0.12 (local tool manifest) |
| Database name | `personal_portfolio` |
| Application DB account | `portfolio_app` (dedicated, least privilege) |
| Secrets | .NET user-secrets / environment variables |
| Package manager | npm via `npm.cmd` / `npx.cmd` (PowerShell policy) |
| Repository | Single repo, `main` branch, frontend + backend separated |
| Project root | `E:\Projects\personal-portfolio` |

Out of scope for the initial version: multi-user roles, SaaS features, blog comments,
author management, PostgreSQL.

---

## Phase tracker

### Phase 1 — Environment verification — COMPLETE

- [x] Node.js 24.19.0, npm 11.17.0, Git 2.55.0, .NET SDK 10.0.114
- [x] MySQL services identified: only `MySQL97` (running, 9.7.1)
- [x] Live server version confirmed as 9.7.1 (handshake) and Workbench record
- [x] MySQL 8.0.46 present as unused binaries only (no service, no data directory)
- [x] EF Core 10.0.12 + Oracle provider 26.10.0 + `net10.0` compatibility proven
- [x] Next.js 16.4.3 + React 19.3 + Tailwind 4 scaffold proven
- [x] Disk space, editors, network reachability checked
- [x] Plan document corrected: MySQL instead of PostgreSQL

### Phase 2 — Project initialization — COMPLETE

- [x] Root folder and Git repository (`main`)
- [x] Next.js frontend (`frontend/`), TypeScript + Tailwind 4 + ESLint
- [x] Frontend production build (`next build`) verified
- [x] Frontend lint (`eslint`) verified
- [x] Backend solution `backend/Portfolio.sln` with four projects
- [x] Project references: Api → Application, Infrastructure; Infrastructure → Application, Domain
- [x] `dotnet build` verified (0 warnings, 0 errors)
- [x] Local `dotnet-ef` 10.0.12 tool manifest
- [x] Root `.gitignore`, `.gitattributes`, README
- [x] Documentation: project plan, setup, database schema, API documentation
- [x] Initial commit (no push)

### Phase 3 — Backend foundation — NOT STARTED

- [ ] Install EF Core 10.0.12 + Oracle provider + Identity packages
- [ ] Configure `DefaultConnection` via user-secrets
- [ ] Create `PortfolioDbContext` and entity configurations
- [ ] Define all entities and relationships from `docs/DATABASE_SCHEMA.md`
- [ ] Configure ASP.NET Core Identity
- [ ] Create initial migration and apply it
- [ ] Verify database connectivity and migration history
- [ ] Seed one administrator without hardcoded credentials

### Phase 4 — Public APIs — NOT STARTED

- [ ] Profile, projects, skills, experience, education, certifications
- [ ] Blogs, social links, contact inquiries
- [ ] Pagination, filtering, search, consistent error responses
- [ ] OpenAPI documentation verified

### Phase 5 — Authentication — NOT STARTED

- [ ] Cookie authentication, login/logout/me endpoints
- [ ] CSRF protection, rate limiting, cookie security
- [ ] Protect all admin endpoints; test unauthorized access

### Phase 6 — Public frontend — NOT STARTED

- [ ] Home, About, Projects + details, Skills, Experience, Education,
      Certifications, Blog + details, Contact
- [ ] Shared layout, navigation, footer, metadata/SEO
- [ ] Data loaded from the real API

### Phase 7 — Admin dashboard — NOT STARTED

- [ ] Login, overview, profile, projects, skills, experience, education,
      certifications, blogs, inquiries, settings
- [ ] Protected routes and admin layout

### Phase 8 — File upload and integration — NOT STARTED

- [ ] Storage provider integration, upload validation, orphan cleanup
- [ ] Connect all forms to the API

### Phase 9 — Testing and optimization — NOT STARTED

- [ ] Backend and frontend test suites, end-to-end workflows
- [ ] Responsive/accessibility/security review, production build

### Phase 10 — Deployment — NOT STARTED

- [ ] Backend hosting, production MySQL, frontend hosting, domains, secrets,
      storage, backups, final verification

---

## Pending environment actions (owner)

- [ ] Create `personal_portfolio` database and `portfolio_app` user in Workbench
      (SQL in `docs/SETUP.md`).
- [ ] Trust the ASP.NET Core development HTTPS certificate when requested
      (`dotnet dev-certs https --trust`). Not yet applied — requires owner approval.
