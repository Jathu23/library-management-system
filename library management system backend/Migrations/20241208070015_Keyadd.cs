using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_management_system.Migrations
{
    /// <inheritdoc />
    public partial class Keyadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EbookLikeDislikes_Ebooks_BookId",
                table: "EbookLikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_EbookLikeDislikes_BookId",
                table: "EbookLikeDislikes");

            migrationBuilder.AddColumn<int>(
                name: "EbookId",
                table: "EbookLikeDislikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EbookLikeDislikes_EbookId",
                table: "EbookLikeDislikes",
                column: "EbookId");

            migrationBuilder.AddForeignKey(
                name: "FK_EbookLikeDislikes_Ebooks_EbookId",
                table: "EbookLikeDislikes",
                column: "EbookId",
                principalTable: "Ebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EbookLikeDislikes_Ebooks_EbookId",
                table: "EbookLikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_EbookLikeDislikes_EbookId",
                table: "EbookLikeDislikes");

            migrationBuilder.DropColumn(
                name: "EbookId",
                table: "EbookLikeDislikes");

            migrationBuilder.CreateIndex(
                name: "IX_EbookLikeDislikes_BookId",
                table: "EbookLikeDislikes",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_EbookLikeDislikes_Ebooks_BookId",
                table: "EbookLikeDislikes",
                column: "BookId",
                principalTable: "Ebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
