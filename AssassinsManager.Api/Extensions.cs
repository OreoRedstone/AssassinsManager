using Microsoft.EntityFrameworkCore;
using MySqlConnector;
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
                store.UseMySqlConnector(config => 
                {
                    config.ConnectionString = builder.Configuration.GetConnectionString("MySqlQuartz");
                });
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
        bool connected = false;
        while(!connected)
        {
            try
            {
                using var context = host.GetScopedService<T>();
                context.Database.Migrate();
                Console.WriteLine("Successfully connected to database.");
                connected = true;
            }
            catch (MySqlException)
            { 
                Console.WriteLine("Database not ready, retrying...");
            }
        }

        return host;
    }

    public static T GetScopedService<T>(this IHost host) where T : notnull
    {
        var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        return services.GetRequiredService<T>();
    }
}