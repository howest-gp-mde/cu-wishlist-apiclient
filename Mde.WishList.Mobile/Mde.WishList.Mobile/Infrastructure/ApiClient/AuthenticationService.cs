using Mde.WishList.Mobile.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mde.WishList.Mobile.Infrastructure.ApiClient
{
    public class AuthenticationResponseDto
    {
        public string Token { get; set; }
        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }
    }

    public static class Constants
    {
        public const string ApiKeyName = "todotoken";
        public const string ApiUrlBase = "https://your-web-api-address:5001/api/v1";
    }

    public class AuthenticationService : IAuthenticationService
    {
        protected TokenReader _tokenReader = new TokenReader();

        public async Task<bool> Authenticate(string userName, string password)
        {
            try
            {
                using (var client = new HttpClient(WebApiClient.GetClientHandler()))
                {
                    var authRequest = new
                    {
                        userName,
                        password
                    };

                    //request maken
                    var response = await client.PostAsJsonAsync($"{Constants.ApiUrlBase}/Users/authenticate", authRequest);
                    
                    //auth dto uitlezen
                    string json = await response.Content.ReadAsStringAsync();
                    var authResult = JsonConvert.DeserializeObject<AuthenticationResponseDto>(json);

                    if (authResult.Succeeded)
                    {
                        //token bijhouden!
                        await SecureStorage.SetAsync(Constants.ApiKeyName, authResult.Token);
                    }

                    return authResult.Succeeded;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        public async Task<string> GetAuthenticationToken()
        {
            return await SecureStorage.GetAsync(Constants.ApiKeyName);
        }

        public async Task<bool> IsAuthenticated()
        {
            var token = await GetAuthenticationToken();
            return !string.IsNullOrWhiteSpace(token)
                && !_tokenReader.IsExpired(token);
        }

        public async Task<string> GetAuthenticatedUserName()
        {
            var token = await GetAuthenticationToken();
            return _tokenReader.GetUserName(token);
        }

    }
}
