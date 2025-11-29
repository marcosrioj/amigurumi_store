namespace AmigurumiStore.Ordering.Application.Dtos;

public record OrderItemDto(Guid Id, Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);
