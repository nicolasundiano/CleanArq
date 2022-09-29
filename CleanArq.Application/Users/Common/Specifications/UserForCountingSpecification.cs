using CleanArq.Application.Common.Specifications;
using CleanArq.Domain.Entities.User;

namespace CleanArq.Application.Users.Common.Specifications;

public class UserForCountingSpecification : BaseSpecification<User>
{
	public UserForCountingSpecification(UserListPagedParams @params) : base(x =>
            string.IsNullOrEmpty(@params.Search) || x.FirstName.ToLower().Contains(@params.Search)
    )
    {
	}
}
