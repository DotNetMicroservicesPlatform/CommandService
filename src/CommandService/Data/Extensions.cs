using CommandService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data;

public static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IPlatformRepository, PlatformRepository>();
        services.AddScoped<ICommandRepository, CommandRepository>();
        return services;
    }

    public static IApplicationBuilder PrepPopulation(this IApplicationBuilder appBuilder, IWebHostEnvironment environment)
    {
        using (var serviceScpoe = appBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScpoe.ServiceProvider.GetService<AppDbContext>();
            var platformsGrpcClient = serviceScpoe.ServiceProvider.GetService<IPlatformDataClient>();            
            SeedData(context, platformsGrpcClient, environment);
        }
        return appBuilder;
    }

    private static void SeedData(AppDbContext context, IPlatformDataClient platformsGrpcClient, IWebHostEnvironment environment)
    {
        if (environment.IsProduction())
        {
            Console.WriteLine("--> Applying Migrations...");
            context.Database.Migrate();
        }

        var platforms = platformsGrpcClient.ReturnAllPlatforms();

        if (platforms == null)
        {
            return;
        }

        Console.WriteLine("--> Seeding new platforms...");

        foreach (var platform in platforms)
        {
            if (!(context.Platforms.Any(p=>p.ExternalPlatformId == platform.Id)))
            {
                context.Platforms.Add(platform);
            }
        }
        context.SaveChanges();

    }
}