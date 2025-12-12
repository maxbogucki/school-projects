using Cars_Bogucki.Models;
using Cars_Bogucki.Services;
using CommunityToolkit.Mvvm.Input;

namespace Cars_Bogucki.ViewModels;

[QueryProperty(nameof(Car), "Car")]
public partial class CarDetailViewModel : BaseViewModel
{
    private readonly CarRepository _repo;

    public CarDetailViewModel(CarRepository repo)
    {
        _repo = repo;
        Title = "Car Details";
    }

    private Car? car;
    public Car? Car
    {
        get => car;
        set => SetProperty(ref car, value);
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        if (Car is null) return;
        await _repo.SaveCarAsync(Car);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task DeleteAsync()
    {
        if (Car is null || Car.Id == 0) return;
        var confirm = await Application.Current?.MainPage?.DisplayAlert("Delete", "Delete this car?", "Yes", "No");
        if (confirm != true) return;
        await _repo.DeleteCarAsync(Car);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task PickImageAsync()
    {
        try
        {
            var result = await FilePicker.Default.PickAsync(new PickOptions
            {
                PickerTitle = "Pick a car image",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                // Copy the file to app data directory
                var newFileName = $"{Guid.NewGuid()}.jpg";
                var newFilePath = Path.Combine(FileSystem.AppDataDirectory, newFileName);

                using (var stream = await result.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFilePath))
                {
                    await stream.CopyToAsync(newStream);
                }

                // Update the Car's ImageUrl with the local file path
                if (Car != null)
                {
                    Car.ImageUrl = newFilePath;
                    OnPropertyChanged(nameof(Car)); // Notify UI to refresh
                }
            }
        }
        catch (Exception ex)
        {
            await Application.Current?.MainPage?.DisplayAlert("Error", $"Unable to pick image: {ex.Message}", "OK");
        }
    }
}