using CleanArq.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArq.Infrastructure.Persistence;

public class BaseDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

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
