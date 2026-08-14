// SCRUM-9; requirements=FR-001, FR-002, FR-003; context=7149814cd2d3675d037545d054f12be2e02faa2b085277c3de97c7911a1602ce; GENERATED DRAFT - DO NOT EXECUTE
public partial class SCRUM_9_ReadyGateDraft : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder) =>
        migrationBuilder.CreateTable(name: "ReadyGateGeneratedAudit", columns: table => new { Id = table.Column<string>() });
    protected override void Down(MigrationBuilder migrationBuilder) =>
        migrationBuilder.DropTable(name: "ReadyGateGeneratedAudit");
}