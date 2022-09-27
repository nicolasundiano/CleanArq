using CleanArq.SharedKernel.Base;
using CleanArq.SharedKernel.Interfaces;

namespace CleanArq.Domain.Entities;

public class User : BaseEntity, IAggregateRoot
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
