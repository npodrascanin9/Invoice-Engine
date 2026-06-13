using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class AmountProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[Quantity] * [Price]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[Quantity] * [Price]",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
