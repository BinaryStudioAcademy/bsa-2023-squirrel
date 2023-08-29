using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public sealed class BranchConfig : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.ProjectId).IsRequired();
        
        builder.HasOne(x => x.ProjectForDefaultBranch)
               .WithOne(x => x.DefaultBranch)
               .HasForeignKey<Project>(x => x.DefaultBranchId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.PullRequestsFromThisBranch)
               .WithOne(x => x.SourceBranch)
               .HasForeignKey(x => x.SourceBranchId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.PullRequestsIntoThisBranch)
               .WithOne(x => x.TargetBranch)
               .HasForeignKey(x => x.TargetBranchId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Commits)
               .WithMany(x => x.Branches)
               .UsingEntity<BranchCommit>(
                   l => l.HasOne(x => x.Commit)
                         .WithMany(x => x.BranchCommits)
                         .HasForeignKey(x => x.CommitId),
                   r => r.HasOne(x => x.Branch)
                         .WithMany(x => x.BranchCommits)
                         .HasForeignKey(x => x.BranchId));
    }
}