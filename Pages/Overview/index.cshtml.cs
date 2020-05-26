using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Attendance.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Pages.Overview
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;

        public IndexModel(Attendance.Models.AttendanceContext context)
        {
            _context = context;
        }

        public PaginatedList<Attendance.Models.Category> Category { get; set; }
        public PaginatedList<Attendance.Models.QuestionPool> QuestionPool { get; set; }

        public async Task OnGetAsync(int? pageIndex, int? qPageIndex)
        {
            IQueryable<Attendance.Models.Category> categoryIQ = from s in _context.Category
                                                                select s;

            categoryIQ = categoryIQ.Where(cat => !cat.CategoryCode.Trim().Equals("") && !cat.CategoryDescription.Trim().Equals(""));

            int pageSize = Attendance.Data.Global.PageSize;
            Category = await PaginatedList<Attendance.Models.Category>.CreateAsync(
                categoryIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            ////////// Question Pool ///////////
            IQueryable<Attendance.Models.QuestionPool> iq = from s in _context.QuestionPoolNew
                                                            select s;

            iq = iq.Where(item => !item.questioncode.Trim().Equals("") && !item.question.Trim().Equals(""));

            QuestionPool = await PaginatedList<Attendance.Models.QuestionPool>.CreateAsync(
                iq.AsNoTracking(), qPageIndex ?? 1, pageSize);

            //Category = await _context.Category.ToListAsync();
        }
    }
}