# Todo List API

This project is a simple Todo List API!

Made using ASP.NET Core, with PostgreSQL as the database, JWT for authentication, and FastEndpoints for a faster access to the endpoints.

Challenge URL: [https://roadmap.sh/projects/todo-list-api](https://roadmap.sh/projects/todo-list-api)

---

## 🚀 Features

- User registration and login (JWT authentication)
- CRUD operations for tasks
- PostgreSQL database integration
- FastEndpoints for high-performance APIs
- OpenAPI/Scalar UI for API documentation

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

### Setup

1. **Clone the repository:**
   ```bash
   git clone <your-repo-url>
   cd ToDoApp
   ```

2. **Configure the database:**
   - Update the connection string in `TodoApp.Api/appsettings.json` to match your PostgreSQL setup.

3. **Apply migrations:**
   ```bash
   dotnet ef database update --project TodoApp.Api
   ```

4. **Run the API:**
   ```bash
   dotnet run --project TodoApp.Api
   ```

5. **Access API documentation:**
   - Scalar UI: [http://localhost:####/scalar](http://localhost:####/scalar)

---

## 🔑 Authentication

- Register a new user via `POST /api/auth/signup`
- Login via `POST /api/auth/login` to receive a JWT token
- Use the JWT token in the `Authorization: Bearer <token>` header for protected endpoints

---

## 📚 API Endpoints

### Auth

- `POST /api/auth/signup` — Register a new user
- `POST /api/auth/login` — Login and receive JWT

### Tasks

- `GET /api/getTasks` — Get all tasks for the authenticated user
- `POST /api/addTask` — Add a new task
- `PUT /api/updateTaskContent` — Update task content
- `PUT /api/updateTaskState` — Update task state
- `DELETE /api/deleteTask` — Delete a task

> For full details and request/response schemas, see the [Scalar UI](http://localhost:####/scalar) when running the project.

---

## 🧪 Testing

- Unit tests are in the `TodoApp.Tests` project.
- Run tests with:
  ```bash
  dotnet test
  ```
