using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class Obligations_Tbl_New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations");

            migrationBuilder.AddColumn<int>(
                name: "FromClientSubjectCode",
                table: "InvoiceItemObligations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToClientSubjectCode",
                table: "InvoiceItemObligations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations",
                columns: new[] { "InvoiceItemId", "FromClientSubjectCode", "ToClientSubjectCode" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Obligation_DifferentClients",
                table: "InvoiceItemObligations",
                sql: "[FromClientSubjectCode] <> [ToClientSubjectCode]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Obligation_DifferentClients",
                table: "InvoiceItemObligations");

            migrationBuilder.DropColumn(
                name: "FromClientSubjectCode",
                table: "InvoiceItemObligations");

            migrationBuilder.DropColumn(
                name: "ToClientSubjectCode",
                table: "InvoiceItemObligations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations",
                column: "InvoiceItemId");
        }
    }
}
