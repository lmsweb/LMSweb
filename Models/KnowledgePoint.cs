using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class KnowledgePoint
    {
        public KnowledgePoint()
        {
            Courses = new HashSet<Course>();
        }

        [Key]
        //[Column(Order = 0)]
        [Display(Name = "編號")]
        public int KID { get; set; }

        [Required]
        [Display(Name = "知識點")]
        public string KContent { get; set; }

        //[Key]
        //[Column(Order = 1)]
        //public string MID { get; set; }
        //public virtual Mission Mission { get; set; }


        public virtual ICollection<Course> Courses { get; set; }
    }
}