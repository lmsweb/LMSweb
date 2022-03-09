using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class QuestionViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public LMSweb.Models.Question Question { get; set; }
        public List<string> String_Options { get; set; }
    }
}