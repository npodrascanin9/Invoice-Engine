using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceItemTransportDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceItemTransportDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceItemId = table.Column<int>(type: "int", nullable: false),
                    TransportCompanyId = table.Column<int>(type: "int", nullable: true),
                    AddressFrom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AddressTo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItemTransportDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItemTransportDetails");
        }
    }
}
