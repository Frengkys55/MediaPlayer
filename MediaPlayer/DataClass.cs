using System;
using System.Collections.Generic;
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

    public struct ProcessedVideo
    {
        public string videoSource;
        public string localAccessLocation;
        public string networkAccessLocation;
        public string videoName;
        public double videoDuration;
        public double frameRate;
        public int startFrame;
        public int endFrame;
        public int videoWidth;
        public int videoHeight;
        public Result result;
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
    
    public struct VideoProcessingSetting
    {
        public VideoResolution processedVideoResolution;
        public VideoFrameRate frameRate;
        public AudioProcessing audioProcessing;
    }

    public struct VideoOriginalInformation
    {
        public int VideoWidth;
        public int VideoHeight;
        public bool ContainAudio;
    }

    public struct VideoProcessingInformation
    {
        public string VideoLocation;
        public string videoSaveLocation;
        public string videoNetworkSaveLocation;
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
        public string TemporaryVideoSaveLocation;
        public string ProcessedVideoSaveLocation;
        public string NetworkProcessedVideoSaveLocation;
        public bool OverrideHostAddress;
        public string VideoProcessingApplicationLocation;
    }

    #endregion System configurations

    #region Log informations
    public struct ProcessingLog
    {
        SystemConfiguration systemConfiguration;
        DatabaseConfiguration databaseConfiguration;

        VideoOriginalInformation originalVideoInformation;
        VideoProcessingInformation videoProcessingInformation;
        VideoProcessingSetting videoProcessingSetting;
    }
    #endregion Log informations
}