﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Configuration;
using System.Diagnostics;

namespace MediaPlayer
{
    public partial class Index : System.Web.UI.Page
    {
        #region Website configurations
        #region Web page configurations
        public static string panelHeight = "180px";
        public static string backgroundImage1 = string.Empty;
        public static string backgroundImage2 = string.Empty;
        public static string backgroundImage3 = string.Empty;
        public static string backgroundImage4 = string.Empty;
        public static string backgroundImage5 = string.Empty;
        public static string backgroundImage6 = string.Empty;
        public static string backgroundImage7 = string.Empty;
        #endregion Web page configurations

        #region Code behind configuration
        public static string videoSaveLocation = ConfigurationManager.AppSettings["videoSaveLocation"];
        bool isVideo = false;
        bool isURL = false;
        bool isFTP = false;
        bool isHTTP = false;
        bool locationChecked = false;
        string downloadedCompleteVideoLocalPath = string.Empty;

        AccessMode mode = AccessMode.Web;

        bool hasError = false;
        string errorMessage = string.Empty;

        VideoPlayerSettings playerSettings;
        #endregion Code behind configuration

        #region Database configurations
        bool useDatabase = true;
        string database = "MediaPlayerDatabase";
        string sessionInfoTable = "SessionInfo";
        string settingsTable = "UserSettings";
        string connectionString = string.Empty;
        #endregion Databae configurations
        #endregion Website configurations

        #region Cloud drive service configurations

        #endregion Cloud drive service configurations

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Loader
            ConfigurationLoader();
            if (!IsPostBack)
            {
                if (!UserLoader())
                {
                    Response.Redirect("Error.aspx?id=21");
                }
                if (!PlayerSettingsLoader())
                {
                    Response.Redirect("Error.aspx?id=22");
                }
            }
            BackgroundImageLoader();
            #endregion Loader

            #region Settings

            #region Page settings
            panelHeight = "180px";
            #endregion Page settings

