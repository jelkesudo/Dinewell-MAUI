using Ispitpredaja.Bussiness;
using Ispitpredaja.ViewModels;

namespace Ispitpredaja;

public partial class OneRestaurant : ContentPage
{
    private OneRestaurantViewModel _viewModel;
    public OneRestaurant()
    {

    }
    public OneRestaurant(int restaurant)
	{
        InitializeComponent();
        BindingContext = new OneRestaurantViewModel(restaurant);
    }
    public void GoBackToRestaurant(object sender, EventArgs e)
    {
        Application.Current.MainPage = new HomePage();
    }
}