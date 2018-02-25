using MaverikApp.Helpers;
using MaverikApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace MaverikApp.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        private const string URL = "http://maverik-project.com";
        private string urlParameters = "/api/v1/unidades_de_distribucion";
        public INavigation Navigation { get; set; }
        public ICommand LogoutCommand { get; set; }
        private string _message;
        List<string> listapatentes = new List<string>();

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public List<string> Unidades { get => listapatentes; set => listapatentes = value; }

        public MainPageViewModel()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //This code lists the RESTful service response//
            HttpResponseMessage response = client.GetAsync(URL+urlParameters).Result;
            if (response.IsSuccessStatusCode)
            {
                var jsonString = response.Content.ReadAsStringAsync();
                Model.RootObject dataObject = JsonConvert.DeserializeObject<Model.RootObject>(jsonString.Result);
                foreach (Model.UnidadesDeDistribucion d in dataObject.unidades_de_distribucion)
                {
                    if (d.disponible == true)
                    {
                        string aux = d.patente;
                        listapatentes.Add(aux);
                        /*System.Diagnostics.Debug.WriteLine("{0}", d.patente);
                        System.Diagnostics.Debug.WriteLine("{0}", d.modelo);
                        System.Diagnostics.Debug.WriteLine("{0}", d.marca);
                        System.Diagnostics.Debug.WriteLine("{0}", d.disponible);*/

                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            LogoutCommand = new Command(Logout);
           // Message = "Welcome to Main Page!";
        }

        private async void Logout()
        {
            Settings.IsLoggedIn = false;
            await Navigation.PushAsync(new LoginPage());
        }
    }
}