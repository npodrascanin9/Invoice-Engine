using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FK_ClientObligations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemObligations_FromClientId",
                table: "InvoiceItemObligations",
                column: "FromClientId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemObligations_ToClientId",
                table: "InvoiceItemObligations",
                column: "ToClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemObligations_FromClientId",
                table: "InvoiceItemObligations",
                column: "FromClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemObligations_ToClientId",
                table: "InvoiceItemObligations",
                column: "ToClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemObligations_FromClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemObligations_ToClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemObligations_FromClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemObligations_ToClientId",
                table: "InvoiceItemObligations");
        }
    }
}
