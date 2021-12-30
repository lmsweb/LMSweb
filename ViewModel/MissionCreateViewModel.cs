using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSweb.ViewModel
{
    public class MissionCreateViewModel
    {
        public string CID { get; set; }
        public IEnumerable<SelectListItem> KnowledgeList { get; set; }
        public IEnumerable<int> SelectKnowledgeList { get; set; }

        public LMSweb.Models.Mission mission { get; set; }

    }
}