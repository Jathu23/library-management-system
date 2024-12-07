using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_management_system.Migrations
{
    /// <inheritdoc />
    public partial class review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeDislikes",
                columns: table => new
                {
                    LikeDislikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: true),
                    BookType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NormalBookId = table.Column<int>(type: "int", nullable: false),
                    EBookId = table.Column<int>(type: "int", nullable: false),
                    AudioBookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeDislikes", x => x.LikeDislikeId);
                    table.ForeignKey(
                        name: "FK_LikeDislikes_Audiobooks_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "Audiobooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDislikes_Ebooks_EBookId",
                        column: x => x.EBookId,
                        principalTable: "Ebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDislikes_NormalBooks_NormalBookId",
                        column: x => x.NormalBookId,
                        principalTable: "NormalBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeDislikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    BookType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NormalBookId = table.Column<int>(type: "int", nullable: false),
                    EBookId = table.Column<int>(type: "int", nullable: false),
                    AudioBookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Audiobooks_AudioBookId",
                        column: x => x.AudioBookId,
                        principalTable: "Audiobooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Ebooks_EBookId",
                        column: x => x.EBookId,
                        principalTable: "Ebooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_NormalBooks_NormalBookId",
                        column: x => x.NormalBookId,
                        principalTable: "NormalBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_LikeDislikes_UserId",
                table: "LikeDislikes",
                column: "UserId");

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
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeDislikes");

            migrationBuilder.DropTable(
                name: "Reviews");
        }
    }
}
