namespace AmigurumiStore.Catalog.Application.Dtos;

public record UpsertProductDto(
    string Name,
    string Description,
    decimal Price,
    int Stock,
    string YarnType,
    string Difficulty,
    string ImageUrl);
