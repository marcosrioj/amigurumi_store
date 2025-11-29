using AmigurumiStore.Catalog.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmigurumiStore.Catalog.Application.Commands;

public record DeleteProductCommand(Guid Id) : IRequest;

public class DeleteProductCommandHandler(CatalogDbContext dbContext) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (entity == null) return;

        dbContext.Products.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
