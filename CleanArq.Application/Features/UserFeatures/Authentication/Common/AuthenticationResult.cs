using CleanArq.Application.Features.UserFeatures.Common.Dtos;

namespace CleanArq.Application.Features.UserFeatures.Authentication.Common;

public record AuthenticationResult(
    UserDto User,
    string Token);