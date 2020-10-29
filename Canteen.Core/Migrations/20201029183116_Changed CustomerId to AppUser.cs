using Microsoft.EntityFrameworkCore.Migrations;

namespace Canteen.Core.Migrations
{
    public partial class ChangedCustomerIdtoAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CstOrders_AppUsers_CustomerIdId",
                table: "CstOrders");

            migrationBuilder.DropIndex(
                name: "IX_CstOrders_CustomerIdId",
                table: "CstOrders");

            migrationBuilder.DropColumn(
                name: "CustomerIdId",
                table: "CstOrders");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "CstOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CstOrders_AppUserId",
                table: "CstOrders",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CstOrders_AppUsers_AppUserId",
                table: "CstOrders",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CstOrders_AppUsers_AppUserId",
                table: "CstOrders");

            migrationBuilder.DropIndex(
                name: "IX_CstOrders_AppUserId",
                table: "CstOrders");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "CstOrders");

            migrationBuilder.AddColumn<int>(
                name: "CustomerIdId",
                table: "CstOrders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CstOrders_CustomerIdId",
                table: "CstOrders",
                column: "CustomerIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_CstOrders_AppUsers_CustomerIdId",
                table: "CstOrders",
                column: "CustomerIdId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
