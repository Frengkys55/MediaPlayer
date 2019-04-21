using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaPlayer
{
    public class ProcessingClass
    {
        /*
         * This class contains the temporary main function of the video processing class.
         * If this success, WCFAIOProcessor will be terminated and continued using this
         * class instead.
         */

        public FunctionResult VideoProcessor(VideoProcessingInformation videoInformation, VideoProcessingSetting videoSetting)
        {
            FunctionResult result = new FunctionResult();

            string videoProcessingArguments = "GetFrames ";
            videoProcessingArguments += videoInformation.VideoLocation;
            videoProcessingArguments += " " + videoInformation.videoSaveLocation;
            videoProcessingArguments += 

            return result;
        }
    }
}