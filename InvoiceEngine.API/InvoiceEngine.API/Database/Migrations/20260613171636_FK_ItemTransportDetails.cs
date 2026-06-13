using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FK_ItemTransportDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemTransportDetails_TransportCompanyId",
                table: "InvoiceItemTransportDetails",
                column: "TransportCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemTransportDetails_TransportCompanyId",
                table: "InvoiceItemTransportDetails",
                column: "TransportCompanyId",
                principalTable: "TransportCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemTransportDetails_TransportCompanyId",
                table: "InvoiceItemTransportDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemTransportDetails_TransportCompanyId",
                table: "InvoiceItemTransportDetails");
        }
    }
}
