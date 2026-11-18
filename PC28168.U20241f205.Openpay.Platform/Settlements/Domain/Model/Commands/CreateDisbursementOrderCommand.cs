using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Enums;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Commands;
/// <summary>Input data for creating a disbursement order.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed record CreateDisbursementOrderCommand(Guid OrderTrackingCode, Guid AffiliateStoreId,
    DateOnly ScheduledFrom, DateOnly ScheduledTo, decimal TransferCapAmount, string TransferCapCurrencyCode,
    string User, string? Remarks, DisbursementStatus? Status);
