using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class InvoicesTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncotermCode = table.Column<string>(type: "VARCHAR(3)", nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    IssuedAt = table.Column<DateOnly>(type: "DATE", nullable: false),
                    ExpiresAt = table.Column<DateOnly>(type: "DATE", nullable: false),
                    TransactionStartDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    TransactionEndDate = table.Column<DateOnly>(type: "DATE", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.CheckConstraint("CK_Invoice_IssueExpireRange", "[ExpiresAt] > [IssuedAt]");
                    table.CheckConstraint("CK_Invoice_TransactionDateRange", "[TransactionEndDate] > [TransactionStartDate]");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
