using AmigurumiStore.Catalog.Application.Data;
using AmigurumiStore.Catalog.Application.Dtos;
using AmigurumiStore.Catalog.Application.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Catalog.Application.Queries;

public record GetProductsQuery : IRequest<IReadOnlyCollection<ProductDto>>;

public class GetProductsQueryHandler(CatalogDbContext dbContext)
    : IRequestHandler<GetProductsQuery, IReadOnlyCollection<ProductDto>>
{
    public async Task<IReadOnlyCollection<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await dbContext.Products.AsNoTracking()
            .OrderByDescending(p => p.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        return products.Select(p => p.ToDto()).ToList();
    }
}
