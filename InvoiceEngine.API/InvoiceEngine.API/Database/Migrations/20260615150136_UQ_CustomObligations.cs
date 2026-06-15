using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class UQ_CustomObligations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomIncotermObligations_InvoiceId",
                table: "CustomIncotermObligations");

            migrationBuilder.CreateIndex(
                name: "UX_CustomIncotermObligations_Invoice_SubjectFrom_SubjectTo_ItemType",
                table: "CustomIncotermObligations",
                columns: new[] { "InvoiceId", "FromSubjectCode", "ToSubjectCode", "ItemTypeCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_CustomIncotermObligations_Invoice_SubjectFrom_SubjectTo_ItemType",
                table: "CustomIncotermObligations");

            migrationBuilder.CreateIndex(
                name: "IX_CustomIncotermObligations_InvoiceId",
                table: "CustomIncotermObligations",
                column: "InvoiceId");
        }
    }
}
