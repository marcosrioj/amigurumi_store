using AmigurumiStore.Catalog.Application.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AmigurumiStore.Catalog.Api.Health;

public class DbHealthCheck(CatalogDbContext dbContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);
        return canConnect ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy("Cannot connect to Catalog database.");
    }
}
