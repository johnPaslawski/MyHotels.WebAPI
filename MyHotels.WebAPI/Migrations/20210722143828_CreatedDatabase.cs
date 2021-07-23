using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHotels.WebAPI.Migrations
{
    public partial class CreatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hotels_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 1, "PL", "Poland" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 2, "DE", "Germany" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { 3, "US", "United States" });

            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Adress", "Country", "CountryId", "Name", "Rating" },
                values: new object[,]
                {
                    { 1, "Sowa 2, 30-324 Wrocław", null, 1, "Hotel Piast", 4.0 },
                    { 2, "Kury 4, 19-969 Kraków", null, 1, "Hotel Kołodziej", 4.5 },
                    { 3, "Walsfdorf Strasse 4, 10-322 Berlin", null, 2, "Hotel Eindhoven", 4.7000000000000002 },
                    { 4, "Einsatz Platz 32, 28-001 Lipsk", null, 2, "Hotel SturmUndDrang", 4.9000000000000004 },
                    { 5, "South 667 Avenue 6, 82-937 Cincinnatti", null, 3, "Hotel Welcome Folks", 5.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CountryId",
                table: "Hotels",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
