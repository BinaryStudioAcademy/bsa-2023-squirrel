using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(x => x.Username);
        builder.HasAlternateKey(x => x.Email);
        builder.Property(x => x.Username)
               .IsRequired()
               .HasMaxLength(25);
        builder.Property(x => x.FirstName)
               .IsRequired()
               .HasMaxLength(25);
        builder.Property(x => x.LastName)
               .IsRequired()
               .HasMaxLength(25);
        builder.Property(x => x.Password)
               .IsRequired()
               .HasMaxLength(100);
        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(50);
        builder.Property(x => x.Salt)
               .IsRequired()
               .HasMaxLength(100);
    }
}