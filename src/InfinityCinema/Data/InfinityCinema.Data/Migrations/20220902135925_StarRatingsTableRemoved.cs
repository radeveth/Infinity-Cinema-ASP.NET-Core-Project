#nullable disable

namespace InfinityCinema.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class StarRatingsTableRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StarRatings");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Genres",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Genres");

            migrationBuilder.CreateTable(
                name: "StarRatings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarRatings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StarRatings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StarRatings_IsDeleted",
                table: "StarRatings",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_StarRatings_MovieId",
                table: "StarRatings",
                column: "MovieId");
        }
    }
}
