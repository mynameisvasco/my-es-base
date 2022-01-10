using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MyEsBase.Infrastructure.EventStore;

namespace MyEsBase.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<StoredEvent> StoredEvents => Set<StoredEvent>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}