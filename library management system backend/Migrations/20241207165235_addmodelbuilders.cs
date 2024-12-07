using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_management_system.Migrations
{
    /// <inheritdoc />
    public partial class addmodelbuilders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeDislikes_Audiobooks_AudioBookId",
                table: "LikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeDislikes_Ebooks_EBookId",
                table: "LikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeDislikes_NormalBooks_NormalBookId",
                table: "LikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Audiobooks_AudioBookId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Ebooks_EBookId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_NormalBooks_NormalBookId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_AudioBookId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EBookId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_NormalBookId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_LikeDislikes_AudioBookId",
                table: "LikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_LikeDislikes_EBookId",
                table: "LikeDislikes");

            migrationBuilder.DropIndex(
                name: "IX_LikeDislikes_NormalBookId",
                table: "LikeDislikes");

            migrationBuilder.DropColumn(
                name: "AudioBookId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "EBookId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "NormalBookId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "AudioBookId",
                table: "LikeDislikes");

            migrationBuilder.DropColumn(
                name: "EBookId",
                table: "LikeDislikes");

            migrationBuilder.DropColumn(
                name: "NormalBookId",
                table: "LikeDislikes");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_BookId",
                table: "LikeDislikes",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDislikes_Audiobook",
                table: "LikeDislikes",
                column: "BookId",
                principalTable: "Audiobooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDislikes_Ebook",
                table: "LikeDislikes",
                column: "BookId",
                principalTable: "Ebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDislikes_NormalBook",
                table: "LikeDislikes",
                column: "BookId",
                principalTable: "NormalBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Audiobook",
                table: "Reviews",
                column: "BookId",
                principalTable: "Audiobooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Ebook",
                table: "Reviews",
                column: "BookId",
                principalTable: "Ebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_NormalBook",
                table: "Reviews",
                column: "BookId",
                principalTable: "NormalBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeDislikes_Audiobook",
                table: "LikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeDislikes_Ebook",
                table: "LikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeDislikes_NormalBook",
                table: "LikeDislikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Audiobook",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Ebook",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_NormalBook",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_LikeDislikes_BookId",
                table: "LikeDislikes");

            migrationBuilder.AddColumn<int>(
                name: "AudioBookId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EBookId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NormalBookId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AudioBookId",
                table: "LikeDislikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EBookId",
                table: "LikeDislikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NormalBookId",
                table: "LikeDislikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AudioBookId",
                table: "Reviews",
                column: "AudioBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EBookId",
                table: "Reviews",
                column: "EBookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_NormalBookId",
                table: "Reviews",
                column: "NormalBookId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_AudioBookId",
                table: "LikeDislikes",
                column: "AudioBookId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_EBookId",
                table: "LikeDislikes",
                column: "EBookId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_NormalBookId",
                table: "LikeDislikes",
                column: "NormalBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDislikes_Audiobooks_AudioBookId",
                table: "LikeDislikes",
                column: "AudioBookId",
                principalTable: "Audiobooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDislikes_Ebooks_EBookId",
                table: "LikeDislikes",
                column: "EBookId",
                principalTable: "Ebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeDislikes_NormalBooks_NormalBookId",
                table: "LikeDislikes",
                column: "NormalBookId",
                principalTable: "NormalBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Audiobooks_AudioBookId",
                table: "Reviews",
                column: "AudioBookId",
                principalTable: "Audiobooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Ebooks_EBookId",
                table: "Reviews",
                column: "EBookId",
                principalTable: "Ebooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_NormalBooks_NormalBookId",
                table: "Reviews",
                column: "NormalBookId",
                principalTable: "NormalBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
