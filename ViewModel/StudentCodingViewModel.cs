using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class StudentCodingViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public string MID { get; set; }
        public int GID { get; set; }
        public string CodePath { get; set; }
        public bool IsEdit { get; set; }
        public string CodeContent { get; set; }
    }
}