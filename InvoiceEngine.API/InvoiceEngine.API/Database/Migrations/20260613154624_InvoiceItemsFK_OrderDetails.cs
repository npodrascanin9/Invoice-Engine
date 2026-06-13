using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceItemsFK_OrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemOrderDetails_InvoiceItemId",
                table: "InvoiceItemOrderDetails",
                column: "InvoiceItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemOrderDetails_InvoiceItemId",
                table: "InvoiceItemOrderDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemOrderDetails_InvoiceItemId",
                table: "InvoiceItemOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemOrderDetails_InvoiceItemId",
                table: "InvoiceItemOrderDetails");
        }
    }
}
