using AmigurumiStore.Catalog.Application.Data;
using AmigurumiStore.Catalog.Application.Dtos;
using AmigurumiStore.Catalog.Application.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Catalog.Application.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;

public class GetProductByIdQueryHandler(CatalogDbContext dbContext) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        return product?.ToDto();
    }
}
