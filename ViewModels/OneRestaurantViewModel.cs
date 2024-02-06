using Ispitpredaja.Bussiness;
using Ispitpredaja.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ispitpredaja.ViewModels
{
    public class OneRestaurantViewModel : BaseViewModel
    {
        private OneRestaurantsService _oneRestaurantsService;
        public MProp<RestaurantDTO> Restaurant { get; set; } = new MProp<RestaurantDTO>();

        public OneRestaurantViewModel()
        {
            
        }

        public OneRestaurantViewModel(int restaurant)
        {
            _oneRestaurantsService = new OneRestaurantsService();
            Restaurant.Value = _oneRestaurantsService.GetRestaurant(restaurant);
        }
    }
}
