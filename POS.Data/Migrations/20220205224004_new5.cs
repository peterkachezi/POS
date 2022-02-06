using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Data.Migrations
{
    public partial class new5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductPackaging_ProductPackagingId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductPackaging");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductPackagingId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPackagingId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductPackagingId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductPackaging",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPackaging", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductPackagingId",
                table: "Products",
                column: "ProductPackagingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductPackaging_ProductPackagingId",
                table: "Products",
                column: "ProductPackagingId",
                principalTable: "ProductPackaging",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
