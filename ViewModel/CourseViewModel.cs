using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LMSweb.Models;

namespace LMSweb.ViewModel
{
    public abstract class CourseLayoutViewModelbase
    {
        [Required]
        public string CID { get; set; }

        [Required]
        public string CName { get; set; }

    }

    public class CourseViewModel : CourseLayoutViewModelbase
    {
        public IEnumerable<LMSweb.Models.Student> students { get; set; }
        public IEnumerable<LMSweb.Models.KnowledgePoint> kps { get; set; }
        public LMSweb.Models.KnowledgePoint knowledgePoint { get; set; }
        public LMSweb.Models.Course Course { get; set; }
    }
}