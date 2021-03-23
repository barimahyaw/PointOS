using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOS.DataAccess.Migrations
{
    public partial class ProductCategoryDbSetAmended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "ProductCategories",
                newName: "Name");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ProductCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "ProductCategories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CreatedUserId",
                table: "ProductCategories",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatedUserId",
                table: "ProductCategories",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductCategories_AspNetUsers_CreatedUserId",
                table: "ProductCategories");

            migrationBuilder.DropIndex(
                name: "IX_ProductCategories_CreatedUserId",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "ProductCategories");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductCategories",
                newName: "ProductName");
        }
    }
}
