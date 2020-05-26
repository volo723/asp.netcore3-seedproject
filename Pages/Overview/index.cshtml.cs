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

        public string CurrentFilterCategory { get; set; }
        public string CurrentFilterQuestionPool { get; set; }

        public async Task OnGetAsync(int? pageIndex, int? qPageIndex, string currentFilterCategory, string currentFilterQuestionPool, string searchCategory, string searchQuestionPool)
        {
            if (searchCategory != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchCategory = currentFilterCategory;
            }

            CurrentFilterCategory = searchCategory;
            IQueryable<Attendance.Models.Category> categoryIQ = from s in _context.Category
                                                                select s;

            categoryIQ = categoryIQ.Where(cat => !cat.CategoryCode.Trim().Equals("") && !cat.CategoryDescription.Trim().Equals(""));
            if (!String.IsNullOrEmpty(searchCategory))
            {
                categoryIQ = categoryIQ.Where(s => s.CategoryCode.Equals(searchCategory)
                                       || s.CategoryDescription.ToLower().Contains(searchCategory.ToLower()));
            }

            int pageSize = Attendance.Data.Global.PageSize;
            Category = await PaginatedList<Attendance.Models.Category>.CreateAsync(
                categoryIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            ////////// Question Pool ///////////
            if (searchQuestionPool != null)
            {
                qPageIndex = 1;
            }
            else
            {
                searchQuestionPool = currentFilterQuestionPool;
            }
            CurrentFilterQuestionPool = searchQuestionPool;
            IQueryable<Attendance.Models.QuestionPool> iq = from s in _context.QuestionPoolNew
                                                            select s;

            iq = iq.Where(item => !item.questioncode.Trim().Equals("") && !item.question.Trim().Equals(""));
            if (!String.IsNullOrEmpty(searchQuestionPool))
            {
                iq = iq.Where(s => s.CategoryCode.Equals(searchQuestionPool) 
                                       || s.CategoryID.ToString().Equals(searchQuestionPool) 
                                       || s.questioncode.Equals(searchQuestionPool)
                                       || s.Origin.ToString().Equals(searchQuestionPool)
                                       || s.question.ToLower().Contains(searchQuestionPool.ToLower()));
            }

            QuestionPool = await PaginatedList<Attendance.Models.QuestionPool>.CreateAsync(
                iq.AsNoTracking(), qPageIndex ?? 1, pageSize);

            //Category = await _context.Category.ToListAsync();
        }
    }
}