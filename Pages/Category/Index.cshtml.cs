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
    public class IndexModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public IndexModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }

        public PaginatedList<Attendance.Models.Category> Category { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Attendance.Models.Category> categoryIQ = from s in _context.Category
                                                                select s;

            categoryIQ = categoryIQ.Where(cat => !cat.CategoryCode.Trim().Equals("") && !cat.CategoryDescription.Trim().Equals(""));

            int pageSize = 10;
            Category = await PaginatedList<Attendance.Models.Category>.CreateAsync(
                categoryIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            //Category = await _context.Category.ToListAsync();
        }
    }
}
