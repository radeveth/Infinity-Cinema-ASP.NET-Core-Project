#nullable disable

namespace InfinityCinema.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class MovieUserStarRtingsTableChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieComments",
                table: "MovieComments");

            migrationBuilder.DropIndex(
                name: "IX_MovieComments_MovieId",
                table: "MovieComments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MovieUserStarRatings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieUserStarRatings");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "MovieUserStarRatings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MovieComments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "MovieComments");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "MovieComments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MovieComments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieComments",
                table: "MovieComments",
                columns: new[] { "MovieId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieComments",
                table: "MovieComments");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "MovieUserStarRatings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieUserStarRatings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "MovieUserStarRatings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "MovieComments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MovieComments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "MovieComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "MovieComments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieComments",
                table: "MovieComments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MovieComments_MovieId",
                table: "MovieComments",
                column: "MovieId");
        }
    }
}
