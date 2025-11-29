using AmigurumiStore.Identity.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Identity.Application.Data;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(120);
            entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(200);
        });
    }
}
