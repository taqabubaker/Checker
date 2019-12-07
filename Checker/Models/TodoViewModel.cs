using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checker.Models
{
    public class TodoViewModel
    {
        public TodoItem[] Items { get; set; }
        public TodoItem[] DoneItems { get; set; }
    }
}
