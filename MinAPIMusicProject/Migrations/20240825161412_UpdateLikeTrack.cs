using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinAPIMusicProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLikeTrack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackUser",
                columns: table => new
                {
                    LikedByUsersId = table.Column<int>(type: "int", nullable: false),
                    LikedTracksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackUser", x => new { x.LikedByUsersId, x.LikedTracksId });
                    table.ForeignKey(
                        name: "FK_TrackUser_Tracks_LikedTracksId",
                        column: x => x.LikedTracksId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackUser_Users_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackUser_LikedTracksId",
                table: "TrackUser",
                column: "LikedTracksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackUser");
        }
    }
}
