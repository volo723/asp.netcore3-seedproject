using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace Attendance.Models
{
    public class QuestionPool
    {
        [Key]
        public Guid questionid { get; set; }

        public string questioncode { get; set; }

        public string question { get; set; }

        public int? CategoryID { get; set; }

        public string CategoryCode { get; set; }

        public int? Origin { get; set; }
    }
}
