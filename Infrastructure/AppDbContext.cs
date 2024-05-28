using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> User { get; set; }

    public override int SaveChanges()

    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaving()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker.Entries();
        DateTime utcNow = DateTime.UtcNow;

        foreach (EntityEntry entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    // Set UpdatedDate to current date/time for updated entities
                    entry.Property("UpdatedDate").CurrentValue = utcNow;
                    break;
                case EntityState.Added:
                    // Set CreatedDate and UpdatedDate to current date/time for new entities
                    entry.Property("CreatedDate").CurrentValue = utcNow;
                    entry.Property("UpdatedDate").CurrentValue = utcNow;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=aichatbots_db;Username=postgres;Password=tu20021998");

        return new AppDbContext(optionsBuilder.Options);
    }
}