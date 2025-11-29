using AmigurumiStore.Catalog.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmigurumiStore.Catalog.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ServiceCollectionExtensions>());
        return services;
    }
}
