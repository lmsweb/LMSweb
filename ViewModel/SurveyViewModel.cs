using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class SurveyViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public LMSweb.Models.Question Question { get; set; }
        public LMSweb.Models.Option Option { get; set; }
        public LMSweb.Models.Response Response{ get; set; }
    }
}