using CleanArq.Application.Common.Interfaces.Persistence;
using CleanArq.Application.Common.Specifications;
using CleanArq.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArq.Infrastructure.Persistence;

public class EfRepository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
{
    private readonly AppDbContext _dbContext;

    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T?> GetAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<List<T>> ListAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<List<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _dbContext.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).CountAsync();
    }

    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
    }

    private IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
            inputQuery = inputQuery.Where(spec.Criteria);

        if (spec.OrderBy != null)
            inputQuery = inputQuery.OrderBy(spec.OrderBy);

        if (spec.OrderByDescending != null)
            inputQuery = inputQuery.OrderByDescending(spec.OrderByDescending);

        if (spec.IsPagingEnabled)
            inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);

        inputQuery = spec.Includes.Aggregate(inputQuery, (current, include) => current.Include(include));

        return inputQuery;
    }
}
