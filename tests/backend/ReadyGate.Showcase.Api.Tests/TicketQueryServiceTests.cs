using ReadyGate.Showcase.Api.Domain;
using ReadyGate.Showcase.Api.Services;

namespace ReadyGate.Showcase.Api.Tests;

public sealed class TicketQueryServiceTests
{
    [Fact]
    public async Task SearchAsync_FiltersByStatusAndPriority()
    {
        await using var database = await TestDatabase.CreateAsync();
        await database.SeedAsync();
        var service = new TicketQueryService(database.Context);

        var result = await service.SearchAsync(TicketStatus.Open, TicketPriority.Critical, CancellationToken.None);

        var ticket = Assert.Single(result.Items);
        Assert.Equal("TKT-TEST-1", ticket.Reference);
        Assert.Equal(1, result.Total);
    }

    [Fact]
    public async Task SearchAsync_ReturnsStableNewestFirstProjection()
    {
        await using var database = await TestDatabase.CreateAsync();
        await database.SeedAsync();
        var service = new TicketQueryService(database.Context);

        var result = await service.SearchAsync(null, null, CancellationToken.None);

        Assert.Equal(2, result.Total);
        Assert.Equal(["TKT-TEST-1", "TKT-TEST-2"], result.Items.Select(item => item.Reference));
        Assert.All(result.Items, item => Assert.Equal("Casey Example", item.CustomerName));
    }
}
