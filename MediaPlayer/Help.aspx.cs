using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaPlayer
{
    public partial class Help : System.Web.UI.Page
    {
        #region CSS location
        public static string customCSSLocation = string.Empty;
        public static string W3CSSLocation = string.Empty;
        #endregion CSS location

        protected void Page_Load(object sender, EventArgs e)
        {
            CSSLoader();
            PageImageLoader();
        }

        protected void imgLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void PageImageLoader()
        {
            if (ConfigurationManager.AppSettings["UseExternalResources"] == "true")
            {
                imgLogo.ImageUrl = ConfigurationManager.AppSettings["MediaPlayerLogoLocationB"];
            }
            else
            {
                imgLogo.ImageUrl = "~/Sources/Images/MediaPlayer2Small.png";
            }
        }

        protected void CSSLoader()
        {
            customCSSLocation = "\"" + HelperClass.CustomCSSLoader() + "\"";
            W3CSSLocation = "\"" + HelperClass.W3CSSLoader() + "\"";
        }
    }
}