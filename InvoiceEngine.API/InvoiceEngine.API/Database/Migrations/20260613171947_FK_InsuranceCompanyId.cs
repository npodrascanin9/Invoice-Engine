using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FK_InsuranceCompanyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemInsuranceDetails_InsuranceCompanyId",
                table: "InvoiceItemInsuranceDetails",
                column: "InsuranceCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InsuranceCompanyId",
                table: "InvoiceItemInsuranceDetails",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InsuranceCompanyId",
                table: "InvoiceItemInsuranceDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemInsuranceDetails_InsuranceCompanyId",
                table: "InvoiceItemInsuranceDetails");
        }
    }
}
