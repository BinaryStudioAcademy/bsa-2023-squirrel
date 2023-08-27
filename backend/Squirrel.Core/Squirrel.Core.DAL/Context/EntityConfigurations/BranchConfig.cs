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

        builder.HasMany(x => x.CommitParents)
               .WithMany(x => x.Branches)
               .UsingEntity<BranchCommit>(
                   l => l.HasOne(x => x.CommitParent)
                         .WithMany(x => x.BranchCommits)
                         .HasForeignKey(x => x.CommitParentId),
                   r => r.HasOne(x => x.Branch)
                         .WithMany(x => x.BranchCommits)
                         .HasForeignKey(x => x.BranchId));
    }
}