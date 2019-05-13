using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaPlayer
{
    public partial class Error : System.Web.UI.Page
    {
        public static string errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";

        public static string pokerFaceImage = string.Empty;
        public static string notInterestedImage = string.Empty;
        public static string TeheperoImage = string.Empty;
        public static string ConfuseImage = string.Empty;


        public static string customCSSLocation = string.Empty;
        public static string W3CSSLocation = string.Empty;
        public static string backgroundPosition = "left";
        protected void Page_Load(object sender, EventArgs e)
        {
            CSSLoader();
            PageImageLoader();
            if (Request.QueryString.Count != 0)
            {
                bool urlHasBeenSet = false;
                if (Request.QueryString["id"] == "1")
                {
                    lbError.Text = "Couldn't play. The browser doesn't support this kind of things (for now)...";
                    backgroundPosition = "left";
                }
                else if (Request.QueryString["id"] == "10")
                {
                    lbError.Text = "There's an error that happening on the service (WCFAIOProcessor)";
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "20")
                {
                    lbError.Text = "There's an error that happening on the service (MediaPlayer)";
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "21")
                {
                    lbError.Text = "Can't load your info. Something is wrong...";
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "22")
                {
                    lbError.Text = "Can't load your settings. Something is wrong...";
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "23")
                {
                    lbError.Text = "Can't create your info. Something is wrong...";
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "24")
                {
                    lbError.Text = "Can't create your settings. Something is wrong...";
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "25")
                {
                    lbError.Text = "Can't load your settings. Something is wrong...";
                    errorImgURL = TeheperoImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                // Player error
                else if (Request.QueryString["id"] == "31")
                {
                    lbError.Text = "Can't process this video name...<br />tehepero...";
                    errorImgURL = TeheperoImage;
                    urlHasBeenSet = true;
                    backgroundPosition = "right";
                }

                else if (Request.QueryString["id"] == "50")
                {
                    lbError.Text = "sumanai... test mode...";
                    backgroundPosition = "left";
                }
                else if (Request.QueryString["id"] == "96")
                {
                    lbError.Text = "well.... you just found a not yet implemented function...";
                    errorImgURL = notInterestedImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "97")
                {
                    lbError.Text = "hmmm.... where did the ID go?";
    errorImgURL = ConfuseImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "98")
                {
                    lbError.Text = "hmmm.... where did the PID go?";
    errorImgURL = ConfuseImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "99")
                {
                    lbError.Text = "hmmm.... what did you do?";
                    backgroundPosition = "left";
                }
                else if (Request.QueryString["id"] == "100")
                {
                    lbError.Text = "This is an error page....";
                    backgroundPosition = "left";
                }
                #region File error handling
                else if (Request.QueryString["id"] == "201")
                {
                    lbError.Text = "Either file name or directory or both is too long....";
                    errorImgURL = TeheperoImage;
                    urlHasBeenSet = true;
                    backgroundPosition = "right";
                }
                #endregion File error handling
                else
                {
                    lbError.Text = "hmmmmmmmm....";
                    errorImgURL = notInterestedImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                if (!urlHasBeenSet)
                {
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                }
            }
            else
            {
                lbError.Text = "hmm? are you lost?<br />let me take you <a href=\"Index.aspx\">home</a>";
errorImgURL = ConfuseImage;
                backgroundPosition = "left";
            }
        }

        protected void PageImageLoader()
        {
            if (ConfigurationManager.AppSettings["UseExternalResources"] == "true")
            {
                pokerFaceImage = ConfigurationManager.AppSettings["PokerFaceImageLocation"];
                notInterestedImage = ConfigurationManager.AppSettings["PokerFaceImageLocation"];
                ConfuseImage = ConfigurationManager.AppSettings["ConfuseImageLocation"];
                TeheperoImage = ConfigurationManager.AppSettings["TeheperoImageLocation"];
            }
            else
            {
                pokerFaceImage = "Sources/Images/poker-face.png";
                notInterestedImage = "Sources/Images/anime_uninterested.png";
                ConfuseImage = "Sources/Images/anime_confuse.png";
                TeheperoImage = "Sources/Images/trtS8mw.png";
            }


        }
        protected void CSSLoader()
        {
            customCSSLocation = "\"" + HelperClass.CustomCSSLoader() + "\"";
            W3CSSLocation = "\"" + HelperClass.W3CSSLoader() + "\"";
        }
    }
}