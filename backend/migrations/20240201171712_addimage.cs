using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class addimage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
    migrationBuilder.AddColumn<string>(
        name: "AfbeeldingBestandsnaam",
        table: "Attracties",
        nullable: true,
        defaultValue: "default.jpg");  
        
    migrationBuilder.Sql("UPDATE Attracties SET AfbeeldingBestandsnaam = 'Images/reuzenrad.jpg' WHERE AttractieId = 1");
}


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
