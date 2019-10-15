using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaPlayer
{
    /// <summary>
    /// ASHX image handler (If using FTP to save files)
    /// </summary>
    public class Loader1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}