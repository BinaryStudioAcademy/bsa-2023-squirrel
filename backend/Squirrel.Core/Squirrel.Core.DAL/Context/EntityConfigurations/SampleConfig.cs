using Squirrel.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Squirrel.Core.DAL.Context
{
    public class SampleConfig : IEntityTypeConfiguration<Sample>
    {
        public void Configure(EntityTypeBuilder<Sample> builder)
        {
            builder.Property(e => e.Title)
             .IsRequired()
             .HasMaxLength(50);

        }
    }
}
