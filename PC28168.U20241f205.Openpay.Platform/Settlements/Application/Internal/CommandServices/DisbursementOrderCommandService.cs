using Microsoft.EntityFrameworkCore;
using PC28168.U20241f205.Openpay.Platform.Shared.Application.Patterns;
using PC28168.U20241f205.Openpay.Platform.Shared.Domain.Repositories;
using PC28168.U20241f205.Openpay.Platform.Shared.Domain.Model.ValueObjects;
using PC28168.U20241f205.Openpay.Platform.Settlements.Application.Errors;
using PC28168.U20241f205.Openpay.Platform.Settlements.Application.Services;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Aggregates;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Commands;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Enums;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.ValueObjects;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Repositories;
namespace PC28168.U20241f205.Openpay.Platform.Settlements.Application.Internal.CommandServices;
/// <summary>Coordinates validation, duplicate detection and persistence for order creation.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed class DisbursementOrderCommandService(IDisbursementOrderRepository repository, IUnitOfWork unitOfWork)
    : IDisbursementOrderCommandService
{
    /// <inheritdoc />
    public async Task<Result<DisbursementOrder, CreateDisbursementOrderError>> HandleAsync(
        CreateDisbursementOrderCommand command, CancellationToken cancellationToken = default)
    {
        try
        {
            if (await repository.ExistsByTrackingCodeAsync(command.OrderTrackingCode, cancellationToken))
                return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                    new("duplicate_order_tracking_code", "A disbursement order with this OrderTrackingCode already exists."));
            var order = new DisbursementOrder(
                new OpenpayReference(command.OrderTrackingCode),
                new StoreIdentifier(command.AffiliateStoreId),
                new DateInterval(command.ScheduledFrom, command.ScheduledTo),
                new FinancialAmount(command.TransferCapAmount, command.TransferCapCurrencyCode),
                command.User, command.Remarks, command.Status ?? DisbursementStatus.Queued);
            await repository.AddAsync(order, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Success(order);
        }
        catch (ArgumentException exception)
        {
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                new("validation_error", exception.Message));
        }
        catch (DbUpdateException)
        {
            return new Result<DisbursementOrder, CreateDisbursementOrderError>.Failure(
                new("persistence_conflict", "The order could not be stored because a unique constraint was violated."));
        }
    }
}
