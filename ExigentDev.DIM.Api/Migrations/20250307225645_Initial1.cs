using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExigentDev.DIM.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DogImage_Dog_DogId",
                table: "DogImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DogImage",
                table: "DogImage");

            migrationBuilder.RenameTable(
                name: "DogImage",
                newName: "DogImages");

            migrationBuilder.RenameIndex(
                name: "IX_DogImage_DogId",
                table: "DogImages",
                newName: "IX_DogImages_DogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DogImages",
                table: "DogImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DogImages_Dog_DogId",
                table: "DogImages",
                column: "DogId",
                principalTable: "Dog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DogImages_Dog_DogId",
                table: "DogImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DogImages",
                table: "DogImages");

            migrationBuilder.RenameTable(
                name: "DogImages",
                newName: "DogImage");

            migrationBuilder.RenameIndex(
                name: "IX_DogImages_DogId",
                table: "DogImage",
                newName: "IX_DogImage_DogId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DogImage",
                table: "DogImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DogImage_Dog_DogId",
                table: "DogImage",
                column: "DogId",
                principalTable: "Dog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
