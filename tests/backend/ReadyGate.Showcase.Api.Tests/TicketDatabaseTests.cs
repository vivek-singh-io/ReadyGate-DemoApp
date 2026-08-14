using Microsoft.EntityFrameworkCore;

namespace ReadyGate.Showcase.Api.Tests;

public sealed class TicketDatabaseTests
{
    [Fact]
    public async Task Seeded_database_preserves_ticket_customer_relationships()
    {
        await using var database = await TestDatabase.CreateAsync();
        await database.SeedAsync();

        var ticket = await database.Context.Tickets
            .AsNoTracking()
            .Include(item => item.Customer)
            .SingleAsync(item => item.Reference == "TKT-TEST-1");

        Assert.Equal("Casey Example", ticket.Customer.Name);
        Assert.Equal("casey@example.test", ticket.Customer.Email);
    }
}
