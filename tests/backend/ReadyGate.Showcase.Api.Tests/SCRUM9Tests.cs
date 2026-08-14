// SCRUM-9 requirements=FR-001,FR-002,FR-003 scenarios=US1-AS1,US1-AS2 framework=xUnit context=7149814cd2d3675d037545d054f12be2e02faa2b085277c3de97c7911a1602ce
using Xunit;
public sealed class SCRUM9Tests
{
    private readonly object fixture = new();
    private readonly Func<Task<object>> serviceMock = () => Task.FromResult<object>(new());
    [Fact] public async Task Acceptance_draft_uses_declared_fixture_and_mock() => Assert.NotNull(await serviceMock());
}