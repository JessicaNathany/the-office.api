using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace the_office.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phrase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Phrase = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CharacterName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phrase", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Season",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Code = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Season", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Character",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameActor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    Gender = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Job = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SeasonId = table.Column<int>(type: "integer", nullable: true),
                    Code = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Character", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Character_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Episode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AirDate = table.Column<DateTime>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SeasonId = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episode_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterSeason",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    SeasonId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSeason", x => new { x.CharacterId, x.SeasonId });
                    table.ForeignKey(
                        name: "FK_CharacterSeason_Character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacterSeason_Season_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Season",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CharacterEpisode",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "integer", nullable: false),
                    EpisodesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEpisode", x => new { x.CharactersId, x.EpisodesId });
                    table.ForeignKey(
                        name: "FK_CharacterEpisode_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CharacterEpisode_Episode_EpisodesId",
                        column: x => x.EpisodesId,
                        principalTable: "Episode",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Character_Code",
                table: "Character",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Character_SeasonId",
                table: "Character",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEpisode_EpisodesId",
                table: "CharacterEpisode",
                column: "EpisodesId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSeason_SeasonId",
                table: "CharacterSeason",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Episode_Code",
                table: "Episode",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Episode_SeasonId",
                table: "Episode",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Phrase_Code",
                table: "Phrase",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Season_Code",
                table: "Season",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEpisode");

            migrationBuilder.DropTable(
                name: "CharacterSeason");

            migrationBuilder.DropTable(
                name: "Phrase");

            migrationBuilder.DropTable(
                name: "Episode");

            migrationBuilder.DropTable(
                name: "Character");

            migrationBuilder.DropTable(
                name: "Season");
        }
    }
}
