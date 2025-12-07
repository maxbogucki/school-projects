using Cars_Bogucki.Models;

namespace Cars_Bogucki.Services;

public class CarRepository
{
    private readonly DatabaseService _db;

    public CarRepository(DatabaseService db)
    {
        _db = db;
    }

    private async Task EnsureInit() => await _db.InitializeAsync();

    public async Task<List<Car>> GetCarsAsync()
    {
        await EnsureInit();
        return await _db.Connection.Table<Car>()
            .OrderBy(c => c.Make)
            .ThenBy(c => c.Model)
            .ToListAsync();
    }

    public async Task<Car?> GetCarAsync(int id)
    {
        await EnsureInit();
        return await _db.Connection.Table<Car>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> SaveCarAsync(Car car)
    {
        await EnsureInit();
        if (car.Id == 0)
            return await _db.Connection.InsertAsync(car);
        else
            return await _db.Connection.UpdateAsync(car);
    }

    public async Task<int> DeleteCarAsync(Car car)
    {
        await EnsureInit();
        return await _db.Connection.DeleteAsync(car);
    }

    public async Task SeedAsync()
    {
        await EnsureInit();
        var count = await _db.Connection.Table<Car>().CountAsync();
        if (count > 0) return;

        var demo = new List<Car>
        {
            new() { Make = "Toyota", Model = "Camry", Year = 2021, Price = 23995, ImageUrl = null },
            new() { Make = "Honda", Model = "Civic", Year = 2022, Price = 21950, ImageUrl = null },
            new() { Make = "Ford", Model = "F-150", Year = 2020, Price = 34900, ImageUrl = null },
            new() { Make = "Tesla", Model = "Model 3", Year = 2023, Price = 39990, ImageUrl = null },
        };

        foreach (var car in demo)
            await SaveCarAsync(car);
    }
}