namespace CleanArq.Application.Features.Authentication.Common;

public record AuthenticationResult(
    UserDto User,
    string Token);