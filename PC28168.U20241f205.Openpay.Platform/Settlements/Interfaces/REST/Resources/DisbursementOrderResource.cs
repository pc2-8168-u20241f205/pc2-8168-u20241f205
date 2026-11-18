namespace PC28168.U20241f205.Openpay.Platform.Settlements.Interfaces.REST.Resources;
/// <summary>Representation of a persisted disbursement order.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed record DisbursementOrderResource(int Id, string OrderTrackingCode, string AffiliateStoreId,
    string ScheduledFrom, string ScheduledTo, decimal TransferCapAmount, string TransferCapCurrencyCode,
    string Status, string User, string? Remarks, DateTimeOffset? UpdatedAt);
