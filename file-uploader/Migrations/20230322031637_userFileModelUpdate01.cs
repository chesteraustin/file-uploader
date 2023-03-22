using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace file_uploader.Migrations
{
    /// <inheritdoc />
    public partial class userFileModelUpdate01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "UserFiles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "UserFiles");
        }
    }
}
