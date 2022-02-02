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
        [Display(Name = "評語")]
        public string PeerA { get; set; }

        [Display(Name = "互動合作分數")]
        public int CooperationScore { get; set; }

        [Required]
        [Display(Name = "被評價學生")]
        public string AssessedSID { get; set; }

        public virtual StudentMission StudentMissions { get; set; }

    }
}