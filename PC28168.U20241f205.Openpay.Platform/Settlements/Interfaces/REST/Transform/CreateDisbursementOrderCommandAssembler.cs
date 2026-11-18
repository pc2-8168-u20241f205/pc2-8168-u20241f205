using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Commands;
using PC28168.U20241f205.Openpay.Platform.Settlements.Interfaces.REST.Resources;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Interfaces.REST.Transform;
/// <summary>Maps an HTTP request resource to an application command.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public static class CreateDisbursementOrderCommandAssembler
{
    /// <summary>Creates a command from a validated resource.</summary>
    public static CreateDisbursementOrderCommand ToCommand(CreateDisbursementOrderResource resource) =>
        new(resource.OrderTrackingCode!.Value, resource.AffiliateStoreId!.Value,
            resource.ScheduledFrom!.Value, resource.ScheduledTo!.Value,
            resource.TransferCapAmount!.Value, resource.TransferCapCurrencyCode,
            resource.User, resource.Remarks, resource.Status);
}
