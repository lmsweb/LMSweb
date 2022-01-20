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
        [Key]
        [Display(Name = "組別編號")]
        public int GID { get; set; }
 
        [Required]
        [Display(Name = "組別名稱")]
        public string GName { get; set; }

        public string CID { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<TeacherAssessment> TeacherA { get; set; }
        //public virtual ICollection<GroupMission> GroupMissions { get; set; }

    }
}