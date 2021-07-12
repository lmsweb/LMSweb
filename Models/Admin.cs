using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LMSweb.Models
{
    public class Admin
    {
        [Key]
        [Display(Name = "管理者編號")]
        public string AID { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string APassword { get; set; }

        [Display(Name = "管理者姓名")]
        public string AName { get; set; }
    }
}