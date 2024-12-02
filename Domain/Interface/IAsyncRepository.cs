using Ardalis.Specification;

// namespace System.SharedKernel.Interfaces;
namespace Domain.Interface;

public interface IAsyncRepository<T> : IRepositoryBase<T> where T : class
{
	IQueryable<T> ApplySpecification(ISpecification<T> specification);

	public Task<bool> AnyAsync(int id, CancellationToken cancellationToken = default);

	public Task<IReadOnlyList<T>> ListNoTrackingAsync(ISpecification<T> spec);

	public void Attach(object entity);

	public void Clear();
}