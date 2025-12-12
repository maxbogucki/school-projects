using Cars_Bogucki.Models;
using Cars_Bogucki.ViewModels;

namespace Cars_Bogucki.Views;

public partial class CarListPage : ContentPage
{
    private CarListViewModel Vm => (BindingContext as CarListViewModel)!;

    public CarListPage(CarListViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Vm.LoadAsync();
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection?.FirstOrDefault() as Car;
        if (selected != null)
        {
            await Vm.OpenDetailsAsync(selected);
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        await Vm.AddNewAsync();
    }
}