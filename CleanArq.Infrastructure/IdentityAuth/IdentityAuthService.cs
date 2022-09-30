using CleanArq.Application.Common.Interfaces.Authentication;
using CleanArq.Domain.Common.Constants;
using CleanArq.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace CleanArq.Infrastructure.IdentityAuth;

public class IdentityAuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityAuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task CreateUserAsync(User user, string password)
    {
        var identityUser = new ApplicationUser
        {
            Email = user.Email,
            UserName = user.Email
        };

        await _userManager.CreateAsync(identityUser, password);
        await _userManager.AddToRoleAsync(identityUser, Roles.User);
    }

    public async Task<bool> UserExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }

    public async Task<bool> PasswordSignInAsync(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, true);

        return result.Succeeded;
    }

    public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
    {
        var identityUser = await _userManager.FindByEmailAsync(email);

        var result = await _userManager.ChangePasswordAsync(identityUser, currentPassword, newPassword);

        return result.Succeeded;
    }
}
