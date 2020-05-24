using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Attendance.Models
{
    public class QuestionPool
    {
        [Key]
        public string questionid { get; set; }

        public string questioncode { get; set; }

        public string question { get; set; }
    }
}
