using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasySound.Data.Migrations
{
    public partial class SoundLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoundLink",
                table: "Sounds",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoundLink",
                table: "Sounds");
        }
    }
}
