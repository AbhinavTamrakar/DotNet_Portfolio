# Frontend — Next.js

Next.js 16 (App Router) + TypeScript + Tailwind CSS 4 frontend for the personal portfolio.

See the repository [README](../README.md) and [docs/SETUP.md](../docs/SETUP.md) for the full
setup, environment variables and database instructions.

## Commands

PowerShell blocks `npm.ps1` on this machine, so use `npm.cmd`:

```powershell
npm.cmd install
npm.cmd run dev     # http://localhost:3000
npm.cmd run lint
npm.cmd run build
npm.cmd run start
```

## Environment

Copy `.env.local.example` to `.env.local`:

```text
API_BASE_URL=https://localhost:7162/api
NEXT_PUBLIC_SITE_URL=http://localhost:3000
```

## Current status

Scaffold only (Phase 2). Public pages and the admin dashboard are built in Phases 6–7.
