using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    public class Question
    {
        [Key]
        public string QID { get; set; }

        [Display(Name = "題型代號")]
        public string Type {get;set;}

        [Display(Name = "問題描述")]
        public string Description { get; set; }

        
        public string CID { get; set; }
        public virtual Course course { get; set; }

        public string MID { get; set; }

        public virtual Mission mission { get; set; }

        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}