using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class ReflectionViewModel
    {
        public string CID { get; set; }
        public string MID { get; set; }
        public IEnumerable<LMSweb.Models.Question> Questions { get; set; }
        public LMSweb.Models.Option Option { get; set; }
        public LMSweb.Models.Response Response { get; set; }

    }
}