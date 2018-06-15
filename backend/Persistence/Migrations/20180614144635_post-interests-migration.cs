using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class postinterestsmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interest_Posts_PostId",
                table: "Interest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interest",
                table: "Interest");

            migrationBuilder.RenameTable(
                name: "Interest",
                newName: "Interests");

            migrationBuilder.RenameIndex(
                name: "IX_Interest_PostId",
                table: "Interests",
                newName: "IX_Interests_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interests",
                table: "Interests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interests_Posts_PostId",
                table: "Interests",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interests_Posts_PostId",
                table: "Interests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interests",
                table: "Interests");

            migrationBuilder.RenameTable(
                name: "Interests",
                newName: "Interest");

            migrationBuilder.RenameIndex(
                name: "IX_Interests_PostId",
                table: "Interest",
                newName: "IX_Interest_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interest",
                table: "Interest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Interest_Posts_PostId",
                table: "Interest",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
