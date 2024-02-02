using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class probeersel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualQueue_Attracties_AttractieId",
                table: "VirtualQueue");

            migrationBuilder.DropIndex(
                name: "IX_VirtualQueue_AttractieId",
                table: "VirtualQueue");

            migrationBuilder.DropColumn(
                name: "AttractieId",
                table: "VirtualQueue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttractieId",
                table: "VirtualQueue",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VirtualQueue_AttractieId",
                table: "VirtualQueue",
                column: "AttractieId");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualQueue_Attracties_AttractieId",
                table: "VirtualQueue",
                column: "AttractieId",
                principalTable: "Attracties",
                principalColumn: "AttractieId");
        }
    }
}
