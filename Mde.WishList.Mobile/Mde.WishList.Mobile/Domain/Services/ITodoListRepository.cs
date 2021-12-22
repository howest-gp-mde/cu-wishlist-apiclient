using Mde.WishList.Mobile.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mde.WishList.Mobile.Domain.Services
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<TodoList>> GetTodoLists();
    }
}
