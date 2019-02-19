using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaPlayer
{
    public partial class Help : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void imgLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}