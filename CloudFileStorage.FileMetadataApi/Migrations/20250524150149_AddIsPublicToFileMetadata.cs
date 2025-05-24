using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudFileStorage.FileMetadataApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPublicToFileMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Files",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Files");
        }
    }
}
