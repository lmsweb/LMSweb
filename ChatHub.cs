using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using LMSweb.Models;


namespace LMSweb
{
    public class ChatHub : Hub
    {
        //全局變數
        protected static List<Student> userInfoList = new List<Student>();
       
        protected static List<LearningBehavior> chatHistoryList = new List<LearningBehavior>();

        //public void AddGroup(string GroupId)
        //{
        //    Groups.Add(Context.ConnectionId, GroupId);

        //    Clients.Client(Context.ConnectionId).addGroup(GroupId);
        //    //Clients.Group(GroupId).groupMessage("Welcome");
        //}
        public void AddToRoom(string GroupId)
        {
            Groups.Add(Context.ConnectionId, GroupId);
            Clients.Client(Context.ConnectionId).addRoom(GroupId);
        }
        public void CreatRoom(string GroupId)
        {
            AddToRoom(GroupId);
        }
        public void Send(string GroupId, string name, string message)
        {
            //Call the addNewMessageToPage method to update clients.           
            //Clients.All.receiveMessage(name, DateTime.Now.ToString("HH:mm"), message);
            Clients.Group(GroupId, new string[0]).groupMessage(GroupId, name, DateTime.Now.ToString("HH:mm"), message);

            //AddChatHistory(name, DateTime.Now.ToString("HH:mm"), message, GroupId);
        }

        public void HistorySend(string GroupId, string name, string time, string message)
        {
            Clients.Group(GroupId, new string[0]).groupMessage(GroupId, name, time, message);
        }

        //public void GroupSend(string GroupId, string name, string message)
        //{
        //    //Call the addNewMessageToPage method to update clients.
        //    Clients.Group(GroupId, new string[0]).groupMessage(GroupId, name, DateTime.Now.ToString("HH:mm"), message);
        //}

        /// </summary>
        /// <param name="chatType">消息類型0公共聊天，1好友，2群</param>
        /// <param name="toId">接收者id</param>
        /// <param name="frmId">發送方id</param>
        /// <param name="roomId">房間id</param>
        public void GetChatHistory(string GroupId, string name, string time,string message)
        {
            //var list = chatHistoryList;

            var list = chatHistoryList;
            list = chatHistoryList.ToList();
            list = new List<LearningBehavior>();
            //var data = JsonHelper.ToJsonString(list);
            var data = message;
            var Time = time;
            var user = userInfoList.FirstOrDefault(x => x.SID == name);
            var conid = Context.ConnectionId;
            
            Clients.Client(conid).initChatHistoryData(data);
        }
        /// <summary>
        /// 添加歷史記錄數據
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        /// <param name="chatType">0公共聊天，1私聊，2群聊</param>
        //public void AddChatHistory(string name, string time, string message ,string GroupId)
        //{
           
        //    LearningBehavior history = new LearningBehavior()
        //    {
                
        //        Time = time,
        //        Detail = message
                
        //    };
        //    chatHistoryList.Add(history);
        //}
    }
}