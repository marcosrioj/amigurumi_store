using AmigurumiStore.Ordering.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Ordering.Application.Commands;

public record UpdateOrderStatusCommand(Guid OrderId, string Status) : IRequest;

public class UpdateOrderStatusCommandHandler(OrderingDbContext dbContext) : IRequestHandler<UpdateOrderStatusCommand>
{
    public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);
        if (order == null) throw new KeyNotFoundException($"Order {request.OrderId} not found");

        order.Status = request.Status;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
