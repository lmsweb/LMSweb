using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LMSweb.Models;

namespace LMSweb.ViewModel
{
    public class EvalutionViewModel
    {
        public string CID { get; set; }
        public string MID { get; set; }
        public string SID { get; set; }
        public IEnumerable<LMSweb.Models.Question> Questions { get; set; }
        public List<Evalution_Response> ERs { get; set; }
        public LMSweb.Models.Option Option { get; set; }
        public LMSweb.Models.EvalutionResponse Response { get; set; }

    }
    public class Evalution_Response
    {
        public int qid { get; set; }

        [Required]
        public string response { get; set; }
        public string comments { get; set; }
        public string sid { get; set; }

    }
}