            #region Mode settings
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["mode"] == "external")
                {
                    if (Request.QueryString["url"] != null)
                    {
                        txtURLSource.Text = Request.QueryString["url"];
                        btnLoadURLVideo_Click(sender, e);
                    }
                    else
                    {
                        Response.Redirect("Error.aspx");
                    }
                }
                else if (Request.QueryString["mode"] == "sample")
                {
                    Response.Redirect(LoadSample());
                }
                else if (Request.QueryString["mode"] == "other")
                {
                    mode = AccessMode.Other;
                }
            }
            #endregion Mode settings

            #endregion Settings
        }

        protected void btnUploadURL_Click(object sender, EventArgs e)
        {
            pnlURLPlay.Visible = true;
            pnlUploadPlay.Visible = false;

            btnUploadURL.Attributes.CssStyle.Clear();
            btnLoadUpload.Attributes.CssStyle.Clear();

            btnUploadURL.CssClass = "w3-button w3-theme w3-hover-theme w3-theme";
            btnLoadUpload.CssClass = "w3-button w3-hover-theme w3-white";
        }

        protected void btnLoadUpload_Click(object sender, EventArgs e)
        {
            pnlUploadPlay.Visible = true;
            pnlURLPlay.Visible = false;

            btnUploadURL.CssClass = "w3-button w3-hover-theme w3-white";
            btnLoadUpload.CssClass = "w3-button w3-hover-theme w3-theme";
        }

        protected void btnLoadURLVideo_Click(object sender, EventArgs e)
        {
            if (mode == AccessMode.Web || mode == AccessMode.External)
            {
                #region In line commands
                // Used to check for commands (wihtout additional information)
                if (txtURLSource.Text.ToLower() == "help" || txtURLSource.Text.ToLower() == "bantuan")
                {
                    Response.Redirect("Help.aspx");
                }
                else if (txtURLSource.Text.ToLower() == "error")
                {
                    Response.Redirect("Error.aspx?id=100");
                }
                else if (txtURLSource.Text.ToLower() == "setting" || txtURLSource.Text.ToLower() == "settings")
                {
                    Response.Redirect("Settings.aspx");
                }
                else if (txtURLSource.Text.ToLower() == "player")
                {
                    Response.Redirect("Player.aspx");
                }
                else if (txtURLSource.Text.ToLower() == "sample")
                {
                    Response.Redirect(LoadSample());
                }
                else if (txtURLSource.Text.ToLower() == "checker")
                {
                    Response.Redirect("Checker.aspx?mode=sample");
                }
                #endregion In line commands

                #region Read player settings
                VideoPlayerSettings settings = HelperClass.ReadPlayerSettings(Session.SessionID, database, settingsTable, connectionString);
                #endregion Read player settings

                #region Main Processing

                #region Service initialization
                VideoProcessingService.Service1Client videoProcessor = new VideoProcessingService.Service1Client();
                #endregion Service initialization

                #region Main process
                string[] receivedVideoInfo = videoProcessor.ProcessVideo2(txtURLSource.Text, true, Session.SessionID, true, 0, (int)settings.resolution, (float)settings.frameRate);
                #endregion Main process

                #region Result processor
                Stopwatch waitTime = new Stopwatch();
                waitTime.Start();

                if (receivedVideoInfo.Length == 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    if (waitTime.ElapsedMilliseconds == 20000)
                    {
                        waitTime.Stop();
                        Response.Redirect("Error.aspx");
                    }
                }
                videoProcessor.Close();
                if (receivedVideoInfo[0].ToLower().Contains("error"))
                {
                    Response.Redirect("Error.aspx?id=10a");
                }
                else
                {
                    string queryString = string.Empty;
                    queryString += "?new=true&";
                    queryString += "path=" + receivedVideoInfo[1] + "&";
                    queryString += "name=" + receivedVideoInfo[2] + "&";
                    queryString += "duration=" + receivedVideoInfo[3] + "&";
                    queryString += "framerate=" + receivedVideoInfo[4] + "&";
                    queryString += "startframe=" + receivedVideoInfo[5] + "&";
                    queryString += "endframe=" + receivedVideoInfo[6] + "&";
                    queryString += "videowidth=" + receivedVideoInfo[7];
                    Response.Redirect("Player.aspx" + queryString);
                }
                #endregion Result processor
                #endregion Main Processing
            }
            else if (mode == AccessMode.Other)
            {
                Server.Transfer("Error.aspx?id=96");
            }
            
        }

        protected void btnURLReset_Click(object sender, EventArgs e)
        {
            txtURLSource.Text = string.Empty;
        }
        
        protected void btnSettings_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Settings.aspx");
            
            if (pnlPlaySettings.Visible == false)
            {
                pnlPlaySettings.Visible = true;
                panelHeight = "250px";
                btnSettings.CssClass = "w3-button w3-theme w3-hover-white w3-border w3-border-theme w3-bar-item";
            }
            else
            {
                //Response.Redirect("Index.aspx");
                pnlPlaySettings.Visible = false;
                panelHeight = "180px";
                btnSettings.CssClass = "w3-button w3-white w3-hover-theme w3-border w3-border-theme w3-bar-item";
            }
        }

        protected void btnUploadVideo_Click(object sender, EventArgs e)
        {
            #region File saving configuration
            
            string saveLocation = string.Empty;
            bool requestDeleteFileAfterComplete = false;

            if (videoSaveLocation.EndsWith("\\"))
            {
                saveLocation = videoSaveLocation + Session.SessionID;
            }
            else
            {
                saveLocation = videoSaveLocation + "\\" + Session.SessionID;
            }

            // Path checking
            if (!Directory.Exists(saveLocation))
            {
                Directory.CreateDirectory(saveLocation);
            }

            if (videoSaveLocation.EndsWith("\\"))
            {
                saveLocation += uplVideo.PostedFile.FileName;
            }
            else
            {
                saveLocation += "\\" + uplVideo.PostedFile.FileName;
            }
            #endregion File saving configuration

            #region Received info configuration
            string networkAccessLocation = string.Empty;
            string videoName = string.Empty;
            string videoDuration = string.Empty;
            string frameRate = string.Empty;
            string startFrame = string.Empty;
            string endFrame = string.Empty;
            string videoWidth = string.Empty;
            string videoHeight = string.Empty;
            #endregion Received info configuration

            #region Video saving
            uplVideo.SaveAs(saveLocation);
            #endregion Video saving

            #region Video processing
            VideoProcessingService.Service1Client client = new VideoProcessingService.Service1Client();
            string[] receivedInfo = client.ProcessVideo2(saveLocation, true, Session.SessionID, true, 854, 480, 30);
            #endregion Video processing

            #region Received info processing
            if (receivedInfo[0].ToLower().Contains("error"))
            {
                Response.Redirect("Error.aspx?id=10");
                return;
            }

            networkAccessLocation = receivedInfo[1];
            videoName = receivedInfo[2];
            videoDuration = receivedInfo[3];
            frameRate = receivedInfo[4];
            startFrame = receivedInfo[5];
            endFrame = receivedInfo[6];
            videoWidth = receivedInfo[7];
            // videoHeight = receivedInfo[8]
            #endregion Received info processing

            #region Query string preparation
            string queryString = string.Empty;
            queryString += "?new=true&";
            queryString += "path=" + networkAccessLocation + "&";
            queryString += "name=" + videoName + "&";
            queryString += "duration=" + videoDuration + "&";
            queryString += "framerate=" + frameRate + "&";
            queryString += "startframe=" + startFrame + "&";
            queryString += "endframe=" + endFrame + "&";
            queryString += "videowidth=" + videoWidth;
            #endregion Query string preparation

            // Transfer page
            Response.Redirect("Player.aspx" + queryString);
        }

        protected void btnUploadReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btnUploadSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx");
        }

        protected string LoadSample()
        {
            return "Player.aspx?new=true&path=http://toshiba/test/Video/egqxbrd4xhzmgdbtk5vln3tn/[VTuber] HimeHina - RettouJoutou [MusicVideo]&name=[VTuber] HimeHina - RettouJoutou [MusicVideo]&duration=242,649&framerate=59,939998626709&startframe=1&endframe=14544&videowidth=854";
        }

        protected void btnMoreSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx");
        }

        VideoPlayerSettings LoadDefault()
        {
            VideoPlayerSettings settings = new VideoPlayerSettings();
            settings.resolution = Resolution.SD_480p;
            settings.frameRate = FrameRate._24fps;
            settings.preloadFrames = PreloadFrames.EnablePreload;
            settings.bufferMode = BufferMode.SingleBuffer;
            return settings;
        }

        #region Loader functions
        protected void BackgroundImageLoader()
        {
            backgroundImage1 = ConfigurationManager.AppSettings["backgroundImage1"];
            backgroundImage2 = ConfigurationManager.AppSettings["backgroundImage2"];
            backgroundImage3 = ConfigurationManager.AppSettings["backgroundImage3"];
            backgroundImage4 = ConfigurationManager.AppSettings["backgroundImage4"];
            backgroundImage5 = ConfigurationManager.AppSettings["backgroundImage5"];
            backgroundImage6 = ConfigurationManager.AppSettings["backgroundImage6"];
            backgroundImage7 = ConfigurationManager.AppSettings["backgroundImage7"];
        }

        protected void ConfigurationLoader()
        {
            #region Database configuration
            if (ConfigurationManager.AppSettings["useDatabase"].ToLower() == "true")
            {
                useDatabase = true;
            }
            else
            {
                useDatabase = false;
            }
            connectionString = ConfigurationManager.AppSettings["databaseConnectionString"];
            #endregion Database configuration
        }

        protected bool UserLoader()
        {
            if (!HelperClass.CheckUser(database, sessionInfoTable, Session.SessionID, connectionString))
            {
                HelperClass.AddUser(database, sessionInfoTable, Session.SessionID, connectionString);
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool PlayerSettingsLoader()
        {
            

            if (!HelperClass.CheckSettings(database, sessionInfoTable, settingsTable, Session.SessionID, connectionString))
            {
                HelperClass.CreateNewSettings(database, settingsTable, Session.SessionID, connectionString);
            }
            else
            {
                return false;
            }

            playerSettings = HelperClass.ReadPlayerSettings(Session.SessionID, database, settingsTable, connectionString);

            return true;
        }

        #endregion Loader functions

        #region Validation functions

        bool CheckURL(string URL)
        {
            if (URL.Contains("http://"))
            {
                isHTTP = true;
                isFTP = false;
                locationChecked = true;
                return true;
            }
            else if (URL.Contains("ftp://"))
            {
                isHTTP = false;
                isFTP = true;
                locationChecked = true;
                return true;
            }

            return false;
        }

        bool CheckVideo(string URL)
        {
            if (URL.EndsWith(".webm") ||
                URL.EndsWith(".mkv") ||
                URL.EndsWith(".flv") ||
                URL.EndsWith(".vob") ||
                URL.EndsWith(".ogv") ||
                URL.EndsWith(".drc") ||
                URL.EndsWith(".mng") ||
                URL.EndsWith(".avi") ||
                URL.EndsWith(".mts") ||
                URL.EndsWith(".m2ts") ||
                URL.EndsWith(".mov") ||
                URL.EndsWith(".qt") ||
                URL.EndsWith(".wmv") ||
                URL.EndsWith(".yuv") ||
                URL.EndsWith(".rm") ||
                URL.EndsWith(".rmvb") ||
                URL.EndsWith(".asf") ||
                URL.EndsWith(".amv") ||
                URL.EndsWith(".mp4") ||
                URL.EndsWith(".mpg") ||
                URL.EndsWith(".mpeg") ||
                URL.EndsWith(".m4v") ||
                URL.EndsWith(".svi") ||
                URL.EndsWith(".3gp") ||
                URL.EndsWith(".3g2"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Validation functions

        string GetFileName(string location, bool withExtension = false)
        {
            string[] tempLocation = location.Split('?');
            string resultLocation = string.Empty;

            // Check if the location is an url
            if (!locationChecked)
            {
                isURL = CheckURL(location);
            }

            if (isHTTP)
            {
                // Check again if the splitted location is still url
                foreach (var item in tempLocation)
                {
                    if (CheckURL(item))
                    {
                        if (item.Contains("?"))
                        {
                            resultLocation = item.Trim('?');
                            break;
                        }
                        else
                        {
                            resultLocation = item;
                        }
                    }
                }

                if (withExtension)
                {
                    return Path.GetFileName(resultLocation);
                }
                else
                {
                    return Path.GetFileNameWithoutExtension(resultLocation);
                }
            }
            else if (isFTP)
            {
                throw new NotImplementedException("Not yet implemented");
            }
            else
            {
                if (withExtension)
                {
                    return Path.GetFileName(location);
                }
                else
                {
                    return Path.GetFileNameWithoutExtension(location);
                }
            }
        }

        string URLDecode(string URL)
        {
            string resultURL = string.Empty;
            if (URL.Contains("%20"))
            {
                resultURL = URL.Replace("%20", " ");
            }
            return resultURL;
        }
    }
}
