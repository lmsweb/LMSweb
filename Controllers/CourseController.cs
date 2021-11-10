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
using System.Web.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using LMSweb.Infrastructure.Helpers;

namespace LMSweb.Controllers
{
    [RoutePrefix("Teacher")]

    public class CourseController : Controller
    {
        private LMSmodel db = new LMSmodel();

        // GET: Course
        
        [Authorize(Roles ="Teacher")]
        public ActionResult TeacherHomePage()
        {
            ClaimsIdentity claims = (ClaimsIdentity)User.Identity; //取得Identity
            var claimData = claims.Claims.Where(x => x.Type == "TID").ToList();   //抓出當初記載Claims陣列中的TID
            var tid = claimData[0].Value; //取值(因為只有一筆)
            var courses = db.Courses.Where(c => c.TID == tid);
            

            return View(courses);
        }

        public ActionResult Stu_Index(string cid)
        {   
            if(cid == null)
            {
                return RedirectToAction("Stu_Index");
            }
            CourseViewModel model = new CourseViewModel();
            var course = db.Courses.Where(c => c.CID == cid).Single();
            model.CID = course.CID;
            model.CName = course.CName;
            model.students = course.Students;
            ViewBag.CID = cid;
            //model.missions = db.Missions.Where(m => m.CID == cid);

            return View(model);
        }
        //public ActionResult Stu_Details()
        //{
        //    return View();
        //}
        public ActionResult Stu_Details(string sid,string cid)
        {
            if (sid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(sid, cid);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Stu_Create()
        {
            return View();
        }

        // POST: Student/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stu_Create([Bind(Include = "SID,CID,SName,SPassword,Sex,Stage,Grade,Score")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Stu_Edit(string sid, string cid)
        {
            if (sid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(sid, cid);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Stu_Edit([Bind(Include = "SID,CID,SName,SPassword,Sex,Stage,Grade,Score")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Stu_Delete(string sid, string cid)
        {
            if (sid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(sid, cid);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Stu_DeleteConfirmed(string sid, string cid)
        {
            Student student = db.Students.Find(sid, cid);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Course/Details/5
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
            CourseViewModel model = new CourseViewModel();
            model.CID = course.CID;
            model.CName = course.CName;

            return View(model);
        }

        // GET: Course/Create
        [Authorize(Roles ="Teacher")]
        public ActionResult Create()
        {
            ViewBag.TID = new SelectList(db.Teachers, "TID", "TName");
            return View();
        }

        // POST: Course/Create
        // 若要避免過量張貼攻擊，請啟用您要繫結的特定屬性。
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [Authorize]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CID,TID,CName")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TID = new SelectList(db.Teachers, "TID", "TName", course.TID);
            return View(course);
        }

        // GET: Course/Edit/5
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

        // POST: Course/Edit/5
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

        // GET: Course/Delete/5
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

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string cid)
        {
            Course course = db.Courses.Find(cid);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private string fileSavedPath = WebConfigurationManager.AppSettings["UploadPath"];

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, string cid)
        {
            JObject jo = new JObject();
            string result = string.Empty;

            if (file == null)
            {
                jo.Add("Result", false);
                jo.Add("Msg", "請上傳檔案!");
                jo.Add("error", "請上傳檔案!");
                result = JsonConvert.SerializeObject(jo);
                return Content(result, "application/json");
            }
            if (file.ContentLength <= 0)
            {
                jo.Add("Result", false);
                jo.Add("Msg", "請上傳正確的檔案.");
                jo.Add("error", "請上傳正確的檔案.");
                result = JsonConvert.SerializeObject(jo);
                return Content(result, "application/json");
            }

            string fileExtName = Path.GetExtension(file.FileName).ToLower();

            if (!fileExtName.Equals(".xls", StringComparison.OrdinalIgnoreCase)
                &&
                !fileExtName.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                jo.Add("Result", false);
                jo.Add("Msg", "請上傳 .xls 或 .xlsx 格式的檔案");
                jo.Add("error", "請上傳 .xls 或 .xlsx 格式的檔案");
                result = JsonConvert.SerializeObject(jo);
                return Content(result, "application/json");
            }

            try
            {
                var uploadResult = this.FileUploadHandler(file);

                result = this.Import(uploadResult, cid);


                //jo.Add("Result", !string.IsNullOrWhiteSpace(uploadResult));
                //jo.Add("Msg", !string.IsNullOrWhiteSpace(uploadResult) ? uploadResult : "");

                //result = JsonConvert.SerializeObject(jo);
            }
            catch (Exception ex)
            {
                jo.Add("Result", false);
                jo.Add("Msg", ex.Message);
                jo.Add("error", ex.Message);
                result = JsonConvert.SerializeObject(jo);
            }
            return Content(result, "application/json");
        }

       
        private string FileUploadHandler(HttpPostedFileBase file)
        {
            string result;

            if (file == null)
            {
                throw new ArgumentNullException("file", "上傳失敗：沒有檔案！");
            }
            if (file.ContentLength <= 0)
            {
                throw new InvalidOperationException("上傳失敗：檔案沒有內容！");
            }

            try
            {
                string virtualBaseFilePath = Url.Content(fileSavedPath);
                string filePath = HttpContext.Server.MapPath(virtualBaseFilePath);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string newFileName = string.Concat(
                    DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                    Path.GetExtension(file.FileName).ToLower());

                string fullFilePath = Path.Combine(Server.MapPath(fileSavedPath), newFileName);
                file.SaveAs(fullFilePath);

                result = newFileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private string Import(string savedFileName, string cid)
        {
            var jo = new JObject();
            string result;

            try
            {
                var fileName = string.Concat(Server.MapPath(fileSavedPath), "/", savedFileName);

                var importStudents = new List<Student>();

                var helper = new ImportDataHelper();
                var checkResult = helper.CheckImportData(fileName, importStudents);

                jo.Add("Result", checkResult.Success);
                jo.Add("Msg", checkResult.ErrorMessage);
                jo.Add("error", checkResult.ErrorMessage);

                if (checkResult.Success)
                {
                    //儲存匯入的資料
                    helper.SaveImportData(importStudents, cid);
                }
                result = JsonConvert.SerializeObject(jo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private List<SelectListItem> GetStudent(IEnumerable<int> SelectStudentList = null)
        {
            return new MultiSelectList(db.Students.Where(x => x.@group == null), "SID", "SName", SelectStudentList).ToList();
        }

        [HttpGet]
        public ActionResult StudentGroup(string CID)
        {
            
            var vmodel = new GroupCreateViewModel();
            vmodel.StudentList = GetStudent();
            vmodel.students = db.Students.Where(x => x.@group != null ).ToList();
            vmodel.CID = CID;
            vmodel.groups = db.Groups.ToList();

            var result = from g in db.Groups
                         from s in db.Students
                         where g.GID == s.@group.GID
                         select new { s.SName, s.@group.GName };
            


            return View(vmodel);
        }

        [HttpPost]
        public ActionResult StudentGroup(string GName, List<string> StudentList, string CID)
        {
            if (ModelState.IsValid)
            {
                Group group = new Group();
                group.GName = GName;

                group.Students = (ICollection <Student>)db.Students.Where(x => StudentList.Contains(x.SID)).ToList();
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index", new { cid = CID });
            }
            var vmodel = new GroupCreateViewModel();
            vmodel.StudentList = GetStudent();

            return View(vmodel);
        }
        

        public ActionResult StudentManagement(string cid)
        {

            if (cid == null)
            {
                return RedirectToAction("StudentGroup");
            }
            CourseViewModel model = new CourseViewModel();
            var course = db.Courses.Where(c => c.CID == cid).Single();
            model.CID = course.CID;
            model.CName = course.CName;
            model.students = course.Students;
            ViewBag.CID = cid;
            

            return View(model);
        }

        [HttpPost]
        public ActionResult GroupN(int n)
        {
            
            var stus = GetRandomElements(db.Students.Where(x => x.@group == null).ToList());
            List<Group> groups = new List<Group>();
            var left_s = stus.Count % n;
            for(int i = 1; i <= n; i++)
            {
                var g = new Group();
                g.GName = "第" + i.ToString() + "組"+"_"+ g.GID;
                groups.Add(g);
            }
            int g_idx = 0;
            for(int i = 0; i < stus.Count; i++)
            {
                groups[g_idx].Students.Add(stus[i]);
                g_idx++;
                g_idx = g_idx % n;
            }
            for (int i = 0; i < n; i++)
            {
                db.Groups.Add(groups[i]);
            }
            db.SaveChanges();
            return RedirectToAction("StudentGroup");

        }

        public static List<t> GetRandomElements<t>(IEnumerable<t> list)
        {
            return list.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public ActionResult GroupDelete(int groupId)
        {

            Group group = db.Groups.Find(groupId);
            if (group == null)
            {
                return HttpNotFound();
            }

            group.Students.Clear();
            db.Groups.Remove(group);

            db.SaveChanges();

            return RedirectToAction("StudentGroup");
        }

        public ActionResult GroupStudentDelete(string groupStuId, string CID)
        {
            Student student = db.Students.Find(groupStuId, CID);

            if (student == null)
            {
                return HttpNotFound();
            }
            Group group = student.group;
            group.Students.Remove(student);
            student.group = null;


            db.SaveChanges();

            return RedirectToAction("StudentGroup");
        }
        public ActionResult AddStuToOtherGroup(int gid, List<string> StudentList, string CID)
        {
            if (ModelState.IsValid)
            {
                //Group group = db.Groups.Find(gid);
                foreach(var sid in StudentList)
                {
                    Student student = db.Students.Find(sid, CID);
                    
                    Group group = db.Groups.Find(gid);
                    //student.group = group;
                    group.Students.Add(student);
 
                }
                

                //group.Students = (ICollection<Student>)db.Students.Where(x => StudentList.Contains(x.SID)).ToList();
                //db.Groups.Add(group);

                
                db.SaveChanges();
                return RedirectToAction("Index", new { cid = CID });
            }
            var vmodel = new GroupCreateViewModel();
            vmodel.StudentList = GetStudent();

            return View(vmodel);                                                                                                                                                                                                         

        }

    }
}
