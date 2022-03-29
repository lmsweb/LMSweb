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
        public string CName { get; set; }
        public string MID { get; set; }
        public string MName { get; set; }
        public string SID { get; set; }
        public string SName { get; set; }
        public int GID { get; set; }
        public string GName { get; set; }
        public string EvaluatorSID { get; set; }
        public string TID { get; set; }
        public string TEvalution { get; set; }
        public string SEvalution { get; set; }
        public int Teachercourse { get; set; }
        public int Classcourse { get; set; }
        //public List<Chat_Achievement> CAs { get; set; }
        public List<Student> student { get; set; }
        public List<Mission> missions { get; set; }
        public List<StudentMission> studentmissions { get; set; }
        public List<LearningBehavior> learningbehavior { get; set; }
        public LMSweb.Models.LearningBehavior LearningBehavior { get; set; }
        public List<Group> group { get; set; }
        public IEnumerable<LMSweb.Models.DefaultQuestion> DefaultQuestion { get; set; }
        public IEnumerable<LMSweb.Models.GroupQuestion> GroupQuestion { get; set; }
        public LMSweb.Models.GroupER GroupER { get; set; }
        public List<LMSweb.Models.GroupER> TeacherER { get; set; }
        public List<LMSweb.Models.GroupER> ClassER { get; set; }
        public List<LMSweb.Models.GroupER> GroupComments { get; set; } // 組間評語
        public List<LMSweb.Models.EvalutionResponse> PeerER { get; set; }
        public List<LMSweb.Models.EvalutionResponse> GPeerER { get; set; }
        public List<LMSweb.Models.EvalutionResponse> SelfER { get; set; }
        public List<LMSweb.Models.EvalutionResponse> GSelfER { get; set; }
        public List<LMSweb.Models.EvalutionResponse> CGroupER { get; set; }//合作學習 小組平均
        public List<LMSweb.Models.EvalutionResponse> CClassER { get; set; }//合作學習 全班平均
        public LMSweb.Models.EvalutionResponse Response { get; set; }
        public LMSweb.Models.GroupOption GroupOption { get; set; }
        public List<LMSweb.Models.Group> Groups { get; set; }
        public List<LMSweb.Models.Course> Courses { get; set; }
        public LMSweb.Models.Group Group { get; set; }
        public LMSweb.Models.Course Course { get; set; }
        public LMSweb.Models.Student Student { get; set; }
        public List<LMSweb.Models.Student> Students { get; set; }
        public int GroupPeople { get; set; } //組內人數
        public int ClassPeople { get; set; } //班級人數
        /// <summary>
        /// 小組成果
        /// </summary>
        //教師分數
        
        public double TeacherScore { get; set; }       
        public double classTeacherScore { get; set; }
        //組間互評分數
        public double GroupScore { get; set; }
        public double classGroupScore { get; set; }
        /// <summary>
        /// 自我評價
        /// </summary>
        public double SelfCode { get; set; }
        public double SelfDraw { get; set; }
        public double SelfDiscuss { get; set; }
        public double SelfContribute { get; set; }
        public double groupSelfCode { get; set; }
        public double groupSelfDraw { get; set; }
        public double groupSelfDiscuss { get; set; }
        public double gorupSelfContribute { get; set; }
        public double classSelfCode { get; set; }
        public double classSelfDraw { get; set; }
        public double classSelfDiscuss { get; set; }
        public double classSelfContribute { get; set; }
        /// <summary>
        /// 同儕互評
        /// </summary>
        public double PeerCode { get; set; }
        public double PeerDraw { get; set; }
        public double PeerDiscuss { get; set; }
        public double PeerContribute { get; set; }
        public double groupPeerCode { get; set; }
        public double groupPeerDraw { get; set; }
        public double groupPeerDiscuss { get; set; }
        public double groupPeerContribute { get; set; }
        public double classPeerCode { get; set; }
        public double classPeerDraw { get; set; }
        public double classPeerDiscuss { get; set; }
        public double classPeerContribute { get; set; }
    }

    
}