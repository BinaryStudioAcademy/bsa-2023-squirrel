using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public class SampleConfig : IEntityTypeConfiguration<Sample>
{
    public void Configure(EntityTypeBuilder<Sample> builder)
    {
        builder.Property(e => e.Title)
               .IsRequired()
               .HasMaxLength(50);
    }
}