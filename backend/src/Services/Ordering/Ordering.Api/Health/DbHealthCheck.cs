using AmigurumiStore.Ordering.Application.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AmigurumiStore.Ordering.Api.Health;

public class DbHealthCheck(OrderingDbContext dbContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);
        return canConnect ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy("Cannot connect to Ordering database.");
    }
}
