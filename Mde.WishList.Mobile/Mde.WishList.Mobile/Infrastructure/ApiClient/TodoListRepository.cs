using Mde.WishList.Mobile.Domain.Entities;
using Mde.WishList.Mobile.Domain.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mde.WishList.Mobile.Infrastructure.ApiClient
{
    public class GetTodoListDto
    {
        [JsonProperty("lists")]
        public IEnumerable<TodoList> Lists { get; set; }
    }

    public class TodoListRepository : ITodoListRepository
    {
        protected readonly IAuthenticationService _authenticationService;

        public TodoListRepository(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<IEnumerable<TodoList>> GetTodoLists()
        {
            try
            {
                using (var client = new HttpClient(WebApiClient.GetClientHandler()))
                {
                    var token = await _authenticationService.GetAuthenticationToken();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("BEARER", token);

                    //request maken
                    var json = await client.GetStringAsync($"{Constants.ApiUrlBase}/TodoLists");
                    var result = JsonConvert.DeserializeObject<GetTodoListDto>(json);

                    return result.Lists;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
