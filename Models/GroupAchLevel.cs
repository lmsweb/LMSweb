using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class GroupAchLevel
    {
        [Key]
        [Display(Name = "小組成果級別")]
        public int groupAch_level { get; set; }

        [Display(Name = "級別積分")]
        public int groupAc_score { get; set; }
    }
}