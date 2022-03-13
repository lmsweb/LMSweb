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
    [Authorize(Roles = "Teacher")]
    public class GroupsController : Controller
    {
        private LMSmodel db = new LMSmodel();

        // G
        // ET: Groups
        public ActionResult Index(string mid, string cid)
        {
            var gmodel = new GroupViewModel();
            gmodel.MID = mid;
            var mis = db.Missions.Find(mid);
            gmodel.Groups = db.Groups.Where(g => mis.CID == g.CID).ToList();
            gmodel.CID = cid;
            gmodel.CName = mis.course.CName;
            var stu = db.Students.Where(s => s.group.CID == cid).ToList();

            return View(gmodel);
        }
        public ActionResult CheckCoding(int gid, string cid, string mid)
        {
            GroupViewModel model = new GroupViewModel();
            
            model.CID = cid;
            model.GID = gid;
            model.MID = mid;

            //ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            //var claimData = claims.Claims.Where(x => x.Type == "MID").ToList();   //抓出當初記載Claims陣列中的SID
            //var sid = claimData[0].Value;
            //var group = db.Students.Find(sid).group;
            //model.GID = group.GID;

            return View(model);
        }
        public ActionResult CheckDrawing(int gid, string cid)
        {
            GroupViewModel model = new GroupViewModel();
            model.CID = cid;
            model.GID = gid;
            return View(model);
        }
        public ActionResult Assessment(int gid, string mid, string cid)
        {
            GroupViewModel model = new GroupViewModel();
            var mis = db.Missions.Find(mid);
            model.MID = mid;
            model.GID = gid;
            model.CID = cid;

            var group = db.Groups.Single(g =>g.GID == gid);

            model.GName = group.GName;  
            model.CName = mis.course.CName;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupViewModel groupVM, string cid, int gid, string mid)
        {

            if (ModelState.IsValid)
            {
                groupVM.TeacherAssessment.GID = gid;
                groupVM.TeacherAssessment.MID = mid;
                groupVM.TeacherAssessment.CID = cid;
                db.TeacherA.Add(groupVM.TeacherAssessment);

                db.SaveChanges();
                groupVM.CID = cid;

                groupVM.Groups = db.Groups.Where(g => g.CID == cid).ToList();

                return View("Index", groupVM);
            }
            
           
            return View(groupVM);
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
       
        public ActionResult Edit(int? id, string cid, int gid, string mid)
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

            var groupVM = new GroupViewModel();
            groupVM.TeacherAssessment = teacherAssessment;
            groupVM.CID = cid;
            groupVM.MID = mid;
            groupVM.GID = gid;

            return View(groupVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GroupViewModel groupVM, string cid, int gid, string mid)
        {
            var teacherAssessment = groupVM.TeacherAssessment;
            teacherAssessment.MID = mid;
            teacherAssessment.GID = gid;
            teacherAssessment.CID = cid;
            if (ModelState.IsValid)
            {

                db.Entry(teacherAssessment).State = EntityState.Modified;
                db.SaveChanges();

                groupVM.CID = cid;
                groupVM.Groups = db.Groups.Where(g => g.CID == cid).ToList();

                return RedirectToAction("Index", groupVM);
            }
            return View(groupVM);
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
