using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class ColLevel
    {
        [Key]
        [Display(Name = "互動合作級別")]
        public int col_level { get; set; }

        [Display(Name = "級別含意")]
        public int col_means { get; set; }

        [Display(Name = "級別積分")]
        public int col_score { get; set; }
    }
}