using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class LearningBehavior
    {
        [Key]
        //[Column(Order = 0)]
        [Display(Name = "編號")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "動作類型")]
        public string ActionType { get; set; }

        [Required]
        [Display(Name = "動作子項")]
        public string subAction { get; set; }

        [Required]
        [Display(Name = "動作內容")]
        public string Detail { get; set; }

        public string Time { get; set; }

        ////[Key]
        ////[Column(Order = 1)]
        //public string MID { get; set; }

        ////[Key]
        ////[Column(Order = 2)]
        //public string SID { get; set; }

        ////[Key]
        ////[Column(Order = 3)]
        //public string GID { get; set; }


        //public virtual Mission Mission { get; set; }

        //public virtual Student Student { get; set; }
        public virtual StudentMission StudentMissions { get; set; }
    }
}