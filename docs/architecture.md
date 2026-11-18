# Architecture overview

```text
HTTP Client
    |
    v
DisbursementOrdersController (Interfaces / REST)
    |  Resource -> Command (Assembler)
    v
DisbursementOrderCommandService (Application)
    |  business use case + duplicate check
    v
DisbursementOrder aggregate (Settlements / Domain)
    |
    v
IDisbursementOrderRepository + IUnitOfWork
    |
    v
EF Core Repository + AppDbContext (Infrastructure)
    |
    v
MySQL: openpay_settlements.disbursement_orders
```

`Shared` provides reusable `DateInterval` and `FinancialAmount` value objects, audit contracts, persistence abstractions and application result patterns. `Settlements` owns the disbursement-order domain model and use case.
