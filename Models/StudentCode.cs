using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class StudentCode
    {
        [Key]
        [Column(Order = 0)]
        public string CID { get; set; }
        public virtual Course course { get; set; }

        [Key]
        [Column(Order = 1)]
        public string MID { get; set; }

        public virtual Mission mission { get; set; }

        [Key]
        [Column(Order = 3)]
        public int GID { get; set; }

        public virtual Group group { get; set; }

        public string CodePath { get; set; }

        public bool IsEdit { get; set; }



    }
}