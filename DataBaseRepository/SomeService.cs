namespace DataBaseRepository
{
    public class SomeService
    {
        //private readonly IRepository<SomeEntity> _repository;
        //public SomeService(IRepository<SomeEntity> repository)
        //{
        //    _repository = repository;
        //}
        // Use _repository to perform data operations
        private static string GenerateInsertQuery<T>()
        {
            // Get the properties of the class
            var properties = typeof(T).GetProperties();

            // Generate the INSERT statement with named parameters
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string parameters = string.Join(", ", properties.Select(p => "@" + p.Name));

            return $"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({parameters})";
        }
    }
}