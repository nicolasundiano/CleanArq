using FluentValidation;

namespace CleanArq.Application.Users.Commands.UpsertAddress;

public class UpsertAddressCommandValidator : AbstractValidator<UpsertAddressCommand>
{
	public UpsertAddressCommandValidator()
	{
		RuleFor(x => x.Street).MaximumLength(256);
        RuleFor(x => x.City).MaximumLength(256);
        RuleFor(x => x.Country).MaximumLength(256);
    }
}
