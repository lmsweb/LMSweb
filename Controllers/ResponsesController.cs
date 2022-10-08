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
    public class ResponsesController : Controller
    {
        private LMSmodel db = new LMSmodel();

        public ActionResult Index()
        {
            var responses = db.Responses.Include(r => r.DefaultQuestion).Include(r => r.Student);

            return View(responses.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }

            return View(response);
        }

        public ActionResult Create()
        {
            ViewBag.QID = new SelectList(db.Questions, "QID", "Type");
            ViewBag.SID = new SelectList(db.Students, "SID", "SName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RID,QID,Answer,SID")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Responses.Add(response);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.QID = new SelectList(db.Questions, "QID", "Type", response.DQID);
            ViewBag.SID = new SelectList(db.Students, "SID", "SName", response.SID);

            return View(response);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }
            ViewBag.QID = new SelectList(db.Questions, "QID", "Type", response.DQID);
            ViewBag.SID = new SelectList(db.Students, "SID", "SName", response.SID);

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RID,QID,Answer,SID")] Response response)
        {
            if (ModelState.IsValid)
            {
                db.Entry(response).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QID = new SelectList(db.Questions, "QID", "Type", response.DQID);
            ViewBag.SID = new SelectList(db.Students, "SID", "SName", response.SID);

            return View(response);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Response response = db.Responses.Find(id);
            if (response == null)
            {
                return HttpNotFound();
            }

            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Response response = db.Responses.Find(id);

            db.Responses.Remove(response);
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
