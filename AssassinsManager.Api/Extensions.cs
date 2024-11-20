using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.AspNetCore;


namespace AssassinsManager.Api;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureQuartz(this WebApplicationBuilder builder)
    {
        builder.Services.AddQuartz(q =>
        {
            q.UsePersistentStore(store =>
            {
                store.UseMySql(builder.Configuration.GetConnectionString("MySqlQuartz"));
                store.UseNewtonsoftJsonSerializer();
            });
        }).AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        return builder;
    }

    public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
    {
        using var context = host.GetScopedService<T>();
        context.Database.Migrate();

        return host;
    }

    public static T GetScopedService<T>(this IHost host) where T : notnull
    {
        var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        return services.GetRequiredService<T>();
    }
}