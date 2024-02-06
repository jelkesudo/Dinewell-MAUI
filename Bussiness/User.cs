using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ispitpredaja.Bussiness
{
    public class User
    {
        public string Email { get; set; }
        public int Id { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime LoginExpiration { get; set; }
        public bool ShouldBeLoggedOut => LoginExpiration < DateTime.UtcNow;
    }
}
