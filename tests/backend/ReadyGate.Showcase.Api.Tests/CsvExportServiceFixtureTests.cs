using System.Text;
using ReadyGate.Showcase.Api.Services;

namespace ReadyGate.Showcase.Api.Tests;

public sealed class CsvExportServiceFixtureTests
{
    [Fact]
    public async Task ExportAsync_DemonstratesRawPiiFixtureForReadyGateAnalysis()
    {
        await using var database = await TestDatabase.CreateAsync();
        await database.SeedAsync();
        var ticketId = database.Context.Tickets.Single(ticket => ticket.Reference == "TKT-TEST-1").Id;
        var service = new CsvExportService(database.Context);

        var bytes = await service.ExportAsync([ticketId], CancellationToken.None);
        var csv = Encoding.UTF8.GetString(bytes);

        // These assertions document intentional security gaps; they are not desired production behavior.
        Assert.Contains("casey@example.test", csv);
        Assert.Contains("+1-202-555-0199", csv);
        Assert.Contains("'=SUM(A1:A2)", csv);
    }
}
