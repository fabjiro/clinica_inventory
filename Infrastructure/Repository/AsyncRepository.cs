using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Interface;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class AsyncRepository<T> : RepositoryBase<T>, IAsyncRepository<T> where T : class
{
	private readonly AppDbContext _dbContext;

	public AsyncRepository(AppDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<bool> AnyAsync(int id, CancellationToken cancellationToken = default)
	{
		return await _dbContext.Set<T>().AnyAsync(cancellationToken);
	}

	public IQueryable<T> ApplySpecification(ISpecification<T> specification)
	{
		return SpecificationEvaluator.Default.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
	}

	public void Attach(object entity)
	{
		_dbContext.Attach(entity);
	}

    public async Task<IReadOnlyList<T>> ListNoTrackingAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).AsNoTracking().ToListAsync();
    }

    public void Clear()
	{
		_dbContext.ChangeTracker.Clear();
	}
}
