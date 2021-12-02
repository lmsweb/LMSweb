using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class PerLevel
    {
        [Key]
        [Display(Name = "個人貢獻度級別")]
        public int per_level { get; set; }

        [Display(Name = "級別含意")]
        public int per_means { get; set; }

        [Display(Name = "級別積分")]
        public int per_score { get; set; }
    }
}