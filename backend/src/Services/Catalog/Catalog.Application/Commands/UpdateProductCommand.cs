using AmigurumiStore.Catalog.Application.Data;
using AmigurumiStore.Catalog.Application.Dtos;
using AmigurumiStore.Catalog.Application.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Catalog.Application.Commands;

public record UpdateProductCommand(Guid Id, UpsertProductDto Product) : IRequest;

public class UpdateProductCommandHandler(CatalogDbContext dbContext) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (entity == null) throw new KeyNotFoundException($"Product {request.Id} not found");

        entity.Apply(request.Product);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
