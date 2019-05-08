using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaPlayer
{
    /*
     * This class contains settings about how videos is processed,
     * how videos is saved, how videos is served, and website
     * settings.
     */
    public struct VideoPlayerSettings
    {
        public Resolution resolution;
        public FrameRate frameRate;
        public BufferMode bufferMode;
        public PreloadFrames preloadFrames;
        public PlaySpeed playSpeed;
    }

    public enum BufferMode
    {
        SingleBuffer = 1,
        DoubleBuffer = 2,
        TripleBuffer = 3
    }
    public enum Resolution
    {
        Original = 0,
        SD_360p = 360,
        SD_480p = 480,
        HD_720p = 720,
        HD_1080p = 1080,
        SUHD_1440p = 1440,
        Other = 7
    }
    public enum FrameRate
    {
        Default = -1,
        Other = 0,
        _24fps = 24,
        _30fps = 30,
        _60fps = 60,
        _120fps = 120
    }
    public enum PreloadFrames
    {
        DisablePreload = 0,
        EnablePreload = 1
    }
    public enum PlaySpeed
    {
        Normal = 0,
        _2x = 2,
        _4x = 4,
        _8x = 8,
        _16x = 16,
        _32x = 32,
    }

    public enum AccessMode
    {
        External,
        Web,
        Other
    }

    public struct DatabaseProcessedInfo
    {
        public ProcessedStatus videoStatus;
        public string SendString;
        public bool HasMessage;
        public string Message;
    }

    public enum ProcessedStatus
    {
        Processing,
        Processed,
        Failed,
        Canceled
    }

    public struct VideoInfo
    {
        int VideoWidth;
        int VideoHeight;
        DateTime VideoDuration;
        bool withAudio;
    }


    public enum PlayMode
    {
        Url,
        FileUpload
    }
    public enum OptionalSettingsStatus
    {
        Opened,
        Closed
    }

    public enum StringConversionMode
    {
        Encode = 1,
        Decode = 2
    }
}