namespace Data.Interfaces;
public interface IGenericRepository<T>  where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAllWithSpecAsync(ISpecification<T> spec);
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
    Task<IReadOnlyList<TResult>> ListAllWithSpecAsync<TResult>(ISpecification<T, TResult> spec);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveAllAsync();
    Task<int> CountAsync(ISpecification<T> spec);
}
