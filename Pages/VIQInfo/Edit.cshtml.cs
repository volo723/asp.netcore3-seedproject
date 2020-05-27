using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;

namespace Attendance.Pages.VIQInfo
{
    public class EditModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public EditModel(Attendance.Models.AttendanceContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(VIQInfoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VIQInfoModelExists(VIQInfoModel.QId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VIQInfoModelExists(int id)
        {
            return _context.VIQInfo.Any(e => e.QId == id);
        }
    }
}
