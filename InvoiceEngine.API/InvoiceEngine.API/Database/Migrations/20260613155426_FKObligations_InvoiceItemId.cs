using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FKObligations_InvoiceItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemObligations_InvoiceItemId",
                table: "InvoiceItemObligations",
                column: "InvoiceItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemObligations_InvoiceItemId",
                table: "InvoiceItemObligations",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemObligations_InvoiceItemId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemObligations_InvoiceItemId",
                table: "InvoiceItemObligations");
        }
    }
}
