using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaPlayer
{
    public class Processor
    {
        public ProcessedVideo ProcessVideo(VideoSettings.VideoConfigurations configurations, string VideoLocation, string VideoDestination = "default", string SessionID = "", bool processAudio = true)
        {
            // Check if input is an http url
            bool isHttp = Validator.IsHttp(VideoLocation);
            string ID = string.Empty;

            // Assigning SessionID
            ID = SessionID;

            #region Working directory setup

            #region Temporary directory
            // Setting up working directory
            string temporaryWorkingDirectory = string.Empty;
            if (temporaryVideoDirectory.EndsWith("\\"))
            {
                temporaryWorkingDirectory = temporaryVideoDirectory + ID;
            }
            else
            {
                temporaryWorkingDirectory = temporaryVideoDirectory + "\\" + ID;
            }

            // Check for directory
            if (!Directory.Exists(temporaryWorkingDirectory))
            {
                Directory.CreateDirectory(temporaryWorkingDirectory);
            }
            #endregion Temporary directory

            #region Processed directory
            string processedVideoDirectory = string.Empty;
            if (VideoDestination != "default")
            {
                throw new NotImplementedException("Custom path is not yet supported.");
                if (VideoDestination.EndsWith("\\"))
                {
                    processedVideoDirectory = VideoDestination + ID;
                }
                else
                {
                    processedVideoDirectory = VideoDestination + "\\" + ID;
                }
                //Check for directory
                if (!Directory.Exists(processedVideoDirectory))
                {
                    Directory.CreateDirectory(processedVideoDirectory);
                }
            }
            else
            {
                if (processedVideoSaveLocation.EndsWith("\\"))
                {
                    processedVideoDirectory = processedVideoSaveLocation + ID;
                }
                else
                {
                    processedVideoDirectory = processedVideoSaveLocation + "\\" + ID;
                }

                //Check for directory
                if (!Directory.Exists(processedVideoDirectory))
                {
                    Directory.CreateDirectory(processedVideoDirectory);
                }
            }

            #endregion Processed directory

            #region Network processed directory
            string networkProcessedVideoDirectory = string.Empty;

            if (!Directory.Exists(processedVideoDirectory))
            {
                throw new Exception("Cannot create network folder if local folder isn't available.");
            }
            if (networkProcessedVideoSaveLocation.EndsWith("/"))
            {
                networkProcessedVideoDirectory = networkProcessedVideoSaveLocation + ID + "/";
            }
            else
            {
                networkProcessedVideoDirectory = networkProcessedVideoSaveLocation + "/" + ID + "/";
            }
            #endregion Network processed directory

            #endregion Working directory setup

            // Check video configurations
            #region  Video settings
            int videoHeight = configurations.VideoHeight;
            int videoWidth = configurations.VideoWidth;
            bool scale = configurations.scale;

            #endregion Video settings

            if (isHttp)
            {
                #region Get filename from URL
                string[] URLParts = VideoLocation.Split('?');
                string resultURL = string.Empty;
                string tempFileName = string.Empty;
                string tempFileNameWithoutExtension = string.Empty;
                foreach (var text in URLParts)
                {
                    if (text.Contains("http://"))
                    {
                        if (text.Contains("?"))
                        {
                            resultURL = text.Trim('?');
                        }
                        else
                        {
                            resultURL = text;
                        }
                        break;
                    }
                }
                tempFileName = Path.GetFileName(resultURL).Replace("%20", " ");
                tempFileNameWithoutExtension = Path.GetFileNameWithoutExtension(resultURL).Replace("%20", " ");

                #endregion Get filename from URL

                #region Get file extension
                string fileNameExtension = Path.GetExtension(tempFileName);
                #endregion Get file extension

                #region Filename without extension to Base64 string
                /*
                 * The file name will be converted to base64 string to remove space
                 * and to be able to recover the original file name later
                 */
                byte[] tempFileNameInBytes = Encoding.UTF8.GetBytes(tempFileName);
                byte[] tempFileNameWithoutExtensionInBytes = Encoding.UTF8.GetBytes(tempFileNameWithoutExtension);

                string localFileName = Convert.ToBase64String(tempFileNameInBytes) + fileNameExtension;
                string localFileNameWithoutExtension = Convert.ToBase64String(tempFileNameWithoutExtensionInBytes);
                #endregion Filename without extension to Base64 string

                #region Working directory setup (second phase)
                #region Temporary download location setup
                string temporaryDownloadLocation = string.Empty;
                if (temporaryWorkingDirectory.EndsWith("\\"))
                {
                    temporaryDownloadLocation = temporaryWorkingDirectory + localFileName;
                }
                else
                {
                    temporaryDownloadLocation = temporaryWorkingDirectory + "\\" + localFileName;
                }
                #endregion Temporary download location setup

                #region Local processed directory (second phase)
                if (processedVideoDirectory.EndsWith("\\"))
                {
                    processedVideoDirectory += tempFileNameWithoutExtension;
                }
                else
                {
                    processedVideoDirectory += "\\" + tempFileNameWithoutExtension;
                }

                if (!Directory.Exists(processedVideoDirectory))
                {
                    Directory.CreateDirectory(processedVideoDirectory);
                }
                #endregion Local processed directory (second phase)

                #region Network processed directory (second phase)
                if (networkProcessedVideoDirectory.EndsWith("/"))
                {
                    networkProcessedVideoDirectory += tempFileNameWithoutExtension;
                }
                else
                {
                    networkProcessedVideoDirectory += "/" + tempFileNameWithoutExtension;
                }
                #endregion Network processed directory (second phase)
                #endregion Working directory download location (Second phase)

                #region File download
                using (var client = new WebClient())
                {
                    client.DownloadFile(VideoLocation, temporaryDownloadLocation);
                }
                #endregion File download

                #region VideoProcessing.exe argument configurations
                string arguments = "GetFrames ";

                #region Temporary location (Source file)
                if (VideoLocation.Contains("\""))
                {
                    arguments += temporaryDownloadLocation;
                }
                else
                {
                    arguments += "\"" + temporaryDownloadLocation + "\" ";
                }
                #endregion Temporary location (Source file)

                #region Target directory
                if (processedVideoDirectory.Contains("\""))
                {
                    arguments += processedVideoDirectory;
                }
                else
                {
                    arguments += "\"" + processedVideoDirectory + "\"";
                }
                #endregion Target directory

                #region Scaling
                if (scale)
                {
                    if (videoWidth > 0)
                    {
                        arguments += " width=" + videoWidth;
                    }
                    if (videoHeight > 0)
                    {
                        arguments += " height=" + videoHeight;
                    }
                }
                #endregion Scaling

                #region Request delete original
                if (deleteTemporaryVideoWhenFinished)
                {
                    // += " request_delete=true";
                }
                #endregion Request delete original

                #region FPS overriding
                if (configurations.fps > 0)
                {
                    arguments += " FPS=" + configurations.fps;
                }
                #endregion FPS overriding

                #region Requester
                arguments += " requester=mediaplayer";
                arguments += " sessionid=" + SessionID;
                arguments += " printinfo=true";
                #endregion Requester
                #endregion VideoProcessing.exe argument configurations

                #region Video info extraction
                VideoProcessor.VideoProcessor processor = new VideoProcessor.VideoProcessor();
                #endregion Video info extraction

                #region Generate info
                ProcessedVideo videoInfo = new ProcessedVideo();

                #region Original file location
                videoInfo.videoSource = VideoLocation;
                videoInfo.localAccessLocation = processedVideoDirectory;
                #endregion Original file location

                #region Video name
                videoInfo.videoName = tempFileNameWithoutExtension;
                FFProbe fFProbe = new FFProbe();

                #endregion Video name

                #region Video frame rate
                videoInfo.frameRate = processor.GetFrameRate(temporaryDownloadLocation);
                #endregion Video frame rate

                #region Local video save location
                if (processedVideoDirectory.EndsWith("\\"))
                {
                    videoInfo.localAccessLocation = processedVideoDirectory + tempFileNameWithoutExtension;
                }
                else
                {
                    videoInfo.localAccessLocation = processedVideoDirectory + "\\" + tempFileNameWithoutExtension;
                }
                #endregion Local video save location

                #region Network video save location
                videoInfo.networkAccessLocation = networkProcessedVideoDirectory;
                #endregion Network video save location

                #region Video duration
                videoInfo.videoDuration = processor.GetTotalDuration(temporaryDownloadLocation).TotalSeconds;
                #endregion Video duration

                #region Video info
                videoInfo.startFrame = 1;

                // Get framerate
                float frameRate = 0;
                frameRate = processor.GetFrameRate(temporaryDownloadLocation);

                // Get endframe
                videoInfo.endFrame = processor.GetTotalFrames(temporaryDownloadLocation);
                videoInfo.videoWidth = videoWidth;
                #endregion Video info

                #endregion Generate info

                #region Program execution
                startInfo.Arguments = arguments;
                if (ConfigurationManager.AppSettings["VideoProcessingWindowType"] == "Normal")
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Normal;
                }
                else
                {
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                }
                startInfo.UseShellExecute = true;
                Process.Start(startInfo);
                #endregion Program execution

                //TODO Cleanup file
                #region Cleanup
                /*
                 * For now, the clean up of temporary downloaded file will be
                 * hanlded by external application.
                 * The reason is because there's an external application and
                 * to prevent the external application from getting error.
                 */

                videoInfo.result = Result.Success;
                #endregion Cleanup

                return videoInfo;
            }
            else
            {
                throw new NotImplementedException("Function is not yet implemented");
                if (!File.Exists(VideoLocation))
                {
                    throw new FileNotFoundException("File not found!");
                }

                #region Get file name
                string tempFileName = Path.GetFileName(VideoLocation);
                string tempFileNameWithoutExtension = Path.GetFileNameWithoutExtension(VideoLocation);
                #endregion Get file name

                #region Working directory setup (second phase)

                #region Local processed directory (second phase)
                if (processedVideoDirectory.EndsWith("\\"))
                {
                    processedVideoDirectory += tempFileNameWithoutExtension;
                }
                else
                {
                    processedVideoDirectory += "\\" + tempFileNameWithoutExtension;
                }
                if (!Directory.Exists(processedVideoDirectory))
                {
                    Directory.CreateDirectory(processedVideoDirectory);
                }
                #endregion Local processed directory (second phase)

                #region Network processed directory (second phase)
                if (networkProcessedVideoDirectory.EndsWith("/"))
                {
                    networkProcessedVideoDirectory += tempFileNameWithoutExtension;
                }
                else
                {
                    networkProcessedVideoDirectory += "/" + tempFileNameWithoutExtension;
                }
                #endregion Network processed directory (second phase)
                #endregion Working directory setup (second phase)

                #region VideoProcessing.exe argument configurations
                string arguments = "GetFrames ";
                if (VideoLocation.Contains("\""))
                {
                    arguments += VideoLocation;
                }
                else
                {
                    arguments += "\"" + VideoLocation + "\" ";
                }

                if (processedVideoDirectory.Contains("\""))
                {
                    arguments += processedVideoDirectory;
                }
                else
                {
                    arguments += "\"" + processedVideoDirectory + "\"";
                }

                if ((scale && videoWidth > 0) || (scale && videoHeight > 0))
                {
                    if (videoWidth > 0)
                    {
                        arguments += " width=";
                        arguments += videoWidth;
                    }
                    if (videoHeight > 0)
                    {
                        arguments += " height=" + videoHeight;
                    }
                }

                if (configurations.fps > 0)
                {
                    arguments += " FPS=" + configurations.fps;
                }

                arguments += " debug=true";

                // arguments += " debug=true";
                #endregion VideoProcessing.exe argument configurations

                #region Video info extraction
                VideoProcessor.VideoProcessor processor = new VideoProcessor.VideoProcessor();
                #endregion Video info extraction

                #region Generate info
                ProcessedVideo videoInfo = new ProcessedVideo();

                #region Video name
                videoInfo.videoName = tempFileNameWithoutExtension;
                FFProbe fFProbe = new FFProbe();

                #endregion Video name

                #region Video frame rate
                videoInfo.frameRate = processor.GetFrameRate(VideoLocation);
                #endregion Video frame rate

                #region Local video save location
                if (processedVideoDirectory.EndsWith("\\"))
                {
                    videoInfo.localAccessLocation = processedVideoDirectory + tempFileNameWithoutExtension;
                }
                else
                {
                    videoInfo.localAccessLocation = processedVideoDirectory + "\\" + tempFileNameWithoutExtension;
                }
                #endregion Local video save location

                #region Network video save location
                videoInfo.networkAccessLocation = networkProcessedVideoDirectory;
                #endregion Network video save location

                #region Video duration
                videoInfo.videoDuration = processor.GetTotalDuration(VideoLocation).TotalSeconds;
                #endregion Video duration

                #region Video info
                videoInfo.startFrame = 1;

                // Get framerate
                float frameRate = 0;
                frameRate = processor.GetFrameRate(VideoLocation);

                // Get endframe
                videoInfo.endFrame = processor.GetTotalFrames(VideoLocation);
                videoInfo.videoWidth = videoWidth;
                #endregion Video info

                #endregion Generate info

                #region Program execution
                startInfo.Arguments = arguments;
                startInfo.WindowStyle = ProcessWindowStyle.Normal;

                startInfo.UseShellExecute = true;
                Process.Start(startInfo);
                #endregion Program execution

                //TODO Cleanup file
                #region Cleanup
                /*
                 * For now, the clean up of temporary downloaded file will be
                 * hanlded by external application.
                 * The reason is because there's an external application and
                 * to prevent the external application from getting error.
                 */
                #endregion Cleanup

                return videoInfo;
            }
        }
    }
}