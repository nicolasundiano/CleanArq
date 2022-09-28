using CleanArq.Domain.Entities.Common;

namespace CleanArq.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity, IAggregateRoot;
    Task<int> CompleteAsync();
}
