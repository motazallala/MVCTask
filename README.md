# 🔐 ASP.NET Core Authentication System (N-Tier Architecture)

This project demonstrates how to implement **user authentication and authorization** using **ASP.NET Core Web API and MVC**, following a clean **N-Tier architecture**. It uses **cookie-based authentication**, **manual credential handling**, and **stored procedures** for data access—no Entity Framework or ASP.NET Identity involved.

---

## 📚 Project Overview

- 🌐 **API**: ASP.NET Core Web API for registration and login endpoints
- 🎨 **MVC**: ASP.NET Core MVC project acting as the frontend client
- 🧱 **Architecture**:
  - `Core`: Domain models and service interfaces
  - `DAL`: ADO.NET-based data access using stored procedures
  - `API`: Authentication and user management endpoints
  - `MVC`: Consumes the API and manages authentication via cookies

---

## 🚀 Features

- 🔐 User registration and login with hashed passwords
- 🍪 Cookie-based authentication (manual implementation)
- ✅ Authentication and authorization with user roles
- ⚙️ Stored procedures for all DB operations (no EF Core)
- 🧩 ADO.NET-based data access layer
- 📦 Clean and testable N-Tier architecture
- 📡 MVC consumes API using `HttpClient`

---

## 🧰 Tech Stack

- ASP.NET Core 7/8 (Web API + MVC)
- SQL Server
- ADO.NET
- Cookie Authentication
- Visual Studio or JetBrains Rider

---

## 📁 Solution Structure

```
📦 MVCTask
├── 📂 MVCTask.Core             # Domain Models & Interfaces
├── 📂 MVCTask.Infrastructure   # Common Classes For All Layers
├── 📂 MVCTask.DAL              # Data Access Layer (ADO.NET + Stored Procedures)
├── 📂 MVCTask.API              # ASP.NET Core Web API
└── 📂 MVCTask.MVC              # MVC Client (UI + Cookie Management)
```

---

## 🛠️ Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/motazallala/MVCTask.git
```

### 2. Configure the Database

- Create a SQL Server database
- Run the provided `.sql` scripts to create:
  - `Users`, `Roles`, and `UserRoles` tables
  - All required stored procedures

### 3. Update Connection Strings

In `appsettings.json` for both API and MVC projects:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=YourDbName;Trusted_Connection=True;"
}
```

### 4. Run the Projects

- Start both API and MVC projects
- Use the MVC login page to submit credentials via `HttpClient` to the API
- On successful login, MVC creates an authentication cookie

---

## 💡 Key Concepts

- 🧾 Passwords are stored as hashes
- 🔄 MVC calls the API for login and registration
- 🧠 Credentials are manually validated in the service layer
- ⚙️ API does not use JWT or ASP.NET Identity
- 🔐 Cookies are used for authenticated sessions

---

## ✅ To Do / Extend

- 🔁 Add refresh session/token expiration logic
- 🔒 Role-based view filtering in MVC
- 💬 Logging & exception handling
- 📊 Admin panel for managing users/roles

---

## 📄 License

This project is licensed under the MIT License. Feel free to use, modify, and distribute it.

---

## 🙌 Author

Made with ❤️ by Motaz Allala

---

