using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LMSweb.ViewModel
{
    public abstract class CourseLayoutViewModelbase
    {
        [Required]
        public string CID { get; set; }

        [Required]
        public string CName { get; set; }



        //public IEnumerable<LMSweb.Models.Mission> missions { get; set; }

    }

    public class CourseViewModel : CourseLayoutViewModelbase
    {
        public IEnumerable<LMSweb.Models.Student> students { get; set; }
        //public LMSweb.Models.Student s { get; set; }
    }

    public class MissionViewModel : CourseLayoutViewModelbase
    {
        public IEnumerable<LMSweb.Models.Mission> missions { get; set; }
        public IEnumerable<LMSweb.Models.Prompt> prompts { get; set; }

        public LMSweb.Models.Mission mis { get; set; }
        public LMSweb.Models.Prompt prompt { get; set; }
    }
}