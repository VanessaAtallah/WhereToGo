using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.NET.Migrations
{
    /// <inheritdoc />
    public partial class PlaceLikesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaceLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaceLikes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaceLikes_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaceLikes_PlaceId_UserId",
                table: "PlaceLikes",
                columns: new[] { "PlaceId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaceLikes_UserId",
                table: "PlaceLikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaceLikes");
        }
    }
}
