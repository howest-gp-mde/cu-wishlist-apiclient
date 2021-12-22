using FreshMvvm;
using Mde.WishList.Mobile.Domain.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Mde.WishList.Mobile.Converters
{
    public class AuthStateToUserNameConverter : IValueConverter
    {
        //protected readonly IAuthenticationService _authenticationService;

        //public AuthStateToUserNameConverter(IAuthenticationService _authenticationService)
        //{

        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool)
            {
                bool isAuthed = (bool)value;
                var authService = FreshIOC.Container.Resolve<IAuthenticationService>();
                if (isAuthed)
                {
                    return authService.GetAuthenticatedUserName().Result;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                throw new ArgumentException("value should be a bool");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
