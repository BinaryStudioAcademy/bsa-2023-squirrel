using Microsoft.EntityFrameworkCore;
using Squirrel.Core.DAL.Context.EntityConfigurations;

namespace Squirrel.Core.DAL.Context;

public static class ModelBuilderExtensions
{
    public static void Configure(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
    }
}