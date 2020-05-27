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
    public class DeleteModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public DeleteModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public VIQInfoModel VIQInfoModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VIQInfoModel = await _context.VIQInfo.FirstOrDefaultAsync(m => m.QId == id);

            if (VIQInfoModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VIQInfoModel = await _context.VIQInfo.FindAsync(id);

            if (VIQInfoModel != null)
            {
                _context.VIQInfo.Remove(VIQInfoModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
