using CleanArq.Application.Common.Specifications;
using CleanArq.Domain.Entities.User;

namespace CleanArq.Application.Users.Common.Specifications;

public class UserSpecification : BaseSpecification<User>
{
    public UserSpecification(string email) : base(x => x.Email.Equals(email))
    {
    }

    public UserSpecification(UserListPaginatedParams @params) : base(x =>
            string.IsNullOrEmpty(@params.Search) || x.FirstName.ToLower().Contains(@params.Search)
    )
    {
        ApplyPagination(@params.PageSize, @params.PageIndex);

        switch (@params.Sort)
        {
            case "firstNameAsc":
                AddOrderBy(x => x.FirstName);
                break;
            case "firstNameDesc":
                AddOrderByDescending(x => x.FirstName);
                break;
            default:
                AddOrderBy(x => x.FirstName);
                break;
        }
    }
}
