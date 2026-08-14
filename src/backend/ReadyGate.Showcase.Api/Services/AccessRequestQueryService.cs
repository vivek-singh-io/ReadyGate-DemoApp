using Microsoft.EntityFrameworkCore;
using ReadyGate.Showcase.Api.Domain;
using ReadyGate.Showcase.Api.Infrastructure.Persistence;

namespace ReadyGate.Showcase.Api.Services;

public sealed record AccessRequestListItem(
    int Id,
    string RequesterName,
    string RequestedRole,
    string BusinessReason,
    AccessRequestStatus Status,
    DateTimeOffset RequestedAtUtc);

public sealed class AccessRequestQueryService(ShowcaseDbContext dbContext)
{
    public Task<List<AccessRequestListItem>> ListPendingAsync(CancellationToken cancellationToken) =>
        dbContext.AccessRequests
            .AsNoTracking()
            .Where(request => request.Status == AccessRequestStatus.Pending)
            .OrderBy(request => request.RequestedAtUtc)
            .Select(request => new AccessRequestListItem(
                request.Id,
                request.RequesterName,
                request.RequestedRole,
                request.BusinessReason,
                request.Status,
                request.RequestedAtUtc))
            .ToListAsync(cancellationToken);
}
