using FreshMvvm;
using Mde.WishList.Mobile.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mde.WishList.Mobile.ViewModels
{
    public class LoginViewModel : FreshBasePageModel
    {
        protected readonly IAuthenticationService _authService;

        public LoginViewModel(IAuthenticationService authService)
        {
            _authService = authService;

#if DEBUG
            UserName = "normal@localhost";
            Password = "Seedpassword1!";
#endif
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value;
                RaisePropertyChanged();
            }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoginCommand => new Command(
            async () =>
            {
                var isAuthed = await _authService.Authenticate(UserName, Password);

                if (isAuthed)
                {
                    await CoreMethods.PushPageModel<TodoListViewModel>(true);
                }
            });
    }
}
