using Microsoft.EntityFrameworkCore.Migrations;

namespace Canteen.Core.Migrations
{
    public partial class Addedrefreshtokenandaccesstokentomodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Money",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AppUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AppUsers");
        }
    }
}
