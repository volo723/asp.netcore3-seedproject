using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;

namespace Attendance.Pages.Qpool
{
    public class EditModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public EditModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Attendance.Models.QuestionPool QuestionPool { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            QuestionPool = await _context.QuestionPoolNew.FirstOrDefaultAsync(m => m.questionid == id);

            if (QuestionPool == null)
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

            _context.Attach(QuestionPool).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionPoolExists(QuestionPool.questionid))
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

        private bool QuestionPoolExists(Guid id)
        {
            return _context.QuestionPoolNew.Any(e => e.questionid == id);
        }
    }
}
