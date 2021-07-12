using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMSweb.Models
{
    public class Student
    {
        public Student()
        {
            Groups = new HashSet<Group>();
            SelfAssessments = new HashSet<SelfAssessment>();
            PeerAssessments = new HashSet<PeerAssessment>();
            LearnB = new HashSet<LearningBehavior>();
        }

        [Key]
        [Column(Order = 0)]
        [Display(Name = "學生編號")]
        public string SID { get; set; }


        [Required]
        [Display(Name = "學生姓名")]
        public string SName { get; set; }


        [Required]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string SPassword { get; set; }


        [Required]
        [Display(Name = "性別")]
        public string Sex { get; set; }


        [Required]
        [Display(Name = "教育階段")]
        public string Stage { get; set; }


        [Required]
        [Display(Name = "年級")]
        public string Grade { get; set; }

        [Display(Name = "積分")]
        public string Score { get; set; }

        [Key]
        [Column(Order = 1)]
        [Display(Name = "課程編號")]
        public string CID { get; set; }
        public virtual Course Course { get; set; }

        [Display(Name = "組別編號")]
        public string GID { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<SelfAssessment> SelfAssessments { get; set; }
        public virtual ICollection<PeerAssessment> PeerAssessments { get; set; }
        public virtual ICollection<LearningBehavior> LearnB { get; set; }
    }
}