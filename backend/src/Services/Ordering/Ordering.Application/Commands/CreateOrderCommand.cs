using AmigurumiStore.Ordering.Application.Data;
using AmigurumiStore.Ordering.Application.Dtos;
using AmigurumiStore.Ordering.Application.Mappings;
using AmigurumiStore.Ordering.Application.Models;
using MediatR;

namespace AmigurumiStore.Ordering.Application.Commands;

public record CreateOrderCommand(CreateOrderDto Order) : IRequest<OrderDto>;

public class CreateOrderCommandHandler(OrderingDbContext dbContext) : IRequestHandler<CreateOrderCommand, OrderDto>
{
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.Order.CustomerId,
            Status = "Pending",
            Items = request.Order.Items.Select(item => new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            }).ToList()
        };

        order.Total = order.Items.Sum(i => i.UnitPrice * i.Quantity);

        await dbContext.Orders.AddAsync(order, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return order.ToDto();
    }
}
