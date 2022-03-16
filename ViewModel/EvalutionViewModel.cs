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
        public string CName { get; set; }
        public string MID { get; set; }
        public string MName { get; set; }
        public string SID { get; set; }
        public int GID { get; set; }
        public string GName { get; set; }
        public string EvaluatorSID { get; set; }
        public string DrawingImgPath { get; set; }
        public string CodeText { get; set; }
        public IEnumerable<LMSweb.Models.Question> Questions { get; set; }
        public IEnumerable<LMSweb.Models.DefaultQuestion> DefaultQuestions { get; set; }
        public List<Evalution_Response> ERs { get; set; }
        public List<Group_Response> GRs { get; set; }
        public LMSweb.Models.Option Option { get; set; }
        public LMSweb.Models.GroupER GroupER { get; set; }
        public LMSweb.Models.EvalutionResponse Response { get; set; }

        public LMSweb.Models.DefaultOption DefaultOption { get; set; }

    }
    public class Evalution_Response
    {
        public int qid { get; set; }


        [Required]
        public string response { get; set; }
        public string comments { get; set; }
        public string sid { get; set; }

    }

    public class Group_Response
    {
        public int qid { get; set; }


        [Required]
        public string response { get; set; }
        public string comments { get; set; }
        public string gid { get; set; }

    }
}