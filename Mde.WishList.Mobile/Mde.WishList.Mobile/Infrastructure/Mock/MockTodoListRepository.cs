using Mde.WishList.Mobile.Domain.Entities;
using Mde.WishList.Mobile.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mde.WishList.Mobile.Infrastructure.Mock
{
    public class MockTodoListRepository : ITodoListRepository
    {
        public Task<IEnumerable<TodoList>> GetTodoLists()
        {
            IEnumerable<TodoList> list = new List<TodoList> {
                    new TodoList {
                        Id = 1,
                        Title = "Fake Todolist 1",
                        Priority = 0
                    },
                    new TodoList {
                        Id = 2,
                        Title = "Fake Todolist 2",
                        Priority = 1
                    },
            };

            return Task.FromResult(list);
        }
    }
}
