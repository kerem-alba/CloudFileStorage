using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudFileStorage.FileMetadataApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangePermissionToEnumInFileShareMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Permission",
                table: "FileShareMetadatas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Permission",
                table: "FileShareMetadatas",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
