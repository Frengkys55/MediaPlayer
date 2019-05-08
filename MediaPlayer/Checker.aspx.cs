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
         * Note:
         * I don't know how to use JSON yet and still confuse about SignalR,
         * so this is the only workaround i can think of.
         */

        public static string CheckReturnValue = "null";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string sessionID = string.Empty;
            string requestedPID = string.Empty;

            if (Request.QueryString.Count != 0)
            {
                // Check for mode
                if (Request.QueryString["mode"] != null)
                {
                    if (Request.QueryString["mode"] == "sample")
                    {
                        CheckReturnValue = "<return>sample</return>";
                    }
                    else
                    {
                        Response.Redirect("Error.aspx");
                    }
                }
                else
                {
                    // Check for processID
                    if (Request.QueryString["id"] != null)
                    {
                        requestedPID = Request.QueryString["id"];
                    }
                    else
                    {
                        Server.Transfer("Error.aspx?id=98");
                    }

                    CheckReturnValue = "<value>" + CheckProcessStatus(requestedPID) + "</value>";
                }
            }
            else
            {
                Server.Transfer("Error.aspx");
            }
        }

        private string CheckProcessStatus(string PID)
        {
            #region Preparation
            SystemConfiguration systemConfiguration = HelperClass.SystemConfigurationLoader();
            string database = "MediaPlayerDatabase";
            string table = "ProcessedVideoInfo";

            string receivedStatus = string.Empty;
            #endregion Preparation

            try
            {
                receivedStatus = SQLClassPeralatan.Peralatan.MintaDataDatabase(database, "VideoStatus", table, "ProcessID", PID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString);
                if (receivedStatus == "1")
                {
                    return "processing";
                }
                else if (receivedStatus == "2")
                {
                    //Read the actual end frame
                    
                    return "success|" + SQLClassPeralatan.Peralatan.MintaDataDatabase(database, "VideoActualEndFrame", table, "ProcessID", PID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString);
                }
                else if (receivedStatus == "3")
                {
                    return "failed";
                }
                else if (receivedStatus == "4")
                {
                    return "canceled";
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