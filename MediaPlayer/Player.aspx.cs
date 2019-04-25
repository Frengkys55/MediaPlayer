using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace MediaPlayer
{
    public partial class Player : System.Web.UI.Page
    {
        public static double videoDuration = 0;
        public static string startFrame = string.Empty;
        public static string videoTotalFrame = string.Empty;
        public static string middleFrame = string.Empty;
        public static double videoFrameRate = 0;
        public static string videoPlaySpeed = string.Empty;
        public static string videoFileName = string.Empty;
        public static string VideoURL = string.Empty;
        public static int videoWidth = 0;
        public static int videoHeight = 0;
        public static string VideoSequenceLocation = ConfigurationManager.AppSettings["videoSequenceLocation"];
        string sessionID = string.Empty;
        bool testMode = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["new"] == "true")
                {
                    VideoURL = Request.QueryString["path"];
                    videoFileName = Request.QueryString["name"];
                    videoDuration = Convert.ToDouble(Request.QueryString["duration"]);
                    videoFrameRate = Convert.ToDouble(Request.QueryString["framerate"]);
                    startFrame = Request.QueryString["startframe"];
                    videoTotalFrame = Request.QueryString["endframe"];
                    videoHeight = Convert.ToInt32(Request.QueryString["videoresolution"]);
                    videoPlaySpeed = Request.QueryString["playspeed"];
                    middleFrame = videoTotalFrame;
                }
                else
                {
                    videoFileName = Request.QueryString["n"];
                    videoTotalFrame = Request.QueryString["tf"];
                    middleFrame = Convert.ToInt32((Convert.ToDouble(videoTotalFrame) / 2)).ToString();
                    videoFrameRate = Convert.ToDouble(Request.QueryString["fr"]);
                    if (Request.QueryString["sid"] != null)
                    {
                        sessionID = Request.QueryString["sid"];
                        testMode = true;
                    }
                    if (testMode)
                    {
                        if (VideoSequenceLocation.EndsWith("/"))
                        {
                            VideoURL = VideoSequenceLocation + sessionID + "/" + videoFileName;
                        }
                        else
                        {
                            VideoURL = VideoSequenceLocation + "/" + sessionID + "/" + videoFileName;
                        }
                    }
                    else
                    {
                        if (VideoSequenceLocation.EndsWith("/"))
                        {
                            VideoURL = VideoSequenceLocation + Session.SessionID + "/" + videoFileName;
                        }
                        else
                        {
                            VideoURL = VideoSequenceLocation + "/" + Session.SessionID + "/" + videoFileName;
                        }
                    }
                }
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            videoWidth = Convert.ToInt32(Request.QueryString["videowidth"]);
            videoHeight = 480;
            // Configuring canvas
            //playerWindow.Attributes.Add("style", "width:" + videoWidth + "px; height:" + videoHeight + "px;");
        }
    }
}