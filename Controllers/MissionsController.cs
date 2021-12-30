using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMSweb.ViewModel;

namespace LMSweb.Models
{
    public class MissionsController : Controller
    {
        private LMSmodel db = new LMSmodel();

        // GET: Missions
        public ActionResult Index(string cid)
        {
            MissionViewModel model = new MissionViewModel();
            if (cid == null)
            {
                model.missions = db.Missions.Include(m => m.course);
                return View(model);
            }
            var course = db.Courses.Where(c => c.CID == cid).Single();
            model.missions = db.Missions.Where(m => m.CID == cid).Include(m => m.course);
            model.CID = course.CID;
            //model.CName = course.CName;
            return View(model);
        }

        // GET: Missions/Details/5
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
            model.CID = mission.course.CID;
            //model.CName = mission.course.CName;
            model.mis = mission;

            return View(model);
        }
        private IEnumerable<SelectListItem> GetKnowledge(IEnumerable<int> SelectKnowledgeList=null)
        {
            return new MultiSelectList(db.KnowledgePoints, "KID", "KContent", SelectKnowledgeList);
        }

        // GET: Missions/Create
        [HttpGet]
        public ActionResult Create(string cid)
        {
            var model = new MissionCreateViewModel();
            model.KnowledgeList = GetKnowledge();
            model.CID = cid;
           
            
            return View(model);
        }

        // POST: Missions/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MID,Start,End,MName,MDetail,CID")] Mission mission)
        //{
        //    System.Diagnostics.Debug.WriteLine("9487");

        //    if (ModelState.IsValid)
        //    {
        //        db.Missions.Add(mission);
        //        db.SaveChanges();
        //        return RedirectToAction("KP_Create");
        //    }

        //    ViewBag.CID = new SelectList(db.Courses, "CID", "CName", mission.CID);
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MissionCreateViewModel model)
            {

            //foreach (int kid in model.SelectKnowledgeList)
            //{
            //    System.Diagnostics.Debug.WriteLine(kid);
            //}

            if (ModelState.IsValid)
            {
                var mission = db.Missions.Add(model.mission);
                mission.CID = model.CID;
                mission.KnowledgePoints = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();
                
                //var KPs = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID));
                //foreach (var kp in KPs)
                //{
                //    mission.KnowledgePoints.Add(kp);
                //    //kp.Missions.Add(mission);
                //}
                //mission.Prompts = db.Prompts.Where(p => model.SelectPromptList.ToList().Contains(p.PID)).ToList();
                db.SaveChanges();
                return RedirectToAction("Index", new { cid = model.CID});   
            }
            var vmodel = new MissionCreateViewModel();
            vmodel.KnowledgeList = GetKnowledge();
            vmodel.CID = model.CID;
           
            vmodel.mission.MID = model.mission.MID;
            
            return View(vmodel);
        }

        // GET: Missions/Edit/5
        public ActionResult Edit(string mid)
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
            vmodel.KnowledgeList = GetKnowledge(mission.KnowledgePoints.Select(kp => kp.KID));
            //vmodel.PromptList = GetPrompt(mission.Prompts.Select(p => p.PID));
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

                db.Entry(mission).State = EntityState.Modified;

                mission = db.Missions.Include(kp => kp.KnowledgePoints).Single(m => m.MID == mission.MID);

                mission.KnowledgePoints = db.KnowledgePoints.Where(x => model.SelectKnowledgeList.ToList().Contains(x.KID)).ToList();
                mission.CID = model.CID;

                

                //mission = db.Missions.Include(p => p.Prompts).Single(m => m.MID == mission.MID);

                //mission.Prompts = db.Prompts.Where(pt => model.SelectPromptList.ToList().Contains(pt.PID)).ToList();
                db.SaveChanges();

                return RedirectToAction("Index", new { cid = model.CID});
            }

            model.KnowledgeList = GetKnowledge(model.SelectKnowledgeList);
            model.CID = mission.course.CID;
           
            //model.PromptList = GetPrompt(model.SelectPromptList);
            return View(model);
        }

        // GET: Missions/Delete/5
        public ActionResult Delete(string mid)
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
            model.CID = mission.course.CID;
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
            

            model.mission.MID = mission.MID + "_Copy_" + mission.CID;
            model.mission.Start = mission.Start;
            model.mission.End = mission.End;
            model.mission.MName = mission.MName;
            db.Missions.Add(model.mission);
            model.mission.MDetail = mission.MDetail;
            model.mission.KnowledgePoints  = mission.KnowledgePoints;
           
            model.mission.CID = cid;
            model.mission.course = db.Courses.Find(cid);

            
            
            db.SaveChanges();

            return RedirectToAction("Index", new { cid = cid });
        }
    }
}
