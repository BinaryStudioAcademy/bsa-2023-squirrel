using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public sealed class PullRequestConfig : IEntityTypeConfiguration<PullRequest>
{
    public void Configure(EntityTypeBuilder<PullRequest> builder)
    {
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.SourceBranchId).IsRequired();
        builder.Property(x => x.TargetBranchId).IsRequired();
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.IsReviewed).IsRequired();
        builder.Property(x => x.UpdatedAt)
               .IsRequired()
               .HasDefaultValueSql("getutcdate()")
               .ValueGeneratedOnAddOrUpdate();
        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("getutcdate()")
               .ValueGeneratedOnAdd();
    }
}