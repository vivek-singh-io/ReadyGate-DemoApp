using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ReadyGate.Showcase.Api.Domain;
using ReadyGate.Showcase.Api.Infrastructure.Persistence;

namespace ReadyGate.Showcase.Api.Tests;

internal sealed class TestDatabase : IAsyncDisposable
{
    private readonly SqliteConnection _connection;

    private TestDatabase(SqliteConnection connection, ShowcaseDbContext context)
    {
        _connection = connection;
        Context = context;
    }

    public ShowcaseDbContext Context { get; }

    public static async Task<TestDatabase> CreateAsync()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<ShowcaseDbContext>()
            .UseSqlite(connection)
            .Options;
        var context = new ShowcaseDbContext(options);
        await context.Database.EnsureCreatedAsync();
        return new TestDatabase(connection, context);
    }

    public async Task SeedAsync()
    {
        var customer = new Customer
        {
            Name = "Casey Example",
            Email = "casey@example.test",
            Phone = "+1-202-555-0199"
        };
        Context.Customers.Add(customer);
        await Context.SaveChangesAsync();
        Context.Tickets.AddRange(
            new SupportTicket
            {
                Reference = "TKT-TEST-1",
                Subject = "Critical open ticket",
                Status = TicketStatus.Open,
                Priority = TicketPriority.Critical,
                ResolutionNotes = "=SUM(A1:A2)",
                UpdatedAtUtc = DateTimeOffset.Parse("2026-08-14T10:00:00Z"),
                CustomerId = customer.Id
            },
            new SupportTicket
            {
                Reference = "TKT-TEST-2",
                Subject = "Resolved low priority ticket",
                Status = TicketStatus.Resolved,
                Priority = TicketPriority.Low,
                ResolutionNotes = "Resolved.",
                UpdatedAtUtc = DateTimeOffset.Parse("2026-08-13T10:00:00Z"),
                CustomerId = customer.Id
            });
        await Context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        await _connection.DisposeAsync();
    }
}
