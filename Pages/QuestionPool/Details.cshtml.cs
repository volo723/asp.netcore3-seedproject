﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;

namespace Attendance.Pages.QuestionPool
{
    public class DetailsModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public DetailsModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }

        public Attendance.Models.QuestionPool QuestionPool { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
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
    }
}
