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
        public string DOID { get; set; }
        public string DQID { get; set; }

        public virtual DefaultQuestion DefaultQuestion { get; set; }

        public string DefaultOptions { get; set; }
    }
}