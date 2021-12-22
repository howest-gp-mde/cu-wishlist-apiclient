using FreshMvvm;
using Mde.WishList.Mobile.Domain.Services;
using Mde.WishList.Mobile.Infrastructure.ApiClient;
using Mde.WishList.Mobile.Infrastructure.Mock;
using Mde.WishList.Mobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mde.WishList.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            FreshIOC.Container.Register<IAuthenticationService, AuthenticationService>().AsMultiInstance();
            FreshIOC.Container.Register<ITodoListRepository, TodoListRepository>().AsMultiInstance();

            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<TodoListViewModel>());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
