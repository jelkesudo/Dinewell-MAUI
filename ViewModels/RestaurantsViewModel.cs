using Ispitpredaja.Bussiness;
using Ispitpredaja.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ispitpredaja.ViewModels
{
    public class RestaurantsViewModel : BaseViewModel
    {
        private RestaurantsService _restaurantService;
        public ObservableCollection<RestaurantDTO> Restaurants { get; set; }
        public MProp<string> Keyword { get; set; } = new MProp<string>();

        public RestaurantsViewModel()
        {
            _restaurantService = new RestaurantsService();

            SearchRestaurants();

            GoToRestaurantCommand = new Command<int>(GoToRestaurant);
            DeleteRestaurantCommand = new Command<int>(DeleteRestaurant);
            UpdateRestaurantCommand = new Command<int>(UpdateRestaurant);
            SearchRestaurantCommand = new Command(SearchRestaurants);
        }

        public ICommand GoToRestaurantCommand { get; }
        public ICommand DeleteRestaurantCommand { get; }
        public ICommand UpdateRestaurantCommand { get; }
        public ICommand SearchRestaurantCommand { get; }

        private void SearchRestaurants()
        {

            FullRestaurants restaurantsAll = _restaurantService.GetRestaurants(Keyword.Value);

            IEnumerable<RestaurantDTO> restaurants = restaurantsAll.Data;

            Restaurants = new ObservableCollection<RestaurantDTO>(restaurants);

            OnPropertyChanged(nameof(Restaurants));
        }

        private void GoToRestaurant(int restaurant)
        {
            Application.Current.MainPage = new OneRestaurant(restaurant);
        }

        private void UpdateRestaurant(int restaurant)
        {
            Application.Current.MainPage = new UpdateRestaurant(restaurant);
        }

        private void DeleteRestaurant(int restaurant)
        {
            var isDeleted = _restaurantService.DeleteRestaurant(restaurant);
            if (isDeleted)
            {
                //var toExpell = Restaurants.Where(x => x.Id ==  restaurant).FirstOrDefault();
                //Restaurants.Remove(toExpell);
                //OnPropertyChanged(nameof(Restaurants));
                SearchRestaurants();
            }
        }
    }
}
