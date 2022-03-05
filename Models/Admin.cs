using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    public class Admin
    {
        [Key]
        public string AID { get; set; }

        public string APassword { get; set; }
        public string AName { get; set; }

    }
}