# 🚀 TinyURL 

A high-performance URL shortening service built with **Blazor Web**, **ASP.NET Core Minimal API**, and **Azure SQL**. This project demonstrates professional cloud architecture, automated deployments, and secure identity management.

## 🏛️ Project Architecture
The application is split into three main components:
* **Frontend:** Blazor Interactive Server App hosted on Azure App Service.
* **Backend:** Minimal API handling business logic and database interactions.
* **Database:** Azure SQL (Serverless) using Microsoft Entra-only authentication.

---

## 💎 Part 3: Infrastructure & Cloud (40 Points)

### 1. Hosted Database 
* **Provider:** Azure SQL Database (`tinyURLdb`).
* **Connection:** Secured via **Managed Identity** (No passwords in code).
* **Status:** Successfully migrated from local SQLite to production SQL Server.

### 2. CI/CD Pipeline 
* **Tool:** GitHub Actions.
* **Trigger:** Automated build and deploy on every `push` to the `main` branch.

### 3. Serverless Cron Job
* **Service:** Azure Functions (Timer Trigger).
* **Schedule:** `0 0 * * * *` (Runs every hour).
* **Logic:** Automatically cleans up expired or processed links from the database.

### 4. Infrastructure as Code
* **Format:** ARM Template / Bicep.
* **Description:** Contains the full definition for the Web Apps, SQL Server, and Storage accounts.

### 5. Environment Variables & Secrets
* **Database:** `DefaultConnection` stored in Azure Connection Strings.
* **Secrets:** `SecretToken` stored in Azure App Settings (not in `appsettings.json`).

### 6. Logging
* **Sink:** Azure Blob Storage.
* **Description:** Application logs are automatically streamed to a storage container for audit and debugging.

---


   
