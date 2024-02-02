using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class attractieRij : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attracties",
                columns: table => new
                {
                    AttractieId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naam = table.Column<string>(type: "TEXT", nullable: false),
                    Capaciteit = table.Column<int>(type: "INTEGER", nullable: false),
                    Duur = table.Column<int>(type: "INTEGER", nullable: false),
                    Beschrijving = table.Column<string>(type: "TEXT", nullable: false),
                    AfbeeldingUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attracties", x => x.AttractieId);
                });

            migrationBuilder.CreateTable(
                name: "VirtualQueue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AttractionId = table.Column<int>(type: "INTEGER", nullable: false),
                    EntryTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsPresent = table.Column<bool>(type: "INTEGER", nullable: false),
                    AttractieId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VirtualQueue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VirtualQueue_Attracties_AttractieId",
                        column: x => x.AttractieId,
                        principalTable: "Attracties",
                        principalColumn: "AttractieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VirtualQueue_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VirtualQueue_AttractieId",
                table: "VirtualQueue",
                column: "AttractieId");

            migrationBuilder.CreateIndex(
                name: "IX_VirtualQueue_UserId",
                table: "VirtualQueue",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VirtualQueue");

            migrationBuilder.DropTable(
                name: "Attracties");
        }
    }
}
