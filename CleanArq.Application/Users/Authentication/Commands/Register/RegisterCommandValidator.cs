using CleanArq.Application.Features.UserFeatures.Authentication.Commands.Register;
using FluentValidation;

namespace CleanArq.Application.Users.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().DependentRules(() =>
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(256);
        });

        RuleFor(x => x.LastName).NotEmpty().DependentRules(() =>
        {
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(256);
        });

        RuleFor(x => x.Email).NotEmpty().DependentRules(() =>
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(320);
        });

        RuleFor(x => x.Password).NotEmpty().DependentRules(() =>
        {
            RuleFor(x => x.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$")
            .WithMessage("Password length must be between 8-20 containing 1 lowercase letter, 1 uppercase letter, 1 number and a special character");
        });
        
    }
}