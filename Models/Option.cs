using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    public class Option
    {
        [Key]
        public string OID { get; set; }
        public string QID { get; set; }

        public virtual Question Question { get; set; }

        public string Options { get; set; }
    }
}