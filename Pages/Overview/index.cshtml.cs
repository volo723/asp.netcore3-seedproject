using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Attendance.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Pages.Overview
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Models.CategoryContext _context;

        public IndexModel(Attendance.Models.CategoryContext context)
        {
            _context = context;
        }
        
        public IList<Category> Category { get; set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}