using AmigurumiStore.Catalog.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Catalog.Application.Data;

public static class CatalogSeeder
{
    public static async Task SeedAsync(CatalogDbContext dbContext, CancellationToken cancellationToken = default)
    {
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.Products.AnyAsync(cancellationToken))
        {
            return;
        }

        var starterProducts = new[]
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sunny Bunny",
                Description = "Beginner friendly bunny with floppy ears.",
                Price = 24.99m,
                Stock = 25,
                YarnType = "Cotton sport",
                Difficulty = "Beginner",
                ImageUrl = "/images/sunny-bunny.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Cozy Fox",
                Description = "Classic fox with wrap-around scarf.",
                Price = 32.00m,
                Stock = 15,
                YarnType = "Acrylic worsted",
                Difficulty = "Intermediate",
                ImageUrl = "/images/cozy-fox.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sky Whale",
                Description = "Plush whale with cloud appliques.",
                Price = 29.50m,
                Stock = 18,
                YarnType = "Velvet bulky",
                Difficulty = "Intermediate",
                ImageUrl = "/images/sky-whale.jpg"
            }
        };

        await dbContext.Products.AddRangeAsync(starterProducts, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
