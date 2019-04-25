using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MediaPlayer.SignalR
{
    public class CheckerHub : Hub
    {
        public void Send(string name, string meessage)
        {
        }
    }
}