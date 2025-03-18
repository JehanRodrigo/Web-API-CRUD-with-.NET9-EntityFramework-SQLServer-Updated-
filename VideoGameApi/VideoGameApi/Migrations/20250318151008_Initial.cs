using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new //creates a table called VideoGames with the following columns
                {
                    Id = table.Column<int>(type: "int", nullable: false) //creates a column called Id with the type int
                        .Annotation("SqlServer:Identity", "1, 1"), //sets the column to auto increment
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true), //creates a column called Title with the type string
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true), //creates a column called Platform with the type string
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: true), //creates a column called Developer with the type string
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true) //creates a column called Publisher with the type string
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id); //sets the Id column as the primary key
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) //deletes the table VideoGames if it exists 
        {
            migrationBuilder.DropTable( 
                name: "VideoGames"); 
        }
    }
}
