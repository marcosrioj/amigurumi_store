using AmigurumiStore.Ordering.Application.Dtos;
using AmigurumiStore.Ordering.Application.Models;

namespace AmigurumiStore.Ordering.Application.Mappings;

public static class OrderMappings
{
    public static OrderDto ToDto(this Order order) =>
        new(order.Id, order.CustomerId, order.Total, order.CreatedAtUtc, order.Status,
            order.Items.Select(i => new OrderItemDto(i.Id, i.ProductId, i.ProductName, i.UnitPrice, i.Quantity))
                .ToList());
}
