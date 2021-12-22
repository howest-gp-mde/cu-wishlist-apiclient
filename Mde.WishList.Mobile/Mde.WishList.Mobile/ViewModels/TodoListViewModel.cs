using FreshMvvm;
using Mde.WishList.Mobile.Domain.Entities;
using Mde.WishList.Mobile.Domain.Services;
using System;
using System.Collections.ObjectModel;

namespace Mde.WishList.Mobile.ViewModels
{
    public class TodoListViewModel : FreshBasePageModel
    {
        protected readonly IAuthenticationService _authService;
        protected readonly ITodoListRepository _todoListRepository;

        public TodoListViewModel(IAuthenticationService authService, ITodoListRepository todoListRepository)
        {
            _authService = authService;
            _todoListRepository = todoListRepository;
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            IsAuthenticated = await _authService.IsAuthenticated();

            if (IsAuthenticated)
            {
                var lists = await _todoListRepository.GetTodoLists();
                TodoLists = new ObservableCollection<TodoList>(lists);

            }
            else
            {
                await CoreMethods.PushPageModel<LoginViewModel>(null, false, true);
            }

            base.ViewIsAppearing(sender, e);
        }

        private ObservableCollection<TodoList> todoLists;

        public ObservableCollection<TodoList> TodoLists
        {
            get { return todoLists; }
            set { todoLists = value;
                RaisePropertyChanged();
            }
        }

        private bool isAuthenticated;

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set { 
                isAuthenticated = value;
                RaisePropertyChanged();
            }
        }


    }
}
