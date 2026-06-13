using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FK_InvoiceClients_m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceClients_ClientId",
                table: "InvoiceClients",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceClients_ClientId",
                table: "InvoiceClients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceClients_InvoiceId",
                table: "InvoiceClients",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceClients_ClientId",
                table: "InvoiceClients");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceClients_InvoiceId",
                table: "InvoiceClients");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceClients_ClientId",
                table: "InvoiceClients");
        }
    }
}
