using CleanArq.Domain.Entities.User;

namespace CleanArq.Application.Common.Interfaces.Authentication;

public interface IAuthRepository
{
    Task CreateUserAsync(User user, string password);
    Task<bool> UserExists(string email);
    Task<bool> PasswordSignInAsync(string email, string password);
}