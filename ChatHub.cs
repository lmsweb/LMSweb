using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using LMSweb.Models;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using LMSweb.ViewModel;

namespace LMSweb
{
    public class ChatHub : Hub
    {
        //全局變數
        private LMSmodel db = new LMSmodel();

        protected static List<Student> userInfoList = new List<Student>();
       
        public void AddToRoom(string GroupId)
        {
            Groups.Add(Context.ConnectionId, GroupId);
            Clients.Client(Context.ConnectionId).addRoom(GroupId);
        }
        public void CreatRoom(string GroupId)
        {
            AddToRoom(GroupId);
            GetChatHistory(GroupId);
        }
        public void Send(string GroupId, string name, string message,string cid,string sid,string mid)
        {
            Clients.Group(GroupId, new string[0]).groupMessage(GroupId, sid, DateTime.Now.ToString("yyyy/MM/dd HH:mm"), message);

            AddChatHistory(message, DateTime.Now.ToString("yyyy/MM/dd HH:mm"), cid, sid, mid, GroupId);
        }

        public void HistorySend(string GroupId, string sid, string time, string message)
        {
            Clients.Group(GroupId, new string[0]).groupMessage(GroupId, sid, time, message);
        }

        public void GetChatHistory(string GroupId)
        {
            int GID = Convert.ToInt32(GroupId);
            List<DiscussViewModel> histories = new List<DiscussViewModel>();

            var chathistory = db.LearnB.Where(h => h.group.GID == GID && h.ActionType == "D").ToList();
            foreach (var item in chathistory)
            {
                DiscussViewModel history = new DiscussViewModel
                {
                    Detail = item.Detail,
                    Time = item.Time,
                    CID = item.CID,
                    SID = item.student.SID,
                    MID = item.mission.MID
                };
                histories.Add(history);
            }
            Clients.All.getChatHistory(histories);
        }
        /// 添加歷史記錄數據
        public string AddChatHistory(string message, string time, string cid, string sid, string mid,string GroupId)
        {          
            try
            {
                LearningBehavior lb = new LearningBehavior();
               
                lb.ActionType = "D";
                lb.subAction = "R";
                lb.Detail = message;
                lb.Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
                lb.CID = cid;
                lb.student = db.Students.Find(sid);
                lb.mission = db.Missions.Find(mid);
                lb.group = db.Groups.Find(Convert.ToInt32(GroupId));

                db.LearnB.Add(lb);
                db.SaveChanges();
                return "history suc";
            }
            catch (DbUpdateException ex)
            {
                //回傳HTTP Internal Server Error
                return "server error";
            }
            catch (Exception ex)
            {
                return "error";
            }

        }
    }
}