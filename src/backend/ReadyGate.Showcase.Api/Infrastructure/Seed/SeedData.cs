using Microsoft.EntityFrameworkCore;
using ReadyGate.Showcase.Api.Domain;

using ReadyGate.Showcase.Api.Infrastructure.Persistence;

namespace ReadyGate.Showcase.Api.Infrastructure.Seed;

public static class SeedData
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ShowcaseDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);

        if (await dbContext.Customers.AnyAsync(cancellationToken))
        {
            return;
        }

        var customers = new[]
        {
            new Customer { Name = "Avery Patel", Email = "avery.patel@example.test", Phone = "+1-202-555-0101" },
            new Customer { Name = "Morgan Lee", Email = "morgan.lee@example.test", Phone = "+1-202-555-0102" },
            new Customer { Name = "Jordan Rivera", Email = "jordan.rivera@example.test", Phone = "+1-202-555-0103" }
        };
        dbContext.Customers.AddRange(customers);
        await dbContext.SaveChangesAsync(cancellationToken);

        dbContext.Tickets.AddRange(
            new SupportTicket { Reference = "TKT-1001", Subject = "Unable to access analytics", Status = TicketStatus.Flagged, Priority = TicketPriority.High, ResolutionNotes = "Identity verification pending.", UpdatedAtUtc = DateTimeOffset.Parse("2026-08-10T08:00:00Z"), CustomerId = customers[0].Id },
            new SupportTicket { Reference = "TKT-1002", Subject = "Invoice displays duplicate line", Status = TicketStatus.InProgress, Priority = TicketPriority.Medium, ResolutionNotes = "Billing team is reviewing the calculation.", UpdatedAtUtc = DateTimeOffset.Parse("2026-08-11T09:30:00Z"), CustomerId = customers[1].Id },
            new SupportTicket { Reference = "TKT-1003", Subject = "Production webhook delayed", Status = TicketStatus.Open, Priority = TicketPriority.Critical, ResolutionNotes = "Awaiting trace correlation from platform team.", UpdatedAtUtc = DateTimeOffset.Parse("2026-08-12T12:15:00Z"), CustomerId = customers[2].Id },
            new SupportTicket { Reference = "TKT-1004", Subject = "Update notification preferences", Status = TicketStatus.Resolved, Priority = TicketPriority.Low, ResolutionNotes = "Preferences updated and confirmed.", UpdatedAtUtc = DateTimeOffset.Parse("2026-08-13T15:45:00Z"), CustomerId = customers[0].Id });

        dbContext.AccessRequests.AddRange(
            new AccessRequest { RequesterName = "Sam Chen", RequestedRole = "Regional Manager", BusinessReason = "Temporary coverage for the west region.", Status = AccessRequestStatus.Pending, RequestedAtUtc = DateTimeOffset.Parse("2026-08-12T06:00:00Z") },
            new AccessRequest { RequesterName = "Taylor Brooks", RequestedRole = "Support Analyst", BusinessReason = "New starter onboarding.", Status = AccessRequestStatus.Pending, RequestedAtUtc = DateTimeOffset.Parse("2026-08-13T07:30:00Z") });

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
