using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LMSweb
{
    public class ChatHub : Hub
    {
        

        public void AddGroup(string GroupId)
        {
            Groups.Add(Context.ConnectionId, GroupId);
           
            Clients.Client(Context.ConnectionId).addGroup(GroupId);
            //Clients.Group(GroupId).groupMessage("Welcome");
        }
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            //Clients.All.addNewMessageToPage(name, message);
            Clients.All.receiveMessage(name, DateTime.Now.ToString("HH:mm"), message);
            //Clients.Group(GroupId, new string[0]).groupMessage(GroupId, name, DateTime.Now.ToString("HH:mm"), message);
        }
    }
}