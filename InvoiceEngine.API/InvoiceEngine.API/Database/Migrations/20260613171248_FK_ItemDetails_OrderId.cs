using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FK_ItemDetails_OrderId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemOrderDetails_OrderId",
                table: "InvoiceItemOrderDetails",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemOrderDetails_OrderId",
                table: "InvoiceItemOrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemOrderDetails_OrderId",
                table: "InvoiceItemOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemOrderDetails_OrderId",
                table: "InvoiceItemOrderDetails");
        }
    }
}
