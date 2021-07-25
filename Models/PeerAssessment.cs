using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class PeerAssessment
    {
        [Key]
        //[Column(Order = 0)]
        [Display(Name = "編號")]
        public int PEID { get; set; }

        [Required]
        [Display(Name = "評價內容")]
        public string PeerA { get; set; }


        [Required]
        [Display(Name = "被評價學生")]
        public string AssessedSID { get; set; }

        ////[Key]
        ////[Column(Order = 1)]
        //[Display(Name = "任務編號")]
        //public string MID { get; set; }

        ////[Key]
        ////[Column(Order = 2)]
        //[Display(Name = "學生編號")]
        //public string SID { get; set; } 


        //public virtual Mission Mission { get; set; }

        //public virtual Student Student { get; set; }

        
        public virtual StudentMission StudentMissions { get; set; }
    }
}