using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MaverikApp.RestAPIClient
{
    public class RestClient<T>
    {
        private const string MainWebServiceUrl = "http://maverik-project.com/api/v1";
        private const string LoginWebServiceUrl = "http://maverik-project.com/api/v1/auth/sign_in";

        public async Task<bool> checkLogin(string email, string password)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(LoginWebServiceUrl + "email=" + email + "/" + "password=" + password);

            return response.IsSuccessStatusCode;
        }
    }
}