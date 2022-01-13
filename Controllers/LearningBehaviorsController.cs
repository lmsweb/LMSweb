using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using LMSweb.Models;
using LMSweb.ViewModel;

namespace LMSweb.Controllers
{
    public class LearningBehaviorsController : Controller
    {
        private LMSmodel db = new LMSmodel();
        LearnBViewModel vm = new LearnBViewModel();
        List<LearningBehavior> LMSList;
        public LearningBehaviorsController()
        {
            LMSList = new List<LearningBehavior>();
        }

        public ActionResult ClassJourney(string CID)
        {
            vm.learningbehavior = db.LearnB.ToList();
            vm.student = db.Students.ToList();
            vm.CID = CID;
            return View(vm);
        }


        public ActionResult GroupJourney(string CID)
        {
            vm.student = db.Students.Where(g => g.group.GID == 2).ToList();
            vm.learningbehavior = db.LearnB.ToList();
            vm.CID = CID;
            return View(vm);
        }

        public ActionResult PersonalJourney(string CID)
        {
            //var results = db.LearnB.Where(j => j.StudentMissions.SID == "S001");
            //ViewBag.Studemt = db.LearnB.Where(s => s.StudentMissions.SID == "S001");
            //產生ViewModel物件
            vm.CID = CID;
            vm.learningbehavior = db.LearnB.Where(s => s.StudentMissions.SID == "S001").ToList();
            return View(vm);
        }
        public ActionResult Chat()
        {
           
            return View();
        }

        public ActionResult DownloadExcelDocument()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "authors.xlsx";

            var workbook = new XLWorkbook();
            IXLWorksheet worksheet =
            workbook.Worksheets.Add("Authors");
            worksheet.Cell(1, 1).Value = "SId";
            worksheet.Cell(1, 2).Value = "StudentMissions";
            worksheet.Cell(1, 3).Value = "subAction";
            worksheet.Cell(1, 4).Value = "Detail";
            worksheet.Cell(1, 5).Value = "Time";
            for (int index = 1; index <= LMSList.Count; index++)
            {
                worksheet.Cell(index + 1, 1).Value =
                LMSList[index - 1].StudentMissions.SID;
                worksheet.Cell(index + 1, 2).Value =
                LMSList[index - 1].ActionType;
                worksheet.Cell(index + 1, 3).Value =
                LMSList[index - 1].subAction;
                worksheet.Cell(index + 1, 4).Value =
                LMSList[index - 1].Detail;
                worksheet.Cell(index + 1, 5).Value =
                LMSList[index - 1].Time;
            }
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            return File(content, contentType, fileName);
        }


    }
}
