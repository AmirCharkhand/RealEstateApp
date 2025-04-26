using RealEstateApp.Factories;
using SQLite;

namespace RealEstateApp.Services
{
    public class SqliteService<T> where T : class, new()
    {
        private SQLiteAsyncConnection? _connection;

        private async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            _connection ??= await SQLiteAsyncConnectionFactory<T>.CreateConnectionAsync();
            return _connection;
        }

        public async Task<List<T>> GetAllItemsAsync()
        {
            var connection = await GetConnectionAsync();
            return await connection.Table<T>().ToListAsync();
        }
        public async Task<T?> GetItemAsync(int id)
        {
            var connection = await GetConnectionAsync();
            return await connection.FindAsync<T>(id);
        }
        public async Task SaveItemAsync(T item)
        {
            var connection = await GetConnectionAsync();
            var result = await connection.InsertAsync(item);
            if (result != 1)
            {
                throw new Exception("Failed to save item.");
            }
        }
        public async Task DeleteItemAsync(T item)
        {
            var connection = await GetConnectionAsync();
            var result = await connection.DeleteAsync(item);
            if (result != 1)
            {
                throw new Exception("Failed to delete item.");
            }
        }
    }
}
