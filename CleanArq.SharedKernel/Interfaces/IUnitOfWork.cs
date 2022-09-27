using CleanArq.SharedKernel.Base;

namespace CleanArq.SharedKernel.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity, IAggregateRoot;
    Task<int> CompleteAsync();
}
