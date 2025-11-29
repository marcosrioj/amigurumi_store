using AmigurumiStore.Ordering.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AmigurumiStore.Ordering.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOrderingApplication(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<OrderingDbContext>(options => options.UseSqlServer(connectionString));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ServiceCollectionExtensions>());
        return services;
    }
}
