using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class SelfAssessment
    {
        [Key]
        //[Column(Order = 0)]
        [Display(Name = "編號")]
        public int SEID { get; set; }

        [Display(Name = "互動合作級別")]
        public int CooperationLevel { get; set; }

        [Display(Name = "個人貢獻度級別")]
        public int PersonalContributionLevel { get; set; }

        [Display(Name = "自我評價")]
        public int SelfA { get; set; }

        ////[Key]
        ////[Column(Order = 1)]
        //[Display(Name = "任務編號")]
        //public string MID { get; set; }

        ////[Key]
        ////[Column(Order = 2)]
        //[Display(Name = "學生編號")]
        //public string SID { get; set; }

        //public virtual Mission mission { get; set; }

        //public virtual Student student { get; set; }
        //public virtual StudentMission studentMission { get; set; }
    }
}