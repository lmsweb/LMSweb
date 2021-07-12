using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class Prompt
    {
        [Key]
        //[Column(Order = 0)]
        [Display(Name = "編號")]
        public int PID { get; set; }


        [Required]
        [Display(Name = "提示內容")]
        public string PContent { get; set; }


        [Display(Name = "任務編號")]
        public string MID { get; set; }
        public virtual Mission Mission { get; set; }
    }
}