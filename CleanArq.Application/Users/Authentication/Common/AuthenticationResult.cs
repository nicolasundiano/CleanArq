using CleanArq.Application.Users.Common.Dtos;

namespace CleanArq.Application.Users.Authentication.Common;

public record AuthenticationResult(
    UserDto User,
    string Token);