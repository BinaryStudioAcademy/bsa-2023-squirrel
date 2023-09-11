using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squirrel.Core.DAL.Migrations
{
    public partial class FixedCyclicDependency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_DefaultBranchId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultBranchId",
                table: "Projects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DefaultBranchId",
                table: "Projects",
                column: "DefaultBranchId",
                unique: true,
                filter: "[DefaultBranchId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_DefaultBranchId",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "DefaultBranchId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DefaultBranchId",
                table: "Projects",
                column: "DefaultBranchId",
                unique: true);
        }
    }
}
