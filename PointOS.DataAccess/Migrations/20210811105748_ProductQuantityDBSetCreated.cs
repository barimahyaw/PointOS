using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOS.DataAccess.Migrations
{
    public partial class ProductQuantityDbSetCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ModifiedUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_AspNetUsers_ModifiedUserId",
                        column: x => x.ModifiedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductQuantities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_CreatedUserId",
                table: "ProductQuantities",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_ModifiedUserId",
                table: "ProductQuantities",
                column: "ModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuantities_ProductId",
                table: "ProductQuantities",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductQuantities");
        }
    }
}
