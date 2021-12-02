using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class Mission
    {
        public Mission()
        {
            //Groups = new HashSet<Group>();
            KnowledgePoints = new HashSet<KnowledgePoint>();
            Prompts = new HashSet<Prompt>();
            //SelfAssessments = new HashSet<SelfAssessment>();
            //PeerAssessments = new HashSet<PeerAssessment>();
            //LearnB = new HashSet<LearningBehavior>();
            //TeacherA = new HashSet<TeacherAssessment>();
        }

        [Key]

        [Display(Name = "任務編號")]
        public string MID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "開始時間")]
        public string Start { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "結束時間")]
        public string End { get; set; }

        [Required]
        [Display(Name = "任務名稱")]
        public string MName { get; set; }


        [Required]
        [Display(Name = "任務內容")]
        public string MDetail { get; set; }


        [Display(Name = "討論權重")]
        public int discuss_k { get; set; }

        [Display(Name = "規劃權重")]
        public int chart_k { get; set; }

        [Display(Name = "撰寫權重")]
        public int code_k { get; set; }

        [Display(Name = "互評分數權重")]
        public int eva_k { get; set; }

        [Display(Name = "個人分數權重")]
        public int per_k {get;set;}


        [Display(Name = "課程編號")]
        public string CID { get; set; }
        public virtual Course course { get; set; }


        //public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        [Display(Name = "知識點")]
        public virtual ICollection<KnowledgePoint> KnowledgePoints { get; set; }

        [Display(Name = "提示")]
        public virtual ICollection<Prompt> Prompts { get; set; }
        //public virtual ICollection<SelfAssessment> SelfAssessments { get; set; }
        //public virtual ICollection<PeerAssessment> PeerAssessments { get; set; }
        //public virtual ICollection<LearningBehavior> LearnB { get; set; }
        //public virtual ICollection<TeacherAssessment> TeacherA { get; set; }
    }
}