using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Data.Migrations
{
    public partial class new4558 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UOMId",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UOMId",
                table: "Products");
        }
    }
}
