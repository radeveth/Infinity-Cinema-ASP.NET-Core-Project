#nullable disable

namespace InfinityCinema.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class CommentsUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieComments_AspNetUsers_ApplicationUserId",
                table: "MovieComments");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieComments_Movies_MovieId",
                table: "MovieComments");

            migrationBuilder.DropIndex(
                name: "IX_MovieComments_ApplicationUserId",
                table: "MovieComments");

            migrationBuilder.DropIndex(
                name: "IX_MovieComments_MovieId",
                table: "MovieComments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "MovieComments");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "MovieComments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "MovieComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "MovieComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_ApplicationUserId",
                table: "MovieComments",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_MovieId",
                table: "MovieComments",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieComments_AspNetUsers_ApplicationUserId",
                table: "MovieComments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieComments_Movies_MovieId",
                table: "MovieComments",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
