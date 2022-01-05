using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMSweb.Models;
using LMSweb.ViewModel;


namespace LMSweb.Controllers
{
    public class KnowledgePointsController : Controller
    {
        private LMSmodel db = new LMSmodel();
        
        // GET: KnowledgePoints
        public ActionResult Index(string CID)
        {
            KPViewModel kpmodel = new KPViewModel();
            kpmodel.CID = CID;
            kpmodel.knowledgePoints = db.KnowledgePoints.ToList();
            return View(kpmodel);
        }

        // GET: KnowledgePoints/Details/5
        public ActionResult Details(string cid)
        {
            if (cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgePoint knowledgePoint = db.KnowledgePoints.Find(cid);
            if (knowledgePoint == null)
            {
                return HttpNotFound();
            }
            return View(knowledgePoint);
        }

        // GET: KnowledgePoints/Create
        public ActionResult Create(string cid)
        {
            var kpvm = new KPViewModel();
            kpvm.CID = cid;

            return View(kpvm);
        }

        // POST: KnowledgePoints/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KPViewModel kpvm)
        {
            if (ModelState.IsValid)
            {
                var kp = db.KnowledgePoints.Add(kpvm.knowledgePoint);
                //kp.CID = kpvm.CID;
              
                db.SaveChanges();
                return RedirectToAction("Index", new { cid = kpvm.CID });
            }
            var vmodel = new KPViewModel();
            vmodel.CID = kpvm.CID;
            return View(vmodel);
        }

        // GET: KnowledgePoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgePoint knowledgePoint = db.KnowledgePoints.Find(id);
            if (knowledgePoint == null)
            {
                return HttpNotFound();
            }
            return View(knowledgePoint);
        }

        // POST: KnowledgePoints/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KID,KContent")] KnowledgePoint knowledgePoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knowledgePoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(knowledgePoint);
        }

        // GET: KnowledgePoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgePoint knowledgePoint = db.KnowledgePoints.Find(id);
            if (knowledgePoint == null)
            {
                return HttpNotFound();
            }
            return View(knowledgePoint);
        }

        // POST: KnowledgePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KnowledgePoint knowledgePoint = db.KnowledgePoints.Find(id);
            db.KnowledgePoints.Remove(knowledgePoint);
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
    }
}
