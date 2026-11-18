using PC28168.U20241f205.Openpay.Platform.Shared.Application.Patterns;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Aggregates;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Commands;
using PC28168.U20241f205.Openpay.Platform.Settlements.Application.Errors;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Application.Services;
/// <summary>Defines the create-order use case.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public interface IDisbursementOrderCommandService
{
    /// <summary>Creates and persists a disbursement order.</summary>
    Task<Result<DisbursementOrder, CreateDisbursementOrderError>> HandleAsync(CreateDisbursementOrderCommand command, CancellationToken cancellationToken = default);
}
