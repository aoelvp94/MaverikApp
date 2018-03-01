using Java.IO;
using MaverikApp.Helpers;
using MaverikApp.Model;
using MaverikApp.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaverikApp.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Commands
        public INavigation Navigation { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion

        private const string URL = "http://maverik-project.com";
        private string urlParameters = "/api/v1/auth/sign_in";
        #region Properties
        private User _user = new User();

        public User User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }
        #endregion

        public LoginPageViewModel()
        {
            LoginCommand = new Command(Login);        
        
    }
        public async void Login()
        {
            try { 
            var client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            //This code lists the RESTful service response//

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
            
            request.Content = new StringContent("{\"email\":\"" + User.Email + "\",\"password\":" + User.Password + "}", Encoding.UTF8, "application/json");

            
           HttpResponseMessage response = await client.SendAsync(request);

                /* var url = string.Format(_endpoint + "AuthenticateAccount");
            var json = JsonConvert.SerializeObject(cred);


            HttpContent content = new StringContent(json,
                Encoding.UTF8,
                WebConstants.ContentTypeJson);
            client.DefaultRequestHeaders.Add("user-agent", _deviceInfo);

            var resp = await client.PostAsync(url, content);

            if (resp.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<VResponse>(resp.Content.ReadAsStringAsync().Result);
                Token = result.Token;
                return result;
            }
            return null;
        }*/

            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
