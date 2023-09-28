using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squirrel.Core.DAL.Migrations
{
    public partial class AddedParentBranchAndChangedCommits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSaved",
                table: "Commits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PostScript",
                table: "Commits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreScript",
                table: "Commits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParentBranchId",
                table: "Branches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Branches_ParentBranchId",
                table: "Branches",
                column: "ParentBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Branches_ParentBranchId",
                table: "Branches",
                column: "ParentBranchId",
                principalTable: "Branches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Branches_ParentBranchId",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_ParentBranchId",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "IsSaved",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "PostScript",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "PreScript",
                table: "Commits");

            migrationBuilder.DropColumn(
                name: "ParentBranchId",
                table: "Branches");
        }
    }
}
