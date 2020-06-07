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
        public PaginatedList<Attendance.Models.QuestionPool> MngQuestionPool { get; set; }

        public string CurrentFilterCategory { get; set; }
        public string CurrentFilterQuestionPool { get; set; }
        public string CurrentFilterMngQuestionPool { get; set; }

        public Guid SelectedCategoryNewID { get; set; }

        public async Task OnGetAsync(int? pageIndex, int? qPageIndex, int? mngQPageIndex, Guid categoryNewID, Guid selectedCategoryNewID,
            string currentFilterCategory, string currentFilterQuestionPool, string currentFilterMngQuestionPool,
            string searchCategory, string searchQuestionPool, string searchMngQuestionPool)
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

            ////////// SIRE Question Pool ///////////
            if (searchQuestionPool != null || (categoryNewID != Guid.Empty && selectedCategoryNewID != Guid.Empty ))
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

            if( categoryNewID != Guid.Empty )
            {
                SelectedCategoryNewID = categoryNewID;
            }
            else
            {
                SelectedCategoryNewID = selectedCategoryNewID;

                if (SelectedCategoryNewID == Guid.Empty &&  Category.Count > 0)
                {
                    SelectedCategoryNewID = Category.First().CategoryNewID;
                }
            }

            iq = iq.Where(item => !item.questioncode.Trim().Equals("") && !item.question.Trim().Equals("") && item.CategoryNewID.Equals(SelectedCategoryNewID) && item.Origin > 0);
            if (!String.IsNullOrEmpty(searchQuestionPool))
            {
                iq = iq.Where(s => s.CategoryCode.Equals(searchQuestionPool) 
                                       || s.CategoryID.ToString().Equals(searchQuestionPool) 
                                       || s.questioncode.Equals(searchQuestionPool)
                                       || s.Origin.ToString().Equals(searchQuestionPool)
                                       || s.question.ToLower().Contains(searchQuestionPool.ToLower()));
            }

            int pageSize1 = Attendance.Data.Global.PageSize / 2;
            QuestionPool = await PaginatedList<Attendance.Models.QuestionPool>.CreateAsync(
                iq.AsNoTracking(), qPageIndex ?? 1, pageSize1);

            ////////// Management Question Pool ///////////
            if (searchMngQuestionPool != null || categoryNewID != null)
            {
                mngQPageIndex = 1;
            }
            else
            {
                searchMngQuestionPool = currentFilterMngQuestionPool;
            }
            CurrentFilterMngQuestionPool = searchMngQuestionPool;
            IQueryable<Attendance.Models.QuestionPool> mngiq = from s in _context.QuestionPoolNew
                                                            select s;
            
            mngiq = mngiq.Where(item => !item.questioncode.Trim().Equals("") && !item.question.Trim().Equals("") && item.CategoryNewID.Equals(SelectedCategoryNewID) && item.Origin == 0);
            if (!String.IsNullOrEmpty(searchMngQuestionPool))
            {
                mngiq = mngiq.Where(s => s.CategoryCode.Equals(searchMngQuestionPool)
                                       || s.CategoryID.ToString().Equals(searchMngQuestionPool)
                                       || s.questioncode.Equals(searchMngQuestionPool)
                                       || s.Origin.ToString().Equals(searchMngQuestionPool)
                                       || s.question.ToLower().Contains(searchMngQuestionPool.ToLower()));
            }

            int pageSize2 = Attendance.Data.Global.PageSize / 2;
            MngQuestionPool = await PaginatedList<Attendance.Models.QuestionPool>.CreateAsync(
                mngiq.AsNoTracking(), mngQPageIndex ?? 1, pageSize2);

            //Category = await _context.Category.ToListAsync();
        }
    }
}