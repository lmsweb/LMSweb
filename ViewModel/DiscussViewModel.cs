using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LMSweb.Models;

namespace LMSweb.ViewModel
{
    public class DiscussViewModel
    {
        public string SID { get; set; }
        public string MID { get; set; }
        public string ActionType { get; set; }
        public string subAction { get; set; }
        public string Detail { get; set; }
        public string Time { get; set; }
    }
}