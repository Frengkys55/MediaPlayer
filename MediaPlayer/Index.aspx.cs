using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Configuration;
using System.Diagnostics;
using System.Text;

namespace MediaPlayer
{
    public partial class Index : System.Web.UI.Page
    {
        #region Website configurations
        #region Web page configurations
        #region Image location
        public static string mediaPlayerLogoLocation = string.Empty;
        #endregion Image location

        #region CSS location
        public static string customCSSLocation = string.Empty;
        public static string W3CSSLocation = string.Empty;
        #endregion CSS location

        public static string panelHeight = "180px";

        #region Background images
        public static string backgroundImage0 = string.Empty;
        public static string backgroundImage1 = string.Empty;
        public static string backgroundImage2 = string.Empty;
        public static string backgroundImage3 = string.Empty;
        public static string backgroundImage4 = string.Empty;
        public static string backgroundImage5 = string.Empty;
        public static string backgroundImage6 = string.Empty;
        public static string backgroundImage7 = string.Empty;
        #endregion Backkground images
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

        #region Selected mode
        PlayMode userPlayMode;
        #endregion Selected mode
        #endregion Website configurations

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                userPlayMode = PlayMode.Url;
                #region Default mode
                if (ConfigurationManager.AppSettings["DefaultMode"].ToLower() == "file")
                {
                    btnLoadUpload_Click(this, e);
                }
                #endregion Default mode
            }

