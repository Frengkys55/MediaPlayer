﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using SQLClassPeralatan;

namespace MediaPlayer
{
    public static class HelperClass
    {
        #region Checking

        #region User checking
        public static bool CheckUser(string database, string userTable, string sessionID, string connectionString)
        {
            return Peralatan.PeriksaDataDatabase(sessionID, "SessionID", database, userTable, connectionString);
        }
        #endregion User checking

        #region Settings checking
        public static bool CheckSettings(string database, UserInfo userInfo, string settingsTable, string connectionString)
        {
            string userID = userInfo.UserID.ToString();
            return Peralatan.PeriksaDataDatabase(userID, "UserID", database, settingsTable, connectionString);
        }
        #endregion Settings checking

        #region Website checking
        public static bool CheckWebsite(string URL)
        {
            #region Preparation
            List<string> blockList = new List<string>();
            #endregion Preparation

            #region Blocked website
            blockList.Add("youtube");
            blockList.Add("youtu.be");
            #endregion Blocked website

            foreach (var item in blockList)
            {
                if (URL.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion Website checking

        #endregion Checking

        #region Settings processing
        public static bool AddUser(string database, string userTable, string sessionID, string connectionString)
        {
            string SQLCommand = string.Empty;
            SQLCommand += "USE " + database + ";";
            SQLCommand += "INSERT INTO " + userTable + " (SessionID) VALUES ('" + sessionID + "');";
            bool result = Peralatan.TambahKeDatabase(SQLCommand, connectionString);
            if (!result)
            {
                throw new Exception(Peralatan.PesanKesalahan);
            }
            return result;
        }
        
        public static bool CreateNewSettings(string database, string table, UserInfo userInfo, string connectionString)
        {
            VideoPlayerSettings playerSettings = new VideoPlayerSettings();
            playerSettings.bufferMode = BufferMode.SingleBuffer;
            playerSettings.frameRate = FrameRate._24fps;
            playerSettings.resolution = Resolution.SD_480p;
            playerSettings.preloadFrames = PreloadFrames.EnablePreload;

            string userID = userInfo.UserID.ToString();

            string SQLCommand = string.Empty;
            SQLCommand += "USE " + database + ";";
            SQLCommand += "INSERT INTO " + table + " (UserID, VideoWidth, VideoHeight, FrameRate, BufferMode, PreloadFrames) VALUES (";
            SQLCommand += userID + ", 0, " + (int)playerSettings.resolution + ", " + (float)playerSettings.frameRate + ", " + (int)playerSettings.bufferMode + ", " + (int)playerSettings.preloadFrames + ");";
            
            return Peralatan.TambahKeDatabase(SQLCommand, connectionString);
        }
        
        public static FunctionResult UpdateSettings(VideoPlayerSettings settings, UserInfo userInfo, string database, string table, string connectionString)
        {
            #region Preparation
            SystemConfiguration systemConfiguration = HelperClass.SystemConfigurationLoader();
            FunctionResult result = new FunctionResult();
            #endregion Preparation

            #region SQL statement
            string SQLCommand = string.Empty;
            SQLCommand = "USE " + database + ";";
            SQLCommand += "UPDATE " + table + " SET ";
            SQLCommand += "VideoHeight=" + (int)settings.resolution + ", ";
            SQLCommand += "FrameRate=" + (int)settings.frameRate + ", ";
            SQLCommand += "BufferMode=" + (int)settings.bufferMode + ", ";
            SQLCommand += "PreloadFrames=" + (int)settings.preloadFrames + " WHERE ";
            SQLCommand += "UserID=" + userInfo.UserID + ";";
            #endregion SQL statement

            if (!Peralatan.UbahDataDatabase(SQLCommand, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString))
            {
                result.functionResult = Result.Fail;
                result.functionErrroInformation.errorMessage = Peralatan.PesanKesalahan;
            }
            else
            {
                result.functionResult = Result.Success;
            }
            return result;
        }

        /// <summary>
        /// Update specific column of a table
        /// </summary>
        /// <param name="database">Database of the item</param>
        /// <param name="table">Table of the item</param>
        /// <param name="target">Column (Key) and value (value) of target</param>
        /// <param name="reference">(Column (Key) and value (value) used as reference</param>
        /// <param name="connectionString">Connection string to connect to database</param>
        /// <returns>Bool</returns>
        public static bool UpdateSettings(string database, string table, string targetColumn, string targetValue, string referenceColumn, string referenceValue, string connectionString)
        {
            string SQLCommand = string.Empty;
            SQLCommand = "USE " + database + ";";
            SQLCommand += "UPDATE " + table + " SET " + targetColumn + "=" + targetValue + " WHERE " + referenceColumn + "=" + referenceValue + ";";

            return Peralatan.UbahDataDatabase(SQLCommand, connectionString);
        }
        
        /// <summary>
        /// Update specific column of a table with multiple reference
        /// </summary>
        /// <param name="database">Database of the item</param>
        /// <param name="table">Table of the item</param>
        /// <param name="target">Column (Key) and value (value) of target</param>
        /// <param name="reference">(List of Column (Key) and value (value) used as reference</param>
        /// <param name="connectionString">Connection string to connect to database</param>
        /// <returns>Bool</returns>
        public static bool UpdateSettings(string database, string table, string targetColumn, string targetValue, List<string> referenceColumn, List<string> referenceValue, string connectionString)
        {
            string SQLCommand = string.Empty;
            SQLCommand = "USE " + database + ";";
            SQLCommand += "UPDATE " + table + " ";
            SQLCommand += "SET " + targetColumn + "=" + targetValue + " ";
            SQLCommand += "WHERE " + referenceColumn + "=" + referenceValue + ";";
            
            return Peralatan.UbahDataDatabase(SQLCommand, connectionString);
        }

        public static UserInfo ReadUserInfo(string SessionID)
        {
            UserInfo receivedUserInfo = new UserInfo();
            receivedUserInfo.SessionID = SessionID;
            SystemConfiguration systemConfiguration = SystemConfigurationLoader();
            string userTableName = "SessionInfo";
            MintaDataDatabase mintaDataDatabase = new MintaDataDatabase("UserID", userTableName, "SessionID", receivedUserInfo.SessionID, systemConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString);
            if (!mintaDataDatabase.TerdapatKesalahan)
            {
                receivedUserInfo.UserID = Convert.ToInt32(mintaDataDatabase.DataDiterima);
            }
            else
            {
                throw new Exception("User information reading failed. User information not found?");
            }
            return receivedUserInfo;
        }

        public static VideoPlayerSettings ReadPlayerSettings(string sessionID, string database, string table, string connectionString)
        {
            VideoPlayerSettings UserSettings = new VideoPlayerSettings();

            string userID = Peralatan.MintaDataDatabase(database, "UserID", "SessionInfo", "SessionID", sessionID, connectionString);

            #region Video 
            string receivedHeight = Peralatan.MintaDataDatabase(database, "VideoHeight", table, "UserID", userID, connectionString);
            if (receivedHeight == ((int)Resolution.Original).ToString())
            {
                UserSettings.resolution = Resolution.Original;
            }
            else if (receivedHeight == ((int)Resolution.SD_360p).ToString())
            {
                UserSettings.resolution = Resolution.SD_360p;
            }
            else if (receivedHeight == ((int)Resolution.SD_480p).ToString())
            {
                UserSettings.resolution = Resolution.SD_480p;
            }
            else if (receivedHeight == ((int)Resolution.HD_720p).ToString())
            {
                UserSettings.resolution = Resolution.HD_720p;
            }
            else if (receivedHeight == ((int)Resolution.HD_1080p).ToString())
            {
                UserSettings.resolution = Resolution.HD_1080p;
            }
            else if (receivedHeight == ((int)Resolution.SUHD_1440p).ToString())
            {
                UserSettings.resolution = Resolution.SUHD_1440p;
            }
            else
            {
                UserSettings.resolution = Resolution.SD_480p;
            }
            #endregion Video resolution

            #region Video framerate
            string receivedFrameRate = Peralatan.MintaDataDatabase(database, "FrameRate", table, "UserID", userID, connectionString);
            if (receivedFrameRate == ((int)FrameRate.Default).ToString())
            {
                UserSettings.frameRate = FrameRate.Default;
            }
            else if (receivedFrameRate == ((int)FrameRate._24fps).ToString())
            {
                UserSettings.frameRate = FrameRate._24fps;
            }
            else if (receivedFrameRate == ((int)FrameRate._30fps).ToString())
            {
                UserSettings.frameRate = FrameRate._30fps;
            }
            else if (receivedFrameRate == ((int)FrameRate._60fps).ToString())
            {
                UserSettings.frameRate = FrameRate._60fps;
            }
            else if (receivedFrameRate == ((int)FrameRate._120fps).ToString())
            {
                UserSettings.frameRate = FrameRate._120fps;
            }
            else
            {
                UserSettings.frameRate = FrameRate.Default;
            }
            #endregion Video framerate

            #region Video buffer mode
            string receivedBufferMode = Peralatan.MintaDataDatabase(database, "BufferMode", table, "UserID", userID, connectionString);
            if (receivedBufferMode == ((int)BufferMode.SingleBuffer).ToString())
            {
                UserSettings.bufferMode = BufferMode.SingleBuffer;
            }
            else if (receivedBufferMode == ((int)BufferMode.DoubleBuffer).ToString())
            {
                UserSettings.bufferMode = BufferMode.DoubleBuffer;
            }
            else if (receivedBufferMode == ((int)BufferMode.TripleBuffer).ToString())
            {
                UserSettings.bufferMode = BufferMode.TripleBuffer;
            }
            else
            {
                UserSettings.bufferMode = BufferMode.SingleBuffer;
            }
            #endregion Video buffer mode

            #region Video preload frames
            string receivedPreload = Peralatan.MintaDataDatabase(database, "PreloadFrames", table, "UserID", userID, connectionString);
            if (receivedPreload == ((int)PreloadFrames.DisablePreload).ToString())
            {
                UserSettings.preloadFrames = PreloadFrames.DisablePreload;
            }
            else if (receivedPreload == ((int)PreloadFrames.EnablePreload).ToString())
            {
                UserSettings.preloadFrames = PreloadFrames.EnablePreload;
            }
            else
            {
                UserSettings.preloadFrames = PreloadFrames.DisablePreload;
            }
            #endregion Video preload frames
            return UserSettings;
        }
        #endregion Settings processing

        public static DatabaseProcessedInfo SetVideoInfo(VideoPlayerSettings settings, UserInfo userInfo, ProcessedVideo processedVideo)
        {
            
            #region Preparation
            DatabaseProcessedInfo result = new DatabaseProcessedInfo();

            string SQLCommand = string.Empty;
            string connectionString = string.Empty;

            string UserID = string.Empty;
            string OriginalVideoURL = string.Empty;
            string OriginalTemporaryVideoLocation = string.Empty;
            string LocalProcessedVideoLocation = string.Empty;
            string NetworkProcessedVideoLocation = string.Empty;
            string OriginalVideoWidth = string.Empty;
            string OriginalVideoHeight = string.Empty;
            string VideoDuration = string.Empty;
            string VideoFrameRate = string.Empty;
            string VideoCalculatedEndFrame = string.Empty;
            string VideoActualEndFrame = string.Empty;
            string VideoTotalFrames = string.Empty;
            string Scaled = string.Empty;
            string ScaledWidth = string.Empty;
            string ScaledHeight = string.Empty;
            string WithAudio = string.Empty;
            string DateProcessed = string.Empty;
            string DateLastAccess = string.Empty;
            string VideoStatus = string.Empty;
            string LogID = string.Empty;
            #endregion Preparation

            // Info prefetch
            UserID = userInfo.UserID.ToString();

            #region Data modification
            byte[] originalURLinBytes = Encoding.UTF8.GetBytes(processedVideo.videoSource);
            OriginalVideoURL = Convert.ToBase64String(originalURLinBytes);

            byte[] localFileinBytes = Encoding.UTF8.GetBytes(processedVideo.localAccessLocation);
            LocalProcessedVideoLocation = Convert.ToBase64String(localFileinBytes);

            byte[] networkFileinBytes = Encoding.UTF8.GetBytes(processedVideo.networkAccessLocation);
            NetworkProcessedVideoLocation = Convert.ToBase64String(networkFileinBytes);
            #endregion Data modification

            // Video info data
            SQLCommand = "USE MediaPlayerDatabase;";
            SQLCommand += "INSERT INTO ProcessedVideoInfo (";
            // Table column
            SQLCommand += "UserID, ";
            SQLCommand += "OriginalVideoURL, ";
            //SQLCommand += "OriginalTemporaryVideoFolder, ";
            SQLCommand += "LocalProcessedVideoFolder, ";
            SQLCommand += "NetworkProcessedVideoFolder, ";
            SQLCommand += "OriginalVideoWidth, ";
            SQLCommand += "OriginalVideoHeight, ";
            SQLCommand += "VideoDuration, ";
            SQLCommand += "VideoFrameRate, ";
            SQLCommand += "VideoCalculatedEndFrame, ";
            SQLCommand += "VideoActualEndFrame, ";
            //SQLCommand += "VideoTotalFrames, ";
            SQLCommand += "Scaled, ";
            SQLCommand += "ScaledWidth, ";
            SQLCommand += "ScaledHeight, ";
            SQLCommand += "WithAudio, ";
            //SQLCommand += "DateProcessed, ";
            //SQLCommand += "DateLastAccess, ";
            SQLCommand += "VideoStatus";
            //SQLCommand += "LogID";
            // End table column
            // Table value
            SQLCommand += ") VALUES (";
            SQLCommand += UserID + ", ";
            SQLCommand += "'" + OriginalVideoURL + "', ";
            SQLCommand += "'" + LocalProcessedVideoLocation + "', ";
            SQLCommand += "'" + NetworkProcessedVideoLocation + "', ";
            SQLCommand += processedVideo.videoWidth + ", ";
            SQLCommand += processedVideo.videoHeight + ", ";
            SQLCommand += "'" + processedVideo.videoDuration + "', ";
            SQLCommand += "'" + processedVideo.frameRate + "', ";
            SQLCommand += processedVideo.endFrame + ", ";
            //SQLCommand += "0, ";
            SQLCommand += "0, ";
            if (settings.resolution == Resolution.Original)
            {
                SQLCommand += "FALSE, ";
            }
            else
            {
                SQLCommand += "1, ";
                SQLCommand += "0, ";
                SQLCommand += ((int)settings.resolution).ToString() + ", ";
            }
            SQLCommand += "1, ";
            SQLCommand += "1);";

            if (Peralatan.TambahKeDatabase(SQLCommand, connectionString))
            {
                result.videoStatus = ProcessedStatus.Processed;
                result.HasMessage = false;
            }
            else
            {
                result.HasMessage = true;
                result.Message = Peralatan.PesanKesalahan;
            }
            
            return result;
        }

        public static bool WriteLog(string sessionID, VideoPlayerSettings userSettings, string connectionString)
        {
            #region Preparation
            // SettingsInformation
            string ID = string.Empty;
            string userSettingsID = string.Empty;


            string SQLCommand = string.Empty;

            #endregion Preparation

            return Peralatan.TambahKeDatabase(SQLCommand, connectionString);
        }

        /// <summary>
        /// Function to load system configurations from Web.config file
        /// </summary>
        /// <returns>Loaded system configurations</returns>
        public static SystemConfiguration SystemConfigurationLoader()
        {
            SystemConfiguration loadedConfiguration = new SystemConfiguration();

            #region Application locations
            #region VideoProcessing.exe location
            loadedConfiguration.VideoProcessorLocation = ConfigurationManager.AppSettings["VideoProcessorLocation"];
            loadedConfiguration.AudioProcessorLocation = ConfigurationManager.AppSettings["AudioProcessorLocation"];
            #endregion VideoProcessing.exe location

            #region FFmpeg location
            loadedConfiguration.FFmpegLocation = ConfigurationManager.AppSettings["FFmpegLocation"];
            loadedConfiguration.FFProbeLocation = ConfigurationManager.AppSettings["FFProbeLocation"];
            #endregion FFmpeg location
            #endregion Application locations

            #region Video working location
            loadedConfiguration.TemporaryVideoSaveLocation = ConfigurationManager.AppSettings["TemporaryVideoDirectory"];
            loadedConfiguration.ProcessedVideoSaveLocation = ConfigurationManager.AppSettings["ProcessedVideoSaveLocation"];
            loadedConfiguration.NetworkProcessedVideoSaveLocation = ConfigurationManager.AppSettings["NetworkProcessedVideoSaveLocation"];
            #endregion Video working location

            #region File operation
            if (ConfigurationManager.AppSettings["DeleteTemporaryVideoWhenFinished"] == "true")
            {
                loadedConfiguration.DeleteTemporaryFileWhenFinished = true;
            }
            else
            {
                loadedConfiguration.DeleteTemporaryFileWhenFinished = false;
            }
            #endregion File operation

            #region Address overriding
            if (ConfigurationManager.AppSettings["overrideHostADdress"] == "true")
            {
                loadedConfiguration.OverrideHostAddress = true;
            }
            else
            {
                loadedConfiguration.OverrideHostAddress = false;
            }
            loadedConfiguration.OldAddress = ConfigurationManager.AppSettings["oldHostAddress"];
            loadedConfiguration.NewAddress = ConfigurationManager.AppSettings["newHostAddress"];
            #endregion Address overriding

            #region VideoProcessing.exe window style
            if (ConfigurationManager.AppSettings["VideoProcessingWindowType"].ToLower() == "normal")
            {
                loadedConfiguration.windowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            }
            else if (ConfigurationManager.AppSettings["VideoProcessingWindowType"].ToLower() == "maximized")
            {
                loadedConfiguration.windowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            }
            else if (ConfigurationManager.AppSettings["VideoProcessingWindowType"].ToLower() == "minimized")
            {
                loadedConfiguration.windowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
            }
            else
            {
                loadedConfiguration.windowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            }
            #endregion VideoProcessing.exe windows style

            #region Database information
            loadedConfiguration.DatabaseProcessingConfiguration.DatabaseConectionString = ConfigurationManager.AppSettings["DatabaseConnectionString"];
            #endregion Database information

            #region Player configurations
            loadedConfiguration.NumberOfImageContainer = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfImageContainers"]);
            #endregion Player configurations

            return loadedConfiguration;
        }

        public static string StringEncoderDecoder(string input, StringConversionMode mode = StringConversionMode.Encode)
        {
            string resultString = string.Empty;

            if (mode == StringConversionMode.Encode)
            {
                resultString = Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
            }
            else if (mode == StringConversionMode.Decode)
            {
                resultString = Encoding.UTF8.GetString(Convert.FromBase64String(input));
            }
            return resultString;
        }

        #region Page settings loader
        // Just a simple location loader for easier javascript and css loading
        public static string CustomCSSLoader()
        {
            if (ConfigurationManager.AppSettings["UseExternalResources"] == "true")
            {
                return ConfigurationManager.AppSettings["CustomCSSLocation"];
            }
            else
            {
                return "/Sources/CSS/Custom.css";
            }
        }
        public static string W3CSSLoader()
        {
            if (ConfigurationManager.AppSettings["UseExternalResources"] == "true")
            {
                return ConfigurationManager.AppSettings["W3CSSLocation"];
            }
            else
            {
                return "/Sources/CSS/W3S/W3.css";
            }
        }
        #endregion Page settings loader

        public static string HostChanger(string input)
        {
            string oldHost = ConfigurationManager.AppSettings["oldHostAddress"];
            string newHost = ConfigurationManager.AppSettings["newHostAddress"];

            return input.Replace(oldHost, newHost);
        }
    }
}  