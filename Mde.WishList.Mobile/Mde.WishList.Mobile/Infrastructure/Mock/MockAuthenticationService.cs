using Mde.WishList.Mobile.Domain.Services;
using System.Threading.Tasks;

namespace Mde.WishList.Mobile.Infrastructure.Mock
{
    public class MockAuthenticationService : IAuthenticationService
    {
        private static bool isFakeAuthenticated = false;

        public Task<bool> Authenticate(string username, string password)
        {
            isFakeAuthenticated = true;
            return Task.FromResult(isFakeAuthenticated);
        }

        public Task<string> GetAuthenticatedUserName()
        {
            return Task.FromResult("fakeUSer");
        }

        public Task<string> GetAuthenticationToken()
        {
            return Task.FromResult("fakeToken");
        }

        public Task<bool> IsAuthenticated()
        {
            return Task.FromResult(isFakeAuthenticated);
        }
    }
}
