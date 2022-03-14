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

        public void AddToRoom(string roomid,string GroupId, string mid)
        {
            Groups.Add(Context.ConnectionId, roomid);
            Clients.Client(Context.ConnectionId).addRoom(roomid);
            
        }
        public void CreatRoom(string roomid, string GroupId, string mid,string sid)
        {
            
            AddToRoom(roomid,GroupId,mid);
            GetChatHistory(roomid, GroupId, mid, sid);

        }
        public void Send(string roomid, string GroupId, string name, string message,string cid,string sid,string mid)
        {
            Clients.Group(roomid, new string[0]).groupMessage(roomid, name, DateTime.Now.ToString("yyyy/MM/dd HH:mm"), message);
            AddChatHistory(message, DateTime.Now.ToString("yyyy/MM/dd HH:mm"), cid, sid, mid, GroupId);
        }

        public void HistorySend(string roomid,string sid, string time, string message)
        {
            Clients.Caller.historyMessage(roomid, sid, time, message);
        }

        public void GetChatHistory(string roomid,string GroupId,string mid,string sid)
        {
            int GID = Convert.ToInt32(GroupId);
            string MID = mid;
            List<DiscussViewModel> histories = new List<DiscussViewModel>();

            var chathistory = db.LearnB.Where(h => h.group.GID == GID && h.ActionType == "D" && h.mission.MID == mid).ToList();
            
            foreach (var item in chathistory)
            {
                DiscussViewModel history = new DiscussViewModel
                {
                    Detail = item.Detail,
                    Time = item.Time,
                    CID = item.CID,
                    SID = item.student.SName,
                    MID = item.mission.MID
                };
                histories.Add(history);
            }
            Clients.Caller.getChatHistory(histories);
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