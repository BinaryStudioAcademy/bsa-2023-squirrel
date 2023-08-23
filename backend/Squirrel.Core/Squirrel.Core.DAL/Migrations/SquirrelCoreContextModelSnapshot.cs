﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Squirrel.Core.DAL.Context;

#nullable disable

namespace Squirrel.Core.DAL.Migrations
{
    [DbContext(typeof(SquirrelCoreContext))]
    partial class SquirrelCoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.RefreshToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Sample", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Samples");

                    b.HasData(
                        new
                        {
                            Id = 2L,
                            Body = "Eligendi quisquam ullam iure praesentium numquam sapiente distinctio ad. Tempore voluptatibus ad et adipisci hic amet. Corporis soluta cupiditate soluta. Provident rerum nemo dolores debitis dicta voluptatem labore dolores adipisci. Adipisci illo quidem sit dolores. Ea dolor animi quod laborum quia perspiciatis sunt tempora.",
                            CreatedAt = new DateTime(2020, 7, 1, 1, 14, 20, 556, DateTimeKind.Unspecified).AddTicks(7372),
                            CreatedBy = 5L,
                            Title = "hic"
                        },
                        new
                        {
                            Id = 3L,
                            Body = "Incidunt perferendis omnis. Quas voluptatem beatae vitae sunt a ut sed repellendus. Accusamus eos enim consequatur et praesentium ad ut beatae eius. Omnis voluptas error et velit autem ipsa atque consequuntur vitae. Nostrum accusamus soluta nisi.",
                            CreatedAt = new DateTime(2020, 11, 26, 1, 10, 54, 982, DateTimeKind.Unspecified).AddTicks(9175),
                            CreatedBy = 4L,
                            Title = "velit"
                        },
                        new
                        {
                            Id = 4L,
                            Body = "Architecto laboriosam culpa cumque dicta in. Perspiciatis amet autem rerum recusandae perspiciatis pariatur. Eum sint molestiae quis neque tempora ab distinctio. Nobis nulla dignissimos voluptas nemo cumque tenetur quod et placeat. Nihil sit eos similique fuga enim dolores ullam suscipit.",
                            CreatedAt = new DateTime(2021, 1, 18, 12, 14, 38, 642, DateTimeKind.Unspecified).AddTicks(7703),
                            CreatedBy = 1L,
                            Title = "est"
                        },
                        new
                        {
                            Id = 5L,
                            Body = "Sapiente et saepe ut atque dolore accusantium soluta cumque perferendis. Magni adipisci labore corrupti. Ratione et quibusdam consequatur voluptatem velit expedita eos maxime.",
                            CreatedAt = new DateTime(2020, 2, 2, 15, 3, 56, 551, DateTimeKind.Unspecified).AddTicks(1864),
                            CreatedBy = 5L,
                            Title = "placeat"
                        },
                        new
                        {
                            Id = 6L,
                            Body = "Iusto aspernatur nihil iure ut blanditiis veritatis quas. Et illum quod atque nulla voluptas quos beatae quaerat consequatur. Ab placeat tenetur perferendis et omnis. Doloremque corrupti deserunt sint enim ex sit.",
                            CreatedAt = new DateTime(2021, 4, 7, 16, 50, 6, 239, DateTimeKind.Unspecified).AddTicks(5929),
                            CreatedBy = 3L,
                            Title = "facere"
                        },
                        new
                        {
                            Id = 7L,
                            Body = "Doloremque omnis facilis unde exercitationem consectetur culpa porro consequatur sed. Vel rem rerum eum harum. Ratione voluptate est officia accusamus doloremque perferendis ea. Unde iure laudantium ut amet repellendus enim consequatur dolor porro. Sed expedita dolorem aperiam ipsa omnis. Ut omnis ipsa quia cupiditate iure.",
                            CreatedAt = new DateTime(2019, 7, 23, 7, 33, 40, 245, DateTimeKind.Unspecified).AddTicks(9313),
                            CreatedBy = 5L,
                            Title = "impedit"
                        },
                        new
                        {
                            Id = 8L,
                            Body = "Nesciunt placeat et consectetur enim. Consectetur magnam perspiciatis ut rem perspiciatis odit dolorem. Modi corrupti corrupti.",
                            CreatedAt = new DateTime(2020, 1, 27, 9, 1, 30, 801, DateTimeKind.Unspecified).AddTicks(8159),
                            CreatedBy = 3L,
                            Title = "corporis"
                        },
                        new
                        {
                            Id = 9L,
                            Body = "Omnis culpa earum modi eos beatae autem. Deleniti labore veritatis dolorum. Omnis perferendis ut sit nulla autem ut voluptatem voluptas ut.",
                            CreatedAt = new DateTime(2021, 3, 25, 21, 11, 5, 602, DateTimeKind.Unspecified).AddTicks(6614),
                            CreatedBy = 5L,
                            Title = "perspiciatis"
                        },
                        new
                        {
                            Id = 10L,
                            Body = "Molestias porro exercitationem omnis et eius. Est consequatur esse sit quia dolorem sequi doloribus corporis. Perspiciatis qui dignissimos.",
                            CreatedAt = new DateTime(2021, 4, 7, 22, 46, 32, 439, DateTimeKind.Unspecified).AddTicks(5958),
                            CreatedBy = 3L,
                            Title = "esse"
                        },
                        new
                        {
                            Id = 11L,
                            Body = "Eos eum perferendis nisi alias et ducimus repudiandae ut. Voluptas rerum ullam omnis placeat non ea voluptatibus. Sint et et asperiores omnis recusandae saepe laborum enim. Non consequatur voluptatem in aut quia quo quo. Commodi aliquid aut quaerat adipisci. Modi ea maxime doloribus qui sint.",
                            CreatedAt = new DateTime(2021, 3, 24, 14, 25, 37, 776, DateTimeKind.Unspecified).AddTicks(56),
                            CreatedBy = 1L,
                            Title = "in"
                        });
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
