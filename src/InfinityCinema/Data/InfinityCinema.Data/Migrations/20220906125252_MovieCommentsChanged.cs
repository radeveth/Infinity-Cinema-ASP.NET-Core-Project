#nullable disable

namespace InfinityCinema.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MovieCommentsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieUserComments_AspNetUsers_UserId",
                table: "MovieUserComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieUserComments",
                table: "MovieUserComments");

            migrationBuilder.DropIndex(
                name: "IX_MovieUserComments_UserId",
                table: "MovieUserComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MovieUserComments");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MovieComments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieUserComments",
                table: "MovieUserComments",
                columns: new[] { "MovieId", "CommentId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_UserId",
                table: "MovieComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieComments_AspNetUsers_UserId",
                table: "MovieComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieComments_AspNetUsers_UserId",
                table: "MovieComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieUserComments",
                table: "MovieUserComments");

            migrationBuilder.DropIndex(
                name: "IX_MovieComments_UserId",
                table: "MovieComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MovieComments");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MovieUserComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieUserComments",
                table: "MovieUserComments",
                columns: new[] { "MovieId", "UserId", "CommentId" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieUserComments_UserId",
                table: "MovieUserComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieUserComments_AspNetUsers_UserId",
                table: "MovieUserComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
