using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class StudentGroup
    {
        [Key]
        [Column(Order = 0)]
        [Display(Name = "學生編號")]
        public string SID { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "組別編號")]
        public string MID { get; set; }

        public virtual Student Student { get; set; }
        public virtual Mission Mission { get; set; }
    }
}