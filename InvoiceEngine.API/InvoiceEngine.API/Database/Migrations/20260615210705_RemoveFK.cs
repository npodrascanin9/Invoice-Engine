using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvoiceEngine.API.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ProductId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InsuranceCompanyId",
                table: "InvoiceItemInsuranceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InvoiceItemId",
                table: "InvoiceItemInsuranceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemOrderDetails_InvoiceItemId",
                table: "InvoiceItemOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemOrderDetails_OrderId",
                table: "InvoiceItemOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemTransportDetails_InvoiceItemId",
                table: "InvoiceItemTransportDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemTransportDetails_TransportCompanyId",
                table: "InvoiceItemTransportDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ArticleId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ArticleId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemTransportDetails_TransportCompanyId",
                table: "InvoiceItemTransportDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemOrderDetails_OrderId",
                table: "InvoiceItemOrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItemInsuranceDetails_InsuranceCompanyId",
                table: "InvoiceItemInsuranceDetails");

            migrationBuilder.DropIndex(
                name: "UX_InsuranceCompanies_IdentificationNumber",
                table: "InsuranceCompanies");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ProductId",
                table: "Articles");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InvoiceItems_InvoiceItemId",
                table: "InvoiceItemInsuranceDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemOrderDetails_InvoiceItems_InvoiceItemId",
                table: "InvoiceItemOrderDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemTransportDetails_InvoiceItems_InvoiceItemId",
                table: "InvoiceItemTransportDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InvoiceItems_InvoiceItemId",
                table: "InvoiceItemInsuranceDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemOrderDetails_InvoiceItems_InvoiceItemId",
                table: "InvoiceItemOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItemTransportDetails_InvoiceItems_InvoiceItemId",
                table: "InvoiceItemTransportDetails");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ArticleId",
                table: "OrderItems",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemTransportDetails_TransportCompanyId",
                table: "InvoiceItemTransportDetails",
                column: "TransportCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemOrderDetails_OrderId",
                table: "InvoiceItemOrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemInsuranceDetails_InsuranceCompanyId",
                table: "InvoiceItemInsuranceDetails",
                column: "InsuranceCompanyId");

            migrationBuilder.CreateIndex(
                name: "UX_InsuranceCompanies_IdentificationNumber",
                table: "InsuranceCompanies",
                column: "IdentificationNumber",
                unique: true,
                filter: "IdentificationNumber IS NOT NULL");

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

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InsuranceCompanyId",
                table: "InvoiceItemInsuranceDetails",
                column: "InsuranceCompanyId",
                principalTable: "InsuranceCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemInsuranceDetails_InvoiceItemId",
                table: "InvoiceItemInsuranceDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemOrderDetails_InvoiceItemId",
                table: "InvoiceItemOrderDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemOrderDetails_OrderId",
                table: "InvoiceItemOrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemTransportDetails_InvoiceItemId",
                table: "InvoiceItemTransportDetails",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItemTransportDetails_TransportCompanyId",
                table: "InvoiceItemTransportDetails",
                column: "TransportCompanyId",
                principalTable: "TransportCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ArticleId",
                table: "OrderItems",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
