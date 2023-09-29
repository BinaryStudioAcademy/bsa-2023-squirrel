using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squirrel.Core.DAL.Migrations
{
    public partial class UserManyToOneBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_AuthorId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_AuthorId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Branches");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CreatedBy",
                table: "Branches",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_CreatedBy",
                table: "Branches",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Users_CreatedBy",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_CreatedBy",
                table: "Branches");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Branches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_AuthorId",
                table: "Branches",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Users_AuthorId",
                table: "Branches",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
