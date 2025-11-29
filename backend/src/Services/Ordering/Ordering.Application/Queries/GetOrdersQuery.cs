using AmigurumiStore.Ordering.Application.Data;
using AmigurumiStore.Ordering.Application.Dtos;
using AmigurumiStore.Ordering.Application.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Ordering.Application.Queries;

public record GetOrdersQuery : IRequest<IReadOnlyCollection<OrderDto>>;

public class GetOrdersQueryHandler(OrderingDbContext dbContext)
    : IRequestHandler<GetOrdersQuery, IReadOnlyCollection<OrderDto>>
{
    public async Task<IReadOnlyCollection<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.AsNoTracking().Include(o => o.Items)
            .OrderByDescending(o => o.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return orders.Select(o => o.ToDto()).ToList();
    }
}
