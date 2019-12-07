using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Checker.Services;
using Checker.Models;

namespace Checker.Controllers
{
    public class TodoController : Controller
    {
        private ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var vm = await _todoItemService.GetIncompleteItemsAsync();
            var model = new TodoViewModel()
            {
                Items = vm.Items,
                DoneItems = vm.DoneItems
            };
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var successful = await _todoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }
            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(int id)
        {
            var successful = await _todoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return BadRequest("Could not mark item as done.");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAll()
        {
            var successful = await _todoItemService.DeleteAll();
            if (!successful)
            {
                return BadRequest("Could not delete items");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSingle(int id)
        {
            var successful = await _todoItemService.DeleteSingle(id);
            if (!successful)
            {
                return BadRequest("Could not delete item");
            }
            return RedirectToAction("Index");
        }
    }
}