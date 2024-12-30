using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BestMovies.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoviesAndDirectors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    Rating = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DirectorMovie",
                columns: table => new
                {
                    DirectorsId = table.Column<int>(type: "INTEGER", nullable: false),
                    MoviesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorMovie", x => new { x.DirectorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_DirectorMovie_Directors_DirectorsId",
                        column: x => x.DirectorsId,
                        principalTable: "Directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectorMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Steven Spielberg" },
                    { 2, "Martin Scorsese" },
                    { 3, "Alfred Hitchcock" },
                    { 4, "Stanley Kubrick" },
                    { 5, "Francis Ford Coppola" },
                    { 6, "Woody Allen" },
                    { 7, "Billy Wilder" },
                    { 8, "John Huston" },
                    { 9, "Peter Jackson" },
                    { 10, "Milos Forman" },
                    { 11, "Clint Eastwood" },
                    { 12, "David Lean" },
                    { 13, "Ingmar Bergman" },
                    { 14, "Joel Coen" },
                    { 15, "John Ford" },
                    { 16, "James Cameron" },
                    { 17, "Sidney Lumet" },
                    { 18, "Charles Chaplin" },
                    { 19, "Tim Burton" },
                    { 20, "Roman Polanski" },
                    { 21, "Quentin Tarantino" },
                    { 22, "Danny Boyle" },
                    { 23, "Ridley Scott" },
                    { 24, "David Fincher" },
                    { 25, "Christopher Nolan" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Rating", "Title", "Year" },
                values: new object[,]
                {
                    { 1, 9.0, "Schindler's List", 1993 },
                    { 2, 7.5999999999999996, "Killers of the Flower Moon", 2023 },
                    { 3, 8.5, "Psycho", 1960 },
                    { 4, 8.3000000000000007, "2001: A Space Odyssey", 1968 },
                    { 5, 8.4000000000000004, "Apocalypse Now", 1979 },
                    { 6, 8.0, "Annie Hall", 1977 },
                    { 7, 8.3000000000000007, "The Apartment", 1960 },
                    { 8, 8.1999999999999993, "The Treasure of the Sierra Madre", 1948 },
                    { 9, 8.9000000000000004, "The Lord of the Rings: The Fellowship of the Ring", 2001 },
                    { 10, 8.6999999999999993, "One Flew Over the Cuckoo's Nest", 1975 }
                });

            migrationBuilder.InsertData(
                table: "DirectorMovie",
                columns: new[] { "DirectorsId", "MoviesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 },
                    { 10, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectorMovie_MoviesId",
                table: "DirectorMovie",
                column: "MoviesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DirectorMovie");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
