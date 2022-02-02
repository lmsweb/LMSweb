using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class TeacherAssessment
    {
        [Key]
        //[Column(Order = 0)]
        [Display(Name = "編號")]
        public int TEID { get; set; }

        [Required]
        [Display(Name = "教師評語")]
        public string TeacherA { get; set; }


        [Required]
        [Display(Name = "小組成果分數")]
        public int GroupAchievementScore { get; set; }

        public int GID { get; set; }
        public virtual Group Group { get; set; }
            
        public string MID { get; set; }
        public virtual Mission Mission { get; set; }

        public virtual ICollection<StudentMission> StudentMissions { get; set; }
    }
}