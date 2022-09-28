using CleanArq.Domain.Entities;

namespace CleanArq.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}