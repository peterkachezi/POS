using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Data.Migrations
{
    public partial class u : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_UOMs_UOMId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UOMId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UOMId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UOMId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UOMId",
                table: "Products",
                column: "UOMId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_UOMs_UOMId",
                table: "Products",
                column: "UOMId",
                principalTable: "UOMs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
