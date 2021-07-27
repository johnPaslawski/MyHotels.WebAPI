using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHotels.WebAPI.Migrations
{
    public partial class DB3_update_Country_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Hotels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
