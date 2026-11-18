namespace PC28168.U20241f205.Openpay.Platform.Shared.Domain.Repositories;
/// <summary>Base contract for aggregate persistence.</summary>
/// <typeparam name="T">Aggregate type.</typeparam>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public interface IBaseRepository<T> where T : class
{
    /// <summary>Adds an aggregate to the persistence context.</summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
}
