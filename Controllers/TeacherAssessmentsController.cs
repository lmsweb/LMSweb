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
    public class TeacherAssessmentsController : Controller
    {
        private LMSmodel db = new LMSmodel();
        public ActionResult Index()
        {
            return View(db.TeacherA.ToList());
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherAssessment teacherAssessment = db.TeacherA.Find(id);
            if (teacherAssessment == null)
            {
                return HttpNotFound();
            }

            return View(teacherAssessment);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TEID,TeacherA,GroupAchievementLevel,GID")] TeacherAssessment teacherAssessment)
        {
            if (ModelState.IsValid)
            {
                db.TeacherA.Add(teacherAssessment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(teacherAssessment);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherAssessment teacherAssessment = db.TeacherA.Find(id);
            if (teacherAssessment == null)
            {
                return HttpNotFound();
            }

            return View(teacherAssessment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TEID,TeacherA,GroupAchievementLevel,GID")] TeacherAssessment teacherAssessment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherAssessment).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(teacherAssessment);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherAssessment teacherAssessment = db.TeacherA.Find(id);
            if (teacherAssessment == null)
            {
                return HttpNotFound();
            }

            return View(teacherAssessment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherAssessment teacherAssessment = db.TeacherA.Find(id);

            db.TeacherA.Remove(teacherAssessment);
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
