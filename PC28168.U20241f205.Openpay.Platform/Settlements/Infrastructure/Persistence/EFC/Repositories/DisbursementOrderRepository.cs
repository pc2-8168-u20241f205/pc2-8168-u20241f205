using Microsoft.EntityFrameworkCore;
using PC28168.U20241f205.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Aggregates;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Repositories;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Infrastructure.Persistence.EFC.Repositories;
/// <summary>EF Core repository implementation for disbursement orders.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed class DisbursementOrderRepository(AppDbContext context) : IDisbursementOrderRepository
{
    public Task<bool> ExistsByTrackingCodeAsync(Guid trackingCode, CancellationToken cancellationToken = default) =>
        context.DisbursementOrders.AnyAsync(x => x.OrderTrackingCode.Value == trackingCode, cancellationToken);
    public async Task AddAsync(DisbursementOrder order, CancellationToken cancellationToken = default) =>
        await context.DisbursementOrders.AddAsync(order, cancellationToken);
}
