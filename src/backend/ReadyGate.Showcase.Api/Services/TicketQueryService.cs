using Microsoft.EntityFrameworkCore;
using ReadyGate.Showcase.Api.Domain;
using ReadyGate.Showcase.Api.Endpoints;
using ReadyGate.Showcase.Api.Infrastructure.Persistence;

namespace ReadyGate.Showcase.Api.Services;

/// <summary>
/// Representative application-service pattern: validation and projection live outside
/// endpoint handlers, while EF queries stay no-tracking and cancellation-aware.
/// </summary>
public sealed class TicketQueryService(ShowcaseDbContext dbContext)
{
    public async Task<TicketListResponse> SearchAsync(
        TicketStatus? status,
        TicketPriority? priority,
        CancellationToken cancellationToken)
    {
        IQueryable<SupportTicket> query = dbContext.Tickets.AsNoTracking();

        if (status.HasValue)
        {
            query = query.Where(ticket => ticket.Status == status.Value);
        }

        if (priority.HasValue)
        {
            query = query.Where(ticket => ticket.Priority == priority.Value);
        }

        var total = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderByDescending(ticket => ticket.UpdatedAtUtc)
            .ThenBy(ticket => ticket.Reference)
            .Select(ticket => new TicketListItem(
                ticket.Id,
                ticket.Reference,
                ticket.Subject,
                ticket.Status,
                ticket.Priority,
                ticket.Customer.Name,
                ticket.UpdatedAtUtc))
            .ToListAsync(cancellationToken);

        return new TicketListResponse(items, total);
    }
}
