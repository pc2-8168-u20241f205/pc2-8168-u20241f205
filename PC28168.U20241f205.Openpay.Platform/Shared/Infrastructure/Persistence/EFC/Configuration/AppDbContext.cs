using Microsoft.EntityFrameworkCore;
using PC28168.U20241f205.Openpay.Platform.Settlements.Domain.Model.Aggregates;
using PC28168.U20241f205.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Interceptors;
namespace PC28168.U20241f205.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
/// <summary>EF Core database context for the Openpay settlements schema.</summary>
/// <remarks>Author: Candy Milagros Vizcarra Mamani.</remarks>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>Gets disbursement orders.</summary>
    public DbSet<DisbursementOrder> DisbursementOrders => Set<DisbursementOrder>();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("openpay_settlements");
        modelBuilder.Entity<DisbursementOrder>(entity =>
        {
            entity.ToTable("disbursement_orders");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.Property(x => x.Status).HasConversion<int>().IsRequired();
            entity.Property(x => x.User).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Remarks).HasMaxLength(85);
            entity.Property(x => x.CreatedAt);
            entity.Property(x => x.UpdatedAt);
            entity.OwnsOne(x => x.OrderTrackingCode, owned =>
            {
                owned.Property(v => v.Value).HasColumnName("order_tracking_code").IsRequired();
                owned.HasIndex(v => v.Value).IsUnique();
            });
            entity.OwnsOne(x => x.AffiliateStoreId, owned =>
            {
                owned.Property(v => v.Value).HasColumnName("affiliate_store_id").IsRequired();
            });
            entity.OwnsOne(x => x.ExecutionSchedule, owned =>
            {
                owned.Property(v => v.ScheduledFrom).HasColumnName("scheduled_from").HasColumnType("date").IsRequired();
                owned.Property(v => v.ScheduledTo).HasColumnName("scheduled_to").HasColumnType("date").IsRequired();
            });
            entity.OwnsOne(x => x.TransferCap, owned =>
            {
                owned.Property(v => v.Amount).HasColumnName("transfer_cap_amount").HasPrecision(18, 2).IsRequired();
                owned.Property(v => v.CurrencyCode).HasColumnName("transfer_cap_currency_code").HasMaxLength(3).IsRequired();
            });

        });
    }
}
