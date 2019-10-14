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
        public static string SadImage = string.Empty;
        public static string SmirkImage = string.Empty;
        public static string PlayDeadImage = string.Empty;
        public static string CryingImage = string.Empty;
        public static string NyaaImage = string.Empty;
        public static string ShockedImage = string.Empty;
        public static string LoveImage = string.Empty;

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
                    if (Request.QueryString["message"] != null)
                    {
                        lbError.Text += "<br />" + Request.QueryString["message"];
                    }
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "22")
                {
                    lbError.Text = "Can't load your settings. Something is wrong...";
                    if (Request.QueryString["message"] != null)
                    {
                        lbError.Text += "<br />" + Request.QueryString["message"];
                    }
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "23")
                {
                    lbError.Text = "Can't create your info. Something is wrong...";
                    if (Request.QueryString["message"] != null)
                    {
                        lbError.Text += "<br />" + Request.QueryString["message"];
                    }
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "24")
                {
                    lbError.Text = "Can't create your settings. Something is wrong...";
                    if (Request.QueryString["message"] != null)
                    {
                        lbError.Text += "<br />" + Request.QueryString["message"];
                    }
                    errorImgURL = pokerFaceImage;
                    backgroundPosition = "left";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "25")
                {
                    lbError.Text = "Can't load your settings. Something is wrong...";
                    if (Request.QueryString["message"] != null)
                    {
                        lbError.Text += "<br />" + Request.QueryString["message"];
                    }
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

                #region File error handling (Error code 2xx)
                else if (Request.QueryString["id"] == "201")
                {
                    lbError.Text = "Either file name or directory or both is too long....";
                    errorImgURL = TeheperoImage;
                    urlHasBeenSet = true;
                    backgroundPosition = "right";
                }

                else if (Request.QueryString["id"] == "202")
                {
                    lbError.Text = "Can't save the video...";
                    if (Request.QueryString["message"] != null)
                    {
                        if (Request.QueryString["encoded"] == "true")
                        {
                            lbError.Text += "<br />" + HelperClass.StringEncoderDecoder(Request.QueryString["message"], StringConversionMode.Decode);
                        }
                        else
                        {
                            lbError.Text += "<br />" + Request.QueryString["message"];
                        }
                    }
                    errorImgURL = TeheperoImage;
                    urlHasBeenSet = true;
                    backgroundPosition = "right";
                }

                #endregion File error handling (Error code 2xx)

                #region Processing error handling (Error code 3xx)
                else if (Request.QueryString["id"] == "301")
                {
                    lbError.Text = "Processing video failed...";
                    if (Request.QueryString["message"] != null)
                    {
                        lbError.Text += "<br />" + Request.QueryString["message"];
                    }
                    errorImgURL = TeheperoImage;
                    urlHasBeenSet = true;
                    backgroundPosition = "right";
                }
                #endregion Processing error handling (Error code 3xx)

                #region User related problems
                else if (Request.QueryString["id"] == "901")
                {
                    lbError.Text = "It seems like the JavaScript was disabled.<br \\>This player uses javascript for playing video so please enable it and then click ";
                    if (Request.QueryString["message"] != null)
                    {
                        if (Request.QueryString["encoded"] == "true")
                        {
                            lbError.Text += "<a href=\"" + HelperClass.StringEncoderDecoder(Request.QueryString["message"], StringConversionMode.Decode) + "\">here</a>.";
                            
                        }
                        else
                        {
                            lbError.Text += "<a href=\"" + Request.QueryString["message"] + "\">here</a>.";
                        }
                    }
                    else
                    {
                        lbError.Text += "<a href=\"Index.aspx\">here</a>.";
                    }
                    errorImgURL = SadImage;
                    urlHasBeenSet = true;
                    backgroundPosition = "left";
                }
                else if (Request.QueryString["id"] == "902")
                {
                    lbError.Text = "You aren't alowed to download from that site...";
                    if (Request.QueryString["message"] != null)
                    {
                        lbError.Text += "<br >" + Request.QueryString["message"];
                    }
                    errorImgURL = SmirkImage;
                    urlHasBeenSet = true;
                    backgroundPosition = "right";
                }
                else if (Request.QueryString["id"] == "903")
                {
                    lbError.Text = "What i need is video and not page, so...";
                    if (Request.QueryString["message"] != null)
                    {
                        lbError.Text += "<br >" + Request.QueryString["message"];
                    }
                    errorImgURL = SmirkImage;
                    urlHasBeenSet = true;
                    backgroundPosition = "right";
                }
                #endregion User related problems
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
                SadImage = ConfigurationManager.AppSettings["SadImageLocation"];
                SmirkImage = ConfigurationManager.AppSettings["SmirkImageLocation"];
                PlayDeadImage = ConfigurationManager.AppSettings["PlayDeadImageLocation"];
                CryingImage = ConfigurationManager.AppSettings["CryingImageLocation"];
                NyaaImage = ConfigurationManager.AppSettings["NyaaImageLocation"];
                ShockedImage = ConfigurationManager.AppSettings["ShockedImageLocation"];
                LoveImage = ConfigurationManager.AppSettings["LoveImageLocation"];
            }
            else
            {
                pokerFaceImage = "Sources/Images/poker-face.png";
                notInterestedImage = "Sources/Images/anime_uninterested.png";
                ConfuseImage = "Sources/Images/anime_confuse.png";
                TeheperoImage = "Sources/Images/trtS8mw.png";
                SadImage = "Sources/Images/(GIF Image, 600 × 400 pixels).gif";
                SmirkImage = "Sources/Images/SeekPng.com_jackie-chan-png_806732.png";
                PlayDeadImage = "Sources/Images/641799.jpg";
                CryingImage = "Sources/Images/641700.jpg";
                NyaaImage = "Sources/Images/karen_by_ror362_d6fus3x.png";
                ShockedImage = "Sources/Images/Z2OOJZg.png";
                LoveImage = "Sources/Images/yaya_by_ror362_d6wwht4.png";
            }
        }
        protected void CSSLoader()
        {
            customCSSLocation = "\"" + HelperClass.CustomCSSLoader() + "\"";
            W3CSSLocation = "\"" + HelperClass.W3CSSLoader() + "\"";
        }
    }
}