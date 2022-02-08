using LMSweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSweb.ViewModel
{
    public class GroupCreateViewModel
    {
        public string CID { get; set; }
        public string CName { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public IEnumerable<string> SelectStudentList { get; set; }
        public IEnumerable<Student> students { get; set; }
        public LMSweb.Models.Group group { get; set; }
        public List<Group> GroupList { get; set; }
        public List<Student> SList { get; set; }
        public IEnumerable<Group> groups { get; set; }
    }
}