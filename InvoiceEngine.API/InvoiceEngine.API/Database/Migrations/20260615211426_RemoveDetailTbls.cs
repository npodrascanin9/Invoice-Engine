using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDetailTbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceItemOrderDetails");

            migrationBuilder.DropTable(
                name: "InvoiceItemTransportDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoiceItemOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceItemId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItemOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItemOrderDetails_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItemTransportDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressFrom = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AddressTo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    InvoiceItemId = table.Column<int>(type: "int", nullable: false),
                    TransportCompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItemTransportDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItemTransportDetails_InvoiceItems_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalTable: "InvoiceItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemOrderDetails_InvoiceItemId",
                table: "InvoiceItemOrderDetails",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemTransportDetails_InvoiceItemId",
                table: "InvoiceItemTransportDetails",
                column: "InvoiceItemId");
        }
    }
}
