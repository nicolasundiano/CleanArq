using CleanArq.Application.Common.Specifications;
using CleanArq.Domain.Entities.User;

namespace CleanArq.Application.Features.UserFeatures.Common;

public class UserWithAddressSpecification : BaseSpecification<User>
{
    public UserWithAddressSpecification(string email) : base(x => x.Email.Equals(email))
    {
        AddInclude(x => x.Address!);
    }
}
