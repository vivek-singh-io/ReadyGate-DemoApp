// SCRUM-9; requirements=FR-001,FR-002,FR-003; context=7149814cd2d3675d037545d054f12be2e02faa2b085277c3de97c7911a1602ce
namespace ReadyGate.Showcase.Application;
public sealed class SCRUM9Service(ILogger<SCRUM9Service> logger, ReadyGateDbContext db)
{
    public async Task<object?> ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Executing SCRUM-9 draft");
        return await db.Runs.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }
}
// Register with AddScoped and expose only through a RequireAuthorization endpoint returning Results.Problem on errors.