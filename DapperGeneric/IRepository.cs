namespace DapperGeneric
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity, string idColumnName);
        Task DeleteAsync(int id);
    }
}