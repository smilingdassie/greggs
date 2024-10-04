using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greggs.Products.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceInPounds = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "PriceInPounds" },
                values: new object[,]
                {
                    { 1, "Sausage Roll", 1m },
                    { 2, "Vegan Sausage Roll", 1.1m },
                    { 3, "Steak Bake", 1.2m },
                    { 4, "Yum Yum", 0.7m },
                    { 5, "Pink Jammie", 0.5m },
                    { 6, "Mexican Baguette", 2.1m },
                    { 7, "Bacon Sandwich", 1.95m },
                    { 8, "Coca Cola", 1.2m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
