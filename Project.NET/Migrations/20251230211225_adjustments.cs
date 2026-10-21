using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.NET.Migrations
{
    /// <inheritdoc />
    public partial class adjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpeningDays",
                table: "Places",
                newName: "OpeningDaysSerialized");

            migrationBuilder.RenameColumn(
                name: "Features",
                table: "Places",
                newName: "FeaturesSerialized");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpeningDaysSerialized",
                table: "Places",
                newName: "OpeningDays");

            migrationBuilder.RenameColumn(
                name: "FeaturesSerialized",
                table: "Places",
                newName: "Features");
        }
    }
}
