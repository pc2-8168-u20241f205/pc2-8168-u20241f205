PC28168.U20241f205.Openpay.Platform
Overview

This project implements an Openpay platform focused on managing disbursement orders through a Domain-Driven Design (DDD) approach and bounded contexts.

The solution is organized into the following bounded contexts:

Shared: Contains shared domain models, value objects, repository abstractions, and common application patterns.
Settlements: Handles disbursement orders, including domain logic, application services, persistence, and REST API endpoints.
Authorship

GitHub Repository: https://github.com/pc2-8168-u20241f205/pc2-8168-u20241f205

GitHub Username: candyvizz

Technologies
C#
.NET
ASP.NET Core
Entity Framework Core
SQL Database
Project Structure
text
PC28168.U20241f205.Openpay.Platform/
├── Shared/
│   ├── Application/
│   ├── Domain/
│   └── Infrastructure/
├── Settlements/
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   └── Interfaces/
└── Program.cs

docs/
requests/
README.md

Version Control

This project uses Git and Git Flow for version control.

Commit messages follow the Conventional Commits specification.