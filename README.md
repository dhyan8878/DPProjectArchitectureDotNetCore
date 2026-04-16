# 🚀 DPProjectArchitectureDotNetCore

A **production-grade Clean Architecture .NET 8 Web API** demonstrating modern backend practices including **CQRS, JWT Authentication, Refresh Tokens, Redis Caching, PostgreSQL, and Dockerized Infrastructure**.

---

# 🧠 Project Overview

This project is designed as a **reference implementation of enterprise backend architecture** using:

* Clean Architecture principles
* CQRS with MediatR
* Repository + Unit of Work patterns
* JWT Authentication with Refresh Tokens
* Redis caching
* PostgreSQL database
* Dockerized infrastructure (DB + Cache + Tools)

---

# 🏗️ Architecture

```
DPProjectArchitectureDotNetCore/
├── DP.Domain/          → Entities, Business Rules
├── DP.Application/     → CQRS, Interfaces, DTOs, Validation
├── DP.Infrastructure/  → EF Core, Repositories, Redis, JWT
└── DP.WebAPI/          → Controllers, Middleware, Configuration
```

---

## 🔄 Dependency Flow

```
WebAPI → Application → Domain
              ↑
        Infrastructure
```

✔ No direct dependency from Application → Infrastructure
✔ Fully compliant with Clean Architecture

---

# ⚙️ Tech Stack

| Category         | Technology                |
| ---------------- | ------------------------- |
| Framework        | .NET 8                    |
| Architecture     | Clean Architecture + CQRS |
| ORM              | EF Core (Code First)      |
| Database         | PostgreSQL                |
| Cache            | Redis                     |
| Auth             | JWT + Refresh Tokens      |
| Validation       | FluentValidation          |
| Logging          | Serilog                   |
| Containerization | Docker                    |
| API Docs         | Swagger                   |

---

# 🔥 Features Implemented

## 🧱 Core Architecture

* Clean Architecture (Domain, Application, Infrastructure, WebAPI)
* CQRS using MediatR
* Repository Pattern
* Unit of Work Pattern
* Dependency Injection via Extension Methods

---

## 🛒 Product Management

* Create Product
* Update Product
* Delete Product
* Get Products (with paging, sorting, filtering)

---

## 📦 Order Management

* Create Order with multiple items
* Get Orders with pagination

---

## 🔐 Authentication & Authorization

* User Registration (DB-based)
* Login with JWT
* Role-based Authorization
* Password hashing using BCrypt
* Refresh Token mechanism (secure session handling)

---

## ⚡ Redis Caching

* Caching applied on query layer (CQRS Read side)
* Cache key strategy implemented
* Cache invalidation on:

  * Create Product
  * Update Product
  * Delete Product
* TTL (Time-to-Live) support

---

## 🛡️ API Enhancements

* Global Exception Handling Middleware
* FluentValidation pipeline behavior
* Structured logging using Serilog

---

## 🐘 Database (PostgreSQL)

* EF Core Code-First approach
* IEntityTypeConfiguration used for all entities
* Migrations support

---

# 🐳 Docker Setup (Infrastructure)

This project uses Docker for:

* PostgreSQL
* pgAdmin (DB GUI)
* Redis
* RedisInsight (Cache GUI)

---

## 📦 Run Infrastructure

```bash
docker-compose up -d
```

---

## 🌐 Services

| Service      | URL                              |
| ------------ | -------------------------------- |
| API (local)  | https://localhost:<port>/swagger |
| pgAdmin      | http://localhost:5050            |
| RedisInsight | http://localhost:5540            |

---

## 🔑 Default Credentials

### PostgreSQL

```
Host: localhost (external) / postgres (docker)
Port: 5433 (external) / 5432 (docker)
Username: postgres
Password: postgres
Database: dpdb
```

### pgAdmin

```
Email: admin@admin.com
Password: admin
```

---

# 🧪 API Usage

## 🔐 Register

```http
POST /api/auth/register
```

```json
{
  "email": "user@test.com",
  "password": "1234"
}
```

---

## 🔐 Login

```http
POST /api/auth/login
```

```json
{
  "email": "user@test.com",
  "password": "1234"
}
```

---

## 🔄 Refresh Token

```http
POST /api/auth/refresh
```

```json
{
  "refreshToken": "your_refresh_token"
}
```

---

## 🛒 Create Product

```http
POST /api/products
```

---

## 📄 Get Products

```http
GET /api/products?pageNumber=1&pageSize=10
```

---

# 🧠 Key Design Decisions

* **Caching only on Query side (CQRS best practice)**
* **No direct EF Core usage in Application layer**
* **All external dependencies abstracted via interfaces**
* **Password hashing abstracted via IPasswordHasher**
* **Token generation abstracted via IJwtTokenGenerator**

---

# ⚠️ Development Setup

### Run API Locally (Recommended)

* API runs in Visual Studio / `dotnet run`
* DB + Redis run via Docker

---

## 🔧 Connection Strings

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5433;Database=dpdb;Username=postgres;Password=postgres"
}
```

---

# 🏆 What This Project Demonstrates

✔ Enterprise-level Clean Architecture
✔ Scalable and maintainable backend design
✔ Secure authentication system
✔ High-performance caching strategy
✔ Docker-based infrastructure
✔ Real-world production patterns

---

# 🚀 Future Enhancements

* CI/CD pipeline (GitHub Actions)
* Cloud deployment (AWS / Azure)
* API versioning
* Distributed logging (ELK / Seq)
* Background jobs (Hangfire)
* Domain Events (advanced DDD)

---

# 👨‍💻 Author

**Dhyanesh Patel**

---

# ⭐ If You Like This Project

Give it a ⭐ on GitHub and feel free to fork or contribute!

---
