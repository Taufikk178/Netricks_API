using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DapperGeneric
{
    public class DapperRepository<T> : IRepository<T>
    {
        private readonly string _connectionString;

        public DapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync<T>($"SELECT * FROM {typeof(T).Name}");
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                var parameters = new { Id = id };
                return await dbConnection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {typeof(T).Name} WHERE Id = @Id", parameters);
            }
        }

        public async Task InsertAsync(T entity)
        {
            // Get the properties of the class
            var properties = typeof(T).GetProperties();

            // Generate the INSERT statement with named parameters
            string columns = string.Join(", ", properties.Select(p => p.Name));
            string parameters = string.Join(", ", properties.Select(p => "@" + p.Name));

            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                await dbConnection.ExecuteAsync($"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({parameters})");
            }
        }

        public async Task UpdateAsync(T entity, string idColumnName = "Id")
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                await dbConnection.ExecuteAsync($"UPDATE {typeof(T).Name} SET PropertyName1 = @PropertyName1, PropertyName2 = @PropertyName2, ... WHERE Id = @Id", entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                var parameters = new { Id = id };
                await dbConnection.ExecuteAsync($"DELETE FROM {typeof(T).Name} WHERE Id = @Id", parameters);
            }
        }
    }
}