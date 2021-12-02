using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class Course
    {
        public Course()
        {
            Missions = new HashSet<Mission>();
            Students = new HashSet<Student>();
        }

        [Key]
        //[Column(Order = 0)]
        [Display(Name = "課程編號")]
        public string CID { get; set; }


        [Required]
        [Display(Name = "課程名稱")]
        public string CName { get; set; }

        //[Key]
        //[Column(Order = 1)]
        
        public string TID { get; set; }
        public virtual Teacher teacher { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }

        public string SID { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<LearningBehavior> LearningBehaviors { get; set; }

    }
}