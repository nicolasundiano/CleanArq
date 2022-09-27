using CleanArq.Domain.Entities;
using CleanArq.SharedKernel.Base;

namespace CleanArq.Application.Features.Authentication.Common;

public class UserSpecification : BaseSpecification<User>
{
	public UserSpecification(string email) : base(x => x.Email.Equals(email))
 	{
	}
}
