using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class rijaanpassing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualQueue_Attracties_AttractieId",
                table: "VirtualQueue");

            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "VirtualQueue");

            migrationBuilder.DropColumn(
                name: "IsPresent",
                table: "VirtualQueue");

            migrationBuilder.AlterColumn<int>(
                name: "AttractieId",
                table: "VirtualQueue",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualQueue_Attracties_AttractieId",
                table: "VirtualQueue",
                column: "AttractieId",
                principalTable: "Attracties",
                principalColumn: "AttractieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VirtualQueue_Attracties_AttractieId",
                table: "VirtualQueue");

            migrationBuilder.AlterColumn<int>(
                name: "AttractieId",
                table: "VirtualQueue",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryTime",
                table: "VirtualQueue",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPresent",
                table: "VirtualQueue",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_VirtualQueue_Attracties_AttractieId",
                table: "VirtualQueue",
                column: "AttractieId",
                principalTable: "Attracties",
                principalColumn: "AttractieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
