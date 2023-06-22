using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the_office.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseSeasonSummaryField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Season",
                type: "character varying(750)",
                maxLength: 750,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "Season",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(750)",
                oldMaxLength: 750);
        }
    }
}
