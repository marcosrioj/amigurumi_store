using AmigurumiStore.Catalog.Application.Dtos;
using AmigurumiStore.Catalog.Application.Models;

namespace AmigurumiStore.Catalog.Application.Mappings;

public static class ProductMappings
{
    public static ProductDto ToDto(this Product product) =>
        new(product.Id, product.Name, product.Description, product.Price, product.Stock, product.YarnType,
            product.Difficulty, product.ImageUrl, product.CreatedAtUtc);

    public static void Apply(this Product product, UpsertProductDto dto)
    {
        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.YarnType = dto.YarnType;
        product.Difficulty = dto.Difficulty;
        product.ImageUrl = dto.ImageUrl;
    }
}
