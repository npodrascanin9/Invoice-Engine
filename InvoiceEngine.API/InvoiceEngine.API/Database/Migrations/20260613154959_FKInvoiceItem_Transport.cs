using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FKInvoiceItem_Transport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemTransportDetails_InvoiceItemId",
                table: "InvoiceItemTransportDetails",
                column: "InvoiceItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemTransportDetails_InvoiceItemId",
                table: "InvoiceItemTransportDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemTransportDetails_InvoiceItemId",
                table: "InvoiceItemTransportDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemTransportDetails_InvoiceItemId",
                table: "InvoiceItemTransportDetails");
        }
    }
}
