using FluentValidation;

namespace CleanArq.Application.Users.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
	public ChangePasswordCommandValidator()
	{
        RuleFor(x => x.NewPassword).NotEmpty().DependentRules(() =>
        {
            RuleFor(x => x.NewPassword).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$")
            .WithMessage("Password length must be between 8-20 containing 1 lowercase letter, 1 uppercase letter, 1 number and a special character");
        });
    }
}
