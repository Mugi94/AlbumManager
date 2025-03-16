using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FixRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Records_RecordId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Tracks_TrackId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_RecordId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_TrackId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "Artists");

            migrationBuilder.CreateTable(
                name: "ArtistRecord",
                columns: table => new
                {
                    ArtistsId = table.Column<int>(type: "integer", nullable: false),
                    RecordsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistRecord", x => new { x.ArtistsId, x.RecordsId });
                    table.ForeignKey(
                        name: "FK_ArtistRecord_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistRecord_Records_RecordsId",
                        column: x => x.RecordsId,
                        principalTable: "Records",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ArtistTrack",
                columns: table => new
                {
                    ArtistsId = table.Column<int>(type: "integer", nullable: false),
                    TracksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistTrack", x => new { x.ArtistsId, x.TracksId });
                    table.ForeignKey(
                        name: "FK_ArtistTrack_Artists_ArtistsId",
                        column: x => x.ArtistsId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistTrack_Tracks_TracksId",
                        column: x => x.TracksId,
                        principalTable: "Tracks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistRecord_RecordsId",
                table: "ArtistRecord",
                column: "RecordsId");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistTrack_TracksId",
                table: "ArtistTrack",
                column: "TracksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistRecord");

            migrationBuilder.DropTable(
                name: "ArtistTrack");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "Artists",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrackId",
                table: "Artists",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_RecordId",
                table: "Artists",
                column: "RecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_TrackId",
                table: "Artists",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Records_RecordId",
                table: "Artists",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Tracks_TrackId",
                table: "Artists",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id");
        }
    }
}
