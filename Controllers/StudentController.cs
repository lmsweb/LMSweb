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
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private LMSmodel db = new LMSmodel();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
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
                return View("Login");
            }
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Student")]
        // GET: Student
        public ActionResult StudentHomePage()
        {
            
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();   //抓出當初記載Claims陣列中的SID
            var sid = claimData[0].Value; //取值(因為只有一筆)
           
            var studentCourse = db.Students.Where(s => s.SID == sid);
            StudentHomePageViewModel vmodel = new StudentHomePageViewModel();
            var course = db.Students.Find(sid).CID;
            vmodel.CID = course;
            vmodel.CName = db.Courses.Find(course).CName;
            vmodel.TName = db.Courses.Find(course).teacher.TName;

            return View(vmodel);
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
            model.CName = course.CName;
          
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
        [HttpGet]
        public ActionResult StudentCoding(string mid, string cid)
        {

            StudentCodingViewModel model = new StudentCodingViewModel();
            model.CID = cid;
            model.MID = mid;

            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();   //抓出當初記載Claims陣列中的SID
            var sid = claimData[0].Value;
            var group = db.Students.Find(sid).group;
            model.GID = group.GID;
            
            return View(model);
        }
        [HttpPost]
        public ActionResult StudentDrawing(HttpPostedFileBase file, string cid, string mid)
        {
            MissionViewModel model = new MissionViewModel();
            model.CID = cid;
            model.MID = mid;

            if (file != null && file.ContentLength > 0)
            {
                string FileName = Path.GetFileName(file.FileName);
                string FilePath = Path.Combine(Server.MapPath(WebConfigurationManager.AppSettings["ImagesPath"]), FileName);
                //string FilePath = @"H:\Microsoft Visual Studio\newLMS\LMSweb\UploadImages\";
                file.SaveAs(FilePath);
            }

            return View(model);
        }
        public ActionResult ShowImage(string id)
        {
            var dir = Server.MapPath("/Images");
            var path = Path.Combine(dir, id + ".jpg");
            return base.File(path, "image/jpg");
        }

        //[AcceptVerbs(HttpVerbs.Get)]
        //[OutputCache(CacheProfile = "CustomerImages")]
        //public FileResult Show(int customerId, string imageName)
        //{
        //    var path = string.Concat(ConfigData.ImagesDirectory, customerId, @"\", imageName);
        //    return new FileStreamResult(new FileStream(path, FileMode.Open), "image/jpeg");
        //}

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
            var gid = stuG.GID;
            var gname = stuG.GName;
            model.GID = gid;
            model.GName = gname;
            model.SID = sid;

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
        
        [HttpGet]
        public ActionResult StudentGoalSetting(string mid, string cid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var response = db.Responses.Where(r => r.SID == SID);
            var qids = response.Select(r => r.QID).ToList();
            var questions = db.Questions.Where(q => qids.Contains(q.QID) && q.Class == "目標設置" && q.MID == mid).ToList();
            if(questions.Any())
            {
                return RedirectToAction("StudentGoal", "Student", new { cid , mid });
            }
            else
            {
                GoalSettingViewModel goalSetVM = new GoalSettingViewModel();
                goalSetVM.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "目標設置").Include(q => q.Options);
                goalSetVM.MID = mid;
                goalSetVM.CID = cid;

                return View(goalSetVM);
            }
           
        }

        [HttpPost]
        public ActionResult StudentGoalSetting([System.Web.Http.FromBody] GoalSettingViewModel goalSetting)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            
            foreach (var qr in goalSetting.QRs)
            {
                var response = new Response();
                response.QID = qr.qid;
                response.Answer = qr.response;
                response.SID = SID;
                db.Responses.Add(response);
            }
            db.SaveChanges();
            //return RedirectToAction("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID });
            return Json(new { redirectToUrl = Url.Action("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID })});
            //return Redirect("Index", );
        }

        public ActionResult StudentGoal(GoalSettingViewModel goalSetting , string cid, string mid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            
            goalSetting.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "目標設置").Include(q => q.Responses);
            goalSetting.MID = mid;
            goalSetting.CID = cid;


            return View(goalSetting);

            //return Json(new { redirectToUrl = Url.Action("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID }) });
        }



        [HttpGet]
        public ActionResult StudentReflection(string mid, string cid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var response = db.Responses.Where(r => r.SID == SID);
            var qids = response.Select(r => r.QID).ToList();
            var questions = db.Questions.Where(q => qids.Contains(q.QID) && q.Class == "自我反思" && q.MID == mid).ToList();
            if (questions.Any())
            {
                return RedirectToAction("StudentReflectionResult", "Student", new { cid, mid });
            }
            else
            {
                GoalSettingViewModel goalSetVM = new GoalSettingViewModel();
                goalSetVM.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "自我反思").Include(q => q.Options);
                goalSetVM.MID = mid;
                goalSetVM.CID = cid;

                return View(goalSetVM);
            }

        }
        [HttpPost]
        public ActionResult StudentReflection([System.Web.Http.FromBody] GoalSettingViewModel goalSetting)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;

            foreach (var qr in goalSetting.QRs)
            {
                var response = new Response();
                response.QID = qr.qid;
                response.Answer = qr.response;
                response.SID = SID;
                db.Responses.Add(response);
            }
            db.SaveChanges();
            //return RedirectToAction("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID });
            return Json(new { redirectToUrl = Url.Action("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID }) });
            //return Redirect("Index", );
        }
        public ActionResult StudentReflectionResult(GoalSettingViewModel goalSetting, string cid, string mid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;

            goalSetting.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "自我反思").Include(q => q.Responses);
            goalSetting.MID = mid;
            goalSetting.CID = cid;


            return View(goalSetting);

            //return Json(new { redirectToUrl = Url.Action("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID }) });
        }
        public ActionResult StudentSelfEvalution(string mid, string cid)
        {
            EvalutionViewModel selfEVM = new EvalutionViewModel();
            var Qclass = db.Questions.Where(q => q.MID == mid && (q.Class == "個人能力" || q.Class == "合作能力")).Include(q => q.Options);
            selfEVM.Questions = Qclass;
            selfEVM.MID = mid;
            selfEVM.CID = cid;

            return View(selfEVM);
        }
        public ActionResult StudentPeerEvalution(string mid, string cid)
        {
            EvalutionViewModel peerEVM = new EvalutionViewModel();
            var Qclass = db.Questions.Where(q => q.MID == mid && (q.Class == "個人能力" || q.Class == "合作能力")).Include(q => q.Options);
            peerEVM.Questions = Qclass;
            peerEVM.MID = mid;
            peerEVM.CID = cid;

            return View(peerEVM);
        }
    }
}
