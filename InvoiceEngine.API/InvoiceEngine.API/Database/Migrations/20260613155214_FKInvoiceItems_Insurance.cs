using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FKInvoiceItems_Insurance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemInsuranceDetails_InvoiceItemId",
                table: "InvoiceItemInsuranceDetails",
                column: "InvoiceItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InvoiceItemId",
                table: "InvoiceItemInsuranceDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InvoiceItemId",
                table: "InvoiceItemInsuranceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemInsuranceDetails_InvoiceItemId",
                table: "InvoiceItemInsuranceDetails");
        }
    }
}
