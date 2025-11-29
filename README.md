# Amigurumi Store

Microservices scaffold for an Amigurumi storefront and admin console, modeled after the conventions in `telus` (monorepo layout, Docker-first orchestration, shared UI packages).

## Stack
- Backend: ASP.NET Core 8 minimal APIs (Catalog, Ordering, Identity) with EF Core + SQL Server.
- Frontend: React 19 RC + Vite, two apps (storefront + admin) sharing UI/config packages.
- Docker: Compose spins up SQL Server, the three APIs, and both web apps on an `amigurumi-net` bridge.
- Backend structure mirrors TELUS patterns: Application layer per service (MediatR commands/queries, DTOs/mappings) with API projects acting as thin transport shells.

## Services
- `catalog-api` (`backend/src/Services/Catalog/Catalog.Api`): CRUD for products, seeded with three plushies.
- `ordering-api` (`backend/src/Services/Ordering/Ordering.Api`): order creation and status updates.
- `identity-api` (`backend/src/Services/Identity/Identity.Api`): basic registration/login with JWT issuing and a seeded admin user (`admin@amigurumi.local` / `ChangeMe123!`).

## Frontend
- Storefront: `frontend/apps/storefront` consumes catalog/orders endpoints and renders the public shop.
- Admin: `frontend/apps/admin` manages catalog inventory.
- Shared packages live under `frontend/packages` (`@amigurumi/ui` + `@amigurumi/config`).

## Run with Docker
```bash
docker compose up --build
```
Services/ports:
- SQL Server: `localhost:1433`
- Identity API: `http://localhost:5001`
- Catalog API: `http://localhost:5002`
- Ordering API: `http://localhost:5003`
- Storefront: `http://localhost:5173`
- Admin: `http://localhost:5174`

## Local development (without Docker)
1) Backend: restore/build each service from `backend/` with `dotnet restore && dotnet run` in the service folder. Update `appsettings.json` connection strings if your SQL Server differs.
2) Frontend: from `frontend/`, run `npm install` then `npm run dev:storefront` or `npm run dev:admin` (env vars `VITE_*` must point at running APIs).

### Version note
.NET 10 and React 19.2 are not released at the time of scaffolding. The code targets .NET 8 and React 19 RC (`19.0.0-rc.1`) so you can run the stack today; bump the versions when the requested releases become available.
