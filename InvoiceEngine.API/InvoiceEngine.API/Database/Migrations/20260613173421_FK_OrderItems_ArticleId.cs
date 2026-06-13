using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class FK_OrderItems_ArticleId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ArticleId",
                table: "OrderItems",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ArticleId",
                table: "OrderItems",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ArticleId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ArticleId",
                table: "OrderItems");
        }
    }
}
