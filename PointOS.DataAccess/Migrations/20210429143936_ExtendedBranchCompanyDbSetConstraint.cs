using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace PointOS.DataAccess.Migrations
{
    public partial class ExtendedBranchCompanyDbSetConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Branches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "Branches",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CreatedUserId",
                table: "Branches",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_AspNetUsers_CreatedUserId",
                table: "Branches",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId",
                table: "Companies",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_AspNetUsers_CreatedUserId",
                table: "Branches");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Branches_CreatedUserId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Branches");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedUserId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId",
                table: "Companies",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
