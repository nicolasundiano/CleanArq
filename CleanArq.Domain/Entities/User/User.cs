using CleanArq.Domain.Entities.Common;

namespace CleanArq.Domain.Entities.User;

public class User : BaseEntity, IAggregateRoot
{
    public User(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public Address? Address { get; private set; } = null!;

    public void SetAddress(string? street, string? city, string? country)
    {
        Address = new Address(street, city, country);
    }
}
