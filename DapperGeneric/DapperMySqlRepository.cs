using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DapperGeneric
{
    public class DapperMySqlRepository<T> : IRepository<T>
    {
        private readonly string _connectionString;

        public DapperMySqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                return await dbConnection.QueryAsync<T>($"SELECT * FROM {typeof(T).Name}");
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
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

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                await dbConnection.ExecuteAsync($"INSERT INTO {typeof(T).Name} ({columns}) VALUES ({parameters})", entity);
            }
        }

        public async Task UpdateAsync(T entity, string idColumnName = "Id")
        {
            // Get the properties of the entity type
            var properties = typeof(T).GetProperties();

            // Build the SET part of the SQL query
            var setClause = string.Join(", ", properties.Where(p => p.Name != idColumnName).Select(p => $"{p.Name} = @{p.Name}"));

            // Build the WHERE part of the SQL query
            var whereClause = $"{idColumnName} = @{idColumnName}";

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                await dbConnection.ExecuteAsync($"UPDATE {{typeof(T).Name}} SET {{setClause}} WHERE {{whereClause}}", entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                //await dbConnection.OpenAsync();
                var parameters = new { Id = id };
                await dbConnection.ExecuteAsync($"DELETE FROM {typeof(T).Name} WHERE Id = @Id", parameters);
            }
        }
    }
}