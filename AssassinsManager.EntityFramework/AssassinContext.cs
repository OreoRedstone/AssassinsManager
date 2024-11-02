using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AssassinsManager.EntityFramework;

public class AssassinContext(DbContextOptions<AssassinContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
            throw new Exception("AssassinContext not properly configured.");
    }
}

public class AssassinContextFactory : IDesignTimeDbContextFactory<AssassinContext>
{
    public AssassinContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AssassinContext>();
        optionsBuilder.UseMySQL("server=assassinsmanager-db;user=root;password=RandomPassword;database=assassinsmanager");

        return new AssassinContext(optionsBuilder.Options);
    }
}