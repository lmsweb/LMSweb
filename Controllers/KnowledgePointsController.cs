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
 
        public ActionResult Create(string cid)
        {
            var model = new KnowledgePoint();
            model.CID = cid;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KnowledgePoint kpmodel)
        {
            if (ModelState.IsValid)
            {
                var kp = db.KnowledgePoints.Add(kpmodel);
                kp.CID = kpmodel.CID;
               
                db.SaveChanges();

                return RedirectToAction("Details", "Course", new { cid = kpmodel.CID });
            } 

            return View(kpmodel);
        }

        public ActionResult Edit(int? kid)
        {
            if (kid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgePoint knowledgePoint = db.KnowledgePoints.Find(kid);
            if (knowledgePoint == null)
            {
                return HttpNotFound();
            }

            return View(knowledgePoint);
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CID,KID,KContent")] KnowledgePoint knowledgePoint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knowledgePoint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Course", new { cid = knowledgePoint.CID });
            }
            return View(knowledgePoint);
        }

        // GET: KnowledgePoints/Delete/5
        public ActionResult Delete(int? kid)
        {
            if (kid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KnowledgePoint knowledgePoint = db.KnowledgePoints.Find(kid);
            if (knowledgePoint == null)
            {
                return HttpNotFound();
            }
            return View(knowledgePoint);
        }

        // POST: KnowledgePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? kid)
        {
            KnowledgePoint knowledgePoint = db.KnowledgePoints.Find(kid);
            db.KnowledgePoints.Remove(knowledgePoint);
            db.SaveChanges();
            return RedirectToAction("Details", "Course", new { cid = knowledgePoint.CID });
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
