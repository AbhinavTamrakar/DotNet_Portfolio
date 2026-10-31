# Local Setup

Verified development environment and the exact steps to run this project locally.

**Last verified:** 2026-10-31

---

## 1. Verified tooling

| Tool | Verified version | Notes |
|---|---|---|
| Windows | 11 Pro (10.0.26200) | PowerShell 5.1 |
| Node.js | 24.19.0 | satisfies Next.js `>=20.9.0` |
| npm | 11.17.0 | invoke as `npm.cmd` in PowerShell |
| Git | 2.55.0.windows.5 | repository branch `main` |
| .NET SDK | 10.0.114 | `net10.0`, runtime 10.0.14 |
| MySQL Server | 9.7.1 | only running instance, service `MySQL97` |
| MySQL 8.0.46 | installed binaries only | **no service, no data directory — unused** |
| MySQL Workbench | 8.0.47 | client for administration |
| VS Code | 1.116.0 | no Visual Studio installed |

### PowerShell: npm is blocked

The Windows execution policy blocks `npm.ps1` / `npx.ps1`:

```text
npm.ps1 cannot be loaded because running scripts is disabled on this system.
```

By owner decision, the execution policy is **not** changed. Always use:

```powershell
npm.cmd install
npm.cmd run dev
npx.cmd create-next-app@latest
```

PowerShell 7 (`pwsh`) is not installed; Windows PowerShell 5.1 is used.

### MySQL client is not on PATH

Use the full path or add it to PATH (owner approval required):

```powershell
& 'C:\Program Files\MySQL\MySQL Server 9.7\bin\mysql.exe' --version
```

The 9.7 client matches the running 9.7.1 server. The 8.0.46 client in
`C:\Program Files\MySQL\MySQL Server 8.0\bin` is legacy and should not be the default.

---

## 2. Repository layout

```text
E:\Projects\personal-portfolio\
├── frontend\        Next.js 16 + TypeScript + Tailwind CSS 4
├── backend\         ASP.NET Core 10 solution
├── docs\            Documentation
├── dotnet-tools.json   Local dotnet-ef 10.0.12 manifest
└── .gitignore / .gitattributes / README.md
```

All source files and project data live on `E:`. npm and NuGet caches remain on `C:`
(`C:\Users\DELL\AppData\Local\npm-cache`, `C:\Users\DELL\.nuget\packages`).
`C:` had ~8.9 GB free at verification time — monitor it during heavy builds.

---

## 3. Database setup (owner action)

MySQL is the only database. Confirmed server: **MySQL 9.7.1** on `127.0.0.1:3306`,
`lower_case_table_names=1`.

Run the following in MySQL Workbench as an administrator. Choose a strong password and
keep it out of source control:

```sql
CREATE DATABASE IF NOT EXISTS personal_portfolio
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_0900_ai_ci;

CREATE USER IF NOT EXISTS 'portfolio_app'@'localhost' IDENTIFIED BY '<STRONG_PASSWORD>';
CREATE USER IF NOT EXISTS 'portfolio_app'@'127.0.0.1' IDENTIFIED BY '<STRONG_PASSWORD>';

GRANT ALL PRIVILEGES ON personal_portfolio.* TO 'portfolio_app'@'localhost';
GRANT ALL PRIVILEGES ON personal_portfolio.* TO 'portfolio_app'@'127.0.0.1';
FLUSH PRIVILEGES;
```

Verify without exposing the password in shell history:

```powershell
& 'C:\Program Files\MySQL\MySQL Server 9.7\bin\mysql.exe' `
  -h 127.0.0.1 -P 3306 -u portfolio_app -p personal_portfolio `
  -e "SELECT VERSION(), DATABASE();"
```

Expected: `9.7.1` and `personal_portfolio`.

> `root` credentials are never used by the application and must not be committed
> or shared.

### Connection string

Stored only in .NET user-secrets for local development:

```text
Server=127.0.0.1;Port=3306;Database=personal_portfolio;
User=portfolio_app;Password=<STRONG_PASSWORD>;SslMode=Preferred;
```

Production uses environment variables or the host's secret store.

---

## 4. Backend

```powershell
# from repository root
dotnet restore backend/Portfolio.sln
dotnet build backend/Portfolio.sln

# local secrets (Phase 3)
dotnet user-secrets init --project backend/Portfolio.Api
dotnet user-secrets set "ConnectionStrings:DefaultConnection" `
  "Server=127.0.0.1;Port=3306;Database=personal_portfolio;User=portfolio_app;Password=<STRONG_PASSWORD>;SslMode=Preferred;" `
  --project backend/Portfolio.Api

dotnet run --project backend/Portfolio.Api
```

- HTTPS: `https://localhost:7162`
- HTTP: `http://localhost:5213`
- OpenAPI (development): `https://localhost:7162/openapi/v1.json`

### EF Core migrations

Use the local tool manifest (already installed):

```powershell
dotnet tool restore
dotnet dotnet-ef migrations add InitialCreate --project backend/Portfolio.Infrastructure --startup-project backend/Portfolio.Api
dotnet dotnet-ef database update --project backend/Portfolio.Infrastructure --startup-project backend/Portfolio.Api
```

Pinned: EF Core **10.0.12**, `dotnet-ef` **10.0.12**, provider **10.0.12**.
Do not install Pomelo or a second MySQL provider, and do not mix EF Core 9/10 packages.

### HTTPS development certificate

A valid ASP.NET Core dev certificate exists (`CN=localhost`, valid until 2027-10-31),
but it is **not yet marked trusted**. When trust is required:

```powershell
dotnet dev-certs https --trust
```

This prompts for confirmation and modifies the current user's certificate store.
It has not been applied yet.

---

## 5. Frontend

```powershell
cd frontend
Copy-Item .env.local.example .env.local
npm.cmd install
npm.cmd run dev      # http://localhost:3000
npm.cmd run lint
npm.cmd run build
```

Environment variables live in `frontend/.env.local` and are never committed.

Current scaffold: Next.js 16.4.3, React 19.3.0, Tailwind CSS 4
(`@import "tailwindcss"`), ESLint 9, TypeScript 5.

---

## 6. Ports

| Service | Port |
|---|---|
| Next.js dev server | 3000 |
| API HTTP | 5213 |
| API HTTPS | 7162 |
| MySQL | 3306 (occupied by `MySQL97`) |

All application ports were free at verification time.

---

## 7. Troubleshooting

| Symptom | Cause | Fix |
|---|---|---|
| `npm.ps1 cannot be loaded` | PowerShell execution policy | Use `npm.cmd` / `npx.cmd` |
| `mysql` not recognized | MySQL `bin` not on PATH | Use the full 9.7 path |
| `Access denied for user 'root'` | root password not available | Use `portfolio_app`, never root |
| Build fails with provider version conflict | mixed EF Core versions | Keep all EF packages at 10.0.12 |
| Long path errors | `LongPathsEnabled=0` | Keep paths short; repo root is short by design |
| Low disk on `C:` | tool caches | Project data stays on `E:`; clean caches if needed |
