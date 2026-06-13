using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FK_Articles_ProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Articles_ProductId",
                table: "Articles",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ProductId",
                table: "Articles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ProductId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ProductId",
                table: "Articles");
        }
    }
}
