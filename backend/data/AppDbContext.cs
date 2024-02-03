using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Attractie> Attracties { get; set; }
    public DbSet<VirtualQueue> VirtualQueue { get; set; }
    public AppDbContext() { }

    // DI
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // voor migration checks
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(@"Data Source=app.db;foreign keys=true;");
        }
    }
}
