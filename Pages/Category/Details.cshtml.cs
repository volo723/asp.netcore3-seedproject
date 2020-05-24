using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;

namespace Attendance.Pages.Category
{
    public class DetailsModel : PageModel
    {
        private readonly Attendance.Models.CategoryContext _context;

        public DetailsModel(Attendance.Models.CategoryContext context)
        {
            _context = context;
        }

        public Attendance.Models.Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category = await _context.Category.FirstOrDefaultAsync(m => m.CategoryID == id);

            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
