using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Squirrel.Core.DAL.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "Id", "Body", "CreatedAt", "CreatedBy", "Title" },
                values: new object[,]
                {
                    { 2L, "Eligendi quisquam ullam iure praesentium numquam sapiente distinctio ad. Tempore voluptatibus ad et adipisci hic amet. Corporis soluta cupiditate soluta. Provident rerum nemo dolores debitis dicta voluptatem labore dolores adipisci. Adipisci illo quidem sit dolores. Ea dolor animi quod laborum quia perspiciatis sunt tempora.", new DateTime(2020, 7, 1, 1, 14, 20, 556, DateTimeKind.Unspecified).AddTicks(7372), 5L, "hic" },
                    { 3L, "Incidunt perferendis omnis. Quas voluptatem beatae vitae sunt a ut sed repellendus. Accusamus eos enim consequatur et praesentium ad ut beatae eius. Omnis voluptas error et velit autem ipsa atque consequuntur vitae. Nostrum accusamus soluta nisi.", new DateTime(2020, 11, 26, 1, 10, 54, 982, DateTimeKind.Unspecified).AddTicks(9175), 4L, "velit" },
                    { 4L, "Architecto laboriosam culpa cumque dicta in. Perspiciatis amet autem rerum recusandae perspiciatis pariatur. Eum sint molestiae quis neque tempora ab distinctio. Nobis nulla dignissimos voluptas nemo cumque tenetur quod et placeat. Nihil sit eos similique fuga enim dolores ullam suscipit.", new DateTime(2021, 1, 18, 12, 14, 38, 642, DateTimeKind.Unspecified).AddTicks(7703), 1L, "est" },
                    { 5L, "Sapiente et saepe ut atque dolore accusantium soluta cumque perferendis. Magni adipisci labore corrupti. Ratione et quibusdam consequatur voluptatem velit expedita eos maxime.", new DateTime(2020, 2, 2, 15, 3, 56, 551, DateTimeKind.Unspecified).AddTicks(1864), 5L, "placeat" },
                    { 6L, "Iusto aspernatur nihil iure ut blanditiis veritatis quas. Et illum quod atque nulla voluptas quos beatae quaerat consequatur. Ab placeat tenetur perferendis et omnis. Doloremque corrupti deserunt sint enim ex sit.", new DateTime(2021, 4, 7, 16, 50, 6, 239, DateTimeKind.Unspecified).AddTicks(5929), 3L, "facere" },
                    { 7L, "Doloremque omnis facilis unde exercitationem consectetur culpa porro consequatur sed. Vel rem rerum eum harum. Ratione voluptate est officia accusamus doloremque perferendis ea. Unde iure laudantium ut amet repellendus enim consequatur dolor porro. Sed expedita dolorem aperiam ipsa omnis. Ut omnis ipsa quia cupiditate iure.", new DateTime(2019, 7, 23, 7, 33, 40, 245, DateTimeKind.Unspecified).AddTicks(9313), 5L, "impedit" },
                    { 8L, "Nesciunt placeat et consectetur enim. Consectetur magnam perspiciatis ut rem perspiciatis odit dolorem. Modi corrupti corrupti.", new DateTime(2020, 1, 27, 9, 1, 30, 801, DateTimeKind.Unspecified).AddTicks(8159), 3L, "corporis" },
                    { 9L, "Omnis culpa earum modi eos beatae autem. Deleniti labore veritatis dolorum. Omnis perferendis ut sit nulla autem ut voluptatem voluptas ut.", new DateTime(2021, 3, 25, 21, 11, 5, 602, DateTimeKind.Unspecified).AddTicks(6614), 5L, "perspiciatis" },
                    { 10L, "Molestias porro exercitationem omnis et eius. Est consequatur esse sit quia dolorem sequi doloribus corporis. Perspiciatis qui dignissimos.", new DateTime(2021, 4, 7, 22, 46, 32, 439, DateTimeKind.Unspecified).AddTicks(5958), 3L, "esse" },
                    { 11L, "Eos eum perferendis nisi alias et ducimus repudiandae ut. Voluptas rerum ullam omnis placeat non ea voluptatibus. Sint et et asperiores omnis recusandae saepe laborum enim. Non consequatur voluptatem in aut quia quo quo. Commodi aliquid aut quaerat adipisci. Modi ea maxime doloribus qui sint.", new DateTime(2021, 3, 24, 14, 25, 37, 776, DateTimeKind.Unspecified).AddTicks(56), 1L, "in" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Samples");
        }
    }
}
