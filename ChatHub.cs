using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LMSweb
{
    public class ChatHub : Hub
    {
        public void Group(String GroupId)
        {
            Groups.Add(Context.ConnectionId, GroupId);
            Clients.Group(GroupId).addMessage("Welcome");
        }

        public void GroupSend(string GroupId, string name, string Message)
        {
            Clients.Group(GroupId).groupMessage(name, DateTime.Now.ToString("HH:mm"), Message);
        }
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
            Clients.All.receiveMessage(name, DateTime.Now.ToString("HH:mm"), message);
        }
    }
}