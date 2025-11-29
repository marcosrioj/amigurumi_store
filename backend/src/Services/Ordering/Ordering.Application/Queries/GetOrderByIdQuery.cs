using AmigurumiStore.Ordering.Application.Data;
using AmigurumiStore.Ordering.Application.Dtos;
using AmigurumiStore.Ordering.Application.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Ordering.Application.Queries;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderDto?>;

public class GetOrderByIdQueryHandler(OrderingDbContext dbContext) : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.AsNoTracking().Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        return order?.ToDto();
    }
}
