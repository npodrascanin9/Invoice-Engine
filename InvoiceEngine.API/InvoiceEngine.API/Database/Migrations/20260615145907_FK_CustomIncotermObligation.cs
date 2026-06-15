using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FK_CustomIncotermObligation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CustomIncotermObligations_InvoiceId",
                table: "CustomIncotermObligations",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceCustomIncotermObligations_InvoiceId",
                table: "CustomIncotermObligations",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceCustomIncotermObligations_InvoiceId",
                table: "CustomIncotermObligations");

            migrationBuilder.DropIndex(
                name: "IX_CustomIncotermObligations_InvoiceId",
                table: "CustomIncotermObligations");
        }
    }
}
