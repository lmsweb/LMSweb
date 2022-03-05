using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class MissionViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public string MID { get; set; }
        public string SID { get; set; }
        public int GID { get; set; }
        public string GName { get; set; }
        public IEnumerable<LMSweb.Models.Mission> missions { get; set; }
        public LMSweb.Models.Mission mis { get; set; }
        public IEnumerable<LMSweb.Models.LearningBehavior> learningbehaviors { get; set; }
        public LMSweb.Models.LearningBehavior lbr { get; set; }
        public List<string> KContents { get; set; }
    }
}