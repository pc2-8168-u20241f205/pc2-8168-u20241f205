# Openpay Settlements API

## Overview

Openpay Settlements API is an ASP.NET Core RESTful service for registering merchant disbursement orders. It exposes the `POST /api/v1/disbursement-orders` operation and persists the aggregate in a MySQL relational database.

**Author:** Candy Milagros Vizcarra Mamani  
**Student code:** u20241f205  
**NRC:** 8168

## Architecture

The solution follows Domain-Driven Design, bounded contexts, layered architecture and a command-oriented CQRS approach.

- **Settlements bounded context:** owns the `DisbursementOrder` aggregate root, its tracking and store identifiers, command, repository contract, application service and REST resources.
- **Shared bounded context:** contains reusable value objects (`DateInterval`, `FinancialAmount`), audit abstractions, `Result`, repository contracts and Unit of Work.
- **Domain:** contains aggregates, value objects, enums, commands and repository abstractions.
- **Application:** coordinates the create-order use case and duplicate detection.
- **Interfaces:** exposes the REST endpoint and uses request/response Resources and Assemblers.
- **Infrastructure:** implements persistence with Entity Framework Core and MySQL, Fluent API mapping, Unit of Work and audit interception.

## Business rules

- `OrderTrackingCode` is unique; duplicate values return HTTP `409 Conflict`.
- `OpenpayReference` and `StoreIdentifier` wrap non-empty GUID values.
- `DateInterval` requires `ScheduledTo` to be later than `ScheduledFrom`; `HasElapsed` returns true only when the reference date is later than `ScheduledTo`.
- `FinancialAmount.Amount` cannot be negative. `CurrencyCode` cannot be null, empty or whitespace. `MultiplyBy` preserves the currency code.
- `DisbursementStatus` values are `Queued = 10` (default), `Processed = 11`, `Cancelled = 12` and `Failed = 13`.
- `User` is required and limited to 20 characters. `Remarks` is optional and limited to 85 characters.
- The aggregate implements audit timestamps. The persistence interceptor sets `CreatedAt` and `UpdatedAt` on insert and refreshes `UpdatedAt` on update.
- Responses include the generated identifier and `updatedAt`.

## Technology stack

- C# 14 and .NET 10 (`net10.0`)
- ASP.NET Core Web API
- Entity Framework Core 10
- MySQL (`MySql.EntityFrameworkCore`)
- OpenAPI and Scalar
- Request localization for `en`, `en-US`, `es` and `es-PE`

## Requirements

- .NET SDK 10
- MySQL Server 8 or a compatible MySQL server

## Configuration

Edit `PC28168.U20241f205.Openpay.Platform/appsettings.json` and set the MySQL credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;port=3306;database=openpay_settlements;user=root;password=your_password"
  }
}
```

The database name is `openpay_settlements`. On startup, EF Core `EnsureCreated` creates the database and tables if they do not exist. For a production deployment, replace this bootstrap approach with versioned EF Core migrations.

## Run

From the solution directory:

```bash
dotnet restore pc28168u20241f205.sln
dotnet build pc28168u20241f205.sln
dotnet run --project PC28168.U20241f205.Openpay.Platform
```

Scalar API documentation: `http://localhost:5128/scalar`  
OpenAPI document: `http://localhost:5128/openapi/v1.json`

## Create a disbursement order

### Endpoint

`POST /api/v1/disbursement-orders`

### Request

```json
{
  "orderTrackingCode": "7a3e6d4c-6c6e-4f7e-a3b8-1a2b3c4d5e6f",
  "affiliateStoreId": "4d7c3b2a-1f0e-4d5c-9a8b-7c6d5e4f3a2b",
  "scheduledFrom": "2026-12-01",
  "scheduledTo": "2026-12-10",
  "transferCapAmount": 2500.00,
  "transferCapCurrencyCode": "PEN",
  "user": "operator01",
  "remarks": "December settlement",
  "status": "Queued"
}
```

`status` is optional and defaults to `Queued`. Do not send `id`; it is database-generated.

### Successful response

HTTP `201 Created`

```json
{
  "id": 1,
  "orderTrackingCode": "7a3e6d4c-6c6e-4f7e-a3b8-1a2b3c4d5e6f",
  "affiliateStoreId": "4d7c3b2a-1f0e-4d5c-9a8b-7c6d5e4f3a2b",
  "scheduledFrom": "2026-12-01",
  "scheduledTo": "2026-12-10",
  "transferCapAmount": 2500.00,
  "transferCapCurrencyCode": "PEN",
  "status": "Queued",
  "user": "operator01",
  "remarks": "December settlement",
  "updatedAt": "2026-11-18T15:00:00+00:00"
}
```

### Error handling and localization

- `400 Bad Request`: invalid request fields or business-rule violations.
- `409 Conflict`: duplicate `orderTrackingCode` or unique-constraint conflict.
- `500 Internal Server Error`: unexpected server failure.

Send `Accept-Language: en`, `en-US`, `es` or `es-PE` to select the response culture. English is the default.

## Example request

The file `requests/disbursement-orders.http` contains a ready-to-use HTTP request.

## Naming conventions

- C# namespaces, types, properties and methods use PascalCase.
- Private fields (where required) use camelCase.
- URL paths use kebab-case.
- Database table and column names use snake_case; table names are plural and the primary key is `id`.

## Scope

Only the create operation (`POST`) is implemented, as required by the assignment. CORS, security and automated testing are outside the requested scope.
