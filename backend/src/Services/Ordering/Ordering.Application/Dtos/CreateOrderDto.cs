namespace AmigurumiStore.Ordering.Application.Dtos;

public record CreateOrderDto(Guid CustomerId, IReadOnlyCollection<CreateOrderItemDto> Items);

public record CreateOrderItemDto(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity);
