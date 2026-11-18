using PC28168.U20241f205.Openpay.Platform.Shared.Domain.Repositories;
using PC28168.U20241f205.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
namespace PC28168.U20241f205.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
/// <summary>EF Core unit of work implementation.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public Task<int> CompleteAsync(CancellationToken cancellationToken = default) => context.SaveChangesAsync(cancellationToken);
}
