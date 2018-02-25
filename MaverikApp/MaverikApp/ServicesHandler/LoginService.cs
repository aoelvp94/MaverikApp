using MaverikApp.Model;
using MaverikApp.RestAPIClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaverikApp.ServicesHandler
{
    class LoginService
    {
        RestClient<User> _restClient = new RestClient<User>();

        public async Task<bool> CheckLoginIfExists(string email, string password)
        {
            var check = await _restClient.checkLogin(email, password);

            return check;
        }
    }
}
