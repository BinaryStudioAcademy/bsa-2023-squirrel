using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;
using Squirrel.Core.DAL.Entities.JoinEntities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public sealed class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        builder.Property(x => x.DbEngine).IsRequired();
        builder.Property(x => x.DefaultBranchId).IsRequired();
        
        builder.HasMany(x => x.Branches)
               .WithOne(x => x.Project)
               .HasForeignKey(x => x.ProjectId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(x => x.PullRequests)
               .WithOne(x => x.Project)
               .HasForeignKey(x => x.ProjectId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Tags)
               .WithMany(x => x.Projects)
               .UsingEntity<ProjectTag>(
                   l => l.HasOne(x => x.Tag)
                         .WithMany(x => x.ProjectTags)
                         .HasForeignKey(x => x.TagId),
                   r => r.HasOne(x => x.Project)
                         .WithMany(x => x.ProjectTags)
                         .HasForeignKey(x => x.ProjectId));
    }
}