using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Mde.WishList.Mobile
{
    public partial class MainPage : ContentPage
    {
        protected string apiUrl = "https://localhost:5001/api/v1";

        public MainPage()
        {
            InitializeComponent();

            
        }

        public class AuthResponse
        {
            public string Token { get; set; }
            public bool Succeeded { get; set; }
            public string[] Errors { get; set; }
        }

        protected async override void OnAppearing()
        {
            using (var client = new HttpClient(GetClientHandler()))
            {

                var authRequest = new 
                {
                    userName = "normal@localhost",
                    password = "Seedpassword1!"
                };
                var response = await client.PostAsJsonAsync(apiUrl + "/Users/authenticate", authRequest);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AuthResponse>(json);

                await SecureStorage.SetAsync("oauth_token", result.Token);

                var token = await SecureStorage.GetAsync("oauth_token");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var json2 = await client.GetStringAsync(apiUrl + "/TodoLists");
            }


            base.OnAppearing();
        }

        private static HttpClientHandler GetClientHandler()
        {
            var httpClientHandler = new HttpClientHandler();
#if DEBUG
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, certificate, chain, errors) => true;
#endif
            return httpClientHandler;
        }
    }
}
