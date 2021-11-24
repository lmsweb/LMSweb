using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LMSweb.Models;

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
        public IEnumerable<LMSweb.Models.KnowledgePoint> kps { get; set; }
        public LMSweb.Models.KnowledgePoint knowledgePoint { get; set; }
        //public LMSweb.Models.Student s { get; set; }
    }

    public class MissionViewModel : CourseLayoutViewModelbase
    {
        public IEnumerable<LMSweb.Models.Mission> missions { get; set; }
        public IEnumerable<LMSweb.Models.Prompt> prompts { get; set; }

        public LMSweb.Models.Mission mis { get; set; }
        public LMSweb.Models.Prompt prompt { get; set; }
    }
    //public class TeacherAssessmentViewModel : CourseLayoutViewModelbase
    //{
    //    public IEnumerable<LMSweb.Models.TeacherAssessment> teacherAssessments { get; set; }
    //}

    public class LearnBViewModel : CourseLayoutViewModelbase
    {
        public List<Student> student { get; set; }

        public List<Mission> missions { get; set; }

        public List<StudentMission> studentmissions { get; set; }

        public List<LearningBehavior> learningbehavior { get; set; }

        public List<Group> group { get; set; }

    }


}