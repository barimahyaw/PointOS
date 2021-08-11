using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOS.DataAccess.Migrations
{
    public partial class ChangedDbSetNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantities_AspNetUsers_CreatedUserId",
                table: "ProductQuantities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantities_AspNetUsers_ModifiedUserId",
                table: "ProductQuantities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductQuantities_Products_ProductId",
                table: "ProductQuantities");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Products_ProductTypes_ProductCategoryId",
            //    table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductQuantities",
                table: "ProductQuantities");

            migrationBuilder.RenameTable(
                name: "ProductQuantities",
                newName: "ProductStocks");

            migrationBuilder.RenameIndex(
                name: "IX_ProductQuantities_ProductId",
                table: "ProductStocks",
                newName: "IX_ProductStocks_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductQuantities_ModifiedUserId",
                table: "ProductStocks",
                newName: "IX_ProductStocks_ModifiedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductQuantities_CreatedUserId",
                table: "ProductStocks",
                newName: "IX_ProductStocks_CreatedUserId");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductStocks",
                table: "ProductStocks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_AspNetUsers_CreatedUserId",
                table: "ProductStocks",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_AspNetUsers_ModifiedUserId",
                table: "ProductStocks",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_Products_ProductId",
                table: "ProductStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_AspNetUsers_CreatedUserId",
                table: "ProductStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_AspNetUsers_ModifiedUserId",
                table: "ProductStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_Products_ProductId",
                table: "ProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductStocks",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "ProductStocks",
                newName: "ProductQuantities");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStocks_ProductId",
                table: "ProductQuantities",
                newName: "IX_ProductQuantities_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStocks_ModifiedUserId",
                table: "ProductQuantities",
                newName: "IX_ProductQuantities_ModifiedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductStocks_CreatedUserId",
                table: "ProductQuantities",
                newName: "IX_ProductQuantities_CreatedUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductQuantities",
                table: "ProductQuantities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantities_AspNetUsers_CreatedUserId",
                table: "ProductQuantities",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantities_AspNetUsers_ModifiedUserId",
                table: "ProductQuantities",
                column: "ModifiedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductQuantities_Products_ProductId",
                table: "ProductQuantities",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId",
                principalTable: "ProductCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
