namespace DataBaseRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Define common CRUD operations
        void Add(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity entity);
        void Delete(int id);
    }

}