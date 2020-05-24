using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;

namespace Attendance.Pages.QuestionPool
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public IndexModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }
    
        public PaginatedList<Attendance.Models.QuestionPool> QuestionPool { get; set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            IQueryable<Attendance.Models.QuestionPool> iq = from s in _context.QuestionPoolNew
                                                                select s;

            iq = iq.Where(item => !item.questioncode.Trim().Equals("") && !item.question.Trim().Equals(""));

            int pageSize = 10;
            QuestionPool = await PaginatedList<Attendance.Models.QuestionPool>.CreateAsync(
                iq.AsNoTracking(), pageIndex ?? 1, pageSize);

            //Category = await _context.Category.ToListAsync();
        }
    }
}
