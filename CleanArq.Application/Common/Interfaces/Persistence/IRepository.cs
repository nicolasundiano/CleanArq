using CleanArq.Application.Common.Specifications;
using CleanArq.Domain.Entities.Common;

namespace CleanArq.Application.Common.Interfaces.Persistence;

public interface IRepository<T> where T : BaseEntity, IAggregateRoot
{
    Task<T?> GetByIdAsync(int id);
    Task<T?> GetAsync(ISpecification<T> spec);
    Task<List<T>> ListAsync();
    Task<List<T>> ListAsync(ISpecification<T> spec);
    Task<int> CountAsync();
    Task<int> CountAsync(ISpecification<T> spec);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
