using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CleanArq.Domain.Entities.User;

namespace CleanArq.Infrastructure.Persistence;

public class AppDbContext : BaseDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options, httpContextAccessor)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
}
