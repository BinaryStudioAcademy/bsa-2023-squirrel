using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public sealed class DbIdentityConfig : IEntityTypeConfiguration<DbIdentity>
{
    public void Configure(EntityTypeBuilder<DbIdentity> builder)
    {
        builder.Property(x => x.DbName).IsRequired().HasMaxLength(100); // Clarify max length.
        builder.Property(x => x.Guid).IsRequired();
        builder.Property(x => x.DbEngine).IsRequired();

        builder.HasAlternateKey(x => x.Guid);

        builder.HasOne(x => x.Project)
               .WithMany(x => x.DbIdentities)
               .HasForeignKey(x => x.ProjectId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}