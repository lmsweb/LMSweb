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

        // GET: TeacherAssessments
        public ActionResult Index()
        {
            return View(db.TeacherA.ToList());
        }

        public ActionResult CheckMission(string gid)
        {
            //TeacherAssessment teacherAssessment = db.TeacherA.Where(ta => ta.GID == gid).ToList();
            return View(db.TeacherA);
        }
        // GET: TeacherAssessments/Details/5
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

        // GET: TeacherAssessments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TeacherAssessments/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
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

        // GET: TeacherAssessments/Edit/5
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

        // POST: TeacherAssessments/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
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

        // GET: TeacherAssessments/Delete/5
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

        // POST: TeacherAssessments/Delete/5
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
