using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squirrel.Core.DAL.Migrations
{
    public partial class RemovedUniqueContraintFromGuidInProjectDatabases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ProjectDatabases_Guid",
                table: "ProjectDatabases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ProjectDatabases_Guid",
                table: "ProjectDatabases",
                column: "Guid");
        }
    }
}
