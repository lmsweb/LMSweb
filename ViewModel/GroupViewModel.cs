﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb.ViewModel
{
    public class GroupViewModel
    {
        public string CID { get; set; }
        public int GID { get; set; }
        public string MID { get; set; }
        public string MName { get; set; }
        public string SID { get; set; }
        public string SName { get; set; }
        public string CName { get; set; }
        public string GName { get; set; }
        public string DrawingImgPath { get; set; }
        public string CodeText { get; set; }
        public bool IsDiscuss { get; set; }
        public List<LMSweb.Models.StudentCode> IsUploadCode { get; set; }
        public List<LMSweb.Models.StudentDraw> IsUploadDraw { get; set; }
        public List<LMSweb.Models.Group> Groups { get; set; }
        public List<LMSweb.Models.Course> Courses { get; set; }
        public List<LMSweb.Models.GroupER> GroupER { get; set; }
        public List<LMSweb.Models.EvalutionResponse> EvalutionResponse { get; set; }
        public LMSweb.Models.TeacherAssessment TeacherAssessment { get; set; }
        public LMSweb.Models.PeerAssessment PeerAssessment { get; set; }
        public LMSweb.Models.Group Group { get; set; }
        public LMSweb.Models.Course Course{ get; set; }
        public LMSweb.Models.Student Student { get; set; }
        public List<LMSweb.Models.Student> Students { get; set; }
        public List<LMSweb.Models.PeerAssessment> PeerAssessments { get; set; }

    }
}