using System.Linq.Expressions;

namespace CleanArq.Application.Common.Specifications;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    { 
    }

    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public int Skip { get; private set; }

    public int Take { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void AddInclude(Expression<Func<T, object>> include)
    {
        Includes.Add(include);
    }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDesExpression)
    {
        OrderByDescending = orderByDesExpression;
    }

    protected void ApplyPagination(int pageSize, int pageIndex)
    {
        Skip = pageSize * (pageIndex - 1);
        Take = pageSize;
        IsPagingEnabled = true;
    }
}
