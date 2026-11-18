namespace PC28168.U20241f205.Openpay.Platform.Shared.Domain.Repositories;
/// <summary>Commits all pending persistence changes atomically.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public interface IUnitOfWork
{
    /// <summary>Persists pending changes.</summary>
    Task<int> CompleteAsync(CancellationToken cancellationToken = default);
}
