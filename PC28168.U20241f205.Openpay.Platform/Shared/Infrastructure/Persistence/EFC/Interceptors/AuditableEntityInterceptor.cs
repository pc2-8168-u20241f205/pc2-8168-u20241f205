using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PC28168.U20241f205.Openpay.Platform.Shared.Domain.Model;
namespace PC28168.U20241f205.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Interceptors;
/// <summary>Automatically maintains audit timestamps during EF Core saves.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private static void ApplyAudit(DbContext? context)
    {
        if (context is null) return;
        var now = DateTimeOffset.UtcNow;
        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
        {
            if (entry.State == EntityState.Added) { entry.Entity.CreatedAt = now; entry.Entity.UpdatedAt = now; }
            else if (entry.State == EntityState.Modified) { entry.Entity.UpdatedAt = now; entry.Property(x => x.CreatedAt).IsModified = false; }
        }
    }
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    { ApplyAudit(eventData.Context); return base.SavingChanges(eventData, result); }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    { ApplyAudit(eventData.Context); return base.SavingChangesAsync(eventData, result, cancellationToken); }
}
