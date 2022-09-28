using CleanArq.SharedKernel.Base;
using CleanArq.SharedKernel.Interfaces;
using System.Collections;

namespace CleanArq.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork, IAggregateRoot
{
    private readonly AppDbContext _context;
    private Hashtable _repositories = null!;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity, IAggregateRoot
    {
        _repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(EfRepository<>);

            var repository = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

            _repositories.Add(type, repository);
        }

        return (IRepository<TEntity>)_repositories[type]!;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
