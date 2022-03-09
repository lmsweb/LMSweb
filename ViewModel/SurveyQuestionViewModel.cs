using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class SurveyQuestionViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public string MID { get; set; }


        public LMSweb.Models.Question Question { get; set; }
        public List<string> String_Options { get; set; }
        public LMSweb.Models.DefaultQuestion DefaultQuestion { get; set; }
        public IEnumerable<LMSweb.Models.DefaultQuestion> DefaultQuestions { get; set; }
        public LMSweb.Models.DefaultOption DefaultOption { get; set; }
        //public IEnumerable<LMSweb.Models.DefaultOption> DefaultOptions { get; set; }
    }
}