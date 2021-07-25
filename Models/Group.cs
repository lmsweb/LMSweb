using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
            TeacherA = new HashSet<TeacherAssessment>();
        }

        [Key]
        [Display(Name = "組別編號")]
        public int GID { get; set; }
 
        [Required]
        [Display(Name = "組別名稱")]
        public string GName { get; set; }

        [Display(Name = "職位")]
        public string Position { get; set; }

        [Display(Name = "任務編號")]
        public string MID { get; set; }
        public virtual Mission mission { get; set; }

        //[Display(Name = "學生編號")]
        //public string SID { get; set; }
        //public virtual Student Student { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<TeacherAssessment> TeacherA { get; set; }
    }
}