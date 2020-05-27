using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Attendance.Models;

namespace Attendance.Pages.VIQInfo
{
    public class CreateModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public CreateModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public VIQInfoModel VIQInfoModel { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.VIQInfo.Add(VIQInfoModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
