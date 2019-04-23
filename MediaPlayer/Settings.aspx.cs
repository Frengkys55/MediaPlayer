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