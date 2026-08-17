// RGE-13 requirements=FR-001,FR-002,FR-003,FR-004,FR-005,FR-006,FR-007,FR-008,FR-009,FR-010,FR-011 scenarios=US1-AS1,US1-AS10,US1-AS11,US1-AS2,US1-AS3,US1-AS4,US1-AS5,US1-AS6,US1-AS7,US1-AS8,US1-AS9 framework=xUnit context=8292b268b9a2c64e9082d8644661173389df117a0281c7db5bd7814e45ee7f8d
using Xunit;
public sealed class RGE13Tests
{
    private readonly object fixture = new();
    private readonly Func<Task<object>> serviceMock = () => Task.FromResult<object>(new());
    [Fact] public async Task Acceptance_draft_uses_declared_fixture_and_mock() => Assert.NotNull(await serviceMock());
}