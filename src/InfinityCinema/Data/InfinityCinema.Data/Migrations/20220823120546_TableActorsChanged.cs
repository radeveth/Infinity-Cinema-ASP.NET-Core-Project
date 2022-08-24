#nullable disable

namespace InfinityCinema.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class TableActorsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Actors",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Actors");
        }
    }
}
