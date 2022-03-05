using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LMSweb
{
    public class CodeServiceHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void editCode(string gid, string content, int line, int ch)
        {
            Clients.All.broadcastCode(content, line, ch);
            //Clients.Group(gid).broadcastCode(content, line, ch);
        }

        public void saveCode(string content)
        {

        }

        public Task joinGroup(string gid)
        {
            return Groups.Add(Context.ConnectionId, gid);
        }

        public Task removeGroup(string gid)
        {
            return Groups.Remove(Context.ConnectionId, gid);
        }
    }
}