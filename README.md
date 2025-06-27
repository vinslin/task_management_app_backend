# 🧠 Task Management System – ASP.NET Core Web API

A real-world **Task Management System** built with **ASP.NET Core Web API**, demonstrating two architectural approaches:

- ✅ **Version 1:** Traditional **N-tier** architecture
- ✅ **Version 2:** Modern **CQRS pattern** using **MediatR** and **Stored Procedures**

This project showcases scalable backend engineering with clean separation of concerns, real-world validation, and data access best practices.

---

## 🏗️ Architecture Overview

### 🔹 Version 1 – N-Tier Architecture

- Controller → Service → Repository → EF Core
- Uses **Entity Framework Core** with LINQ queries
- Straightforward and ideal for MVP or smaller projects

### 🔹 Version 2 – CQRS Architecture

- Uses **MediatR** for Command and Query separation
- All database operations are performed via **Stored Procedures**
- Business logic and handler coordination lives in `/services/CQRS`
- DTOs, Validators, and Mapping live in the `/resources` folder

---

## 🗂️ Project Structure

📁 task_management_app_backend
├── Controllers/ # API entry points (v1 and v2)
├── Middleware/ # Custom exception handling
├── data/
│ ├── Data/ # ApplicationDbContext
│ └── Repository/ # Repo interfaces and implementations (V1)
├── services/
│ ├── IServices/ # Abstractions
│ └── CQRS/ # Command / Query Handlers (V2)
├── resources/
│ ├── Dtos/ # Request / Response / Middle DTOs
│ ├── Validators/ # FluentValidation
│ ├── Mapper/ # AutoMapper Profiles
│ └── StoredProcedures/ # .sql scripts for DB ops
└── Program.cs # Application startup and DI


---

## 💡 Core Concepts Implemented

| Concept             | Details                                           |
|---------------------|---------------------------------------------------|
| **CQRS**            | Separate read and write models (via MediatR)      |
| **MediatR**         | Decouples request handling from controllers       |
| **Stored Procedures** | Used instead of EF queries for DB performance  |
| **FluentValidation** | Strong input validation layer                    |
| **AutoMapper**      | DTO ↔ Entity conversion                          |
| **N-Tier (V1)**     | Classical architecture for learning & comparison |
| **Middleware**      | Global error handling with logs                  |
| **DTO Design**      | Request, Response, and Intermediate segregation   |

---

## 🔁 Request Flow Comparison

### 🧱 Version 1 – N-Tier


# 🧭 Version 1 – N-Tier Architecture Code Flow

This section explains how **Version 1** of the Task Management System follows a classic **N-Tier Architecture**. It’s a clean, layered approach where each layer has a dedicated responsibility and communicates only with the layer directly below or above it.

---

## ✅ Flow Overview
Client → Controller → Service → Repository → EF Core → DTO → Response
![image](https://github.com/user-attachments/assets/81b8a989-f5ff-461c-b13b-cbb1a97e3ded)

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

### ⚙️ Version 2 – CQRS + Stored Procedure

Client → V2 Controller → MediatR (via Handler) → Service → Stored Procedure → DTO → Response


![image](https://github.com/user-attachments/assets/3aa6fd46-c1a9-4590-99b3-f9d3efeeebad)
![image](https://github.com/user-attachments/assets/81cdd086-1773-4a69-b841-b832d9e361d0)

---


## 🧪 How to Run

### ✅ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/)
- SQL Server (LocalDB or full instance)
- Visual Studio / VS Code
- Swagger / Postman

### ⚙️ Setup

```bash
# 1. Clone the repository
git clone https://github.com/your-username/task-management-api.git

# 2. Open the solution in Visual Studio or VS Code

# 3. Update the connection string in appsettings.json

# 4. Run the application
dotnet run

Then Open 
https://localhost:5001/swagger



📈 Future Enhancements

🔐 JWT Authentication & Role-based Authorization

📬 Background jobs for notifications (Hangfire)

📊 Admin dashboard with real-time stats

🌐 Frontend integration (React or Blazor)

