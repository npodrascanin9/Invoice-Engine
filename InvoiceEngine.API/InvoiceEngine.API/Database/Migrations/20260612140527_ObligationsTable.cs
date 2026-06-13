using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class ObligationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceItemObligations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceItemId = table.Column<int>(type: "int", nullable: false),
                    FromClientId = table.Column<int>(type: "int", nullable: false),
                    FromSubjectType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ToClientId = table.Column<int>(type: "int", nullable: false),
                    ToSubjectType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ObligationDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OwingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItemObligations", x => x.Id);
                    table.CheckConstraint("CK_Obligation_DifferentClients", "[FromClientId] <> [ToClientId]");
                    table.CheckConstraint("CK_Obligation_DifferentSubjects", "[FromSubjectType] <> [ToSubjectType]");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItemObligations");
        }
    }
}
