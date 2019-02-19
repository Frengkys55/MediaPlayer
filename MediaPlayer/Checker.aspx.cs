using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MediaPlayer
{
    public partial class Checker : System.Web.UI.Page
    {

        /*
         * How it works
         * 
         * 1. Check for the required informations (if not available, redirect to error.aspx)
         * 2. Check video status using video URL as input
         * 3. If video status is processed, check for actual end frame
         * 4. If video status is processing, return string inprocess
         * 
         */

        public static string CheckReturnValue = "null";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionID = string.Empty;
            string requestedURL = string.Empty;

            if (Request.QueryString.Count != 0)
            {
                // Check for mode
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"] == "sample")
                    {
                        CheckReturnValue = "<return>sample</return>";
                    }
                }
                else
                {
                    // Check for id
                    if (Request.QueryString["id"] != null)
                    {
                        sessionID = Request.QueryString["id"];
                    }
                    else
                    {
                        Server.Transfer("Error.aspx?id=97");
                    }

                    // Check for url
                    if (Request.QueryString["url"] != null)
                    {
                        requestedURL = Request.QueryString["url"];
                    }
                    else
                    {
                        Server.Transfer("Error.aspx?id=98");
                    }
                    CheckReturnValue = "<value>" + CheckProcessStatus(sessionID, requestedURL) + "</value>";
                }
            }
            else
            {
                Server.Transfer("Error.aspx?id=99");
            }
        }

        private string CheckProcessStatus(string sessionID, string videoURL)
        {
            string database = string.Empty;
            string table = string.Empty;
            string connectionString = string.Empty;

            string receivedStatus = string.Empty;

            try
            {
                receivedStatus = SQLClassPeralatan.Peralatan.MintaDataDatabase(database, "VideoStatus", table, "OriginalVideoURL", videoURL, connectionString);
                if (receivedStatus == "0")
                {
                    return "error";
                }
                else if (receivedStatus == "1")
                {
                    return "inprocess";
                }
                else if (receivedStatus == "2")
                {
                    return SQLClassPeralatan.Peralatan.MintaDataDatabase(database, "VideoActualEndFrame", table, "OriginalVideoURL", videoURL, connectionString);
                }
                else if (receivedStatus == "3")
                {
                    return "error";
                }

            }
            catch (Exception err)
            {
                return "error";
            }

            return receivedStatus;
        }
    }
}