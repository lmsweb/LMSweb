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
            model.CID = cid;
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
            model.CID = cid;
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
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList();   //抓出當初記載Claims陣列中的SID
            var sid = claimData[0].Value;
            var group = db.Students.Find(sid).group;
            var cname = db.Courses.Find(cid).CName;
            var misChat = db.Missions.Find(mid).IsDiscuss;
            model.CID = cid;
            model.MID = mid;
            model.CName = cname;
            model.GID = group.GID;
            model.IsDiscuss = misChat;
            return View(model);
        }

        [HttpGet]
        public ActionResult StudentDrawing(string mid, string cid)
        {
            DrawingViewModel model = new DrawingViewModel();
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "SID").ToList(); //抓出當初記載Claims陣列中的SID
            var sid = claimData[0].Value;
            var stu = db.Students.Where(s => s.SID == sid);
            var stuG = db.Students.Find(sid).group;
            var gid = stuG.GID;
            var gname = stuG.GName;
            var cname = db.Courses.Find(cid).CName;
            model.CID = cid;
            model.MID = mid;
            model.GID = gid;
            model.GName = gname;
            model.CName = cname;

            return View(model);
        }

        private string imgfileSavedPath = WebConfigurationManager.AppSettings["ImagesPath"];
        [HttpPost]
        public ActionResult StudentDrawing(HttpPostedFileBase file, string cid, string mid)
        {
            DrawingViewModel model = new DrawingViewModel();
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

            if (file != null && file.ContentLength > 0)
            {
                StudentDraw studentDraw = new StudentDraw();
                //string FileName = string.Concat(
                //    (model.GName + "-" + model.MID),
                //    Path.GetExtension(file.FileName).ToLower());
                ////Path.GetFileName(file.FileName);

                //string virtualBaseFilePath = Url.Content(imgfileSavedPath);
                //string filePath = HttpContext.Server.MapPath(virtualBaseFilePath);

                ////string FilePath = Path.Combine(Server.MapPath(WebConfigurationManager.AppSettings["ImagesPath"]), FileName);
                //////string FilePath = @"H:\Microsoft Visual Studio\newLMS\LMSweb\UploadImages\";
                ////file.SaveAs(FilePath);
                //string fullFilePath = Path.Combine(Server.MapPath(imgfileSavedPath), FileName);
                //file.SaveAs(fullFilePath);
                string virtualBaseFilePath = Url.Content(imgfileSavedPath);
                string filePath = HttpContext.Server.MapPath(virtualBaseFilePath);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string newFileName = string.Concat(
                    (model.GName + "-" + model.MID),
                    Path.GetExtension(file.FileName).ToLower());

                string fullFilePath = Path.Combine(Server.MapPath(imgfileSavedPath), newFileName);
                file.SaveAs(fullFilePath);

                studentDraw.DrawingImgPath = newFileName;
                studentDraw.CID = cid;
                studentDraw.MID = mid;
                studentDraw.GID = gid;

                db.StudentDraws.Add(studentDraw);
                db.SaveChanges();
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
        public ActionResult StudentGoalSetting(string mid, string cid)    //學生任務內容裡面的目標設置btn的Action
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var response = db.Responses.Where(r => r.SID == SID);
            var qids = response.Select(r => r.QID).ToList();
            var questions = db.Questions.Where(q => qids.Contains(q.QID) && q.Class == "目標設置" && q.MID == mid).ToList();
            var cname = db.Courses.Find(cid).CName;
            if(questions.Any())
            {
                return RedirectToAction("StudentGoal", "Student", new { cid , mid, SID });
            }
            else
            {
                GoalSettingViewModel goalSetVM = new GoalSettingViewModel();
                goalSetVM.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "目標設置").Include(q => q.Options);
                goalSetVM.MID = mid;
                goalSetVM.CID = cid;
                goalSetVM.SID = SID;
                goalSetVM.CName = cname;

                return View(goalSetVM);
            }
           
        }

        [HttpPost]
        public ActionResult StudentGoalSetting([System.Web.Http.FromBody] GoalSettingViewModel goalSetting)  //填表單送出的Post
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

        public ActionResult StudentGoal(GoalSettingViewModel goalSetting , string cid, string mid, string SID)  ///學生已填過目標設置
        {
            var cname = db.Courses.Find(cid).CName;
            goalSetting.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "目標設置").Include(q => q.Responses);
            goalSetting.MID = mid;
            goalSetting.CID = cid;
            goalSetting.SID = SID;
            goalSetting.CName = cname;

            return View(goalSetting);

        }

        [HttpGet]
        public ActionResult StudentReflection(string mid, string cid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var response = db.Responses.Where(r => r.SID == SID);
            var qids = response.Select(r => r.QID).ToList();
            var questions = db.Questions.Where(q => qids.Contains(q.QID) && q.Class == "自我反思" && q.MID == mid).ToList();
            var cname = db.Courses.Find(cid).CName;
            if (questions.Any())
            {
                return RedirectToAction("StudentReflectionResult", "Student", new { cid, mid, SID });
            }
            else
            {
                GoalSettingViewModel goalSetVM = new GoalSettingViewModel();
                goalSetVM.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "自我反思").Include(q => q.Options);
                goalSetVM.MID = mid;
                goalSetVM.CID = cid;
                goalSetVM.CName = cname;

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
        }
        public ActionResult StudentReflectionResult(GoalSettingViewModel goalSetting, string cid, string mid, string SID)
        {
            var cname = db.Courses.Find(cid).CName;
            goalSetting.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "自我反思").Include(q => q.Responses);
            goalSetting.MID = mid;
            goalSetting.CID = cid;
            goalSetting.SID = SID;
            goalSetting.CName = cname;
            return View(goalSetting);
        }

        //以下自評互評
        [HttpGet]
        public ActionResult StudentSelfEvalution(string sid, string mid, string cid)//學生任務內容裡面的目標設置btn的Action
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var evalution = db.EvalutionResponse.Where(r => r.SID == SID && r.EvaluatorSID == SID );
            var qids = evalution.Select(r => r.QID).ToList();
            var questions = db.Questions.Where(q => qids.Contains(q.QID) && (q.Class == "個人能力"||q.Class == "合作能力") && q.MID == mid).ToList();
            var cname = db.Courses.Find(cid).CName;
            if (questions.Any())
            {
                return RedirectToAction("StudentSelfER", "Student", new { SID, cid, mid });
            }
            else
            {
                EvalutionViewModel SelfEVM = new EvalutionViewModel();
                SelfEVM.Questions = db.Questions.Where(q => q.MID == mid && (q.Class == "個人能力" || q.Class == "合作能力")).Include(q => q.Options);
                SelfEVM.MID = mid;
                SelfEVM.CID = cid;
                SelfEVM.SID = SID;
                SelfEVM.EvaluatorSID = SID;
                SelfEVM.CName = cname;

                return View(SelfEVM);
            }
        }
        [HttpPost]
        public ActionResult StudentSelfEvalution([System.Web.Http.FromBody] EvalutionViewModel evalution)  //填表單送出的Post
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;

            foreach (var qr in evalution.ERs)
            {
                var response = new EvalutionResponse();
                response.QID = qr.qid;
                response.Answer = qr.response;
                response.Comments = qr.comments;
                response.SID = SID;
                response.EvaluatorSID = SID;
                db.EvalutionResponse.Add(response);
            }
            db.SaveChanges();
            //return RedirectToAction("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID });
            return Json(new { redirectToUrl = Url.Action("StudentMissionDetail", "Student", new { cid = evalution.CID, mid = evalution.MID }) });
            //return Redirect("Index", );
        }
        public ActionResult StudentSelfER(EvalutionViewModel evalution, string sid, string cid, string mid)  ///學生已填過目標設置
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var cname = db.Courses.Find(cid).CName;

            evalution.Questions = db.Questions.Where(q => q.MID == mid && (q.Class == "個人能力"|| q.Class == "合作能力")).Include(q => q.EvalutionResponses);
            evalution.MID = mid;
            evalution.CID = cid;
            evalution.EvaluatorSID = SID;
            evalution.CName = cname;
            evalution.SID = SID;

            return View(evalution);
        }
        [HttpGet]
        public ActionResult StudentPeerEvalution(string sid,string mid, string cid, string gid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value; ;
            var evalution = db.EvalutionResponse.Where(r => r.SID == sid && r.EvaluatorSID == SID);
            var qids = evalution.Select(r => r.QID).ToList();
            var questions = db.Questions.Where(q => qids.Contains(q.QID) && (q.Class == "個人能力" || q.Class == "合作能力") && q.MID == mid).ToList();
            var cname = db.Courses.Find(cid).CName;
            if (questions.Any())
            {
                return RedirectToAction("StudentPeerER", "Student", new { sid,cid, mid, evasid = SID });
            }
            else
            {
                EvalutionViewModel PeerEVM = new EvalutionViewModel();
                PeerEVM.Questions = db.Questions.Where(q => q.MID == mid && (q.Class == "個人能力" || q.Class == "合作能力")).Include(q => q.Options);
                PeerEVM.MID = mid;
                PeerEVM.CID = cid;
                PeerEVM.SID = sid;
                PeerEVM.EvaluatorSID = SID;
                PeerEVM.CName = cname;

                return View(PeerEVM);
            }
        }
        [HttpPost]
        public ActionResult StudentPeerEvalution([System.Web.Http.FromBody] EvalutionViewModel evalution)  //填表單送出的Post
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var evaSID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var sid = evalution.SID;
            foreach (var qr in evalution.ERs)
            {
                var response = new EvalutionResponse();
                response.QID = qr.qid;
                response.Answer = qr.response;
                response.Comments = qr.comments;
                response.SID = sid;
                response.EvaluatorSID = evaSID;

                db.EvalutionResponse.Add(response);
            }
            db.SaveChanges();

            return Json(new { redirectToUrl = Url.Action("Index", "PeerAssessments", new { cid = evalution.CID, mid = evalution.MID }) });

        }
        public ActionResult StudentPeerER(EvalutionViewModel evalution,string sid, string cid, string mid, string evasid)  ///學生已填過目標設置
        {
            var cname = db.Courses.Find(cid).CName;
            evalution.Questions = db.Questions.Where(q => q.MID == mid && (q.Class == "個人能力" || q.Class == "合作能力")).Include(q => q.EvalutionResponses);
            evalution.MID = mid;
            evalution.CID = cid;
            evalution.SID = sid;
            evalution.EvaluatorSID = evasid;
            evalution.CName = cname;
            return View(evalution);           
        }
        [HttpGet]
        public ActionResult StudentGroupEvalution(string mid, string cid, int gid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var evalution = db.GroupERs.Where(r => r.GID == gid && r.EvaluatorSID == SID);
            var qids = evalution.Select(r => r.QID).ToList();
            var questions = db.DefaultQuestions.Where(q => qids.Contains(q.DQID) && q.Class == "組間互評").ToList();
            var cname = db.Courses.Find(cid).CName;
            if (questions.Any())
            {
                return RedirectToAction("StudentGroupER", "Student", new { cid, mid, gid });
            }
            else
            {
                EvalutionViewModel GroupEVM = new EvalutionViewModel();
                GroupEVM.DefaultQuestions = db.DefaultQuestions.Where(q => q.Class == "組間互評").Include(q => q.DefaultOptions);
                GroupEVM.MID = mid;
                GroupEVM.GID = gid;
                GroupEVM.CID = cid;
                GroupEVM.CName = cname;

                return View(GroupEVM);
            }
        }
        [HttpPost]
        public ActionResult StudentGroupEvalution([System.Web.Http.FromBody] EvalutionViewModel evalution, string cid,int gid)  //填表單送出的Post
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;

            foreach (var qr in evalution.GRs)
            {
                var response = new GroupER();
                response.QID = qr.qid;
                response.Answer = qr.response;
                response.Comments = qr.comments;
                response.GID = gid;
                response.EvaluatorSID = SID;
                response.CID = cid;
                db.GroupERs.Add(response);
            }
            db.SaveChanges();
            //return RedirectToAction("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID });
            return Json(new { redirectToUrl = Url.Action("StudentMissionDetail", "Student", new { cid = evalution.CID, mid = evalution.MID }) });
            //return Redirect("Index", );
        }
        public ActionResult StudentGroupER(EvalutionViewModel evalution, string cid, string mid, int gid)  ///學生已填過目標設置
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var SID = claims.Claims.Where(x => x.Type == "SID").SingleOrDefault().Value;
            var cname = db.Courses.Find(cid).CName;
            evalution.DefaultQuestions = db.DefaultQuestions.Where(q => q.Class == "組間互評").Include(q => q.GroupER);//我組間互評的的資料是用預設問題的這張表格(因為每個任務都需要),那這樣的話是不是必須得要改model去關聯GroupER這張表,還是改ViewModel就好?
            evalution.SID = SID;
            evalution.MID = mid;
            evalution.CID = cid;
            evalution.GID = gid;
            evalution.CName = cname;
            return View(evalution);

            //return Json(new { redirectToUrl = Url.Action("StudentMissionDetail", "Student", new { cid = goalSetting.CID, mid = goalSetting.MID }) });
        }
    }
}
