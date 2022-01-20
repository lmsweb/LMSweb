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
    public class GroupsController : Controller
    {
        private LMSmodel db = new LMSmodel();

        // GET: Groups
        public ActionResult Index(string cid)
        {

            var gmodel = new GroupViewModel();
            gmodel.CID = cid;
            gmodel.Groups = db.Groups.Where(g => g.CID == cid).ToList();
            return View(gmodel);
        }
        public ActionResult CheckCoding(string gid, string cid)
        {
            GroupViewModel model = new GroupViewModel();
            model.CID = cid;
            model.GID = gid;
            return View(model);
        }
        public ActionResult CheckDrawing(string gid, string cid)
        {
            GroupViewModel model = new GroupViewModel();
            model.CID = cid;
            model.GID = gid;
            return View(model);
        }
        public ActionResult Assessment(string gid, string cid)
        {
            GroupViewModel model = new GroupViewModel();
            model.CID = cid;
            model.GID = gid;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TEID,TeacherA,GroupAchievementLevel,GID")] TeacherAssessment teacherAssessment)
        {
            if (ModelState.IsValid)
            {
                db.TeacherA.Add(teacherAssessment);
                db.SaveChanges();
                return new HttpStatusCodeResult(200);
            }

            return View(teacherAssessment);
        }
        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Groups/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "GName")] Group group)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Groups.Add(group);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(group);
        //}

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GID,GName")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            

            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
