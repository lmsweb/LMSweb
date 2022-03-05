using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class StudentCodingViewModel
    {
        public string CID { get; set; }
        public string MID { get; set; }
        public int GID { get; set; }
        public string CODE_PATH { get; set; }
        public LMSweb.Models.StudentCode studentCode { get; set; }
    }
}