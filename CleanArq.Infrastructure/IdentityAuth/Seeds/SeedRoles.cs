using CleanArq.Domain.Common.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CleanArq.Infrastructure.IdentityAuth.Seeds;

public class SeedRoles
{
    public static async Task SeedRolesAsync(IdentityAuthDbContext identityDbContext, 
        RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<IdentityAuthDbContext>();
            logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
