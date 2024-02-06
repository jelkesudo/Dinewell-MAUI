using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ispitpredaja.Bussiness
{
    public abstract class BaseService
    {
        protected virtual string BaseUrl => "http://localhost:5000/api/";
        protected RestClient Client { get; }

        public BaseService()
        {
            Client = new RestClient(BaseUrl);
        }
    }
}
