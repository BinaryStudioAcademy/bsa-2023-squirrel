using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Squirrel.Core.DAL.Entities;

namespace Squirrel.Core.DAL.Context.EntityConfigurations;

public class ChangeRecordConfig : IEntityTypeConfiguration<ChangeRecord>
{
    public void Configure(EntityTypeBuilder<ChangeRecord> builder)
    {
        builder.Property(x => x.UniqueChangeId).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasDefaultValueSql("getutcdate()")
               .ValueGeneratedOnAdd();

        builder.HasOne(x => x.User)
               .WithMany(x => x.ChangeRecords)
               .HasForeignKey(x => x.CreatedBy)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}
