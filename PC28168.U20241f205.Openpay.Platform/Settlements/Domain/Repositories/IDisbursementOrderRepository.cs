using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Aggregates;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Repositories;
/// <summary>Persistence contract for disbursement order aggregates.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public interface IDisbursementOrderRepository
{
    /// <summary>Checks whether a tracking reference already exists.</summary>
    Task<bool> ExistsByTrackingCodeAsync(Guid trackingCode, CancellationToken cancellationToken = default);
    /// <summary>Adds an order for persistence.</summary>
    Task AddAsync(DisbursementOrder order, CancellationToken cancellationToken = default);
}
