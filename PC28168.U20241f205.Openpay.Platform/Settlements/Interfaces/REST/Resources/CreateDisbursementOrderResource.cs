using System.ComponentModel.DataAnnotations;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Enums;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Interfaces.REST.Resources;
/// <summary>HTTP request resource used to create a disbursement order.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed class CreateDisbursementOrderResource
{
    /// <summary>Unique tracking GUID.</summary>
    [Required] public Guid? OrderTrackingCode { get; init; }
    /// <summary>Affiliated store GUID.</summary>
    [Required] public Guid? AffiliateStoreId { get; init; }
    /// <summary>First scheduled date (yyyy-MM-dd).</summary>
    [Required] public DateOnly? ScheduledFrom { get; init; }
    /// <summary>Last scheduled date (yyyy-MM-dd).</summary>
    [Required] public DateOnly? ScheduledTo { get; init; }
    /// <summary>Maximum amount that can be transferred.</summary>
    [Required] public decimal? TransferCapAmount { get; init; }
    /// <summary>Currency code, for example PEN.</summary>
    [Required] public string TransferCapCurrencyCode { get; init; } = string.Empty;
    /// <summary>Operator identifier.</summary>
    [Required, StringLength(20, MinimumLength = 1)] public string User { get; init; } = string.Empty;
    /// <summary>Optional remarks.</summary>
    [StringLength(85)] public string? Remarks { get; init; }
    /// <summary>Optional initial status; defaults to Queued.</summary>
    public DisbursementStatus? Status { get; init; }
}
