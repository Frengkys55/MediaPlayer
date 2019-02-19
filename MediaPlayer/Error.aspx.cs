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
                if (Request.QueryString["id"] == "1")
                {
                    lbError.Text = "Couldn't play. The browser does not support this kind of thins (for now)...";
                }
                else if (Request.QueryString["id"] == "50")
                {
                    lbError.Text = "sumanai... test mode...";
                }
                else if (Request.QueryString["id"] == "96")
                {
                    lbError.Text = "well.... you just found a not yet implemented function...";
                }
                else if (Request.QueryString["id"] == "97")
                {
                    lbError.Text = "hmmm.... where did the id go?";
                }
                else if (Request.QueryString["id"] == "98")
                {
                    lbError.Text = "hmmm.... where did the url go?";
                }
                else if (Request.QueryString["id"] == "99")
                {
                    lbError.Text = "hmmm.... what did you do?";
                }
                else if (Request.QueryString["id"] == "100")
                {
                    lbError.Text = "This is an error page....";
                }
                errorImgURL = "http://toshiba/Sources/Images/Other/anime-poker-face-2.png";
    }
            else
            {
                lbError.Text = "hmm? are you lost?<br />let me take you <a href=\"Index.aspx\">home</a>";
                errorImgURL = "http://toshiba/Sources/Images/Other/anime_confuse.png";
            }
        }
    }
}