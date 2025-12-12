using SQLite;

namespace Cars_Bogucki.Services;

public class DatabaseService
{
    private readonly Lazy<SQLiteAsyncConnection> _database;

    public SQLiteAsyncConnection Connection => _database.Value;

    public DatabaseService()
    {
        _database = new Lazy<SQLiteAsyncConnection>(() =>
        {
            var path = Path.Combine(FileSystem.AppDataDirectory, "cars.db3");
            var conn = new SQLiteAsyncConnection(path);
            return conn;
        });
    }

    public async Task InitializeAsync()
    {
        await Connection.CreateTableAsync<Models.Car>();
    }
}