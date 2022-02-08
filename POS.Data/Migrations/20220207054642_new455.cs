using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Data.Migrations
{
    public partial class new455 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReOrderLevel",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReOrderLevel",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
