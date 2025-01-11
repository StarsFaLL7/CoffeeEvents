using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureStartup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContextFactory<ApplicationDbContext>();
        return services;
    }

    public static void CheckAndMigrateDatabase(IServiceScope scope)
    {
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}