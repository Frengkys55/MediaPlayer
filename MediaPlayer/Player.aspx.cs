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
        public static string customCSSLocation = string.Empty;
        public static string W3CSSLocation = string.Empty;
        public static string LoadingIcon = string.Empty;
        public static string checkerAddress = string.Empty;
        public static string userRequestedURL = string.Empty;

        public static string videoDuration = string.Empty;
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
        public static string processProgressBar = string.Empty;
        public static string processTime = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CSSLoader();
            PageImageLoader();
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["new"] == "true")
                {
                    if (ConfigurationManager.AppSettings["overridehost"] == "true")
                    {
                        VideoURL = HelperClass.HostChanger(Request.QueryString["path"]);
                    }
                    else
                    {
                        VideoURL = Request.QueryString["path"];
                    }
                    if (Request.QueryString["mode"] != null)
                    {
                        if (Request.QueryString["mode"] == "upload")
                        {
                            try
                            {
                                videoFileName = HelperClass.StringEncoderDecoder(HttpUtility.UrlDecode(Request.QueryString["name"]), StringConversionMode.Decode);
                            }
                            catch (Exception err)
                            {
                                //Response.Redirect("Error.aspx?id=31");
                                videoFileName = "(Name conversion fail) " + HttpUtility.UrlDecode(Request.QueryString["name"]);
                            }
                        }
                        else
                        {
                            videoFileName = Request.QueryString["name"];
                        }
                    }
                    else
                    {
                        videoFileName = Request.QueryString["name"];
                    }
                    
                    videoDuration = TimeSecondToTimeStringConverter(Convert.ToDouble(Request.QueryString["duration"]));
                    videoFrameRate = Convert.ToDouble(Request.QueryString["framerate"]);
                    startFrame = Request.QueryString["startframe"];
                    videoTotalFrame = Request.QueryString["endframe"];
                    videoHeight = Convert.ToInt32(Request.QueryString["videoresolution"]);
                    videoPlaySpeed = playSpeedIncrement = Request.QueryString["playspeed"];
                    middleFrame = videoTotalFrame;
                    CheckerAddress = ConfigurationManager.AppSettings["hostAddress"] + "Checker.aspx?id=" + Request.QueryString["pid"];

                    if (Request.QueryString["timeposition"] != null)
                    {
                        audioStartDuration = TimeStringToSecondConverter(Request.QueryString["timeposition"]);
                        if (audioStartDuration >= Convert.ToDouble(Request.QueryString["duration"]))
                        {
                            audioStartDuration = 0;
                        }

                        startFrame = TimeToFrameConverter(audioStartDuration, videoFrameRate).ToString();
                    }
                    else
                    {
                        audioStartDuration = 0;
                    }
                    if (Request.QueryString["playspeed"] != null)
                    {
                        playSpeedIncrement = Request.QueryString["playspeed"];
                    }
                    else
                    {
                        playSpeedIncrement = "0";
                    }

                    #region Video player settings

                    #region Progress bar settings
                    if (Request.QueryString["progressbar"] != null)
                    {
                        if (Request.QueryString["progressbar"] == "true")
                        {
                            processProgressBar = "true";
                        }
                        else
                        {
                            processProgressBar = "false";
                        }
                    }
                    else
                    {
                        // Default
                        processProgressBar = "true";
                    }
                    #endregion Progress bar settings

                    #region Time settings
                    if (Request.QueryString["time"] != null)
                    {
                        if (Request.QueryString["time"] == "true")
                        {
                            processTime = "true";
                        }
                        else
                        {
                            processTime = "false";
                        }
                    }
                    else
                    {
                        // Default
                        processTime = "true";
                    }
                    #endregion Time settings

                    #endregion Video player settings
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
                videoDuration = "0";
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

            userRequestedURL = Request.Url.AbsoluteUri;
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
        private string TimeSecondToTimeStringConverter(double seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            string convertedTime = string.Empty;

            if (time.Hours.ToString().Length < 2)
            {
                convertedTime += "0" + time.Hours + ":";
            }
            else
            {
                convertedTime += time.Hours + ":";
            }

            if (time.Minutes.ToString().Length < 2)
            {
                convertedTime += "0" + time.Minutes + ":";
            }
            else
            {
                convertedTime += time.Minutes + ":";
            }

            if (time.Seconds.ToString().Length < 2)
            {
                convertedTime += "0" + time.Seconds;
            }
            else
            {
                convertedTime += time.Seconds;
            }

            return convertedTime;
        }

        protected void CSSLoader()
        {
            customCSSLocation = "\"" + HelperClass.CustomCSSLoader() + "\"";
            W3CSSLocation = "\"" + HelperClass.W3CSSLoader() + "\"";
        }

        protected void PageImageLoader()
        {
            if (ConfigurationManager.AppSettings["UseExternalResources"] == "true")
            {
                #region Home button
                btnHome.ImageUrl = ConfigurationManager.AppSettings["PlayerPageUsedIcons"];
                #endregion Home button

                #region Play button
                imgInnerControlPlay.ImageUrl = ConfigurationManager.AppSettings["PlayControlIconLocation"];
                #endregion Play button

                #region Reverse button
                imgInnerControlReverse.ImageUrl = ConfigurationManager.AppSettings["ReverseControlIconLocation"];
                #endregion Reverse button

                #region Fast forward button
                imgInnerControlFastForward.ImageUrl = ConfigurationManager.AppSettings["FastForwardControlIconLocation"];
                #endregion Fast forward button

                #region Loading icon
                LoadingIcon = "\"" + ConfigurationManager.AppSettings["LoadingIconLocation"] + "\"";
                #endregion Loading icon
            }
            else
            {
                #region Home button
                btnHome.ImageUrl = "~/Sources/Images/MediaPlayer2Small.png";
                #endregion Home button

                #region Play button
                imgInnerControlPlay.ImageUrl = "~/Sources/Images/Controls/PlayW.png";
                #endregion Play button

                #region Reverse button
                imgInnerControlReverse.ImageUrl = "~/Sources/Images/Controls/BackwardW.png";
                #endregion Reverse button

                #region Fast forward button
                imgInnerControlFastForward.ImageUrl = "~/Sources/Images/Controls/ForwardW.png";
                #endregion Fast forward button

                #region Loading icon
                LoadingIcon = "\"Sources/Images/1522250897_1491486976_ภาพ 2.gif\"";
                #endregion Loading icon
            }
        }
    }
}