using Squirrel.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Squirrel.Core.DAL.Context;

public class SquirrelCoreContext : DbContext
{
    public DbSet<Sample> Samples => Set<Sample>();

    public SquirrelCoreContext(DbContextOptions<SquirrelCoreContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Configure();
        modelBuilder.Seed();
    }
}