using LMSweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class StudentReflectionEditViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public string MID { get; set; }
        public string MName { get; set; }
        public string SID { get; set; }
        public bool IsDiscuss { get; set; }

        public List<Question_ResponseEdit> QRs { get; set; }

        public IEnumerable<DefaultQuestion> DefaultQuestions { get; set; }
        public List<LMSweb.Models.Response> ReflectionAswer { get; set; }


    }
    public class Question_ResponseEdit
    {
        public int qid { get; set; }

        public string response { get; set; }

        public string mid { get; set; }
    }
}
