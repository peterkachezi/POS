using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Data.Migrations
{
    public partial class new9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Bomet" },
                    { 26, "Migori" },
                    { 27, "Mambasa" },
                    { 28, "Muranga" },
                    { 29, "Nairobi" },
                    { 30, "Nakuru" },
                    { 31, "Nandi" },
                    { 32, "Narok" },
                    { 33, "Nyamira" },
                    { 34, "Nyandarua" },
                    { 25, "Meru" },
                    { 35, "Nnyeri" },
                    { 37, "Siaya" },
                    { 38, "Taita Taveta" },
                    { 39, "Tana River" },
                    { 40, "Tharaka-Nithi" },
                    { 41, "Trans-Nzoia" },
                    { 42, "Turkana" },
                    { 43, "Uasin Gishu" },
                    { 44, "Vihiga" },
                    { 45, "Wajir" },
                    { 36, "Samburu" },
                    { 46, "West Pokot" },
                    { 24, "Marsabit" },
                    { 22, "Makueni" },
                    { 2, "Bungoma" },
                    { 3, "Busia" },
                    { 4, "Elgeyo-Marakwet" },
                    { 5, "Embu" },
                    { 6, "Garissa" },
                    { 7, "Homa Bay" },
                    { 8, "Isiolo" },
                    { 9, "Kajiado" },
                    { 10, "Kakamega" },
                    { 23, "Mandera" },
                    { 11, "Kericho" },
                    { 13, "Kilifi" },
                    { 14, "Kirinyaga" },
                    { 15, "Kisii" },
                    { 16, "Kisumu" },
                    { 17, "Kitui" },
                    { 18, "Kwale" },
                    { 19, "Laikipia" },
                    { 20, "Lamu" },
                    { 21, "Machakos" },
                    { 12, "Kiambu" },
                    { 47, "Baringo" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Counties");
        }
    }
}
