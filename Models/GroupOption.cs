using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    public class GroupOption
    {
        [Key]
        public int GOID { get; set; }
        public int GQID { get; set; }
        public virtual GroupQuestion GroupQuestion { get; set; }

        public string OptionNum { get; set; }
    }
}