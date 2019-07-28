using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MediaPlayer
{
    #region Enums

    #region Function enum
    public enum Result
    {
        Fail,
        Success
    }
    #endregion Function enum

    #region Video enums

    [Obsolete("Use Resolution in SiteSettings.cs instead", true)]
    public enum VideoResolution
    {
        Original,
        Custom = 1,
        SD_240p = 240,
        SD_360p = 360,
        SD_480p = 480,
        HD_720p = 720,
        FHD_1080p = 1080,
        QHD_1440p = 1440
    }

    [Obsolete("Use Resolution in SiteSettings.cs instead", true)]
    public enum VideoFrameRate
    {
        Original = 0,
        Custom = 1,
        FPS_24 = 24,
        FPS_30 = 30,
        FPS_60 = 60,
        FPS_120 = 120
    }

    public enum AudioProcessing
    {
        ProcessAudio,
        NotProcessAudio
    }
    #endregion Video enums

    #endregion Enums
    public struct ErrorInformation
    {
        public string errorMessage;
        public Exception errorException;
    }

    public struct FunctionResult
    {
        public Result functionResult;
        public ErrorInformation functionErrroInformation;
    }
    
    #region Video informations
    #region From "Processor.cs" in WCFAIOProcessor service
    public struct ProcessedVideo
    {
        public string videoSource;
        public string localAccessLocation;
        public string networkAccessLocation;
        public string videoName;
        public string processedVideoName;
        public double videoDuration;
        public double frameRate;
        public int startFrame;
        public int endFrame;
        public int videoWidth;
        public int videoHeight;
        public Result result;
        public int processID;
    }

    public struct VideoConfigurations
    {
        // Settings value
        public int VideoWidth { set; get; }
        public int VideoHeight { set; get; }
        public string videoCodec { set; get; }
        public string audioCodec { set; get; }
        public bool scale { set; get; }
        public float fps { set; get; }
    }

    #endregion From "Processor.cs" in WCFAIOProcessor service

    public struct VideoProcessingSetting
    {
        public Resolution processedVideoResolution;
        public int height; // if processedVideoResolution is set to Custom
        public FrameRate frameRate;
        public float FPS; // if frameRate is set to Custom
        public AudioProcessing audioProcessing;
    }

    public struct VideoOriginalInformation
    {
        public int VideoWidth;
        public int VideoHeight;
        public bool ContainAudio;
    }
    public struct VideoProcessingLocations
    {
        public string VideoLocation;
        public string videoSaveLocation;
        public string videoNetworkSaveLocation;
    }
    public struct VideoProcessingInformation
    {
        public VideoProcessingSetting VideoSetting;
        public VideoProcessingLocations VideoLocations;

    }
    #endregion Video informations

    #region System configurations
    public struct DatabaseConfiguration
    {
        public string DatabaseConectionString;
        public string TableName;
    }

    public struct SystemConfiguration
    {
        #region Processor application location

        #region VideoProcessing.exe
        public string VideoProcessorLocation;
        public string AudioProcessorLocation; // Skip this variable!
        #endregion VideoProcessing.exe

        #region FFmpeg location
        public bool useCustomFFmpeg;
        public string FFmpegLocation;
        public string FFProbeLocation;
        #endregion FFmpeg location
        #endregion Processor application location

        #region Video working location
        public string TemporaryVideoSaveLocation;
        public string ProcessedVideoSaveLocation;
        public string NetworkProcessedVideoSaveLocation;
        #endregion Video working location

        #region File operation
        public bool DeleteTemporaryFileWhenFinished;
        #endregion File operation

        #region Address overriding
        public bool OverrideHostAddress;
        public string OldAddress;
        public string NewAddress;
        #endregion Address overriding

        #region VideoProcessing.exe window style
        public ProcessWindowStyle windowStyle;
        #endregion VideoProcessing.exe window style

        public int NumberOfImageContainer;

        public DatabaseConfiguration DatabaseProcessingConfiguration;
    }

    #endregion System configurations

    #region User informations

    #region User information
    public struct UserInfo
    {
        public int UserID;
        public string SessionID;
    }
    #endregion User information

    #endregion User informations

    #region Log informations
    public struct ProcessingLog
    {
        SystemConfiguration systemConfiguration;
        DatabaseConfiguration databaseConfiguration;

        VideoOriginalInformation originalVideoInformation;
        VideoProcessingInformation videoProcessingInformation;
        VideoProcessingSetting videoProcessingSetting;
    }

    public struct ProcessedVideoLog
    {

    }
    #endregion Log informations
}