using CleanArq.Application.Common.Specifications;
using CleanArq.Domain.Entities.User;

namespace CleanArq.Application.Features.Authentication.Common;

public class UserSpecification : BaseSpecification<User>
{
	public UserSpecification(string email) : base(x => x.Email.Equals(email))
 	{
		AddInclude(x => x.Address);
	}
}
