using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;

namespace Attendance.Pages.QuestionPoolPartial
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public IndexModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }

        public IList<Attendance.Models.QuestionPool> QuestionPool { get;set; }

        public async Task OnGetAsync()
        {
            QuestionPool = await _context.QuestionPoolNew.ToListAsync();
        }
    }
}
