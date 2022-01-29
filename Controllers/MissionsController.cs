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
            var course = db.Courses.Where(c => c.CID == cid).Single();
            model.missions = db.Missions.Where(m => m.CID == cid);
            model.CID = course.CID;

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
            

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MissionCreateViewModel model)
        {
            //var inputlist = model.SelectKnowledgeList.ToList();
            var kps = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();
            string kp_str = "";
            foreach (var kp in kps)
            {
                kp_str += kp.KID.ToString() + "," ;
            }

            //mission.relatedKP = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();

            model.mission.relatedKP = kp_str;
            model.mission.CID = model.CID;

            var n1 = model.mission.discuss_k;
            var n2 = model.mission.chart_k;
            var n3 = model.mission.code_k;
            var n = n1 + n2 + n3;
            var n1Score = (int)(((decimal)n1 / n) * 100);
            var n2Score = (int)(((decimal)n2 / n) * 100);
            var n3Score = (int)(((decimal)n3 / n) * 100);
            model.mission.discuss_k = n1Score;
            model.mission.chart_k = n2Score;
            model.mission.code_k = n3Score;

            var m1 = model.mission.eva_k;
            var m2 = model.mission.per_k;
            var m = m1 + m2;
            var m1Score = (int)(((decimal)m1 / m) * 100);
            var m2Score = (int)(((decimal)m2 / m) * 100);
            model.mission.eva_k = m1Score;
            model.mission.per_k = m2Score;

            if (ModelState.IsValid)
            {
                db.Missions.Add(model.mission);
                db.SaveChanges();
                return RedirectToAction("Index", new { cid = model.CID });
            }
            var vmodel = new MissionCreateViewModel();
            vmodel.KnowledgeList = GetKnowledge(vmodel.CID);
            vmodel.CID = model.CID;
            vmodel.mission.MID = model.mission.MID;

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
            //ViewBag.CID = new SelectList(db.Courses, "CID", "CName", mission.CID);


            var vmodel = new MissionCreateViewModel();
            vmodel.mission = mission;
            vmodel.KnowledgeList = GetKnowledge(cid, db.KnowledgePoints.Where(kp => kp.CID == mission.CID).Select(kp => kp.KID));
            
            vmodel.CID = mission.CID;
            //vmodel.CName = mission.course.CName;

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

                //db.Entry(mission).State = EntityState.Modified;

                //mission = db.Missions.Include(kp => kp.KnowledgePoints).Single(m => m.MID == mission.MID);

                var kps = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();
                string kp_str = "";
                foreach (var kp in kps)
                {
                    kp_str += kp.KID.ToString() + ",";
                }
                model.mission.relatedKP = kp_str;
                mission.CID = model.CID;

                

               

                //mission.Prompts = db.Prompts.Where(pt => model.SelectPromptList.ToList().Contains(pt.PID)).ToList();
                db.SaveChanges();

                return RedirectToAction("Index", new { cid = model.CID});
            }

            //model.KnowledgeList = GetKnowledge(model.SelectKnowledgeList);
            model.CID = mission.CID;
           
            //model.PromptList = GetPrompt(model.SelectPromptList);
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
            //model.CName = mission.course.CName;
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
        public ActionResult SelectMissions(string cid)
        {
            MissionViewModel model = new MissionViewModel();
            
            model.missions = db.Missions.ToList();
            model.CID = cid;
            
            return View(model);
        }

        public ActionResult AddMissions(string mid, string cid)
        {
            Mission mission = db.Missions.Find(mid);  //old
            //Mission m = new Mission();
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
            model.mission.MID = mission.MID + "_Copy_" + mission.CID;
            model.mission.Start = mission.Start;
            model.mission.End = mission.End;
            model.mission.MName = mission.MName;

            //db.Missions.Add(model.mission);
            model.mission.MDetail = mission.MDetail;
            model.mission.code_k = mission.code_k;
            model.mission.discuss_k = mission.discuss_k;
            model.mission.chart_k = mission.chart_k;
            model.mission.eva_k = mission.eva_k;
            model.mission.per_k = mission.per_k;
            model.KnowledgeList = GetKnowledge(cid);


            model.mission.CID = cid;
            model.CID = cid;
            //model.mission.CID = db.Courses.Find(cid);

            
            
            //db.SaveChanges();

            return View(model);
        }
    }
}
