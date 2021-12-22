using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mde.WishList.Mobile.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string username, string password);

        Task<string> GetAuthenticationToken();

        Task<bool> IsAuthenticated();

        Task<string> GetAuthenticatedUserName();
    }
}
