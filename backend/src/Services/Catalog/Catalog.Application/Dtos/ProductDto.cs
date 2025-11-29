namespace AmigurumiStore.Catalog.Application.Dtos;

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int Stock,
    string YarnType,
    string Difficulty,
    string ImageUrl,
    DateTime CreatedAtUtc);
