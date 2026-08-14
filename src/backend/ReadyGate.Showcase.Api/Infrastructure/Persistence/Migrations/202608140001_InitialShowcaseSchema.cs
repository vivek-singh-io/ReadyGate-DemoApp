using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReadyGate.Showcase.Api.Infrastructure.Persistence.Migrations;

/// <summary>
/// Representative database-change pattern: schema changes are explicit, reversible,
/// and versioned alongside the code that consumes them.
/// </summary>
public partial class InitialShowcaseSchema : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AccessRequests",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                RequesterName = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                RequestedRole = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                BusinessReason = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                Status = table.Column<string>(type: "TEXT", maxLength: 24, nullable: false),
                RequestedAtUtc = table.Column<long>(type: "INTEGER", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_AccessRequests", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Customers",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 254, nullable: false),
                Phone = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Customers", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Tickets",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Reference = table.Column<string>(type: "TEXT", maxLength: 24, nullable: false),
                Subject = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                Status = table.Column<string>(type: "TEXT", maxLength: 24, nullable: false),
                Priority = table.Column<string>(type: "TEXT", maxLength: 24, nullable: false),
                ResolutionNotes = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false),
                UpdatedAtUtc = table.Column<long>(type: "INTEGER", nullable: false),
                CustomerId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tickets", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tickets_Customers_CustomerId",
                    column: x => x.CustomerId,
                    principalTable: "Customers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Tickets_CustomerId",
            table: "Tickets",
            column: "CustomerId");

        migrationBuilder.CreateIndex(
            name: "IX_Tickets_Reference",
            table: "Tickets",
            column: "Reference",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "AccessRequests");
        migrationBuilder.DropTable(name: "Tickets");
        migrationBuilder.DropTable(name: "Customers");
    }
}
