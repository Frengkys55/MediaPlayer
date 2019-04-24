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
        VideoPlayerSettings savedSettings = new VideoPlayerSettings();
        VideoPlayerSettings newSettings = new VideoPlayerSettings();
        string settingsDatabase = string.Empty;
        string settingsTable = string.Empty;
        string connectionString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
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
            #endregion User info loading

            #region User player configuration loading
            // Check user player configuration
            VideoPlayerSettings userPlayerSetting = new VideoPlayerSettings();
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
                    Response.Redirect("Error.aspx?id=24");
                }
            }
            #endregion User player configuration loading

            #endregion Preparation

            
        }

        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            VideoPlayerSettings settings = new VideoPlayerSettings();
            HelperClass.UpdateSettings(settings, settingsDatabase, settingsTable, connectionString);
        }

        protected void btnCancelSettings_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }
    }
}