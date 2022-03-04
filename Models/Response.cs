using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    public class Response
    {
        [Key]
        public string RID { get; set; }
        public string QID { get; set; }
        public virtual Question Question { get; set; }

        public string Answer { get; set;}
        public string SID { get; set; }
        public virtual Student Student { get; set; }
    }
}