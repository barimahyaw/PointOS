using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOS.DataAccess.Migrations
{
    public partial class SalesDbContextUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId1",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CreatedUserId1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreatedUserId1",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LogoPath",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Sales",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedUserId",
                table: "Companies",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId",
                table: "Companies",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CreatedUserId",
                table: "Companies");

            migrationBuilder.AddColumn<Guid>(
                name: "Guid",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Sales",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId1",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoPath",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedUserId1",
                table: "Companies",
                column: "CreatedUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId1",
                table: "Companies",
                column: "CreatedUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
