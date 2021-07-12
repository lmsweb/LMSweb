using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMSweb.Models;

namespace LMSweb.Controllers
{
    public class KnowledgePointsController : Controller
    {
        private LMSmodel db = new LMSmodel();

        // GET: KnowledgePoints
        public ActionResult Index()
        {
            return View(db.KnowledgePoints.ToList());
        }

        // GET: KnowledgePoints/Details/5
        public ActionResult Details(int? id)
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

        // GET: KnowledgePoints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KnowledgePoints/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KID,KContent")] KnowledgePoint knowledgePoint)
        {
            if (ModelState.IsValid)
            {
                db.KnowledgePoints.Add(knowledgePoint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(knowledgePoint);
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
