using Microsoft.Maui;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ispitpredaja.Bussiness
{
    public class RestaurantsService : BaseService
    {
        public FullRestaurants GetRestaurants(string keyword = null)
        {
            var stringRequest = "restaurant/admin?";

            if (!string.IsNullOrEmpty(keyword))
            {
                stringRequest += $"name={keyword}";
            }

            var restRequest = new RestRequest(stringRequest);

            var user = Preferences.Default.Get("user", "");

            var deserializedUser = JsonConvert.DeserializeObject<User>(user);

            restRequest.AddHeader("Authorization", "Bearer " + deserializedUser.Token);

            var data = Client.Get<FullRestaurants>(restRequest);

            return data.Data;
        }

        public bool UpdateRestaurant(UpdateRestaurantDTO dto, int id)
        {
            var restRequest = new RestRequest($"restaurant/admin/{id}");

            var user = Preferences.Default.Get("user", "");

            var deserializedUser = JsonConvert.DeserializeObject<User>(user);

            restRequest.AddHeader("Authorization", "Bearer " + deserializedUser.Token);

            restRequest.AddJsonBody(JsonConvert.SerializeObject(dto));

            var data = Client.Put(restRequest);

            return data.StatusCode == System.Net.HttpStatusCode.NoContent;
        }

        public bool DeleteRestaurant(int id)
        {
            var restRequest = new RestRequest($"restaurant/admin/{id}");

            var user = Preferences.Default.Get("user", "");

            var deserializedUser = JsonConvert.DeserializeObject<User>(user);

            restRequest.AddHeader("Authorization", "Bearer " + deserializedUser.Token);

            var data = Client.Delete(restRequest);

            return data.StatusCode == System.Net.HttpStatusCode.NoContent;
        }
    }
    public class FullRestaurants
    {
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int PagesCount { get; set; }
        public IEnumerable<RestaurantDTO> Data { get; set; }
    }
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string AddressName { get; set; }
        public string WorkingHours { get; set; }
        public int AddressNumber { get; set; }
        public int WorkFrom { get; set; }
        public int WorkTo { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public IEnumerable<FoodCategoriesDTO> FoodCategories { get; set; } = new List<FoodCategoriesDTO>();
    }

    public class FoodCategoriesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<MealsDTO> Meals { get; set; } = new List<MealsDTO>();
        public IEnumerable<SidesDTO> Sides { get; set; } = new List<SidesDTO>();
    }

    public class MealsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class SidesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateRestaurantDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
        public int WorkFrom { get; set; }
        public int WorkTo { get; set; }
    }
}
