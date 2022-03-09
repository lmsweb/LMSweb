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
    public class QuestionsController : Controller
    {
        private LMSmodel db = new LMSmodel();

        public ActionResult SelectQuestion (string mid, string cid)
        {
            
            SurveyQuestionViewModel sQvmodel = new SurveyQuestionViewModel();
            sQvmodel.DefaultQuestions = db.DefaultQuestions.Where(dq =>dq.Class == "目標設置").Include(dq => dq.DefaultOptions).ToList();
            sQvmodel.MID = mid;
            sQvmodel.CID = cid;
            sQvmodel.CName = db.Courses.Find(cid).CName;
            return View(sQvmodel);
        }
        public ActionResult SelectReflectionQuestion(string mid, string cid)
        {

            SurveyQuestionViewModel sQvmodel = new SurveyQuestionViewModel();
            sQvmodel.DefaultQuestions = db.DefaultQuestions.Where(dq => dq.Class == "自我反思").Include(dq => dq.DefaultOptions).ToList();
            sQvmodel.MID = mid;
            sQvmodel.CID = cid;
            sQvmodel.CName = db.Courses.Find(cid).CName;
            return View(sQvmodel);
        }

        public ActionResult AddQuestion(string mid, string cid,int dqid)
        {
            DefaultQuestion defaultQuestion = db.DefaultQuestions.Find(dqid); //default


            var question = new Question();
            question.Description = defaultQuestion.Description;
            question.MID = mid;
            question.Class = "目標設置";
            question.Type = defaultQuestion.Type;
            db.Questions.Add(question);
            db.SaveChanges();
            
            if(! (defaultQuestion.Type == "問答"))
            {
                var defaultOptions = db.DefaultOptions.Where(o => o.DQID == dqid).ToList();
                foreach(var d_option in defaultOptions)
                {
                    var option = new Option();
                    option.QID = question.QID;
                    option.Question = question;
                    option.OptionName = d_option.DOptions;
                    db.Options.Add(option);
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", new { cid = cid, mid = mid });
        }
        public ActionResult AddReflectionQuestion(string mid, string cid, int dqid)
        {
            DefaultQuestion defaultQuestion = db.DefaultQuestions.Find(dqid); //default


            var question = new Question();
            question.Description = defaultQuestion.Description;
            question.MID = mid;
            question.Class = "自我反思";
            question.Type = defaultQuestion.Type;
            db.Questions.Add(question);
            db.SaveChanges();

            if (!(defaultQuestion.Type == "問答"))
            {
                var defaultOptions = db.DefaultOptions.Where(o => o.DQID == dqid).ToList();
                foreach (var d_option in defaultOptions)
                {
                    var option = new Option();
                    option.QID = question.QID;
                    option.Question = question;
                    option.OptionName = d_option.DOptions;
                    db.Options.Add(option);
                }
                db.SaveChanges();
            }
            return RedirectToAction("ReflectionIndex", new { cid = cid, mid = mid });
        }
        // GET: Questions
        public ActionResult Index(SurveyViewModel svmodel, string mid, string cid)
        {
            var questions = db.Questions.Where(q => q.MID == mid && q.Class =="目標設置").Include(q => q.Options);
            svmodel.Questions = questions;
            svmodel.CID = cid;
            svmodel.MID = mid;
            svmodel.CName = db.Courses.Find(cid).CName;

            return View(svmodel);
        }
        public ActionResult ReflectionIndex(SurveyViewModel svmodel, string mid, string cid)
        {
            var questions = db.Questions.Where(q => q.MID == mid && q.Class == "自我反思").Include(q => q.Options);
            svmodel.Questions = questions;
            svmodel.CID = cid;
            svmodel.MID = mid;
            svmodel.CName = db.Courses.Find(cid).CName;
            return View(svmodel);
        }

        // GET: Questions/Create
        public ActionResult Create(string mid, string cid)
        {
            SurveyQuestionViewModel suQvmodel = new SurveyQuestionViewModel();
            suQvmodel.Question = new Question();
            suQvmodel.Question.MID = mid;
            suQvmodel.MID = mid;
            suQvmodel.CID = cid;
            suQvmodel.CName = db.Courses.Find(cid).CName;
            return View(suQvmodel);
        }
        // http://localhost:56564/Questions?cid=C001&mid=M220307033113

        // POST: Questions/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SurveyQuestionViewModel suQvmodel)
        {
            //surveyViewModel.Question.MID = mid;
            if (ModelState.IsValid)
            {
                var mission = db.Missions.Find(suQvmodel.Question.MID);
                mission.Questions.Add(suQvmodel.Question);
                db.Questions.Add(suQvmodel.Question);
                db.SaveChanges();

                if (!(suQvmodel.Question.Type == "問答"))
                {
                    foreach (var s_option in suQvmodel.String_Options)
                    {
                        if (s_option != "")
                        {
                            var option = new Option();
                            option.QID = suQvmodel.Question.QID;
                            option.OptionName = s_option;
                            option.Question = suQvmodel.Question;
                            db.Options.Add(option);
                        }
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("Index", new { cid = suQvmodel.CID, mid = suQvmodel.MID });
            }
            return View(suQvmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReflection(SurveyQuestionViewModel suQvmodel)
        {
            //surveyViewModel.Question.MID = mid;
            if (ModelState.IsValid)
            {
                var mission = db.Missions.Find(suQvmodel.Question.MID);
                mission.Questions.Add(suQvmodel.Question);
                db.Questions.Add(suQvmodel.Question);
                db.SaveChanges();

                if (!(suQvmodel.Question.Type == "問答"))
                {
                    foreach (var s_option in suQvmodel.String_Options)
                    {
                        if (s_option != "")
                        {
                            var option = new Option();
                            option.QID = suQvmodel.Question.QID;
                            option.OptionName = s_option;
                            option.Question = suQvmodel.Question;
                            db.Options.Add(option);
                        }
                    }
                    db.SaveChanges();
                }

                return RedirectToAction("ReflectionIndex", new { cid = suQvmodel.CID, mid = suQvmodel.MID });
            }
            return View(suQvmodel);
        }
        public ActionResult CreateReflection(string mid, string cid)
        {
            SurveyQuestionViewModel suQvmodel = new SurveyQuestionViewModel();
            suQvmodel.Question = new Question();
            suQvmodel.Question.MID = mid;
            suQvmodel.CID = cid;
            suQvmodel.MID = mid;
            suQvmodel.CName = db.Courses.Find(cid).CName;
            return View(suQvmodel);
        }
        // GET: Questions/Edit/5
        public ActionResult Edit(int qid, string mid, string cid)
        {
           
            Question question = db.Questions.Where(qus => qus.QID == qid).Include(qus => qus.Options).Single();
            if (question == null)
            {
                return HttpNotFound();
            }
            
            QuestionViewModel Qvmodel= new QuestionViewModel();
            Qvmodel.Question = question;
            Qvmodel.CID = cid;
            Qvmodel.CName = db.Courses.Find(cid).CName;
            //Qvmodel.Question.Options = option;

            return View(Qvmodel);
        }

        // POST: Questions/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionViewModel Qvmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Qvmodel.Question).State = EntityState.Modified;
                var question_et = db.Questions.Where(qus => qus.QID == Qvmodel.Question.QID).Include(qus => qus.Options).Single();
                if (question_et.Options != null)
                {
                    db.Options.RemoveRange(question_et.Options);
                    question_et.Options.Clear();
                }
                Qvmodel.Question.mission = question_et.mission;
                if (Qvmodel.Question.Type != "問答")
                {
                    foreach (var s_option in Qvmodel.String_Options)
                    {
                        if (s_option != "")
                        {
                            var option = new Option();
                            option.QID = Qvmodel.Question.QID;
                            option.OptionName = s_option;
                            option.Question = Qvmodel.Question;
                            db.Options.Add(option);
                            Qvmodel.Question.Options.Add(option);
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index", new { cid = Qvmodel.CID, mid = question_et.MID });
            }


           return View();
        }

        // GET: Questions/Delete/5
        //public ActionResult Delete(int qid, string mid, string cid)
        //{

        //    Question question = db.Questions.Find(qid);
        //    if (question == null)
        //    {
        //        return HttpNotFound();
        //    }


        //    return View(question);
        //}

        // POST: Questions/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int qidHidden, string midHidden, string cidHidden)
        {

            Question question = db.Questions.Find(qidHidden);
            db.Questions.Remove(question);

            var option = db.Options.Where(o => o.QID == qidHidden);
            db.Options.RemoveRange(option);

            db.SaveChanges();

            //SurveyViewModel svmodel = new SurveyViewModel();
            //svmodel.MID = mid;
            //svmodel.CID = cid;
            return RedirectToAction("Index", new { cid = cidHidden, mid = midHidden });
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
