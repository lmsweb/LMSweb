using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using LMSweb.Models;
using LMSweb.Service;
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
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var TID = claims.Claims.Where(x => x.Type == "TID").SingleOrDefault().Value;
            var gmodel = new GroupViewModel();
            gmodel.MID = mid;
            var mis = db.Missions.Find(mid);
            var mname = mis.MName;
            gmodel.Groups = db.Groups.Where(g => mis.CID == g.CID).ToList();
            gmodel.CID = cid;
            gmodel.CName = mis.course.CName;
            gmodel.MName = mname;
            var stu = db.Students.Where(s => s.group.CID == cid).ToList();
            gmodel.IsUploadDraw = db.StudentDraws.Where(sd => sd.MID == mid).ToList();
            gmodel.IsUploadCode = db.StudentCodes.Where(sc => sc.MID == mid).ToList();
            gmodel.GroupER = db.GroupERs.Where(sg => sg.MID == mid && sg.EvaluatorSID == TID).ToList();
            
            return View(gmodel);
        }
        public ActionResult CheckCoding(int gid, string cid, string mid)
        {
            DrawingViewModel dvmodel = new DrawingViewModel();

            dvmodel.CID = cid;
            dvmodel.GID = gid;
            dvmodel.MID = mid;
            var pt = db.StudentDraws.Where(p => p.GID == gid && p.MID == mid).Select(p => p.DrawingImgPath).SingleOrDefault();
            dvmodel.DrawingImgPath = pt;

            return View(dvmodel);
        }
        public ActionResult CheckDrawing(int gid, string cid)
        {
            GroupViewModel model = new GroupViewModel();
            model.CID = cid;
            model.GID = gid;
            return View(model);
        }
        private string codefileSavedPath = WebConfigurationManager.AppSettings["CodePath"];
        [HttpGet]
        public ActionResult Assessment(int gid, string mid, string cid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var TID = claims.Claims.Where(x => x.Type == "TID").SingleOrDefault().Value;
            var evalution = db.GroupERs.Where(r => r.GID == gid && r.EvaluatorSID == TID && r.MID == mid);
            var qids = evalution.Select(r => r.GQID).ToList();
            var questions = db.GroupOptions.Where(q => qids.Contains(q.GQID)).ToList();
            //var ta = db.TeacherA.Where(t => t.GID == gid && t.MID == mid).SingleOrDefault();

            //if (ta != null)
            //{
            //    var id = ta.TEID;
            //    return RedirectToAction("Edit", "Groups", new { id, cid, gid, mid });
            //}
            if (questions.Any())
            {
                return RedirectToAction("StudentTeacherER", "Groups", new { cid, mid, gid });
            }
            else
            {
                EvalutionViewModel model = new EvalutionViewModel();
                model.MID = mid;
                model.GID = gid;
                model.CID = cid;

                var group = db.Groups.Single(g => g.GID == gid);
                var pt = db.StudentDraws.Where(p => p.GID == gid && p.MID == mid).Select(p => p.DrawingImgPath).SingleOrDefault();
                var code = db.StudentCodes.Find(cid, mid, gid);
                var cname = db.Courses.Find(cid).CName;
                var gname = db.Groups.Find(gid).GName;
                var mname = db.Missions.Find(mid).MName;
                //var evalution = db.GroupERs.Where(r => r.GID == gid && r.EvaluatorSID == TID);
                //var qids = evalution.Select(r => r.GQID).ToList();
                //var questions = db.GroupOptions.Where(q => qids.Contains(q.GQID)).ToList();
                var readcode = new TextIO();
                model.GroupQuestion = db.GroupQuestions.Include(q => q.GroupOptions);
                model.MID = mid;
                model.GID = gid;
                model.CID = cid;
                model.TID = TID;
                model.CName = cname;
                model.MName = mname;
                model.GName = gname;
                if (code != null)
                {
                    string virtualBaseFilePath = Url.Content(codefileSavedPath);
                    string filePath = HttpContext.Server.MapPath(virtualBaseFilePath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string readcodepath = $"{filePath}{code.CodePath}.txt";
                    model.CodeText = readcode.readCodeText(readcodepath);
                }
                if (pt != null)
                {
                    model.DrawingImgPath = pt;
                }
                return View(model);
            }
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Assessment([System.Web.Http.FromBody] EvalutionViewModel groupVM, int gid, string mid, string cid)
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var TID = claims.Claims.Where(x => x.Type == "TID").SingleOrDefault().Value;
            int GID = gid;
            var MID = mid;
            var CID = cid;
            foreach (var qr in groupVM.TRs)
            {
                var response = new GroupER();
                response.GQID = qr.qid;
                response.Answer = qr.response;
                response.Comments = qr.comments;
                response.GID = GID;
                response.EvaluatorSID = TID;
                response.CID = CID;
                response.MID = MID;
                db.GroupERs.Add(response);
            }
            db.SaveChanges();
            if (ModelState.IsValid)
            {
                //groupVM.TeacherAssessment.GID = GID;
                //groupVM.TeacherAssessment.MID = MID;
                //groupVM.TeacherAssessment.CID = CID;
                //db.TeacherA.Add(groupVM.TeacherAssessment);
                //db.SaveChanges();
                var pt = db.StudentDraws.Where(p => p.GID == gid && p.MID == mid).Select(p => p.DrawingImgPath).SingleOrDefault();
                var code = db.StudentCodes.Find(cid, mid, gid);
                var readcode = new TextIO();
                if (code != null)
                {
                    string virtualBaseFilePath = Url.Content(codefileSavedPath);
                    string filePath = HttpContext.Server.MapPath(virtualBaseFilePath);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string readcodepath = $"{filePath}{code.CodePath}.txt";
                    groupVM.CodeText = readcode.readCodeText(readcodepath);
                    groupVM.IsUploadCode = db.StudentCodes.Where(sc => sc.MID == mid).ToList();

                }
                else
                {
                    groupVM.IsUploadCode = null;
                }
                if (pt != null)
                {
                    groupVM.DrawingImgPath = pt;
                    groupVM.IsUploadDraw = db.StudentDraws.Where(sd => sd.MID == mid).ToList();

                }
                else
                {
                    groupVM.IsUploadDraw = null;
                }

                groupVM.CID = cid;
                groupVM.MID = mid;
                var course = db.Courses.Find(cid).CName;
                groupVM.CName = course;
                groupVM.Groups = db.Groups.Where(g => g.CID == cid).ToList();
                //return View("Index", groupVM);
                return Json(new { redirectToUrl = Url.Action("Index", "Groups", new { gid = groupVM.GID, cid = groupVM.CID, mid = groupVM.MID }) });
            }
            return View(groupVM);
        }
        
        public ActionResult Edit(int? id, string cid, int gid, string mid)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TeacherAssessment teacherAssessment = db.TeacherA.Find(id);
            var groupVM = new GroupViewModel();
            groupVM.CID = cid;
            groupVM.MID = mid;
            groupVM.GID = gid;
            
            var code = db.StudentCodes.Find(cid, mid, gid);
            var course = db.Courses.Find(cid);
            var gname = db.Groups.Find(gid);
            groupVM.CName = course.CName;
            groupVM.GName = gname.GName;
            var pt = db.StudentDraws.Where(p => p.GID == gid && p.MID == mid).Select(p => p.DrawingImgPath).SingleOrDefault();
            var readcode = new TextIO();
            if (teacherAssessment != null)
            {
                groupVM.TeacherAssessment = teacherAssessment;
            }
            if (code != null )
            {
                string virtualBaseFilePath = Url.Content(codefileSavedPath);
                string filePath = HttpContext.Server.MapPath(virtualBaseFilePath);
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string readcodepath = $"{filePath}/{code.CodePath}.txt";
                groupVM.CodeText = readcode.readCodeText(readcodepath);
                groupVM.IsUploadCode = db.StudentCodes.Where(sc => sc.MID == mid).ToList();

            }
            if (pt != null)
            {
                groupVM.DrawingImgPath = pt;
                groupVM.IsUploadDraw = db.StudentDraws.Where(sd => sd.MID == mid).ToList();

            }
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

        //// GET: Groups/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Group group = db.Groups.Find(id);
        //    if (group == null)
        //    {
        //        return HttpNotFound();
        //    }
            
        //    return View(group);
        //}

        //// POST: Groups/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Group group = db.Groups.Find(id);
        //    db.Groups.Remove(group);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult StudentTeacherER(EvalutionViewModel evalution, string cid, string mid, int gid)  ///學生已填過組間評價
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var TID = claims.Claims.Where(x => x.Type == "TID").SingleOrDefault().Value;
            var cname = db.Courses.Find(cid).CName;
            var mname = db.Missions.Find(mid).MName;
            var gname = db.Groups.Find(gid).GName;
            var misChat = db.Missions.Find(mid).IsDiscuss;
            var pt = db.StudentDraws.Where(p => p.GID == gid && p.MID == mid).Select(p => p.DrawingImgPath).SingleOrDefault();
            var code = db.StudentCodes.Find(cid, mid, gid);
            var readcode = new TextIO();
            if (code != null)
            {
                string virtualBaseFilePath = Url.Content(codefileSavedPath);
                string filePath = HttpContext.Server.MapPath(virtualBaseFilePath);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                string readcodepath = $"{filePath}{code.CodePath}.txt";
                evalution.CodeText = readcode.readCodeText(readcodepath);
            }
            if (pt != null)
            {
                evalution.DrawingImgPath = pt;
            }
            evalution.IsDiscuss = misChat;
            //evalution.Questions = db.Questions.Where(q => q.MID == mid && q.Class == "組間互評").Include(q => q.EvalutionResponses);
            ////evalution.DefaultQuestions = db.DefaultQuestions.Where(q => q.Class == "組間互評").Include(q => q.GroupER);//我組間互評的的資料是用預設問題的這張表格(因為每個任務都需要),那這樣的話是不是必須得要改model去關聯GroupER這張表,還是改ViewModel就好?
            evalution.GroupQuestion = db.GroupQuestions.Include(q => q.GroupERs);
            //evalution.SID = SID;
            evalution.MID = mid;
            evalution.CID = cid;
            evalution.GID = gid;
            evalution.CName = cname;
            evalution.MName = mname;
            evalution.GName = gname;
            evalution.TID = TID;

            return View(evalution);
        }
    }
}
