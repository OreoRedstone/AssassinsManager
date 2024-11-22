using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AssassinsManager.EntityFramework;

public class AssassinCoreContext(DbContextOptions<AssassinCoreContext> options) : IdentityDbContext<IdentityUser, IdentityRole, string>(options)
{
    public DbSet<Blog> Blogs{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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
        var conString = "server=localhost;port=3306;user=root;password=RandomPassword;database=assassinsmanager";
        optionsBuilder.UseMySql(conString, ServerVersion.AutoDetect(conString));

        return new AssassinCoreContext(optionsBuilder.Options);
    }
}