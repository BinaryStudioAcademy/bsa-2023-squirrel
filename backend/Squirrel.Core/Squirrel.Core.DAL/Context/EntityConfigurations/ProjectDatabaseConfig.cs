using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public sealed class ProjectDatabaseConfig : IEntityTypeConfiguration<ProjectDatabase>
{
    public void Configure(EntityTypeBuilder<ProjectDatabase> builder)
    {
        builder.Property(x => x.DbName).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Guid).IsRequired();

        builder.HasAlternateKey(x => x.Guid);
        
        builder.HasOne(x => x.Project)
               .WithMany(x => x.ProjectDatabases)
               .HasForeignKey(x => x.ProjectId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}