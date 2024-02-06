using Ispitpredaja.ViewModels;

namespace Ispitpredaja;

public partial class UpdateRestaurant : ContentPage
{
	public UpdateRestaurant()
	{
		InitializeComponent();
	}

    public UpdateRestaurant(int restaurant)
    {
        InitializeComponent();

        BindingContext = new UpdateRestaurantViewModel(restaurant);
    }
}