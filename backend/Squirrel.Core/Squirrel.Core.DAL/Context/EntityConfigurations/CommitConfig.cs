using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public class CommitConfig : IEntityTypeConfiguration<Commit>
{
    public void Configure(EntityTypeBuilder<Commit> builder)
    {
        builder.Property(x => x.Message).IsRequired().HasMaxLength(200);
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();

        builder.HasMany(x => x.CommitFiles)
               .WithOne(x => x.Commit)
               .HasForeignKey(x => x.CommitId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.CommitParents)
               .WithOne(x => x.Commit)
               .HasForeignKey(x => x.CommitId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}