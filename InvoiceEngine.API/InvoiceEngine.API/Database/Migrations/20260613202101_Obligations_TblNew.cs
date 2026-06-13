using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class Obligations_TblNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemObligations_FromClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemObligations_ToClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemObligations_FromClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemObligations_ToClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Obligation_DifferentClients",
                table: "InvoiceItemObligations");

            migrationBuilder.DropColumn(
                name: "FromClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropColumn(
                name: "ToClientId",
                table: "InvoiceItemObligations");

            migrationBuilder.DropColumn(
                name: "ObligationDescription",
                table: "InvoiceItemObligations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations",
                column: "InvoiceItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations");

            migrationBuilder.AddColumn<int>(
                name: "FromClientId",
                table: "InvoiceItemObligations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToClientId",
                table: "InvoiceItemObligations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ObligationDescription",
                table: "InvoiceItemObligations",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItemObligations",
                table: "InvoiceItemObligations",
                columns: new[] { "InvoiceItemId", "FromClientId", "ToClientId" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemObligations_FromClientId",
                table: "InvoiceItemObligations",
                column: "FromClientId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemObligations_ToClientId",
                table: "InvoiceItemObligations",
                column: "ToClientId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Obligation_DifferentClients",
                table: "InvoiceItemObligations",
                sql: "[FromClientId] <> [ToClientId]");

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
    }
}
