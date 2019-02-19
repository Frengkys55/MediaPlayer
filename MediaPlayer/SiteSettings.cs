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
    }

    public enum BufferMode
    {
        SingleBuffer = 1,
        DoubleBuffer = 2,
        TripleBuffer = 3
    }
    public enum Resolution
    {
        Original = 1,
        SD_360p,
        SD_480p,
        HD_720p,
        HD_1080p,
        SUHD_1440p,
        Other
    }
    public enum FrameRate
    {
        Default,
        _24fps,
        _30fps,
        _60fps,
        _120fps
    }
    public enum PreloadFrames
    {
        EnablePreload = 1,
        DisablePreload
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
}