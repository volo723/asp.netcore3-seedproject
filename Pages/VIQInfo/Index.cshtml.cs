using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;

namespace Attendance.Pages.VIQInfo
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public IndexModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }

        public IList<VIQInfoModel> VIQInfoModel { get;set; }

        public async Task OnGetAsync()
        {
            VIQInfoModel = await _context.VIQInfo.ToListAsync();
        }
    }
}
