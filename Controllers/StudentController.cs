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
using System.IO;
using Newtonsoft.Json;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;
using LMSweb.Infrastructure.Helpers;


namespace LMSweb.Controllers
{
    [Authorize]
    public class StudentController : Controller
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
            var result = db.Students.Where(x => x.SID == login.ID && x.SPassword == login.Password).FirstOrDefault(); //驗證
            if (result != null) //資料庫有資料(這個人)
            {
                //授權

                // 建立使用者的登入資訊
                ClaimsIdentity identity = new ClaimsIdentity(new[] {
                    //加入使用者的相關資訊
                    new Claim(ClaimTypes.Role, "Student"),
                    new Claim(ClaimTypes.Name, result.SName),
                    new Claim("SID",result.SID)
                }, "Student");

                Request.GetOwinContext().Authentication.SignIn(identity); //授權(登入)

                return RedirectToAction("StudentHomePage", "Student");
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

        // GET: Student
        public ActionResult StudentHomePage()
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();   //抓出當初記載Claims陣列中的SID
            var sid = claimData[0].Value; //取值(因為只有一筆)

            var studentCourse = db.Students.Where(s => s.SID == sid);


            return View(studentCourse);
            //return View(db.Students.ToList());
        }
        public ActionResult StudentCourse(string cid)
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
            CourseViewModel model = new CourseViewModel();
            model.CID = course.CID;
            model.CName = course.CName;

            return View(model);
        }
        public ActionResult StudentMission(string cid)
        {
            MissionViewModel model = new MissionViewModel();
            if (cid == null)
            {
                model.missions = db.Missions.Where(m => m.CID == cid);
                return View(model);
            }
            var course = db.Courses.Single(c => c.CID == cid);
            model.missions = db.Missions.Where(m => m.CID == cid);
            model.CID = course.CID;
          
            return View(model);
        }
        public ActionResult StudentMissionDetail(string cid, string mid)
        {
            if (mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(mid);
            if (mission == null)
            {
                return HttpNotFound();
            }
            var model = new MissionViewModel();
            model.CID = mission.CID;
            model.MID = mid;
            //model.CName = mission.course.CName;
            var kps = mission.relatedKP.Split(',');
            model.KContents = new List<string>();
            for (int i = 0; i < kps.Length - 1; i++)
            {
                model.KContents.Add(db.KnowledgePoints.Find(int.Parse(kps[i])).KContent);
            }
            model.mis = mission;
            model.CName = mission.course.CName;

            return View(model);
        }
        public ActionResult StudentMDetailWindow(string cid, string mid)
        {
            if (mid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Missions.Find(mid);
            if (mission == null)
            {
                return HttpNotFound();
            }
            var model = new MissionViewModel();
            model.CID = mission.CID;
            model.MID = mid;
            //model.CName = mission.course.CName;
            var kps = mission.relatedKP.Split(',');
            model.KContents = new List<string>();
            for (int i = 0; i < kps.Length - 1; i++)
            {
                model.KContents.Add(db.KnowledgePoints.Find(int.Parse(kps[i])).KContent);
            }
            model.mis = mission;

            return View(model);
        }
        public ActionResult StudentCoding(string mid, string cid)
        {
            MissionViewModel model = new MissionViewModel();
            model.CID = cid;
            model.MID = mid;
            return View(model);
        }
        public ActionResult StudentDrawing(string mid, string cid)
        {
            MissionViewModel model = new MissionViewModel();
            model.CID = cid;
            model.MID = mid;
            return View(model);
        }
        public ActionResult Chat(string cid, string mid)
        {
            MissionViewModel model = new MissionViewModel();
            var mission = db.Missions.Single(m => m.MID == mid);
            model.CID = cid;
            model.MID = mid;
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();   //抓出當初記載Claims陣列中的SID
            var sid = claimData[0].Value;
            var stu = db.Students.Where(s => s.SID == sid);
            var stuG = db.Students.Find(sid).group;
            var gname = stuG.GName;
            model.GName = gname;
            model.SID = sid;
            //ViewBag.CID = new SelectList(db.Courses, "CID", "CName", mission.CID);

            return View(model);
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
