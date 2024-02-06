using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ispitpredaja.Bussiness
{
    public class OneRestaurantsService : BaseService
    {
        public RestaurantDTO GetRestaurant(int id)
        {
            var restRequest = new RestRequest($"restaurant/admin/{id}");

            var user = Preferences.Default.Get("user", "");

            var deserializedUser = JsonConvert.DeserializeObject<User>(user);

            restRequest.AddHeader("Authorization", "Bearer " + deserializedUser.Token);

            var data = Client.Get<RestaurantDTO>(restRequest);

            return data.Data;
        }
    }
}
