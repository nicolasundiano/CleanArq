using CleanArq.Application.Features.UserFeatures.Common;

namespace CleanArq.Application.Features.UserFeatures.Authentication.Common;

public record AuthenticationResult(
    UserDto User,
    string Token);