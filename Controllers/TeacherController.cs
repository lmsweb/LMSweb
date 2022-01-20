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
using System.Security.Claims;

namespace LMSweb.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private LMSmodel db = new LMSmodel();


        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel login)
        {
            var result = db.Teachers.Where(x => x.TID == login.ID && x.TPassword == login.Password).FirstOrDefault(); //驗證
            if (result != null) //資料庫有資料(這個人)
            {
                //授權

                // 建立使用者的登入資訊
                ClaimsIdentity identity = new ClaimsIdentity(new[] {
                    //加入使用者的相關資訊
                    new Claim(ClaimTypes.Role, "Teacher"),
                    new Claim(ClaimTypes.Name, result.TName),
                    new Claim("TID",result.TID)
                }, "Teacher");

                Request.GetOwinContext().Authentication.SignIn(identity); //授權(登入)

                return RedirectToAction("TeacherHomePage", "Teacher");　
            }
            else
            {
                ModelState.AddModelError("", "輸入的帳密可能有誤或是沒有註冊");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult TeacherHomePage()
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "TID").ToList();   //抓出當初記載Claims陣列中的TID
            var tid = claimData[0].Value; //取值(因為只有一筆)
            var courses = db.Courses.Where(c => c.TID == tid);


            return View(courses);
        }
        // GET: Teacher
        //public ActionResult Index()
        //{
        //    ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
        //    var claimData = claims.Claims.Where(x => x.Type == "TID").ToList();   //抓出當初記載Claims陣列中的TID
        //    var tid = claimData[0].Value; //取值(因為只有一筆)
        //    var courses = db.Courses.Where(c => c.TID == tid);
        //    return View(courses);
        //    //return View(db.Courses.ToList());
        //}

        // GET: Teacher/Details/5
        public ActionResult Details(string cid)
        {
            if (cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(cid);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            ViewBag.TID = new SelectList(db.Teachers, "TID", "TName");
            return View();
            // return View();
        }

        // POST: Teacher/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CID,TID,CName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("TeacherHomePage");
            }

            ViewBag.TID = new SelectList(db.Teachers, "TID", "TName", course.TID);
            return View(course);
        }

        // GET: Teacher/Edit/5
        public ActionResult Edit(string cid)
        {
            if (cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(cid);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.TID = new SelectList(db.Teachers, "TID", "TName", course.TID);
            return View(course);
        }

        // POST: Teacher/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CID,TID,CName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TID = new SelectList(db.Teachers, "TID", "TName", course.TID);
            return View(course);
        }

        // GET: Teacher/Delete/5
        public ActionResult Delete(string cid)
        {
            if (cid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(cid);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string cid)
        {
            Course course = db.Courses.Find(cid);
            db.Courses.Remove(course);
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
