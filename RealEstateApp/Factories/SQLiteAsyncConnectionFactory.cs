using SQLite;

namespace RealEstateApp.Factories
{
    public static class SQLiteAsyncConnectionFactory<T> where T : class, new()
    {
        public static async Task<SQLiteAsyncConnection> CreateConnectionAsync()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "properties.db");
            var connection = new SQLiteAsyncConnection(dbPath);
            await connection.CreateTableAsync<T>();
            return connection;
        }
    }
}
