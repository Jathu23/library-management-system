using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_management_system.Migrations
{
    /// <inheritdoc />
    public partial class Keyadd12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudiobookLikeDislikes_Audiobooks_AudiobookId",
                table: "AudiobookLikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_EbookLikeDislikes_Ebooks_EbookId",
                table: "EbookLikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_NormalBookLikeDislikes_NormalBooks_NormalBookId",
                table: "NormalBookLikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_NormalBookLikeDislikes_NormalBookId",
                table: "NormalBookLikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_EbookLikeDislikes_EbookId",
                table: "EbookLikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_AudiobookLikeDislikes_AudiobookId",
                table: "AudiobookLikeDislikes");

            migrationBuilder.DropColumn(
                name: "NormalBookId",
                table: "NormalBookLikeDislikes");

            migrationBuilder.DropColumn(
                name: "EbookId",
                table: "EbookLikeDislikes");

            migrationBuilder.DropColumn(
                name: "AudiobookId",
                table: "AudiobookLikeDislikes");

            migrationBuilder.CreateIndex(
                name: "IX_NormalBookLikeDislikes_BookId",
                table: "NormalBookLikeDislikes",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_EbookLikeDislikes_BookId",
                table: "EbookLikeDislikes",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_AudiobookLikeDislikes_BookId",
                table: "AudiobookLikeDislikes",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudiobookLikeDislikes_Audiobooks_BookId",
                table: "AudiobookLikeDislikes",
                column: "BookId",
                principalTable: "Audiobooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EbookLikeDislikes_Ebooks_BookId",
                table: "EbookLikeDislikes",
                column: "BookId",
                principalTable: "Ebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NormalBookLikeDislikes_NormalBooks_BookId",
                table: "NormalBookLikeDislikes",
                column: "BookId",
                principalTable: "NormalBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudiobookLikeDislikes_Audiobooks_BookId",
                table: "AudiobookLikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_EbookLikeDislikes_Ebooks_BookId",
                table: "EbookLikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_NormalBookLikeDislikes_NormalBooks_BookId",
                table: "NormalBookLikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_NormalBookLikeDislikes_BookId",
                table: "NormalBookLikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_EbookLikeDislikes_BookId",
                table: "EbookLikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_AudiobookLikeDislikes_BookId",
                table: "AudiobookLikeDislikes");

            migrationBuilder.AddColumn<int>(
                name: "NormalBookId",
                table: "NormalBookLikeDislikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EbookId",
                table: "EbookLikeDislikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AudiobookId",
                table: "AudiobookLikeDislikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NormalBookLikeDislikes_NormalBookId",
                table: "NormalBookLikeDislikes",
                column: "NormalBookId");

            migrationBuilder.CreateIndex(
                name: "IX_EbookLikeDislikes_EbookId",
                table: "EbookLikeDislikes",
                column: "EbookId");

            migrationBuilder.CreateIndex(
                name: "IX_AudiobookLikeDislikes_AudiobookId",
                table: "AudiobookLikeDislikes",
                column: "AudiobookId");

            migrationBuilder.AddForeignKey(
                name: "FK_AudiobookLikeDislikes_Audiobooks_AudiobookId",
                table: "AudiobookLikeDislikes",
                column: "AudiobookId",
                principalTable: "Audiobooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EbookLikeDislikes_Ebooks_EbookId",
                table: "EbookLikeDislikes",
                column: "EbookId",
                principalTable: "Ebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NormalBookLikeDislikes_NormalBooks_NormalBookId",
                table: "NormalBookLikeDislikes",
                column: "NormalBookId",
                principalTable: "NormalBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
