using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using LMSweb.Models;
using LMSweb.ViewModel;

namespace LMSweb.Controllers
{
    public class LearningBehaviorsController : Controller
    {
        private LMSmodel db = new LMSmodel();
        LearnBViewModel vm = new LearnBViewModel();
        public ActionResult StudentJourney(string cid, string mid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity;
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();
            var sid = claimData[0].Value;
            var stu = db.Students.Where(s => s.SID == sid);
            var stuG = db.Students.Find(sid).group;
            var sname = db.Students.Find(sid).SName;
            var gqid = 1002.ToString();
            vm.Groups = db.Groups.Where(g => g.CID == cid).ToList();
            vm.SID = sid;
            vm.GID = stuG.GID;
            vm.MID = mid;
            var gid = stuG.GID.ToString();
            vm.SName = sname;
            vm.TeacherER = db.GroupERs.Where(sg => sg.GID == stuG.GID && sg.MID == mid).ToList();
            vm.ClassER = db.GroupERs.ToList();
            vm.missions = db.Missions.Where(m => m.CID == cid).ToList();           
            vm.Teachercourse = db.Groups.Where(c => c.CID == cid).Count();
            vm.CID = cid;
            var TeacherCor = db.GroupERs.Where(c => c.GQID == 1 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var TeacherLogi = db.GroupERs.Where(c => c.GQID == 2 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));            
            var TeacherRead = db.GroupERs.Where(c => c.GQID == 3 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var TeacherScore = ((TeacherCor * 0.4) + (TeacherLogi * 0.4) + (TeacherRead * 0.2));
            vm.TeacherScore = Math.Round(TeacherScore, 1, MidpointRounding.ToEven);
            var classTeacherCor = db.GroupERs.Where(c => c.GQID == 1 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherLogi = db.GroupERs.Where(c => c.GQID == 2 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherRead = db.GroupERs.Where(c => c.GQID == 3 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherScore = ((classTeacherCor * 0.4) + (classTeacherLogi * 0.4) + (classTeacherRead * 0.2));
            vm.classTeacherScore = Math.Round(classTeacherScore, 1, MidpointRounding.ToEven);
            return View(vm);
        }
        public ActionResult StudentEvalutionJourney(string cid, string mid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity;
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();
            var sid = claimData[0].Value;
            var stu = db.Students.Where(s => s.SID == sid);
            var stuG = db.Students.Find(sid).group;
            var sname = db.Students.Find(sid).SName;
            var gqid = 1002.ToString();
            vm.Groups = db.Groups.Where(g => g.CID == cid).ToList();
            vm.SID = sid;
            vm.GID = stuG.GID;
            vm.MID = mid;
            var gid = stuG.GID;
            vm.SName = sname;
            vm.TeacherER = db.GroupERs.Where(sg => sg.GID == stuG.GID).ToList();
            vm.ClassER = db.GroupERs.ToList();
            vm.Teachercourse = db.Groups.Where(c => c.CID == cid).Count();
            vm.missions = db.Missions.Where(m => m.CID == cid).ToList();
            vm.PeerER = db.EvalutionResponse.Where(p => p.CID == cid && p.SID == sid && p.EvaluatorSID != sid && p.MID == mid).ToList();
            vm.GPeerER = db.EvalutionResponse.Where(p => p.CID == cid && p.SID != p.EvaluatorSID  && p.MID == mid).ToList();
            vm.SelfER = db.EvalutionResponse.Where(p => p.CID == cid && p.SID == sid && p.EvaluatorSID == sid && p.MID == mid).ToList();
            vm.GSelfER = db.EvalutionResponse.Where(p => p.CID == cid && p.SID == p.EvaluatorSID && p.MID == mid).ToList();
            vm.student = db.Students.Where(g => g.group.GID == gid).ToList();
            //小組成果 教師
            vm.CID = cid;
            var TeacherCor = db.GroupERs.Where(c => c.GQID == 1 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var TeacherLogi = db.GroupERs.Where(c => c.GQID == 2 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var TeacherRead = db.GroupERs.Where(c => c.GQID == 3 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var TeacherScore = ((TeacherCor * 0.4) + (TeacherLogi * 0.4) + (TeacherRead * 0.2));
            vm.TeacherScore = Math.Round(TeacherScore, 1, MidpointRounding.ToEven);
            var classTeacherCor = db.GroupERs.Where(c => c.GQID == 1 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherLogi = db.GroupERs.Where(c => c.GQID == 2 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherRead = db.GroupERs.Where(c => c.GQID == 3 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherScore = ((classTeacherCor * 0.4) + (classTeacherLogi * 0.4) + (classTeacherRead * 0.2));
            vm.classTeacherScore = Math.Round(classTeacherScore, 1, MidpointRounding.ToEven);
            //小組成果 組間
            var GroupCor = db.GroupERs.Where(c => c.GQID == 1 && c.EvaluatorSID != "T004" && c.EvaluatorSID != "T001" && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var GroupLogi = db.GroupERs.Where(c => c.GQID == 2 && c.EvaluatorSID != "T004" && c.EvaluatorSID != "T001" && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var GroupRead = db.GroupERs.Where(c => c.GQID == 3 && c.EvaluatorSID != "T004" && c.EvaluatorSID != "T001" && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var GroupScore = ((GroupCor * 0.4) + (GroupLogi * 0.4) + (GroupRead * 0.2));
            vm.GroupScore = Math.Round(GroupScore, 1, MidpointRounding.ToEven);
            var classGroupCor = db.GroupERs.Where(c => c.GQID == 1 && c.EvaluatorSID != "T004" && c.EvaluatorSID != "T001" && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classGroupLogi = db.GroupERs.Where(c => c.GQID == 2 && c.EvaluatorSID != "T004" && c.EvaluatorSID != "T001" && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classGroupRead = db.GroupERs.Where(c => c.GQID == 3 && c.EvaluatorSID != "T004" && c.EvaluatorSID != "T001" && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classGroupScore = ((classGroupCor * 0.4) + (classGroupLogi * 0.4) + (classGroupRead * 0.2));
            vm.classGroupScore = Math.Round(classGroupScore, 1, MidpointRounding.ToEven);
            vm.GroupComments = db.GroupERs.Where(sg => sg.GQID == 4 && sg.GID == stuG.GID && sg.MID == mid && sg.EvaluatorSID != "T004" && sg.EvaluatorSID != "T001").ToList();
            //自我評價
            var SelfDiscuss = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 20 && p.SID == sid && p.EvaluatorSID == sid && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.SelfDiscuss = Math.Round(SelfDiscuss, 1, MidpointRounding.ToEven);
            var SelfDraw = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 21 && p.SID == sid && p.EvaluatorSID == sid && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.SelfDraw = Math.Round(SelfDraw, 1, MidpointRounding.ToEven);
            var SelfCode = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 22 && p.SID == sid && p.EvaluatorSID == sid && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.SelfCode = Math.Round(SelfCode, 1, MidpointRounding.ToEven);
            var SelfContribute = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 23 && p.SID == sid && p.EvaluatorSID == sid && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.SelfContribute = Math.Round(SelfContribute, 1, MidpointRounding.ToEven);
            var classSelfDiscuss = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 20 && p.SID == p.EvaluatorSID && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.classSelfDiscuss = Math.Round(classSelfDiscuss, 1, MidpointRounding.ToEven);
            var classSelfDraw = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 21 && p.SID ==  p.EvaluatorSID && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.classSelfDraw = Math.Round(classSelfDraw, 1, MidpointRounding.ToEven);
            var classSelfCode = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 22 && p.SID == p.EvaluatorSID && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.classSelfCode = Math.Round(classSelfCode, 1, MidpointRounding.ToEven);
            var classSelfContribute = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 23 && p.SID == p.EvaluatorSID && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.classSelfContribute = Math.Round(classSelfContribute, 1, MidpointRounding.ToEven);
            //同儕互評
            var PeerDiscuss = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 20 && p.SID != sid && p.EvaluatorSID == sid && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.PeerDiscuss = Math.Round(PeerDiscuss, 1, MidpointRounding.ToEven);
            var PeerDraw = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 21 && p.SID != sid && p.EvaluatorSID == sid && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.PeerDraw = Math.Round(PeerDraw, 1, MidpointRounding.ToEven);
            var PeerCode = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 22 && p.SID != sid && p.EvaluatorSID == sid && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.PeerCode = Math.Round(PeerCode, 1, MidpointRounding.ToEven);
            var PeerContribute = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 23 && p.SID != sid && p.EvaluatorSID == sid && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.PeerContribute = Math.Round(PeerContribute, 1, MidpointRounding.ToEven);
            var classPeerDiscuss = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 20 && p.SID != p.EvaluatorSID && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.classPeerDiscuss = Math.Round(classPeerDiscuss, 1, MidpointRounding.ToEven);
            var classPeerDraw = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 21 && p.SID != p.EvaluatorSID && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.classPeerDraw = Math.Round(classPeerDraw, 1, MidpointRounding.ToEven);
            var classPeerCode = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 22 && p.SID != p.EvaluatorSID && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.classPeerCode = Math.Round(classPeerCode, 1, MidpointRounding.ToEven);
            var classPeerContribute = db.EvalutionResponse.Where(p => p.CID == cid && p.DQID == 23 && p.SID != p.EvaluatorSID && p.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            vm.classPeerContribute = Math.Round(classPeerContribute, 1, MidpointRounding.ToEven);

            return View(vm);
        }
        public ActionResult TeacherJourney(string cid,string mid,string sid)
        {          
            var stu = db.Students.Where(s => s.SID == sid);
            var stuG = db.Students.Find(sid).group;
            var sname = db.Students.Find(sid).SName;
            var gqid = 1002.ToString();
            vm.Groups = db.Groups.Where(g => g.CID == cid).ToList();
            vm.SID = sid;
            vm.GID = stuG.GID;
            vm.MID = mid;
            var gid = stuG.GID.ToString();
            vm.SName = sname;
            vm.TeacherER = db.GroupERs.Where(sg => sg.GID == stuG.GID && sg.MID == mid).ToList();
            vm.ClassER = db.GroupERs.ToList();
            vm.missions = db.Missions.Where(m => m.CID == cid).ToList();
            vm.Teachercourse = db.Groups.Where(c => c.CID == cid).Count();
            vm.CID = cid;
            var TeacherCor = db.GroupERs.Where(c => c.GQID == 1 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var TeacherLogi = db.GroupERs.Where(c => c.GQID == 2 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var TeacherRead = db.GroupERs.Where(c => c.GQID == 3 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.GID == stuG.GID && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var TeacherScore = ((TeacherCor * 0.4) + (TeacherLogi * 0.4) + (TeacherRead * 0.2));
            vm.TeacherScore = Math.Round(TeacherScore, 1, MidpointRounding.ToEven);
            var classTeacherCor = db.GroupERs.Where(c => c.GQID == 1 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherLogi = db.GroupERs.Where(c => c.GQID == 2 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherRead = db.GroupERs.Where(c => c.GQID == 3 && (c.EvaluatorSID == "T004" || c.EvaluatorSID == "T001") && c.MID == mid).ToList().Average(t => Convert.ToInt32(t.Answer));
            var classTeacherScore = ((classTeacherCor * 0.4) + (classTeacherLogi * 0.4) + (classTeacherRead * 0.2));
            vm.classTeacherScore = Math.Round(classTeacherScore, 1, MidpointRounding.ToEven);
            return View(vm);
            //var gqid = 1002.ToString();
            //vm.Groups = db.Groups.Where(g => g.CID == cid).ToList();
            //vm.TeacherER = db.GroupERs.ToList();
            //vm.ClassER = db.GroupERs.ToList();
            //vm.missions = db.Missions.Where(m => m.CID == cid).ToList();
            //vm.group = db.Groups.Where(g => g.CID == cid).ToList();
            //vm.Teachercourse = db.Groups.Where(c => c.CID == cid).Count();
            //return View(vm);
        }
        public ActionResult TeacherEvalutionJourney(string cid)
        {
            var gqid = 1002.ToString();
            vm.Groups = db.Groups.Where(g => g.CID == cid).ToList();
            vm.TeacherER = db.GroupERs.ToList();
            vm.ClassER = db.GroupERs.ToList();
            //var Teachercourse = 0;
            //var classcourse = 0;
            ////classcourse = classcourse / db.Groups.Where(c => c.CID == cid).Count();
            //vm.Classcourse = classcourse;
            //vm.Teachercourse = Teachercourse;
            SelectList selectGid = new SelectList(db.Groups.Where(c => c.CID == cid).ToList(), "GID", "GName");
            vm.missions = db.Missions.Where(m => m.CID == cid).ToList();
            ViewBag.SelectList = selectGid;
            vm.Teachercourse = db.Groups.Where(c => c.CID == cid).Count();
            return View(vm);
        }
        public ActionResult PersonalJourney(string CID)
        {
            //var results = db.LearnB.Where(j => j.StudentMissions.SID == "S001");
            //ViewBag.Studemt = db.LearnB.Where(s => s.StudentMissions.SID == "S001");
            //產生ViewModel物件
            vm.CID = CID;
           // vm.learningbehavior = db.LearnB.Where(s => s.StudentMissions.SID == "S001").ToList();(因修改Model所以註解)
            return View(vm);
        }
        public ActionResult Chat()
        {         
            return View();
        }
        [HttpPost]
        public JsonResult Studsnts(int gid)
        {
            var GID = gid.ToString();
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();

            if (!string.IsNullOrWhiteSpace(GID))
            {
                var orders = this.GetStu(gid);
                if (orders.Count() > 0)
                {
                    foreach (var order in orders)
                    {
                        items.Add(new KeyValuePair<string, string>(
                            order.SID,
                            string.Format("{0}", order.SName)));
                    }
                }
            }
            return this.Json(items);
        }
        private IEnumerable<Student> GetStu(int gid)
        {
            var GID = gid.ToString();
            using (LMSmodel db = new LMSmodel())
            {
                var query = db.Students.Where(x => x.group.GID == gid).OrderBy(x => x.SName);
                return query.ToList();
            }
        }
    }
}
