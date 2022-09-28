using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CleanArq.Domain.Entities;
using Microsoft.AspNetCore.Http;
using CleanArq.Domain.Entities.Common;

namespace CleanArq.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<User> Users { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added
                    || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                ((BaseEntity)entityEntry.Entity).CreatedBy = this._httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
            }
            else
            {
                Entry((BaseEntity)entityEntry.Entity).Property(p => p.CreatedAt).IsModified = false;
                Entry((BaseEntity)entityEntry.Entity).Property(p => p.CreatedBy).IsModified = false;
            }

            ((BaseEntity)entityEntry.Entity).ModifiedAt = DateTime.UtcNow;
            ((BaseEntity)entityEntry.Entity).ModifiedBy = this._httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "MyApp";
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
