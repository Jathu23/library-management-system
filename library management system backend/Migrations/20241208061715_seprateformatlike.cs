using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_management_system.Migrations
{
    /// <inheritdoc />
    public partial class seprateformatlike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AudiobookLikeDislikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AudiobookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudiobookLikeDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudiobookLikeDislikes_Audiobooks_AudiobookId",
                        column: x => x.AudiobookId,
                        principalTable: "Audiobooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AudiobookLikeDislikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EbookLikeDislikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EbookLikeDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EbookLikeDislikes_Ebooks_BookId",
                        column: x => x.BookId,
                        principalTable: "Ebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EbookLikeDislikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NormalBookLikeDislikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NormalBookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NormalBookLikeDislikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NormalBookLikeDislikes_NormalBooks_NormalBookId",
                        column: x => x.NormalBookId,
                        principalTable: "NormalBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NormalBookLikeDislikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AudiobookLikeDislikes_AudiobookId",
                table: "AudiobookLikeDislikes",
                column: "AudiobookId");

            migrationBuilder.CreateIndex(
                name: "IX_AudiobookLikeDislikes_UserId",
                table: "AudiobookLikeDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EbookLikeDislikes_BookId",
                table: "EbookLikeDislikes",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_EbookLikeDislikes_UserId",
                table: "EbookLikeDislikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NormalBookLikeDislikes_NormalBookId",
                table: "NormalBookLikeDislikes",
                column: "NormalBookId");

            migrationBuilder.CreateIndex(
                name: "IX_NormalBookLikeDislikes_UserId",
                table: "NormalBookLikeDislikes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudiobookLikeDislikes");

            migrationBuilder.DropTable(
                name: "EbookLikeDislikes");

            migrationBuilder.DropTable(
                name: "NormalBookLikeDislikes");
        }
    }
}
