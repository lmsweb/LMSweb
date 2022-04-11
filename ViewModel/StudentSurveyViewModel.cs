using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class StudentSurveyViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public string MID { get; set; }
        public string MName { get; set; }
        public List<LMSweb.Models.Student> Students { get; set; }
        public List<LMSweb.Models.Response> gsResponses { get; set; } //目標設置
        public List<LMSweb.Models.Response> reResponses { get; set; } //自我反思
        public List<LMSweb.Models.EvalutionResponse> eResponses { get; set; } //自平戶平
        public List<LMSweb.Models.GroupER> gResponses { get; set; } //組間互評


        public List<LMSweb.Models.DefaultQuestion> DefaultQuestions { get; set; }
        public bool IsGoalSetting { get; set; }
        public bool IsSPEvalution { get; set; }
        public bool IsGroupEvalution { get; set; }
        public bool IsReflection { get; set; }
    }
}