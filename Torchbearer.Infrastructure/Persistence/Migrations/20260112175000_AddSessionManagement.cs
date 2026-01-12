using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Torchbearer.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ScheduledAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    GameMasterId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxPartySize = table.Column<int>(type: "INTEGER", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    GoldReward = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExperienceReward = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Players_GameMasterId",
                        column: x => x.GameMasterId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SessionCharacters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SessionId = table.Column<int>(type: "INTEGER", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    SignedUpAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionCharacters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionCharacters_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionCharacters_CharacterId",
                table: "SessionCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionCharacters_SessionId_CharacterId",
                table: "SessionCharacters",
                columns: new[] { "SessionId", "CharacterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_GameMasterId",
                table: "Sessions",
                column: "GameMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionCharacters");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
