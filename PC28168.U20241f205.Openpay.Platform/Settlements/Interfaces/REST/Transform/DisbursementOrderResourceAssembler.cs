using System.Globalization;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Aggregates;
using PC28168.U20241f205.Openpay.Platform.Settlements.Interfaces.REST.Resources;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Interfaces.REST.Transform;
/// <summary>Maps domain aggregates to public API resources.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public static class DisbursementOrderResourceAssembler
{
    /// <summary>Creates a response resource from an aggregate.</summary>
    /// <param name="order">Persisted order.</param>
    /// <returns>API response representation.</returns>
    public static DisbursementOrderResource ToResource(DisbursementOrder order) => new(
        order.Id, order.OrderTrackingCode.Value.ToString(), order.AffiliateStoreId.Value.ToString(),
        order.ExecutionSchedule.ScheduledFrom.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        order.ExecutionSchedule.ScheduledTo.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
        order.TransferCap.Amount, order.TransferCap.CurrencyCode, order.Status.ToString(),
        order.User, order.Remarks, order.UpdatedAt);
}
