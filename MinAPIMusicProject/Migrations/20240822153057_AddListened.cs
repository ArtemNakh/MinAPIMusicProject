using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinAPIMusicProject.Migrations
{
    /// <inheritdoc />
    public partial class AddListened : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Listened",
                table: "Tracks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Listened",
                table: "Tracks");
        }
    }
}
