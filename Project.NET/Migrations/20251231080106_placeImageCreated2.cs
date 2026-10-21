using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.NET.Migrations
{
    /// <inheritdoc />
    public partial class placeImageCreated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCover",
                table: "PlaceImages",
                newName: "IsCover");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "PlaceImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "PlaceImages");

            migrationBuilder.RenameColumn(
                name: "IsCover",
                table: "PlaceImages",
                newName: "isCover");
        }
    }
}
