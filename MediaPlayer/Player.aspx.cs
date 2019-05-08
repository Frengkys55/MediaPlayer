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
        public static int audioStartDuration = 0;
        public static string playSpeedIncrement = "0";
        public static double videoStartRequestDuration = 0;
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
        public static string CheckerAddress = string.Empty;
        string sessionID = string.Empty;
        bool testMode = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["new"] == "true")
                {
                    VideoURL = Request.QueryString["path"];
                    try
                    {
                        videoFileName = HelperClass.StringEncoderDecoder(HttpUtility.UrlDecode(Request.QueryString["name"]), StringConversionMode.Decode);
                    }
                    catch (Exception err)
                    {

                        Response.Redirect("Error.aspx");
                    }
                    videoDuration = Convert.ToDouble(Request.QueryString["duration"]);
                    videoFrameRate = Convert.ToDouble(Request.QueryString["framerate"]);
                    startFrame = Request.QueryString["startframe"];
                    videoTotalFrame = Request.QueryString["endframe"];
                    videoHeight = Convert.ToInt32(Request.QueryString["videoresolution"]);
                    videoPlaySpeed = Request.QueryString["playspeed"];
                    middleFrame = videoTotalFrame;
                    CheckerAddress = ConfigurationManager.AppSettings["hostAddress"] + "Checker.aspx?id=" + Request.QueryString["pid"];

                    if (Request.QueryString["timeposition"] != null)
                    {
                        audioStartDuration = TimeStringToSecondConverter(Request.QueryString["timeposition"]);
                        if (audioStartDuration >= videoDuration)
                        {
                            audioStartDuration = 0;
                        }

                        startFrame = TimeToFrameConverter(audioStartDuration, videoFrameRate).ToString();
                    }
                    if (Request.QueryString["playspeed"] != null)
                    {
                        playSpeedIncrement = Request.QueryString["playspeed"];
                    }
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
            else
            {
                VideoURL = string.Empty;
                videoFileName = string.Empty;
                videoDuration = 0;
                videoFrameRate = 0;
                startFrame = string.Empty;
                videoTotalFrame = string.Empty;
                videoHeight = 0;
                videoPlaySpeed = string.Empty;
                middleFrame = string.Empty;
                CheckerAddress = string.Empty;

                audioStartDuration = 0;
                startFrame = string.Empty;
                playSpeedIncrement = string.Empty;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            videoWidth = Convert.ToInt32(Request.QueryString["videowidth"]);
            videoHeight = 480;
            // Configuring canvas
            //playerWindow.Attributes.Add("style", "width:" + videoWidth + "px; height:" + videoHeight + "px;");
        }

        protected void btnHome_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        private int TimeStringToSecondConverter(string time)
        {
            #region Preparation
            int convertedTime = 0;

            #region Time information
            int days = 0; // Not used
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            #endregion Time information

            #endregion Preparation

            if (time.Contains(":"))
            {
                string[] timeArray = time.Split(':');
                if (timeArray.Length == 2)
                {
                    #region Minute parsing
                    if (Int32.TryParse(timeArray[0], out minutes))
                    {
                        if (minutes > 60)
                        {
                            minutes = 0;
                        }
                    }
                    else
                    {
                        minutes = 0 * 60;
                    }
                    #endregion Minute parsing

                    #region Second parsing
                    if (Int32.TryParse(timeArray[1], out seconds))
                    {
                        if (seconds > 60)
                        {
                            seconds = 0;
                        }
                    }
                    else
                    {
                        seconds = 0;
                    }
                    #endregion Second parsing

                    TimeSpan timeSpan = new TimeSpan(0, minutes, seconds);
                    convertedTime = Convert.ToInt32(timeSpan.TotalSeconds);
                }
                else if (timeArray.Length == 3)
                {
                    #region Hours parsing
                    if (Int32.TryParse(timeArray[0], out hours))
                    {
                        if (hours > 24)
                        {
                            hours = 0;
                        }
                    }
                    else
                    {
                        hours = 0 * 60;
                    }
                    #endregion Hours parsing

                    #region Minutes parsing
                    if (Int32.TryParse(timeArray[1], out minutes))
                    {
                        if (minutes > 60)
                        {
                            minutes = 0;
                        }
                    }
                    else
                    {
                        minutes = 0 * 60;
                    }
                    #endregion Minutes parsing

                    #region Seconds parsing
                    if (Int32.TryParse(timeArray[2], out seconds))
                    {
                        if (seconds > 60)
                        {
                            seconds = 0;
                        }
                    }
                    else
                    {
                        seconds = 0;
                    }
                    #endregion Seconds parsing

                    TimeSpan timeSpan = new TimeSpan(0, minutes, seconds);
                    convertedTime = Convert.ToInt32(timeSpan.TotalSeconds);
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                convertedTime = Convert.ToInt32(time);
            }

            return convertedTime;
        }
        private int TimeToFrameConverter(int timeInSecond, double frameRate)
        {
            int framePosition = 0;
            int convertedTime = timeInSecond;

            framePosition = Convert.ToInt32(convertedTime * frameRate);
            
            return framePosition;
        }
    }
}