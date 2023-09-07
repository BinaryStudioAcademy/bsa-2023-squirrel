﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Squirrel.Core.DAL.Context;

#nullable disable

namespace Squirrel.Core.DAL.Migrations
{
    [DbContext(typeof(SquirrelCoreContext))]
    [Migration("20230903135649_UserNameIsNotUnique")]
    partial class UserNameIsNotUnique
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CommentedEntity")
                        .HasColumnType("int");

                    b.Property<int>("CommentedEntityId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Commit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Commits");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.CommitFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BlobId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CommitId")
                        .HasColumnType("int");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommitId");

                    b.ToTable("CommitFiles");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.CommitParent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CommitId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CommitId");

                    b.HasIndex("ParentId");

                    b.ToTable("CommitParents");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.JoinEntities.BranchCommit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("CommitId")
                        .HasColumnType("int");

                    b.Property<bool>("IsHead")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMerged")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CommitId");

                    b.ToTable("BranchCommits");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.JoinEntities.ProjectTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TagId");

                    b.ToTable("ProjectTags");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.JoinEntities.PullRequestReviewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("PullRequestId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PullRequestId");

                    b.HasIndex("UserId");

                    b.ToTable("PullRequestReviewers");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.JoinEntities.UserProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("UserProjects");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DbEngine")
                        .HasColumnType("int");

                    b.Property<int>("DefaultBranchId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DefaultBranchId")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.PullRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<bool>("IsReviewed")
                        .HasColumnType("bit");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("SourceBranchId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TargetBranchId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SourceBranchId");

                    b.HasIndex("TargetBranchId");

                    b.ToTable("PullRequests");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailNotification")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<bool>("IsGoogleAuth")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Salt")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("SquirrelNotification")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Branch", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.Project", "Project")
                        .WithMany("Branches")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Comment", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Commit", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.User", "Author")
                        .WithMany("Commits")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.CommitFile", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.Commit", "Commit")
                        .WithMany("CommitFiles")
                        .HasForeignKey("CommitId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Commit");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.CommitParent", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.Commit", "Commit")
                        .WithMany("CommitsAsChild")
                        .HasForeignKey("CommitId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Squirrel.Core.DAL.Entities.Commit", "ParentCommit")
                        .WithMany("CommitsAsParent")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Commit");

                    b.Navigation("ParentCommit");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.JoinEntities.BranchCommit", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.Branch", "Branch")
                        .WithMany("BranchCommits")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Squirrel.Core.DAL.Entities.Commit", "Commit")
                        .WithMany("BranchCommits")
                        .HasForeignKey("CommitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Commit");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.JoinEntities.ProjectTag", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.Project", "Project")
                        .WithMany("ProjectTags")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Squirrel.Core.DAL.Entities.Tag", "Tag")
                        .WithMany("ProjectTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.JoinEntities.PullRequestReviewer", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.PullRequest", "PullRequest")
                        .WithMany("PullRequestReviewers")
                        .HasForeignKey("PullRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Squirrel.Core.DAL.Entities.User", "User")
                        .WithMany("PullRequestReviewers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PullRequest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.JoinEntities.UserProject", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.Project", "Project")
                        .WithMany("UserProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Squirrel.Core.DAL.Entities.User", "User")
                        .WithMany("UserProjects")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Project", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.Branch", "DefaultBranch")
                        .WithOne("ProjectForDefaultBranch")
                        .HasForeignKey("Squirrel.Core.DAL.Entities.Project", "DefaultBranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("DefaultBranch");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.PullRequest", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.User", "Author")
                        .WithMany("PullRequests")
                        .HasForeignKey("CreatedBy")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Squirrel.Core.DAL.Entities.Project", "Project")
                        .WithMany("PullRequests")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Squirrel.Core.DAL.Entities.Branch", "SourceBranch")
                        .WithMany("PullRequestsFromThisBranch")
                        .HasForeignKey("SourceBranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Squirrel.Core.DAL.Entities.Branch", "TargetBranch")
                        .WithMany("PullRequestsIntoThisBranch")
                        .HasForeignKey("TargetBranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Project");

                    b.Navigation("SourceBranch");

                    b.Navigation("TargetBranch");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.RefreshToken", b =>
                {
                    b.HasOne("Squirrel.Core.DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Branch", b =>
                {
                    b.Navigation("BranchCommits");

                    b.Navigation("ProjectForDefaultBranch");

                    b.Navigation("PullRequestsFromThisBranch");

                    b.Navigation("PullRequestsIntoThisBranch");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Commit", b =>
                {
                    b.Navigation("BranchCommits");

                    b.Navigation("CommitFiles");

                    b.Navigation("CommitsAsChild");

                    b.Navigation("CommitsAsParent");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Project", b =>
                {
                    b.Navigation("Branches");

                    b.Navigation("ProjectTags");

                    b.Navigation("PullRequests");

                    b.Navigation("UserProjects");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.PullRequest", b =>
                {
                    b.Navigation("PullRequestReviewers");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.Tag", b =>
                {
                    b.Navigation("ProjectTags");
                });

            modelBuilder.Entity("Squirrel.Core.DAL.Entities.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Commits");

                    b.Navigation("PullRequestReviewers");

                    b.Navigation("PullRequests");

                    b.Navigation("UserProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
