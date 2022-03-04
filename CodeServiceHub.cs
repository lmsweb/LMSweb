using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSweb
{
    public class CodeServiceHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void editCode(string content, int line, int ch)
        {
            Clients.All.broadcastCode(content, line, ch);
        }

        public void saveCode(string content)
        {

        }
    }
}