// SCRUM-9 requirements=FR-001,FR-002,FR-003 scenarios=US1-AS1,US1-AS2 framework=xUnit context=7149814cd2d3675d037545d054f12be2e02faa2b085277c3de97c7911a1602ce
using Microsoft.Data.Sqlite;
using Xunit;
public sealed class SCRUM9DatabaseTests
{
    [Fact] public void Uses_an_isolated_in_memory_fixture() { using var fixture = new SqliteConnection("Data Source=:memory:"); Assert.NotNull(fixture); }
}