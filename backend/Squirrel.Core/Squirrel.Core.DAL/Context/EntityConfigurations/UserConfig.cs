using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(x => x.Email);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Username).IsUnique();
        builder.Property(x => x.Username).IsRequired().HasMaxLength(25);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(25);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(25);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
        builder.Property(x => x.PasswordHash).HasMaxLength(100);
        builder.Property(x => x.Salt).HasMaxLength(100);
        builder.Property(x => x.AvatarUrl).HasMaxLength(500);
        builder.Property(x => x.IsGoogleAuth).IsRequired();

        builder.HasMany(x => x.OwnProjects)
               .WithOne(x => x.Author)
               .HasForeignKey(x => x.CreatedBy)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Commits)
               .WithOne(x => x.Author)
               .HasForeignKey(x => x.CreatedBy)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Comments)
               .WithOne(x => x.Author)
               .HasForeignKey(x => x.CreatedBy)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.PullRequests)
               .WithOne(x => x.Author)
               .HasForeignKey(x => x.CreatedBy)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.ReviewedRequests)
               .WithMany(x => x.Reviewers)
               .UsingEntity<PullRequestReviewer>(
                      l => l.HasOne(x => x.PullRequest)
                            .WithMany(x => x.PullRequestReviewers)
                            .HasForeignKey(x => x.PullRequestId),
                      r => r.HasOne(x => x.User)
                            .WithMany(x => x.PullRequestReviewers)
                            .HasForeignKey(x => x.UserId));

        builder.HasMany(x => x.Projects)
               .WithMany(x => x.Users)
               .UsingEntity<UserProject>(
                      l => l.HasOne(x => x.Project)
                            .WithMany(x => x.UserProjects)
                            .HasForeignKey(x => x.ProjectId),
                      r => r.HasOne(x => x.User)
                            .WithMany(x => x.UserProjects)
                            .HasForeignKey(x => x.UserId));

        builder.HasMany(x => x.ChangeRecords)
              .WithOne(x => x.User)
              .HasForeignKey(x => x.CreatedBy)
              .IsRequired()
              .OnDelete(DeleteBehavior.NoAction);
    }
}