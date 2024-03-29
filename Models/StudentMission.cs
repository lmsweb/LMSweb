﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class StudentMission
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "學生")]
        public string SID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "任務編號")]
        public string MID { get; set; }

        public int total_score { get; set; }

        [Display(Name = "個人貢獻度分數")]
        public int PersonalScore { get; set; }

        [Display(Name = "自我評價")]
        public int SelfA { get; set; }
        public virtual Student Student { get; set; }
        public virtual Mission Mission { get; set; }

        // public virtual SelfAssessment SelfAssessment { get; set; }
        public virtual ICollection<PeerAssessment> PeerAssessments { get; set; }
        public virtual TeacherAssessment TeacherAssessment { get; set; }
        public virtual ICollection<LearningBehavior> LearnBs { get; set; }


    }
}