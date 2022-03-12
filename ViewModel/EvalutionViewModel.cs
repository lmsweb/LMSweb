using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class EvalutionViewModel
    {
        public string CID { get; set; }
        public string MID { get; set; }
        public string SID { get; set; }
        public IEnumerable<LMSweb.Models.Question> Questions { get; set; }
        public List<Question_Response> QRs { get; set; }
        public LMSweb.Models.Option Option { get; set; }
        public LMSweb.Models.EvalutionResponse Response { get; set; }

    }
}