using AmigurumiStore.Catalog.Application.Dtos;
using AmigurumiStore.Catalog.Application.Data;
using AmigurumiStore.Catalog.Application.Mappings;
using AmigurumiStore.Catalog.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Catalog.Application.Commands;

public record CreateProductCommand(UpsertProductDto Product) : IRequest<ProductDto>;

public class CreateProductCommandHandler(CatalogDbContext dbContext) : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product { Id = Guid.NewGuid() };
        product.Apply(request.Product);

        await dbContext.Products.AddAsync(product, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return product.ToDto();
    }
}
