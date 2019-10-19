# MediaPlayer
This application is just a simple web based video playing program similar to YouTube. But what makes it different is that instead of converting video (like mkv to mp4), the application will extract all frames instead and play each frame of the video like flip books.

# How it works
1. Application will try to get the video name from url and convert it to Base64 string (to eliminate space and preserve some characters)
2. The application then creating the nececary working directories (for download and saving)
3. After completing, application will download the file to download directory
4. Application will then create a command on how to process the file and pass the command to a console application (VideoProcessing.exe) and leave
5. While "VideoProcessing.exe" is processing the video, application will redirect the page to Player.aspx and wait for processing to complete
6. When the processing complete, application now ready to play the video

# What type of video format the application can play
This application uses FFmpeg for processing, so it supports all the format supported by FFmpeg

# What it uses
1. NReco (to run FFmpeg much simpler)

# TODO List
1. Running the application completely protable
2. Resource loader for loading image from FTP server

# Known bugs
1. Video and audio out-of-sync when playing from specific time (This is a problem with VideoProcessing.exe)
