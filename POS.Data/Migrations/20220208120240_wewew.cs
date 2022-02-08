using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Data.Migrations
{
    public partial class wewew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),

                    ProductId = table.Column<Guid>(nullable: false),

                    ProductCode = table.Column<string>(nullable: true),

                    Quantity = table.Column<int>(nullable: false),

                    ReOrderLeve = table.Column<int>(nullable: false),

                    CreateDate = table.Column<DateTime>(nullable: false),

                    UpdatedDate = table.Column<DateTime>(nullable: false),

                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),

                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
