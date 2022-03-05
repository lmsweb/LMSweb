using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    public class DefaultQuestion
    {
        [Key]
        public string DQID { get; set; }

        [Display(Name = "題型代號")]
        public string Type { get; set; }

        [Display(Name = "問題描述")]
        public string Description { get; set; }

        public virtual ICollection<DefaultOption> DefaultOptions { get; set; }

    }
}