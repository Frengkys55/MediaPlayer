using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLClassPeralatan;

namespace MediaPlayer
{
    public static class HelperClass
    {
        public static bool WriteLog(string sessionID,  VideoPlayerSettings userSettings, string connectionString)
        {
            #region Preparation
            // SettingsInformation
            string ID = string.Empty;
            string userSettingsID = string.Empty;


            string SQLCommand = string.Empty;

            #endregion Preparation

            return Peralatan.TambahKeDatabase(SQLCommand, connectionString);
        }
        public static bool CreateNewSettings(string database, string table, string connectionString)
        {
            VideoPlayerSettings playerSettings = new VideoPlayerSettings();
            playerSettings.bufferMode = BufferMode.SingleBuffer;
            playerSettings.frameRate = FrameRate.Default;
            playerSettings.resolution = Resolution.Original;
            playerSettings.preloadFrames = PreloadFrames.DisablePreload;

            string SQLCommand = string.Empty;
            
            return Peralatan.TambahKeDatabase(SQLCommand, connectionString);
        }

        public static void UpdateSettings(VideoPlayerSettings settings, string database, string table, string connectionString)
        {
            string SQLCommand = string.Empty;
            SQLCommand = "USE " + database + ";";
            SQLCommand += "UPDATE " + table + " ";
            
            throw new NotImplementedException("This function is not yet implemented");
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

        public static VideoPlayerSettings ReadPlayerSettings(string sessionID, string database, string table, string connectionString)
        {
            VideoPlayerSettings UserSettings = new VideoPlayerSettings();

            // Read userID
            string userID = Peralatan.MintaDataDatabase(database, "ID", table, "SessionID", sessionID, connectionString);

            #region Video resolution
            if (Peralatan.MintaDataDatabase(database, "VideoResolution", table, "UserID", userID, connectionString) == "1")
            {
                UserSettings.resolution = Resolution.Original;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoResolution", table, "UserID", userID, connectionString) == "2")
            {
                UserSettings.resolution = Resolution.SD_360p;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoResolution", table, "UserID", userID, connectionString) == "3")
            {
                UserSettings.resolution = Resolution.SD_480p;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoResolution", table, "UserID", userID, connectionString) == "4")
            {
                UserSettings.resolution = Resolution.HD_720p;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoResolution", table, "UserID", userID, connectionString) == "5")
            {
                UserSettings.resolution = Resolution.HD_1080p;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoResolution", table, "UserID", userID, connectionString) == "6")
            {
                UserSettings.resolution = Resolution.SUHD_1440p;
            }
            else
            {
                UserSettings.resolution = Resolution.SD_480p;
            }
            #endregion Video resolution

            #region Video framerate
            if (Peralatan.MintaDataDatabase(database, "VideoFrameRate", table, "UserID", userID, connectionString) == "0")
            {
                UserSettings.frameRate = FrameRate.Default;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoFrameRate", table, "UserID", userID, connectionString) == "1")
            {
                UserSettings.frameRate = FrameRate._24fps;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoFrameRate", table, "UserID", userID, connectionString) == "2")
            {
                UserSettings.frameRate = FrameRate._30fps;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoFrameRate", table, "UserID", userID, connectionString) == "3")
            {
                UserSettings.frameRate = FrameRate._60fps;
            }
            else if (Peralatan.MintaDataDatabase(database, "VideoFrameRate", table, "UserID", userID, connectionString) == "4")
            {
                UserSettings.frameRate = FrameRate._120fps;
            }
            else
            {
                UserSettings.frameRate = FrameRate.Default;
            }
            #endregion Video framerate

            #region Video buffer mode
            if (Peralatan.MintaDataDatabase(database, "BufferMode", table, "UserID", userID, connectionString) == "0")
            {
                UserSettings.bufferMode = BufferMode.SingleBuffer;
            }
            else if (Peralatan.MintaDataDatabase(database, "BufferMode", table, "UserID", userID, connectionString) == "1")
            {
                UserSettings.bufferMode = BufferMode.DoubleBuffer;
            }
            else if (Peralatan.MintaDataDatabase(database, "BufferMode", table, "UserID", userID, connectionString) == "2")
            {
                UserSettings.bufferMode = BufferMode.TripleBuffer;
            }
            else
            {
                UserSettings.bufferMode = BufferMode.SingleBuffer;
            }
            #endregion Video buffer mode

            #region Video preload frames
            if (Peralatan.MintaDataDatabase(database, "PreloadFrames", table, "UserID", userID, connectionString) == "0")
            {
                UserSettings.preloadFrames = PreloadFrames.DisablePreload;
            }
            else if (Peralatan.MintaDataDatabase(database, "PreloadFrames", table, "UserID", userID, connectionString) == "1")
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

        public static DatabaseProcessedInfo SetVideoInfo(VideoPlayerSettings settings, string SessionID)
        {
            DatabaseProcessedInfo result = new DatabaseProcessedInfo();

            string SQLCommand = string.Empty;
            string connectionString = string.Empty;

            string UserID = string.Empty;
            string OriginalVideoURL = string.Empty;
            string OriginalTemporaryVideoLocation = string.Empty;
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

            // Info prefetch
            UserID = Peralatan.MintaDataDatabase("MediaPlayerDatabase", "SessionID", "SessionInfo", "SessionID", SessionID, connectionString);
            

            // Video info data
            SQLCommand = "USE MediaPlayerDatabase;";
            SQLCommand += "INSERT INTO ProcessedVideoInfo (";
            // Table column
            SQLCommand += "UserID, ";
            SQLCommand += "OriginalVideoURL, ";
            SQLCommand += "OriginalTemporaryVideoFolder, ";
            SQLCommand += "LocalProcessedVideoFolder, ";
            SQLCommand += "NetworkProcessedVideoFolder, ";
            SQLCommand += "OriginalVideoWidth, ";
            SQLCommand += "OriginalVideoHeight, ";
            SQLCommand += "VideoDuration, ";
            SQLCommand += "VideoFrameRate, ";
            SQLCommand += "VideoCalculatedEndFrame, ";
            SQLCommand += "VideoActualEndFrame, ";
            SQLCommand += "VideoTotalFrames, ";
            SQLCommand += "Scaled, ";
            SQLCommand += "ScaledWidth, ";
            SQLCommand += "ScaledHeight, ";
            SQLCommand += "WithAudio, ";
            SQLCommand += "DateProcessed, ";
            SQLCommand += "DateLastAccess, ";
            SQLCommand += "VideoStatus, ";
            SQLCommand += "LogID";
            // End table column
            // Table value
            SQLCommand += ") VALUE (";
            SQLCommand += "";

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
    }
}  