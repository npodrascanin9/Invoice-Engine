using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class Obligations_CompKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemObligations_InvoiceItemId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InvoiceItemObligations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations",
                columns: new[] { "InvoiceItemId", "FromClientId", "ToClientId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InvoiceItemObligations",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemObligations_InvoiceItemId",
                table: "InvoiceItemObligations",
                column: "InvoiceItemId");
        }
    }
}
