using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Data.Migrations
{
    public partial class hrrr78 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CyberSaleDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SellingPrice = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    TaxAmount = table.Column<string>(maxLength: 100, nullable: true),
                    PaymentStatus = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CyberSaleDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CyberSales",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 128, nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    OrderNumber = table.Column<string>(nullable: false),
                    Change = table.Column<decimal>(nullable: false),
                    CashPaid = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CyberSales", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CyberSaleDetails");

            migrationBuilder.DropTable(
                name: "CyberSales");
        }
    }
}
