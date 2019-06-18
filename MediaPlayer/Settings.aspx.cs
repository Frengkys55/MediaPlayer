using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaPlayer
{
    public partial class SiteSettings : System.Web.UI.Page
    {
        public static string customCSSLocation = string.Empty;
        public static string W3CSSLocation = string.Empty;
        
        VideoPlayerSettings savedSettings = new VideoPlayerSettings();
        VideoPlayerSettings newSettings = new VideoPlayerSettings();
        string settingsDatabase = string.Empty;
        string settingsTable = string.Empty;
        string connectionString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            CSSLoader();
            //savedSettings = HelperClass.ReadPlayerSettings(Session.SessionID, settingsDatabase, settingsTable, connectionString);

            #region Preparation

            #region System setting loading
            SystemConfiguration systemConfiguration = HelperClass.SystemConfigurationLoader();
            #endregion System setting loading

            #region User info loading
            UserInfo userInfo = new UserInfo {
                SessionID = Session.SessionID
            };

            // Try reading user information
            try
            {
                if (HelperClass.CheckUser("MediaPlayerDatabase", "SessionInfo", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                {
                    // Load user info
                    SQLClassPeralatan.MintaDataDatabase mintaDataDatabase = new SQLClassPeralatan.MintaDataDatabase("UserID", "SessionInfo", "SessionID", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString);
                    userInfo.UserID = Convert.ToInt32(mintaDataDatabase.DataDiterima);
                }
                else
                {
                    // Add user info
                    if (HelperClass.AddUser("MediaPlayerDatabase", "SessionInfo", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                    {
                        // Read user id
                        SQLClassPeralatan.MintaDataDatabase mintaDataDatabase = new SQLClassPeralatan.MintaDataDatabase("UserID", "SessionInfo", "SessionID", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString);
                        if (!mintaDataDatabase.TerdapatKesalahan)
                        {
                            userInfo.UserID = Convert.ToInt32(mintaDataDatabase.DataDiterima);
                        }
                        else
                        {
                            Response.Redirect("Error.aspx?id=21");
                        }
                    }
                    else
                    {
                        Response.Redirect("Error.aspx?id=23");
                    }
                }
            }
            catch (Exception err)
            {
                Response.Redirect("Error.aspx?id=21&message=" + err.Message);
            }
            #endregion User info loading

            #region User player configuration loading
            VideoPlayerSettings userPlayerSetting = new VideoPlayerSettings();

            try
            {
                if (!IsPostBack)
                {
                    // Check user player configuration
                    if (HelperClass.CheckSettings("MediaPlayerDatabase", userInfo, "UserSettings", systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                    {
                        userPlayerSetting = HelperClass.ReadPlayerSettings(userInfo.SessionID, "MediaPlayerDatabase", "UserSettings", systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString);
                    }
                    else
                    {
                        if (HelperClass.CreateNewSettings("MediaPlayerDatabase", "UserSettings", userInfo, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
                        {
                            userPlayerSetting = HelperClass.ReadPlayerSettings(userInfo.SessionID, "MediaPlayerDatabase", "UserSettings", systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString);
                        }
                        else
                        {
                            Response.Redirect("Error.aspx?id=24&message=Maybe a server problem");
                        }
                    }
                }

            }
            catch (Exception err)
            {
                Response.Redirect("Error.aspx?id=24&message=" + err.Message);
            }
            #endregion User player configuration loading

            #endregion Preparation

            if (!IsPostBack)
            {
                lstVideoResolution.SelectedValue = ((int)userPlayerSetting.resolution).ToString();
                lstFrameRate.SelectedValue = ((int)userPlayerSetting.frameRate).ToString();
                lstFrameBufferMode.SelectedValue = ((int)userPlayerSetting.bufferMode).ToString();
                lstFramePreload.SelectedValue = ((int)userPlayerSetting.preloadFrames).ToString();
                lstPlayMode.SelectedIndex = 0;
            }
            
        }

        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            #region Preparation
            VideoPlayerSettings settings = new VideoPlayerSettings();
            SystemConfiguration systemConfiguration = HelperClass.SystemConfigurationLoader();
            UserInfo userInfo = new UserInfo();
            #endregion Preparation

            #region User info loading
            userInfo.SessionID = Session.SessionID;
            // Try reading user information
            if (HelperClass.CheckUser("MediaPlayerDatabase", "SessionInfo", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
            {
                // Load user info
                SQLClassPeralatan.MintaDataDatabase mintaDataDatabase = new SQLClassPeralatan.MintaDataDatabase("UserID", "SessionInfo", "SessionID", userInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString);
                userInfo.UserID = Convert.ToInt32(mintaDataDatabase.DataDiterima);
            }
            else
            {
                Response.Redirect("Error.aspx?id=21");
            }
            #endregion User info loading

            #region Resolution
            int selectedResolution = Convert.ToInt32(lstVideoResolution.SelectedValue);
            
            if (selectedResolution == (int)Resolution.Original)
            {
                settings.resolution = Resolution.Original;
            }
            else if (selectedResolution == (int)Resolution.SD_360p)
            {
                settings.resolution = Resolution.SD_360p;
            }
            else if (selectedResolution == (int)Resolution.SD_480p)
            {
                settings.resolution = Resolution.SD_480p;
            }
            else if (selectedResolution == (int)Resolution.HD_720p)
            {
                settings.resolution = Resolution.HD_720p;
            }
            else if (selectedResolution == (int)Resolution.HD_1080p)
            {
                settings.resolution = Resolution.HD_1080p;
            }
            else if (selectedResolution == (int)Resolution.SUHD_1440p)
            {
                settings.resolution = Resolution.SUHD_1440p;
            }
            else
            {
                settings.resolution = Resolution.Other;
            }
            #endregion Resolution

            #region Framerate
            int selectedFramerate = Convert.ToInt32(lstFrameRate.SelectedValue);
            if (selectedFramerate == (int)FrameRate.Default)
            {
                settings.frameRate = FrameRate.Default;
            }
            else if (selectedFramerate == (int)FrameRate._24fps)
            {
                settings.frameRate = FrameRate._24fps;
            }
            else if (selectedFramerate == (int)FrameRate._30fps)
            {
                settings.frameRate = FrameRate._30fps;
            }
            else if (selectedFramerate == (int)FrameRate._60fps)
            {
                settings.frameRate = FrameRate._60fps;
            }
            else if (selectedFramerate == (int)FrameRate._120fps)
            {
                settings.frameRate = FrameRate._120fps;
            }
            else
            {
                settings.frameRate = FrameRate.Other;
            }
            #endregion Framerate

            #region Buffer mode
            int selectedBufferMode = Convert.ToInt32(lstFrameBufferMode.SelectedValue);

            if (selectedBufferMode == (int)BufferMode.SingleBuffer)
            {
                settings.bufferMode = BufferMode.SingleBuffer;
            }
            else if (selectedBufferMode == (int)BufferMode.DoubleBuffer)
            {
                settings.bufferMode = BufferMode.DoubleBuffer;
            }
            else
            {
                settings.bufferMode = BufferMode.TripleBuffer;
            }
            #endregion Buffer mode

            #region Frame preload
            int selectedFramePreload = Convert.ToInt32(lstFramePreload.SelectedValue);

            if (selectedFramePreload == (int)PreloadFrames.EnablePreload)
            {
                settings.preloadFrames = PreloadFrames.EnablePreload;
            }
            else
            {
                settings.preloadFrames = PreloadFrames.DisablePreload;
            }

            #endregion Frame preload

            FunctionResult result = HelperClass.UpdateSettings(settings, userInfo, "MediaPlayerDatabase", "UserSettings", connectionString);
            if (result.functionResult == Result.Fail)
            {
                Response.Redirect("Error.aspx?id=25");
            }
            Response.Redirect("Index.aspx");
        }

        protected void btnCancelSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
        protected void CSSLoader()
        {
            customCSSLocation = "\"" + HelperClass.CustomCSSLoader() + "\"";
            W3CSSLocation = "\"" + HelperClass.W3CSSLoader() + "\"";
        }
    }
}