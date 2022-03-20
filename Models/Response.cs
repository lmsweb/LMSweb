using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    [MetadataType(typeof(Response))]
    public class Response
    {
        [Key]
        public int RID { get; set; }
        public int DQID { get; set; }
        public virtual DefaultQuestion DefaultQuestion { get; set; }

        
        public string Answer { get; set;}
        
        public string SID { get; set; }
        public virtual Student Student { get; set; }
        public string MID { get; set; }

        public virtual Mission mission { get; set; }
        public string CID { get; set; }
        public virtual Course Course { get; set; }
    }
}