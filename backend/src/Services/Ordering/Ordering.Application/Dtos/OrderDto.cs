namespace AmigurumiStore.Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    decimal Total,
    DateTime CreatedAtUtc,
    string Status,
    IReadOnlyCollection<OrderItemDto> Items);
