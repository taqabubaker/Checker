using Checker.Data;
using Checker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checker.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;
        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TodoViewModel> GetIncompleteItemsAsync()
        {
            var vm = new TodoViewModel();

            vm.Items =  await _context.Items
            .Where(x => x.IsDone == false)
            .OrderByDescending(x => x.Id)
            .ToArrayAsync();

            vm.DoneItems = await _context.Items
            .Where(x => x.IsDone == true)
            .OrderByDescending(x => x.Id)
            .ToArrayAsync();

            return vm;
        }

        public async Task<bool> AddItemAsync(TodoItem newItem)
        {
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);
            _context.Items.Add(newItem);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }
        public async Task<bool> MarkDoneAsync(int id)
        {
            var item = await _context.Items
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
            if (item == null) return false;
            item.IsDone = true;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }

        public async Task<bool> DeleteAll()
        {
            var done = await _context.Items
            .Where(x => x.IsDone == true)
            .OrderByDescending(x => x.Id)
            .ToArrayAsync();

            foreach (var item in done)
            {
                _context.Attach(item).State = EntityState.Deleted;
            }

            var saveResult = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSingle(int id)
        {
            var item = await _context.Items
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
            if (item == null) return false;
            _context.Attach(item).State = EntityState.Deleted;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
