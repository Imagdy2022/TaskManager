# Task Manager — ElectroPi Technical Assessment

Full-stack task management application built with **ASP.NET Core 9** (CQRS / Clean Architecture) and **Angular 22**.

---

## Prerequisites

| Tool | Version |
|------|---------|
| .NET SDK | 9.0+ |
| Node.js | 18+ |
| SQL Server | 2019+ (local instance on `localhost`) |
| Angular CLI | 22+ (`npm i -g @angular/cli`) |

---

## Getting Started

### 1. Database

The app connects to SQL Server on `localhost` with SQL authentication:

```
Server=localhost;Database=TaskManagerDb;User Id=Islam;Password=Sosta@2022;
MultipleActiveResultSets=true;TrustServerCertificate=True
```

To change credentials, edit `src/TaskManager.API/appsettings.json` → `ConnectionStrings.DefaultConnection`.

Create the database and apply migrations:

```bash
cd src/TaskManager.API
dotnet ef database update
```

### 2. Backend API

```bash
cd src/TaskManager.API
dotnet run --urls http://localhost:5000
```

Swagger UI: http://localhost:5000/swagger

### 3. Frontend

```bash
cd task-manager-ui
npm install
npm start
```

App: http://localhost:4200

---

## Project Structure

```
src/
  TaskManager.Domain/          # Entities, enums (no dependencies)
  TaskManager.Application/     # CQRS handlers, validators, interfaces, DTOs
  TaskManager.Infrastructure/  # EF Core DbContext, repository implementations
  TaskManager.API/             # Controllers, middleware, DI wiring

task-manager-ui/
  src/app/
    projects/                  # ProjectList, ProjectDetail, ProjectForm components
    tasks/                     # TaskForm component
    shared/                    # ConfirmDialog component
    services/                  # ProjectService, TaskService (HttpClient)
    models/                    # TypeScript interfaces
```

---

## API Endpoints

All responses are wrapped in `ApiResponse<TItem>`:

```json
{ "isSuccess": true, "statusCode": 200, "message": "Success", "data": { ... }, "errors": null }
```

### Projects

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/projects` | List all projects |
| GET | `/api/projects/{id}` | Project detail with tasks |
| POST | `/api/projects` | Create project |
| PUT | `/api/projects/{id}` | Update project |
| DELETE | `/api/projects/{id}` | Delete project (cascades tasks) |

### Tasks

| Method | Route | Description |
|--------|-------|-------------|
| GET | `/api/tasks/{id}` | Get task by ID |
| GET | `/api/tasks/by-project/{projectId}` | Tasks for a project |
| GET | `/api/tasks/by-status/{status}` | Tasks filtered by status (0=ToDo, 1=InProgress, 2=Done) |
| POST | `/api/tasks` | Create task |
| PUT | `/api/tasks/{id}` | Update task |
| PATCH | `/api/tasks/{id}/status` | Update task status only |
| DELETE | `/api/tasks/{id}` | Delete task |

---

## Architecture Decisions

### Clean Architecture (4 layers)
Dependencies point inward: `API → Application → Domain`. Infrastructure implements interfaces defined in Application. This makes the domain and business logic testable without infrastructure concerns.

### CQRS with MediatR
Commands (mutations) and Queries (reads) live in separate classes/folders under `Application/{Entity}/Commands` and `Application/{Entity}/Queries`. Each handler has a single responsibility. The pipeline behavior (`ValidationBehavior<TRequest, TResponse>`) runs FluentValidation before every command.

### Repository Pattern
`IProjectRepository` and `ITaskRepository` interfaces live in Application; EF Core implementations live in Infrastructure. Switching the data store requires only swapping the Infrastructure project.

### ApiResponse\<TItem\> wrapper
Every endpoint returns a consistent envelope with `isSuccess`, `statusCode`, `message`, `data`, and `errors`. Error cases (404, 400, 500) are handled centrally in `ExceptionMiddleware` using the same wrapper.

### Microservices-ready layout
The folder structure mirrors a future split: Projects and Tasks are separate feature slices with their own commands, queries, validators, and repository interfaces. Extracting either into its own service requires moving the relevant Application slice and its Infrastructure implementation.

### Angular 22 — zoneless change detection
Angular 22 defaults to zoneless mode. `ChangeDetectorRef.detectChanges()` is called explicitly after every HTTP callback so the view updates immediately without needing Zone.js to patch async operations.

---

## Task Status Values

| Value | Label |
|-------|-------|
| 0 | To Do |
| 1 | In Progress |
| 2 | Done |
