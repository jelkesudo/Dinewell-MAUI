using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ispitpredaja.Bussiness
{
    public class RegisterService : BaseService
    {
        public bool RegisterUser(RegisterDTO obj)
        {
            var restRequest = new RestRequest("register");

            var userSend = new object();

            userSend = new
            {
                Email = obj.Email,
                Username = obj.Username,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Password = obj.Password,
            };

            if (obj.Address != null)
            {
                userSend = new
                {
                    Email = obj.Email,
                    Username = obj.Username,
                    FirstName = obj.FirstName,
                    LastName = obj.LastName,
                    Password = obj.Password,
                    Address = obj.Address,
                    AddressNumber = obj.AddressNumber
                };
            }

            var userJson = JsonConvert.SerializeObject(userSend);
            restRequest.AddJsonBody(userJson);
            var response = Client.Post(restRequest);
            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }
    }
    public class RegisterDTO
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int AddressNumber { get; set; }
    }
}
