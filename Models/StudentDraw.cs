using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class StudentDraw
    {
        [Key]
        [Display(Name ="流程圖編號")]
        public int Id { get; set; }
        public string DrawingImgPath { get; set; }
        public int GID { get; set; }
        public virtual Group Group { get; set; }
        public string MID { get; set; }
        public virtual Mission Mission { get; set; }
        public string CID { get; set; }
        public virtual Course Course { get; set; }
    }
}