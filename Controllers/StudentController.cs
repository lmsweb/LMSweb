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

                return RedirectToAction("Index", "Course");
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
        public ActionResult Index()
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();   //抓出當初記載Claims陣列中的SID
            var sid = claimData[0].Value; //取值(因為只有一筆)
            //var mission = ViewModel.MissionCreateViewModel.Missions.Where(m => m.SID == sid);
            //return View(mission);
            return View(db.Students.ToList());
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
