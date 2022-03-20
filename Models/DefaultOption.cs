using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    public class DefaultOption
    {
        [Key]
        public int DOID { get; set; }
        public int DQID { get; set; }

        public virtual DefaultQuestion DefaultQuestion { get; set; }

        [Display(Name = "作答")]
        public string DOptions { get; set; }
    }
}