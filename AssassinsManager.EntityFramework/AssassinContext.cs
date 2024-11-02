using Microsoft.EntityFrameworkCore;

namespace AssassinsManager.EntityFramework;

public class AssassinContext(DbContextOptions<AssassinContext> options) : DbContext(options)
{
    public static AssassinContext FromMySqlConnectionString(string connectionString)
         => new (new DbContextOptionsBuilder<AssassinContext>()
                        .UseMySql(
                            connectionString,
                            new MySqlServerVersion(new Version(9, 1, 0))).Options);
}