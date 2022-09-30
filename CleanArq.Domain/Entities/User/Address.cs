using CleanArq.Domain.Entities.Common;

namespace CleanArq.Domain.Entities.User;

public class Address : BaseEntity
{
    public Address(string? street, string? city, string? country)
    {
        Street = street;
        City = city;
        Country = country;
    }

    public string? Street { get; private set; }
    public string? City { get; private set; }
    public string? Country { get; private set; }

    public int UserId { get; private set; }

    public void Update(string? street, string? city, string? country)
    {
        Street = street;
        City = city;
        Country = country;
    }
}
