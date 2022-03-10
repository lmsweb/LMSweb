using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMSweb.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QID { get; set; }

        [Display(Name = "題型")]
        public string Type {get;set;}

        [Display(Name = "問卷分類")]
        public string Class { get; set; }  //目標設置、反思題目

        [Display(Name = "題目")]
        public string Description { get; set; }
        
        public string MID { get; set; }

        public virtual Mission mission { get; set; }

        [Display(Name = "選項")]
        public virtual ICollection<Option> Options { get; set; }
        public virtual ICollection<Response> Responses { get; set; }
    }
}