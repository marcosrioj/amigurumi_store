using AmigurumiStore.Identity.Application.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AmigurumiStore.Identity.Api.Health;

public class DbHealthCheck(IdentityDbContext dbContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var canConnect = await dbContext.Database.CanConnectAsync(cancellationToken);
        return canConnect ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy("Cannot connect to Identity database.");
    }
}
