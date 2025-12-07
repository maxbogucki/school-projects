using Cars_Bogucki.ViewModels;

namespace Cars_Bogucki.Views;

public partial class CarDetailPage : ContentPage
{
    public CarDetailPage(CarDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}