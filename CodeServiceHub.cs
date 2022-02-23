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
    }
}