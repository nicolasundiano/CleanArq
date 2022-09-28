using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArq.Infrastructure.IdentityAuth;

public class IdentityAuthDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityAuthDbContext(DbContextOptions<IdentityAuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
