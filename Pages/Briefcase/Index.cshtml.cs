using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.Models;
using Attendance.Services;

namespace Attendance.Pages.Briefcase
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.Models.AttendanceContext _context;
        private readonly IBriefcaseRepository _briefcaseRepository;
        public IList<VIQInfoModel> VIQInfoModel { get; set; }
        public IEnumerable<VIQInfoModel> RegisteredQuestionnaires { get; set; }

        public int AlertType;

        public IndexModel(Attendance.Models.AttendanceContext context, IBriefcaseRepository briefcaseRepository)
        {
            _context = context;
            _briefcaseRepository = briefcaseRepository;
            AlertType = 0;
        }   

        public async Task OnGetAsync(string action, int? id, int alerttype = 0)
        {
            VIQInfoModel = await _context.VIQInfo.ToListAsync();

            if ( "transfer" == action)
            {
                //add questionnaire
                VIQInfoModel viqinfo = VIQInfoModel.FirstOrDefault(item => item.QId == id);
                bool result = _briefcaseRepository.AddQuestionnaire(viqinfo);
                if (result == false)
                {
                    TempData["message"] = "The questionnaire was transferred already.";
                    TempData["result"] = "Info!";
                }
                else
                {
                    TempData["message"] = "The questionnaire was transferred successfully.";
                    TempData["result"] = "Success!";
                }
            }
            else if( action == "remove" )
            {
                _briefcaseRepository.RemoveQuestionnaire(id?? -1);
                TempData["message"] = "The questionnaire was removed.";
                TempData["result"] = "Success!";
            }
            
            RegisteredQuestionnaires = _briefcaseRepository.GetRegisteredQuestionnaires();
            AlertType = 1 - alerttype;
        }
    }
}
