using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AssassinsManager.EntityFramework;

public class AssassinCoreContext(DbContextOptions<AssassinCoreContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("AssassinCore");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
            throw new Exception($"{typeof(AssassinCoreContext)} not properly configured.");
    }
}

public class AssassinContextFactory : IDesignTimeDbContextFactory<AssassinCoreContext>
{
    public AssassinCoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AssassinCoreContext>();
        optionsBuilder.UseMySQL("server=assassinsmanager-db;user=root;password=RandomPassword;database=assassinsmanager");

        return new AssassinCoreContext(optionsBuilder.Options);
    }
}