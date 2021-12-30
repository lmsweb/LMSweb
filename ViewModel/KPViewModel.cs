using LMSweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class KPViewModel
    {
        public string CID { get; set; }
        public IEnumerable<LMSweb.Models.KnowledgePoint> kps { get; set; }
        public LMSweb.Models.KnowledgePoint knowledgePoint { get; set; }
        public List<KnowledgePoint> knowledgePoints { get; set; }
    }
}