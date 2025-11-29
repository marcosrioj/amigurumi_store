using AmigurumiStore.Catalog.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Catalog.Application.Data;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Description).HasMaxLength(1024);
            entity.Property(p => p.Price).HasPrecision(10, 2);
            entity.Property(p => p.YarnType).HasMaxLength(80);
            entity.Property(p => p.Difficulty).HasMaxLength(40);
            entity.Property(p => p.ImageUrl).HasMaxLength(2048);
            entity.Property(p => p.CreatedAtUtc).HasDefaultValueSql("GETUTCDATE()");
        });
    }
}
