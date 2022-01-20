using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class GroupViewModel
    {
        public string CID { get; set; }
        public string GID { get; set; }
        public string MID { get; set; }
        public List<LMSweb.Models.Group> Groups { get; set; }
        public LMSweb.Models.TeacherAssessment TeacherAssessment { get; set; }
    }
}