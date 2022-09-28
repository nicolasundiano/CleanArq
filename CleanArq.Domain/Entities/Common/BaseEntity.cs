namespace CleanArq.Domain.Entities.Common;

public abstract class BaseEntity
{
    public virtual int Id { get; protected set; }
    public string CreatedBy { get; set; } = null!;
    public DateTimeOffset CreatedAt { get; set; }
    public string ModifiedBy { get; set; } = null!;
    public DateTimeOffset ModifiedAt { get; set; }
}
