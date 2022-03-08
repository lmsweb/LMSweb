using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModel
{
    [MetadataType(typeof(LoginViewModel))]
    public class LoginViewModel
    {
        [Required]
        public string ID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}