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
        [Display(Name = "教師評價")]
        public string TeacherA { get; set; }


        [Required]
        [Display(Name = "小組成果級別")]
        public int GroupAchievementLevel { get; set; }

        //[Key]
        //[Column(Order = 1)]
        [Display(Name = "任務編號")]
        public string MID { get; set; }

        //[Key]
        //[Column(Order = 2)]
        [Display(Name = "組別編號")]
        public string GID { get; set; }

        public virtual Mission Mission { get; set; }

        public virtual Group Group { get; set; }
    }
}