using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class ObligationsRemoveColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Obligation_DifferentSubjects",
                table: "InvoiceItemObligations");

            migrationBuilder.DropColumn(
                name: "FromSubjectType",
                table: "InvoiceItemObligations");

            migrationBuilder.DropColumn(
                name: "ToSubjectType",
                table: "InvoiceItemObligations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FromSubjectType",
                table: "InvoiceItemObligations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToSubjectType",
                table: "InvoiceItemObligations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Obligation_DifferentSubjects",
                table: "InvoiceItemObligations",
                sql: "[FromSubjectType] <> [ToSubjectType]");
        }
    }
}
