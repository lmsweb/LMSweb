using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
            KnowledgePoints = new HashSet<KnowledgePoint>();
            Missions = new HashSet<Mission>();
            LearningBehaviors = new HashSet<LearningBehavior>();
            StudentCode = new HashSet<StudentCode>();
        }

        [Key]
        [Display(Name = "課程編號")]
        public string CID { get; set; }

        [Required]
        [Display(Name = "課程名稱")]
        public string CName { get; set; }

        public string CreateTime { get; set; }

        [Required]
        public string TID { get; set; }
        public virtual Teacher teacher { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<LearningBehavior> LearningBehaviors { get; set; }
        public virtual ICollection<KnowledgePoint> KnowledgePoints { get; set; }

        public virtual ICollection<StudentCode> StudentCode { get; set; }
    }
}