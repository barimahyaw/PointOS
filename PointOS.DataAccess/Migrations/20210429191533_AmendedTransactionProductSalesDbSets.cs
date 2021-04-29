using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOS.DataAccess.Migrations
{
    public partial class AmendedTransactionProductSalesDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_CreatedUserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CreatedUserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Sales");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Transactions",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductPricingId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId1",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreatedUserId",
                table: "Transactions",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductPricingId",
                table: "Sales",
                column: "ProductPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TransactionId1",
                table: "Sales",
                column: "TransactionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_ProductPricing_ProductPricingId",
                table: "Sales",
                column: "ProductPricingId",
                principalTable: "ProductPricing",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Transactions_TransactionId1",
                table: "Sales",
                column: "TransactionId1",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_CreatedUserId",
                table: "Transactions",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_ProductPricing_ProductPricingId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Transactions_TransactionId1",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_CreatedUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CreatedUserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ProductPricingId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_TransactionId1",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ProductPricingId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "TransactionId1",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "Sales",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Sales",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CreatedUserId",
                table: "Sales",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_CreatedUserId",
                table: "Sales",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
