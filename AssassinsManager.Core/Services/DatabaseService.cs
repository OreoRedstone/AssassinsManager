using System;
using AssassinsManager.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AssassinsManager.Core.Services;

public class DatabaseService
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
                MigrateDatabase();
                dbStarted = true;
                Console.WriteLine("Successfully connected to database.");
            }
            catch (MySqlException)
            {
                Console.WriteLine("Database not started yet, retrying...");
            }
        }
    }

    public void MigrateDatabase()
    {
        using var context = GetContext();

        context.Database.Migrate();
    }

    public AssassinContext GetContext()
        => new(new DbContextOptionsBuilder<AssassinContext>()
                .UseMySQL(connectionString)
                .Options);
}
