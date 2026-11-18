using PC28168.U20241f205.Openpay.Platform.Shared.Domain.Model;
using PC28168.U20241f205.Openpay.Platform.Shared.Domain.Model.ValueObjects;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Enums;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.ValueObjects;

namespace PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Aggregates;

/// <summary>Aggregate root representing a merchant disbursement order.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed class DisbursementOrder : IAuditableEntity
{
    /// <summary>Gets the generated database identifier.</summary>
    public int Id { get; private set; }
    /// <summary>Gets the unique Openpay tracking reference.</summary>
    public OpenpayReference OrderTrackingCode { get; private set; } = null!;
    /// <summary>Gets the affiliated store identifier.</summary>
    public StoreIdentifier AffiliateStoreId { get; private set; } = null!;
    /// <summary>Gets the execution schedule.</summary>
    public DateInterval ExecutionSchedule { get; private set; } = null!;
    /// <summary>Gets the transfer cap.</summary>
    public FinancialAmount TransferCap { get; private set; } = null!;
    /// <summary>Gets the order status.</summary>
    public DisbursementStatus Status { get; private set; } = DisbursementStatus.Queued;
    /// <summary>Gets the operator user identifier.</summary>
    public string User { get; private set; } = string.Empty;
    /// <summary>Gets optional remarks.</summary>
    public string? Remarks { get; private set; }
    /// <inheritdoc />
    public DateTimeOffset? CreatedAt { get; set; }
    /// <inheritdoc />
    public DateTimeOffset? UpdatedAt { get; set; }

    private DisbursementOrder() { }

    /// <summary>Creates a disbursement order and enforces its business invariants.</summary>
    /// <param name="orderTrackingCode">Unique order tracking reference.</param>
    /// <param name="affiliateStoreId">Affiliated store identifier.</param>
    /// <param name="executionSchedule">Execution date interval.</param>
    /// <param name="transferCap">Maximum transfer amount.</param>
    /// <param name="user">Operator identifier (1-20 characters).</param>
    /// <param name="remarks">Optional remarks (up to 85 characters).</param>
    /// <param name="status">Initial status; defaults to Queued.</param>
    public DisbursementOrder(OpenpayReference orderTrackingCode, StoreIdentifier affiliateStoreId,
        DateInterval executionSchedule, FinancialAmount transferCap, string user, string? remarks = null,
        DisbursementStatus status = DisbursementStatus.Queued)
    {
        OrderTrackingCode = orderTrackingCode ?? throw new ArgumentNullException(nameof(orderTrackingCode));
        AffiliateStoreId = affiliateStoreId ?? throw new ArgumentNullException(nameof(affiliateStoreId));
        ExecutionSchedule = executionSchedule ?? throw new ArgumentNullException(nameof(executionSchedule));
        TransferCap = transferCap ?? throw new ArgumentNullException(nameof(transferCap));
        if (string.IsNullOrWhiteSpace(user)) throw new ArgumentException("User is required.", nameof(user));
        if (user.Length > 20) throw new ArgumentException("User cannot exceed 20 characters.", nameof(user));
        if (remarks?.Length > 85) throw new ArgumentException("Remarks cannot exceed 85 characters.", nameof(remarks));
        if (!Enum.IsDefined(status)) throw new ArgumentException("Status is not valid.", nameof(status));
        User = user.Trim();
        Remarks = remarks;
        Status = status;
    }
}
