using LMSweb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class GoalSettingViewModel
    {
        public string CID { get; set; }
        public string MID { get; set; }
        public List<Question_Response> QRs { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }

    public class Question_Response 
    {
        public int  qid { get; set; }

        [Required]
        public string response { get; set; }
    }
}