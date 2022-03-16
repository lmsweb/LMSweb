using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class StudentHomePageViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public string TName { get; set; }
        public string GName { get; set; }
        public bool Enter { get; set; }
        public List<LMSweb.Models.Group> Groups { get; set; }
    }
}