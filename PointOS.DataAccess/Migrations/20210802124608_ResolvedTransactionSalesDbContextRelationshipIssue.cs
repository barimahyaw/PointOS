using Microsoft.EntityFrameworkCore.Migrations;

namespace PointOS.DataAccess.Migrations
{
    public partial class ResolvedTransactionSalesDbContextRelationshipIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Transactions_TransactionId1",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Sales_TransactionId1",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId1",
                table: "Sales");


            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Transactions",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TransactionId",
                table: "Sales",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Transactions_TransactionId",
                table: "Sales",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "TransactionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Transactions_TransactionId",
                table: "Sales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Sales_TransactionId",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId1",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_TransactionId1",
                table: "Sales",
                column: "TransactionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Transactions_TransactionId1",
                table: "Sales",
                column: "TransactionId1",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
