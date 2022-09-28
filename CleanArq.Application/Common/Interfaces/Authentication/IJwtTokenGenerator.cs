using CleanArq.Domain.Entities.User;

namespace CleanArq.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}