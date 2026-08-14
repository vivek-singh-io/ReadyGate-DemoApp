using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace ReadyGate.Showcase.Api.Tests;

public sealed class TicketEndpointTests : IClassFixture<ShowcaseApiFactory>, IDisposable
{
    private readonly HttpClient _client;

    public TicketEndpointTests(ShowcaseApiFactory factory) => _client = factory.CreateClient();

    public void Dispose() => _client.Dispose();

    [Fact]
    public async Task GetTickets_WithoutViewPermission_IsForbidden()
    {
        var response = await _client.GetAsync("/api/tickets");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetTickets_WithViewPermission_AppliesFilters()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/tickets?status=Flagged&priority=High");
        request.Headers.Add("X-Demo-Permissions", "view_tickets");

        var response = await _client.SendAsync(request);
        var payload = await response.Content.ReadFromJsonAsync<TicketListPayload>();

        response.EnsureSuccessStatusCode();
        Assert.NotNull(payload);
        Assert.Equal(1, payload.Total);
        Assert.Equal("TKT-1001", Assert.Single(payload.Items).Reference);
    }

    [Fact]
    public async Task Export_WithoutPermission_SucceedsAsIntentionalFixture()
    {
        var response = await _client.PostAsJsonAsync("/api/tickets/export", new { ticketIds = new[] { 1 } });
        var csv = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        Assert.Contains("avery.patel@example.test", csv);
    }

    private sealed record TicketListPayload(IReadOnlyList<TicketPayload> Items, int Total);
    private sealed record TicketPayload(int Id, string Reference);
}

public sealed class ShowcaseApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly string _databasePath = Path.Combine(Path.GetTempPath(), $"readygate-showcase-tests-{Guid.NewGuid():N}.db");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureAppConfiguration((_, configuration) =>
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:Showcase"] = $"Data Source={_databasePath}"
            }));
    }

    public Task InitializeAsync() => Task.CompletedTask;

    Task IAsyncLifetime.DisposeAsync()
    {
        Dispose();
        Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
        if (File.Exists(_databasePath))
        {
            File.Delete(_databasePath);
        }

        return Task.CompletedTask;
    }
}