            #region Loader
            if (ConfigurationManager.AppSettings["loadBackground"] == "true")
            {
                BackgroundImageLoader();
            }
            CSSLoader();
            PageImageLoader();
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
                        Response.Redirect("Error.aspx?id=98");
                    }
                }
                else if (Request.QueryString["mode"] == "sample")
                {
                    Response.Redirect(LoadSample("sample1"));
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
            if (pnlPlaySettings.Visible)
            {
                btnSettings_Click(this, e);
            }

            pnlURLPlay.Visible = true;
            pnlUploadPlay.Visible = false;

            btnUploadURL.Attributes.CssStyle.Clear();
            btnLoadUpload.Attributes.CssStyle.Clear();

            btnUploadURL.CssClass = "w3-button w3-theme w3-hover-theme w3-theme";
            btnLoadUpload.CssClass = "w3-button w3-hover-theme w3-white";
            btnSettings.CssClass = "w3-button w3-white w3-hover-theme w3-border w3-border-theme w3-bar-item";
        }

        protected void btnLoadUpload_Click(object sender, EventArgs e)
        {
            if (pnlPlaySettings.Visible)
            {
                btnUploadSettings_Click(this, e);
            }
            pnlUploadPlay.Visible = true;
            pnlURLPlay.Visible = false;

            btnUploadURL.CssClass = "w3-button w3-hover-theme w3-white";
            btnLoadUpload.CssClass = "w3-button w3-hover-theme w3-theme";
            btnUploadSettings.CssClass = "w3-button w3-white w3-hover-theme w3-border w3-border-theme w3-bar-item";
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
                else if (txtURLSource.Text.ToLower() == "sample1" || txtURLSource.Text.ToLower() == "sample2" || txtURLSource.Text.ToLower() == "sample3" || txtURLSource.Text.ToLower() == "sample4" || txtURLSource.Text.ToLower() == "sample5")
                {
                    Response.Redirect(LoadSample(txtURLSource.Text));
                }
                else if (txtURLSource.Text.ToLower() == "checker")
                {
                    Response.Redirect("Checker.aspx?mode=sample");
                }
                #endregion In line commands

                #region (Old) Main Processing

                //#region Old main processing
                //#region Service initialization
                //VideoProcessingService.Service1Client videoProcessor = new VideoProcessingService.Service1Client();
                //#endregion Service initialization

                //#region Main process
                //string[] receivedVideoInfo = videoProcessor.ProcessVideo2(txtURLSource.Text, true, Session.SessionID, true, 0, (int)settings.resolution, (float)settings.frameRate);
                //#endregion Main process
                //#endregion Old main processing

                //#region Result processor
                //Stopwatch waitTime = new Stopwatch();
                //waitTime.Start();

                //if (receivedVideoInfo.Length == 0)
                //{
                //    System.Threading.Thread.Sleep(1000);
                //    if (waitTime.ElapsedMilliseconds == 20000)
                //    {
                //        waitTime.Stop();
                //        Response.Redirect("Error.aspx");
                //    }
                //}
                //videoProcessor.Close();
                //if (receivedVideoInfo[0].ToLower().Contains("error"))
                //{
                //    Response.Redirect("Error.aspx?id=10a");
                //}
                //else
                //{
                //    string queryString = string.Empty;
                //    queryString += "?new=true&";
                //    queryString += "path=" + receivedVideoInfo[1] + "&";
                //    queryString += "name=" + receivedVideoInfo[2] + "&";
                //    queryString += "duration=" + receivedVideoInfo[3] + "&";
                //    queryString += "framerate=" + receivedVideoInfo[4] + "&";
                //    queryString += "startframe=" + receivedVideoInfo[5] + "&";
                //    queryString += "endframe=" + receivedVideoInfo[6] + "&";
                //    queryString += "videowidth=" + receivedVideoInfo[7];
                //    Response.Redirect("Player.aspx" + queryString);
                //}
                //#endregion Result processor
                #endregion (Old) Main Processing

                #region  New processing
                #region Preparation
                string databaseName = "MediaPlayerDatabase";
                string userTableName = "SessionInfo";
                string settingsTable = "UserSettings";

                Processor mainProcessor = new Processor();
                UserInfo userInfo = new UserInfo();
                VideoProcessingInformation videoProcessingInformation = new VideoProcessingInformation();

                #region System configuration loader
                SystemConfiguration systemConfiguration = HelperClass.SystemConfigurationLoader();
                #endregion System configuration loader

                #region User information loader
                // Check for user information in database
                if (HelperClass.CheckUser(databaseName, userTableName, Session.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                {
                    userInfo.SessionID = Session.SessionID;
                    SQLClassPeralatan.MintaDataDatabase mintaDataDatabase = new SQLClassPeralatan.MintaDataDatabase("UserID", userTableName, "SessionID", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString); ;
                    if (!mintaDataDatabase.TerdapatKesalahan)
                    {
                        userInfo.UserID = Convert.ToInt32(mintaDataDatabase.DataDiterima);
                    }
                }
                else
                {
                    userInfo.SessionID = Session.SessionID;
                    if (!HelperClass.AddUser(databaseName, userTableName, userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                    {
                        Response.Redirect("Error.aspx?id=23");
                    }
                    SQLClassPeralatan.MintaDataDatabase mintaDataDatabase = new SQLClassPeralatan.MintaDataDatabase("UserID", userTableName, "SessionID", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString); ;
                    if (!mintaDataDatabase.TerdapatKesalahan)
                    {
                        userInfo.UserID = Convert.ToInt32(mintaDataDatabase.DataDiterima);
                    }
                }
                #endregion User information loader

                #region Player settings loader
                VideoPlayerSettings settings = new VideoPlayerSettings();
                // Check settings
                if (HelperClass.CheckSettings(databaseName, userInfo, settingsTable, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                {
                    settings = HelperClass.ReadPlayerSettings(Session.SessionID, database, settingsTable, connectionString);
                }
                else
                {
                    if (HelperClass.CreateNewSettings(databaseName, settingsTable, userInfo, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                    {
                        settings = HelperClass.ReadPlayerSettings(Session.SessionID, database, settingsTable, connectionString);
                    }
                }
                #endregion Player settings loader

                #endregion Preparation;

                #region Information generation
                videoProcessingInformation.VideoLocations.VideoLocation = txtURLSource.Text;
                videoProcessingInformation.VideoLocations.videoSaveLocation = systemConfiguration.ProcessedVideoSaveLocation;
                videoProcessingInformation.VideoLocations.videoNetworkSaveLocation = systemConfiguration.NetworkProcessedVideoSaveLocation;
                videoProcessingInformation.VideoSetting.processedVideoResolution = settings.resolution;
                videoProcessingInformation.VideoSetting.frameRate = settings.frameRate;
                videoProcessingInformation.VideoSetting.audioProcessing = AudioProcessing.ProcessAudio;

                userInfo.SessionID = Session.SessionID;
                #endregion Information generation

                #region Main process
                ProcessedVideo processedVideo = mainProcessor.ProcessVideo(videoProcessingInformation, systemConfiguration, userInfo);
                #endregion Main process

                string queryString = string.Empty;
                if (processedVideo.result == Result.Success)
                {
                    queryString += "?new=true&";
                    queryString += "path=" + processedVideo.networkAccessLocation + "&";
                    queryString += "name=" + processedVideo.videoName + "&";
                    queryString += "duration=" + processedVideo.videoDuration + "&";
                    queryString += "framerate=" + processedVideo.frameRate + "&";
                    queryString += "startframe=" + processedVideo.startFrame + "&";
                    queryString += "endframe=" + processedVideo.endFrame + "&";
                    queryString += "videoresolution=" + processedVideo.videoHeight + "&";
                    queryString += "pid=" + processedVideo.processID;
                    if (Convert.ToInt32(lstPlayingSpeed.SelectedValue) > 0)
                    {
                        queryString += "&playspeed=" + lstPlayingSpeed.SelectedValue;
                    }
                    if (txtCustomPlayTime.Text != string.Empty)
                    {
                        queryString += "&timeposition=" + txtCustomPlayTime.Text;
                    }


                    Response.Redirect("Player.aspx" + queryString);
                }

                #endregion New processing
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
            //Response.Redirect("Error.aspx?id=96");
            //try
            //{
                #region Preparation

                #region Initialization

                #region Database configuration
                string databaseName = "MediaPlayerDatabase";
                string userTableName = "SessionInfo";
                string settingsTable = "UserSettings";
                #endregion Database configuration

                #region Processor initialization
                Processor mainProcessor = new Processor();
                #endregion Processor initialization

                #region User information
                UserInfo userInfo = new UserInfo();
                #endregion User information

                #region Video processing information
                string videoName = string.Empty;
                string videoNameWithoutExtension = string.Empty;
                ProcessedVideo processedVideo = new ProcessedVideo();
                VideoProcessingInformation videoProcessingInformation = new VideoProcessingInformation();
                #endregion Video processing information

                #endregion Initialization

                #region System configuration loader
                SystemConfiguration systemConfiguration = HelperClass.SystemConfigurationLoader();
                #endregion System configuration loader

                #region User information loader
                // Check for user information in database
                if (HelperClass.CheckUser(databaseName, userTableName, Session.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                {
                    userInfo.SessionID = Session.SessionID;
                    SQLClassPeralatan.MintaDataDatabase mintaDataDatabase = new SQLClassPeralatan.MintaDataDatabase("UserID", userTableName, "SessionID", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString); ;
                    if (!mintaDataDatabase.TerdapatKesalahan)
                    {
                        userInfo.UserID = Convert.ToInt32(mintaDataDatabase.DataDiterima);
                    }
                }
                else
                {
                    userInfo.SessionID = Session.SessionID;
                    if (!HelperClass.AddUser(databaseName, userTableName, userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                    {
                        Response.Redirect("Error.aspx?id=23");
                    }
                    SQLClassPeralatan.MintaDataDatabase mintaDataDatabase = new SQLClassPeralatan.MintaDataDatabase("UserID", userTableName, "SessionID", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString); ;
                    if (!mintaDataDatabase.TerdapatKesalahan)
                    {
                        userInfo.UserID = Convert.ToInt32(mintaDataDatabase.DataDiterima);
                    }
                }
                #endregion User information loader

                #region Player settings loader
                VideoPlayerSettings settings = new VideoPlayerSettings();

                if (HelperClass.CheckSettings(databaseName, userInfo, settingsTable, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                {
                    settings = HelperClass.ReadPlayerSettings(Session.SessionID, database, settingsTable, connectionString);
                }
                else
                {
                    if (HelperClass.CreateNewSettings(databaseName, settingsTable, userInfo, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                    {
                        settings = HelperClass.ReadPlayerSettings(Session.SessionID, database, settingsTable, connectionString);
                    }
                }
                #endregion Player settings loader

                #region Video information preparation
                //processedVideo.videoName =  HttpUtility.UrlEncode(HelperClass.StringEncoderDecoder(Path.GetFileNameWithoutExtension(uplVideo.FileName), StringConversionMode.Encode));
                processedVideo.videoName = HttpUtility.UrlEncode(HelperClass.StringEncoderDecoder(Path.GetFileName(uplVideo.FileName), StringConversionMode.Encode));
                //processedVideo.processedVideoName = Convert.ToBase64String(Encoding.UTF8.GetBytes(processedVideo.videoName));
                #endregion Video information preparation

                #endregion Preparation

                #region File saving configuration

                //string saveLocation = string.Empty;

                bool requestDeleteFileAfterComplete = false;

                #region Temporary download location
                if (videoSaveLocation.EndsWith("\\"))
                {
                    processedVideo.localAccessLocation = videoSaveLocation + Session.SessionID;
                }
                else
                {
                    processedVideo.localAccessLocation = videoSaveLocation + "\\" + Session.SessionID;
                }

                // Path checking
                if (!Directory.Exists(processedVideo.localAccessLocation))
                {
                    Directory.CreateDirectory(processedVideo.localAccessLocation);
                }

                if (processedVideo.localAccessLocation.EndsWith("\\"))
                {
                    //processedVideo.localAccessLocation += processedVideo.processedVideoName + Path.GetExtension(uplVideo.FileName);
                    processedVideo.localAccessLocation += Convert.ToBase64String(Encoding.UTF8.GetBytes(processedVideo.videoName)) + Path.GetExtension(uplVideo.FileName);
                }
                else
                {
                    processedVideo.localAccessLocation += "\\" + Convert.ToBase64String(Encoding.UTF8.GetBytes(processedVideo.videoName)) + Path.GetExtension(uplVideo.FileName); ;
                }
                #endregion Temporary download location


                #endregion File saving configuration

                #region Received info configuration
                string networkAccessLocation = string.Empty;
                //string videoName = string.Empty;
                string videoDuration = string.Empty;
                string frameRate = string.Empty;
                string startFrame = string.Empty;
                string endFrame = string.Empty;
                string videoWidth = string.Empty;
                string videoHeight = string.Empty;

                #endregion Received info configuration

                #region Video saving
                uplVideo.SaveAs(processedVideo.localAccessLocation);
                #endregion Video saving

                #region Information generation
                videoProcessingInformation.VideoLocations.VideoLocation = processedVideo.localAccessLocation;
                videoProcessingInformation.VideoLocations.videoSaveLocation = systemConfiguration.ProcessedVideoSaveLocation;
                videoProcessingInformation.VideoLocations.videoNetworkSaveLocation = systemConfiguration.NetworkProcessedVideoSaveLocation;
                videoProcessingInformation.VideoSetting.processedVideoResolution = settings.resolution;
                videoProcessingInformation.VideoSetting.frameRate = settings.frameRate;
                videoProcessingInformation.VideoSetting.audioProcessing = AudioProcessing.ProcessAudio;

                #endregion Information generation

                #region Video processing

                ProcessedVideo processedVideo2 = mainProcessor.ProcessVideo(videoProcessingInformation, systemConfiguration, userInfo, true);

                //VideoProcessingService.Service1Client client = new VideoProcessingService.Service1Client();
                //string[] receivedInfo = client.ProcessVideo2(saveLocation, true, Session.SessionID, true, 854, 480, 30);
                #endregion Video processing

                #region Result combination
                string temporaryData = processedVideo.videoName;
                processedVideo = processedVideo2;
                processedVideo.videoName = temporaryData;
                #endregion Result combination

                #region Query string preparation
                string queryString = string.Empty;

                if (processedVideo.result == Result.Success)
                {
                    queryString += "?new=true&";
                    queryString += "mode=upload&";
                    queryString += "path=" + processedVideo.networkAccessLocation + "&";
                    queryString += "name=" + processedVideo.videoName + "&";
                    queryString += "duration=" + processedVideo.videoDuration + "&";
                    queryString += "framerate=" + processedVideo.frameRate + "&";
                    queryString += "startframe=" + processedVideo.startFrame + "&";
                    queryString += "endframe=" + processedVideo.endFrame + "&";
                    queryString += "videoresolution=" + processedVideo.videoHeight + "&";
                    queryString += "pid=" + processedVideo.processID;
                    if (Convert.ToInt32(lstPlayingSpeed.SelectedValue) > 0)
                    {
                        queryString += "&playspeed=" + lstPlayingSpeed.SelectedValue;
                    }
                    if (txtCustomPlayTime.Text != string.Empty)
                    {
                        queryString += "&timeposition=" + txtCustomPlayTime.Text;
                    }


                    Response.Redirect("Player.aspx" + queryString);
                }
                else
                {
                    Response.Redirect("Error.aspx?id=301");
                }

                #endregion Query string preparation

                // Transfer page
                Response.Redirect("Player.aspx" + queryString);
            //}
            //catch (Exception err)
            //{
            //    Response.Redirect("Error.aspx?id=202&message=" + err.Message);
            //}
            
        }

        protected void btnUploadReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }

        protected void btnUploadSettings_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Settings.aspx");

            if (pnlPlaySettings.Visible == false)
            {
                pnlPlaySettings.Visible = true;
                panelHeight = "250px";
                btnUploadSettings.CssClass = "w3-button w3-theme w3-hover-white w3-border w3-border-theme w3-bar-item";
            }
            else
            {
                //Response.Redirect("Index.aspx");
                pnlPlaySettings.Visible = false;
                panelHeight = "180px";
                btnUploadSettings.CssClass = "w3-button w3-white w3-hover-theme w3-border w3-border-theme w3-bar-item";
            }
        }

        protected void btnMoreSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Settings.aspx");
        }

        protected string LoadSample(string information)
        {
            int sampleNumber = 0;
            if (Int32.TryParse(information.ToLower().Replace("sample", ""), out sampleNumber))
            {
                return HelperClass.HostChanger(HelperClass.StringEncoderDecoder(ConfigurationManager.AppSettings["SampleVideo" + sampleNumber], StringConversionMode.Decode));
            }
            else
            {
                return HelperClass.HostChanger(HelperClass.StringEncoderDecoder(ConfigurationManager.AppSettings["SampleVideo1"], StringConversionMode.Decode));
            }
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
            backgroundImage0 = ConfigurationManager.AppSettings["backgroundImage0"];
            backgroundImage1 = ConfigurationManager.AppSettings["backgroundImage1"];
            backgroundImage2 = ConfigurationManager.AppSettings["backgroundImage2"];
            backgroundImage3 = ConfigurationManager.AppSettings["backgroundImage3"];
            backgroundImage4 = ConfigurationManager.AppSettings["backgroundImage4"];
            backgroundImage5 = ConfigurationManager.AppSettings["backgroundImage5"];
            backgroundImage6 = ConfigurationManager.AppSettings["backgroundImage6"];
            backgroundImage7 = ConfigurationManager.AppSettings["backgroundImage7"];
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
                imgLogo.ImageUrl = ConfigurationManager.AppSettings["IndexPageUsedIcons"];
            }
            else
            {
                imgLogo.ImageUrl = "~/Sources/Images/MediaPlayer2Small.png";
            }
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

        //protected bool PlayerSettingsLoader()
        //{
            
        //    if (!HelperClass.CheckSettings(database, sessionInfoTable, settingsTable, Session.SessionID, connectionString))
        //    {
        //        HelperClass.CreateNewSettings(database, settingsTable, Session.SessionID, connectionString);
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //    playerSettings = HelperClass.ReadPlayerSettings(Session.SessionID, database, settingsTable, connectionString);

        //    return true;
        //}

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
