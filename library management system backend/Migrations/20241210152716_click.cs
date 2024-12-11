using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_management_system.Migrations
{
    /// <inheritdoc />
    public partial class click : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Click",
                table: "EbookMetadatas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Click",
                table: "AudiobookMetadatas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Click",
                table: "EbookMetadatas");

            migrationBuilder.DropColumn(
                name: "Click",
                table: "AudiobookMetadatas");
        }
    }
}
