using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudFileStorage.FileMetadataApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPermissionToFileMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Permission",
                table: "Files",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permission",
                table: "Files");
        }
    }
}
