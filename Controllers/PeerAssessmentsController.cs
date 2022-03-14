using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using LMSweb.Models;
using LMSweb.ViewModel;

namespace LMSweb.Controllers
{
    public class PeerAssessmentsController : Controller
    {
        private LMSmodel db = new LMSmodel();

        // GET: PeerAssessments
        public ActionResult Index(string mid, string cid)
        {
            var gmodel = new GroupViewModel();   
            var mis = db.Missions.Find(mid);
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();   //抓出當初記載Claims陣列中的SID
            var sid = claimData[0].Value;
            var stu= db.Students.Where(s => s.SID == sid);
            var stuG = db.Students.Find(sid).group;
            var cname = db.Courses.Find(cid).CName;
            var pa = db.PeerA.SingleOrDefault(p => p.AssessedSID == sid && p.MID == mid);
            gmodel.Groups = db.Groups.Where(g => g.GID == stuG.GID).ToList();
            gmodel.PeerAssessment = pa;
            gmodel.CName = cname;
            gmodel.CID = cid;
            gmodel.MID = mid;
            return View(gmodel);
        }

        // GET: PeerAssessments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeerAssessment peerAssessment = db.PeerA.Find(id);
            if (peerAssessment == null)
            {
                return HttpNotFound();
            }
            return View(peerAssessment);
        }

        // GET: PeerAssessments/Create
        public ActionResult Create(string sid, string mid, int gid, string cid)
        {
            GroupViewModel model = new GroupViewModel();
            model.MID = mid;
            model.GID = gid;
            model.SID = sid;
            model.CID = cid;
            return View(model);
        }

        // POST: PeerAssessments/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PEID,PeerA,CooperationScore,AssessedSID,MID")] PeerAssessment peerAssessment, string mid, int gid, string cid)
        {
            if (ModelState.IsValid)
            {
                //peerAssessment.StudentMissions.MID = mid;
                
                db.PeerA.Add(peerAssessment);
                db.SaveChanges();
                var gmodel = new GroupViewModel();
                gmodel.MID = mid;
                gmodel.GID = gid;
                gmodel.CID = cid;
                return RedirectToAction("Index", gmodel);
            }

            return View(peerAssessment);
        }

        // GET: PeerAssessments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeerAssessment peerAssessment = db.PeerA.Find(id);
            if (peerAssessment == null)
            {
                return HttpNotFound();
            }
            return View(peerAssessment);
        }

        // POST: PeerAssessments/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PEID,PeerA,CooperationScore,AssessedSID")] PeerAssessment peerAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(peerAssessment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(peerAssessment);
        }

        // GET: PeerAssessments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PeerAssessment peerAssessment = db.PeerA.Find(id);
            if (peerAssessment == null)
            {
                return HttpNotFound();
            }
            return View(peerAssessment);
        }

        // POST: PeerAssessments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PeerAssessment peerAssessment = db.PeerA.Find(id);
            db.PeerA.Remove(peerAssessment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult SelfEvaluation()
        {
            return View();
        }

        public ActionResult PeerEvaluation()
        {
            return View();
        }
    }
}
