using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class StudentViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public LMSweb.Models.Student student { get; set; }
    }
}