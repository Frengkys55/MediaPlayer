using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaPlayer
{
    public partial class Error : System.Web.UI.Page
    {
        public static string errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                bool urlHasBeenSet = false;
                if (Request.QueryString["id"] == "1")
                {
                    lbError.Text = "Couldn't play. The browser doesn't support this kind of things (for now)...";
                }
                else if (Request.QueryString["id"] == "10")
                {
                    lbError.Text = "There's an error that happening on the service (WCFAIOProcessor)";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "20")
                {
                    lbError.Text = "There's an error that happening on the service (MediaPlayer)";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "21")
                {
                    lbError.Text = "Can't load your info. Something is wrong...";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "22")
                {
                    lbError.Text = "Can't load your settings. Something is wrong...";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "23")
                {
                    lbError.Text = "Can't create your info. Something is wrong...";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "24")
                {
                    lbError.Text = "Can't create your settings. Something is wrong...";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "50")
                {
                    lbError.Text = "sumanai... test mode...";
                }
                else if (Request.QueryString["id"] == "96")
                {
                    lbError.Text = "well.... you just found a not yet implemented function...";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime_uninterested.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "97")
                {
                    lbError.Text = "hmmm.... where did the id go?";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime_confuse.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "98")
                {
                    lbError.Text = "hmmm.... where did the url go?";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime_confuse.png";
                    urlHasBeenSet = true;
                }
                else if (Request.QueryString["id"] == "99")
                {
                    lbError.Text = "hmmm.... what did you do?";
                }
                else if (Request.QueryString["id"] == "100")
                {
                    lbError.Text = "This is an error page....";
                }
                else
                {
                    lbError.Text = "hmmmmmmmm....";
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime_uninterested.png";
                    urlHasBeenSet = true;
                }
                if (!urlHasBeenSet)
                {
                    errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
                }
            }
            else
            {
                lbError.Text = "hmm? are you lost?<br />let me take you <a href=\"Index.aspx\">home</a>";
                errorImgURL = "http://toshiba/Sources/Images/Other/anime_confuse.png";
            }
        }
    }
}