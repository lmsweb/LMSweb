using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LMSweb.ViewModel;

namespace LMSweb.Models
{
    public class MissionsController : Controller
    {
        private LMSmodel db = new LMSmodel();
        public ActionResult Index(string cid)
        {
            MissionViewModel model = new MissionViewModel();
            if (cid == null)
            {
                model.missions = db.Missions.Where(m => m.CID == cid);
                return View(model);
            }
            var course = db.Courses.Single(c => c.CID == cid);
            model.missions = db.Missions.Where(m => m.CID == cid);
            model.CID = course.CID;
            model.CName = course.CName;
            return View(model);
        }
        public ActionResult Details(string mid)
        {
            if (mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(mid);
            if (mission == null)
            {
                return HttpNotFound();
            }
            var model = new MissionViewModel();
            model.CID = mission.CID;
            //model.CName = mission.course.CName;
            var kps = mission.relatedKP.Split(',');
            model.KContents = new List<string>();
            for(int i = 0; i < kps.Length - 1; i++)
            {
                model.KContents.Add(db.KnowledgePoints.Find(int.Parse(kps[i])).KContent);
            }
            model.mis = mission;
            model.MID = mission.MID;
            model.CName = mission.course.CName;

            return View(model);
        }

        public JsonResult GetKnowledgeJSON(string cid, IEnumerable<int> SelectKnowledgeList = null)
        {
             return Json(new { Data = new MultiSelectList(db.KnowledgePoints.Where(kp => kp.CID == cid), "KID", "KContent", SelectKnowledgeList) }, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<SelectListItem> GetKnowledge(string cid, IEnumerable<int> SelectKnowledgeList=null)
        {
            return new MultiSelectList(db.KnowledgePoints.Where(kp => kp.CID == cid), "KID", "KContent", SelectKnowledgeList);
        }
        [HttpGet]
        public ActionResult Create(string cid)
        {
            var model = new MissionCreateViewModel();
            model.KnowledgeList = GetKnowledge(cid);
            model.CID = cid;
            var course = db.Courses.Single(c => c.CID == cid);
            model.CName = course.CName;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MissionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var kps = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();
                string kp_str = "";
                foreach (var kp in kps)
                {
                    kp_str += kp.KID.ToString() + ",";
                }

                //mission.relatedKP = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();

                model.mission.relatedKP = kp_str;
                model.mission.CID = model.CID;
                db.Missions.Add(model.mission);
                db.SaveChanges();
                if (model.mission.AddMetacognition)
                {
                    return RedirectToAction("Index","Questions", new { cid = model.CID, mid = model.mission.MID});
                }
                else
                {
                    return RedirectToAction("Index", new { cid = model.CID });
                }
            }
            var vmodel = new MissionCreateViewModel();
            vmodel.KnowledgeList = GetKnowledge(model.CID);
            vmodel.CID = model.CID;
            var course = db.Courses.Single(c => c.CID == model.CID);
            model.CName = course.CName;

            return View(vmodel);
        }

        public ActionResult Edit(string mid, string cid)
        {
            if (mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(mid);
            if (mission == null)
            {
                return HttpNotFound();
            }
            var vmodel = new MissionCreateViewModel();
            vmodel.mission = mission;
            
            vmodel.KnowledgeList = GetKnowledge(cid, db.KnowledgePoints.Where(kp => kp.CID == mission.CID).Select(kp => kp.KID));
            
            vmodel.CID = mission.CID;
            vmodel.CName = mission.course.CName;

            return View(vmodel);
        }

        // POST: Missions/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MissionCreateViewModel model)
        {
            var mission = model.mission;
            
            if (ModelState.IsValid)
            {
                db.Entry(mission).State = EntityState.Modified;
                db.SaveChanges();

                var kps = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();
                string kp_str = "";
                foreach (var kp in kps)
                {
                    kp_str += kp.KID.ToString() + ",";
                }
                model.mission.relatedKP = kp_str;
                mission.CID = model.CID;

                db.SaveChanges();

                return RedirectToAction("Index", new { cid = model.CID});
            }

            //model.KnowledgeList = GetKnowledge(model.SelectKnowledgeList);
            model.CID = mission.CID;

            return View(model);
        }

        // GET: Missions/Delete/5
        public ActionResult Delete(string mid, string cid)
        {
            if (mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(mid);
            if (mission == null)
            {
                return HttpNotFound();
            }
            var model = new MissionViewModel();
            //model.mis.CID = mission.CID;
            model.CID = cid;
            model.CName = db.Courses.Find(cid).CName;
            model.mis = mission;

            return View(model);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string mid)
        {

            Mission mission = db.Missions.Find(mid);
            var cid = mission.CID;

            var learningBehavior = db.LearnB.Where(l => l.mission.MID == mid);
            var teacherA = db.TeacherA.Where(t => t.MID == mid);
            var question = db.Questions.Where(q => q.MID == mid);
            var peerA = db.PeerA.Where(p => p.MID == mid);
            var option = db.Options.Where(o => o.Question.MID == mid);
            var response = db.Responses.Where(r => r.Question.MID == mid);

            db.Questions.RemoveRange(question);
            db.LearnB.RemoveRange(learningBehavior);
            db.TeacherA.RemoveRange(teacherA);
            db.PeerA.RemoveRange(peerA);
            db.Responses.RemoveRange(response);
            db.Options.RemoveRange(option);

            db.Missions.Remove(mission);
            db.SaveChanges();
            return RedirectToAction("Index", new { cid = cid });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult SelectMissions(string cid, string missionCourse)
        {
            MissionViewModel model = new MissionViewModel();
            
            model.missions = db.Missions.ToList();
            model.CID = cid;
            var course = db.Courses.Single(c => c.CID == cid);
            model.CName = course.CName;

           
            return View(model);
        }

        public ActionResult AddMissions(string mid, string cid)
        {
            Mission mission = db.Missions.Find(mid);  //old
            
            var model = new MissionCreateViewModel();   //new

            model.mission = new Mission();

            //var kps = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();
            //string kp_str = "";
            //foreach (var kp in kps)
            //{
            //    kp_str += kp.KID.ToString() + ",";
            //}

            //mission.relatedKP = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();

            //model.mission.relatedKP = kp_str;
            model.mission.MID = mission.MID;
            model.mission.Start = mission.Start;
            model.mission.End = mission.End;
            model.mission.MName = mission.MName;

            //db.Missions.Add(model.mission);
            model.mission.MDetail = mission.MDetail;
            model.mission.discuss_k = mission.discuss_k;
            model.mission.per_k = mission.per_k;
            model.mission.group_k = mission.group_k;
            model.KnowledgeList = GetKnowledge(cid);

            model.mission.CID = cid;
            model.CID = cid;
            model.CName = mission.course.CName;
            //model.mission.CID = db.Courses.Find(cid);
           
            //db.SaveChanges();

            return View(model);
        }
        [HttpPost]
        public ActionResult SwitchDrawing(string mid, string cid, string type, bool sw)
        {
            Mission mission = db.Missions.Find(mid);
            db.Entry(mission).State = EntityState.Modified;
            if (type == "is_Coding")
            {
                mission.IsCoding = sw;
            }
            else if(type == "is_Discuss")
            {
                mission.IsDiscuss = sw;
            }
            else if(type == "is_Drawing")
            {
                mission.IsDrawing = sw;
            }
            else if (type == "is_Assess")
            {
                mission.IsAssess = sw;
            }

            db.SaveChanges();
            return Json(new { Status = HttpStatusCode.OK , type = type, sw = sw});
        }

    }
}
