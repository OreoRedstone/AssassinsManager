using AssassinsManager.Core.Services.Interfaces;
using AssassinsManager.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AssassinsManager.Core.Services;

public class DatabaseService : IDatabaseService
{
    private readonly string connectionString;

    public DatabaseService(IConfiguration config)
    {
        connectionString = config.GetConnectionString("MySql") ?? "";
        var dbStarted = false;
        while (!dbStarted)
        {
            try
            {
                InitialConfigure();
                dbStarted = true;
                Console.WriteLine("Successfully connected to database.");
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database not started yet, retrying...");
            }
        }
    }

    private void InitialConfigure()
    {
        using var context = GetContext();

        context.Database.Migrate();
    }

    private AssassinCoreContext GetContext()
        => new(new DbContextOptionsBuilder<AssassinCoreContext>()
                .UseMySQL(connectionString)
                .Options);
}
