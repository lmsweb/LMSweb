using LMSweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class LearnBViewModel
    {
        public string CID { get; set; }
        public List<Student> student { get; set; }

        public List<Mission> missions { get; set; }

        public List<StudentMission> studentmissions { get; set; }

        public List<LearningBehavior> learningbehavior { get; set; }
        public LMSweb.Models.LearningBehavior LearningBehavior { get; set; }

        public List<Group> group { get; set; }
    }
}