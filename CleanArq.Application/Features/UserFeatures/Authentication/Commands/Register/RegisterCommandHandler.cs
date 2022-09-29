﻿using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using CleanArq.Application.Common.Interfaces.Authentication;
using CleanArq.Application.Common.Interfaces.Persistence;
using CleanArq.Domain.Entities.User;
using CleanArq.Application.Features.UserFeatures.Authentication.Common;
using CleanArq.Application.Features.UserFeatures.Common.Specifications;
using CleanArq.Application.Features.UserFeatures.Common.Dtos;

namespace CleanArq.Application.Features.UserFeatures.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IAuthRepository authRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _authRepository = authRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var spec = new UserSpecification(command.Email);

        if (await _unitOfWork.Repository<User>().GetAsync(spec) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        if (await _authRepository.UserExists(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User(command.FirstName, command.LastName, command.Email);

        _unitOfWork.Repository<User>().Add(user);
        await _unitOfWork.CompleteAsync();

        await _authRepository.CreateUserAsync(user, command.Password);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            },
            token
            );
    }
}
