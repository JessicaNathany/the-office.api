using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the_office.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeasonFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Season",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Season",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Season",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Season",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalEpisodes",
                table: "Season",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Season");

            migrationBuilder.DropColumn(
                name: "TotalEpisodes",
                table: "Season");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Season",
                newName: "Description");
        }
    }
}
