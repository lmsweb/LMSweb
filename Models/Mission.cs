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

        [Display(Name = "加入後設認知")]
        public bool AddMetacognition { get; set; }

        [Display(Name = "討論權重")]
        public int discuss_k { get; set; }

        [Display(Name = "規劃權重")]
        public int chart_k { get; set; }

        [Display(Name = "撰寫權重")]
        public int code_k { get; set; }

        [Display(Name = "互動合作分數權重")]
        public int eva_k { get; set; }

        [Display(Name = "個人分數權重")]
        public int per_k {get;set;}

        [Display(Name = "小組分數權重")]
        public int group_k { get; set; }

        [Display(Name = "知識點")]
        public string relatedKP { get; set; }

        [Display(Name = "課程編號")]
        public string CID { get; set; }

        public virtual Course course { get; set; }

        //public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<StudentMission> StudentMissions { get; set; }
        
    }
}