using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSweb.ViewModel
{
    public class MissionCreateViewModel : CourseLayoutViewModelbase
    {
        public IEnumerable<SelectListItem> KnowledgeList { get; set; }
        public IEnumerable<int> SelectKnowledgeList { get; set; }
        //public IEnumerable<SelectListItem> PromptList { get; set; }
        //public IEnumerable<int> SelectPromptList { get; set; }

        public LMSweb.Models.Mission mission { get; set; }
        public LMSweb.Models.Prompt prompt { get; set; }

}
}