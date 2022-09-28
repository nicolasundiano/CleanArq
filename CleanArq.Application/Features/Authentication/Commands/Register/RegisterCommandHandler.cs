﻿using CleanArq.Application.Features.Authentication.Common;
using CleanArq.Domain.Entities;
using CleanArq.SharedKernel.Interfaces;
using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using CleanArq.Application.Common.Interfaces.Authentication;

namespace CleanArq.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
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

        if (await _unitOfWork.Repository<User>().GetBySpecAsync(spec) is not null || await _authRepository.UserExists(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
        };

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