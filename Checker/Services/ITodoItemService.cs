using Checker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checker.Services
{
    public interface ITodoItemService
    {
        Task<TodoViewModel> GetIncompleteItemsAsync();
        Task<bool> AddItemAsync(TodoItem newItem);
        Task<bool> MarkDoneAsync(int id);
        Task<bool> DeleteAll();
        Task<bool> DeleteSingle(int id);
    }
}
