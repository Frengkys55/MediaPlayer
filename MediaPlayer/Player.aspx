<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="MediaPlayer.Player" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= videoFileName.Replace("%20", " ") %> - MediaPlayer</title>
    <link rel="stylesheet" href=<%= W3CSSLocation %> />
    <style>
        /*@font-face {
            font-family: 'SegoeUI-Light';
            src: local('/Fonts/SegoeUI/SegoeUI-Light.woff') format('woff'), url('/Fonts/SegoeUI/SegoeUI-Light.ttf') format('truetype'), url('/Fonts/SegoeUI/SegoeUI-Light.svg#SegoeUI-Light') format('svg');
            font-weight: normal;
            font-style: normal;
        }

        @font-face {
            font-family: 'segoeuil';
            src: url('segoeuil.eot');
            src: url('segoeuil.woff2') format('woff2'), url('segoeuil.eot?#iefix') format('embedded-opentype');
            font-weight: normal;
            font-style: normal;

        }*/

        body{
            font-family:'Segoe UI Light';
        }
        /*Background colors*/
        .Background-Color-Dark-Grey {
            background-color: #404040;
        }
        .Background-Color-White{
            background-color:white;
        }

        /*Background hover colors*/
        .Hover-Background-Color-Dark-Grey:hover {
            background-color: #404040;
        }

        /*Text colors*/
        .Text-Color-Dark-Grey {
            color: #404040;
        }

        /*Hover text colors*/
        .Hover-Text-Color-White:hover {
            color: white;
        }

        .Hover-Text-Color-Dark-Grey:hover {
            color: #404040;
        }

        .Button-Even-Spacing {
            display: flex;
            -ms-flex-wrap: wrap;
            -webkit-flex-wrap: wrap;
            flex-wrap: wrap;
            align-content: center;
            justify-content: center;
        }

        /*
            Custom W3 CSS theme
        */
        .w3-theme-l5 {
            color: #000 !important;
            background-color: #f4f4f4 !important
        }

        .w3-theme-l4 {
            color: #000 !important;
            background-color: #d9d9d9 !important
        }

        .w3-theme-l3 {
            color: #000 !important;
            background-color: #b3b3b3 !important
        }

        .w3-theme-l2 {
            color: #fff !important;
            background-color: #8c8c8c !important
        }

        .w3-theme-l1 {
            color: #fff !important;
            background-color: #666666 !important
        }

        .w3-theme-d1 {
            color: #fff !important;
            background-color: #393939 !important
        }

        .w3-theme-d2 {
            color: #fff !important;
            background-color: #333333 !important
        }

        .w3-theme-d3 {
            color: #fff !important;
            background-color: #2d2d2d !important
        }

        .w3-theme-d4 {
            color: #fff !important;
            background-color: #262626 !important
        }

        .w3-theme-d5 {
            color: #fff !important;
            background-color: #202020 !important
        }

        .w3-theme-light {
            color: #000 !important;
            background-color: #f4f4f4 !important
        }

        .w3-theme-dark {
            color: #fff !important;
            background-color: #202020 !important
        }

        .w3-theme-action {
            color: #fff !important;
            background-color: #202020 !important
        }

        .w3-theme {
            color: #fff !important;
            background-color: #404040 !important
        }

        .w3-text-theme {
            color: #404040 !important
        }

        .w3-border-theme {
            border-color: #404040 !important
        }

        .w3-hover-theme:hover {
            color: #fff !important;
            background-color: #404040 !important
        }

        .w3-hover-text-theme:hover {
            color: #404040 !important
        }

        .w3-hover-border-theme:hover {
            border-color: #404040 !important
        }


    </style>
    <style>
        .buttonHover:hover > .buttonChild{
            filter: invert(100%);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="w3-bar w3-theme-dark">
            <div class="w3-bar-item"><%= videoFileName %></div>
            <div class="w3-bar-item w3-right w3-hide-small w3-button w3-hover-white buttonHover">
                <asp:ImageButton
                    ID="btnHome"
                    runat="server"
                    ImageUrl="~/Sources/Images/MediaPlayer2Small.png"
                    Height="18px"
                    CssClass="buttonChild"
                    OnClick="btnHome_Click"
                    AlternateText="Home"/>
            </div>
            <div class="w3-bar-item w3-right w3-red">Experimental</div>
            
        </div>

        <div class="w3-panel" id="pnlVideo">
            <div class="w3-container w3-border w3-border-theme w3-center">
                <img
                    id="frame1"
                    alt="Player window"
                    src=<%= LoadingIcon %>
                    style=" display:normal;
                            text-align:center;
                            max-height:480px;" />
                <audio
                    id="audio"
                    src='<%= VideoURL %>/audio.mp3'
                    hidden="hidden"></audio>
                <!--Preloaded frames location-->
                <img
                    id="preloadFrame1"
                    alt="preloadFrame1"
                    src="#"
                    style="display:none" />
                <img
                    id="preloadFrame2"
                    alt="preloadFrame2"
                    src="#"
                    style="display:none" />
                <img
                    id="preloadFrame3"
                    alt="preloadFrame3"
                    src="#"
                    style="display:none" />
                <img
                    id="preloadFrame4"
                    alt="preloadFrame4"
                    src="#"
                    style="display:none" />
                <img
                    id="preloadFrame5"
                    alt="preloadFrame5"
                    src="#"
                    style="display:none" />
                <!--Preloaded frames location-->
            </div>
            <div id="pnlPlayerControls" class="w3-container w3-theme-d5" style="display:none">
                <div class="w3-bar">
                    <div
                        id="btnControlPlay"
                        class=" w3-bar-item
                                w3-button
                                w3-hover-white
                                buttonHover">
                        <asp:Image
                            ID="imgInnerControlPlay"
                            runat="server"
                            ImageUrl="Sources/Images/Controls/PlayW.png"
                            CssClass="buttonChild"
                            Height="20px" />
                        
                    </div>
                    <div
                        id="btnControlReverse"
                        class=" w3-bar-item
                                w3-button
                                w3-hover-white
                                buttonHover">
                        <asp:Image
                            ID="imgInnerControlReverse"
                            runat="server"
                            ImageUrl="~/Sources/Images/Controls/BackwardW.png"
                            AlternateText="Reverse"
                            CssClass="buttonChild"
                            Height="20px" />
                    </div>
                    <div
                        id="btnControlFastForward"
                        class=" w3-bar-item
                                w3-button
                                w3-hover-white
                                buttonHover">
                        <asp:Image
                            ID="imgInnerControlFastForward"
                            runat="server"
                            ImageUrl="~/Sources/Images/Controls/ForwardW.png"
                            AlternateText="Fast Forward"
                            CssClass="buttonChild"
                            Height="20px" />
                    </div>
                    <div
                        id="time"
                        class=" w3-bar-item
                                w3-right">
                        <%= videoDuration %>
                    </div>
                </div>
            </div>
            <div class="w3-white">
                <div
                    id="progressBar"
                    class="w3-theme"
                    style="width: 0%; height: 5px">
                </div>
            </div>
        </div>
        <div id="pnlError" class="Position-Screen-Middle w3-display-container" style="display:none">
            <div class="w3-display-middle">
                <h1>
                    <asp:Label
                        ID="lbError"
                        runat="server"
                        Text="Couldn't play. The browser doesn't support this kind of things (for now)....">
                    </asp:Label>
                </h1>
            </div>
        </div>
        
        <div hidden="hidden">
            <div class="" style="width:640px;z-index:2">
                <img id="frame" alt="frame1" src="http://toshiba/Sources/Images/GIF/loading.gif" style="width:640px; position:absolute" class="w3-border w3-border-theme" />
            </div>
        </div>

        <div class="w3-bottom w3-red">
            <div>
                Currently known bug: Fast forward and slowing down video bug (video loop)
            </div>
        </div>

        <script>

            //#region Player settings

            //#region Frame buffer

            /*
             * 1 = Single buffer
             * 2 = Double buffer (Experimental)
             * 3 = Triple buffer (Not yet supported)
             */

            var frameBufferMode = 1;
            var currentBuffer = 0;
            var nextBuffer = 0;
            //#endregion Frame buffer

            //#region Frame rate
            
            //#region Frame preload

            /*
             * 0 = Enable preload
             * 1 = Disable preload
             */
            var framePreload = true;
            //#endregion Frame preload

            //#region Animation information
            var animationId;
            var checkerID;
            var videoReady = false;
            var isVideoPlaying = false;
            var animationWaitTime = 0;
            //#endregion Animation information

            //#region Preload information
            var currentUsedPreloadFrame = 1;
            //#endregion Preload information

            //#region Player controls
            var isPlaying = false;
            var controlPlayButtonCSS = ""
            var processProgressBar = <%= processProgressBar %>;
            var processTime = <%= processTime %>;
            //#endregion Player controls

            //#endregion Player settings

            //#region Video settings
            var videoURL = '<%= VideoURL %>';
            var audioLocation = '<%= VideoURL %>' + '/audio.mp3';
            var videoFrameRate = <%= videoFrameRate %>;
            var currentWorkingFrame;
            var currentDisplayedFrame = 0;
            var startFrame = <%= startFrame %>;
            var endFrame = <%= videoTotalFrame %>;
            var videoWidth = <%= videoWidth %>;
            var videoHeight = <%= videoHeight %>;
            var audioLevel; // Not yet supported
            var audioStartSecond = <%= audioStartDuration %>;
            var frameIncrementValue = <%= playSpeedIncrement %>;
            var frameHoldValue = 0;
            var currentFrameHoldValue = 0;
            var frameCounter = 0;
            //#endregion Video settings

            // Checker information extractor
            function ActualEndFrameExtractor(information) {
                var extractedEndFrame;
                var temp = information.split("|");
                extractedEndFrame = parseInt(temp[1].substring(0, temp[1].search("<")));
                return extractedEndFrame;
            }

            function VideoProcessingProgressWorker(information) {
                var receivedValue;
                var temp = information.split("|");
                receivedValue = parseInt(temp[1].substring(0, temp[1].search("<")));
                document.getElementById("progressBar").style.width = ((receivedValue / endFrame) * 100) + "%";
            }

            function processChecker() {
                var pageInString;
                

                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4) {
                        if (xhr.status == 200) {
                            // Write page to object
                            pageInString = xhr.responseText;

                            // Process object
                            if (pageInString.search("success") !== -1) {
                                // Stop the checking
                                clearInterval(checkerID);

                                // Get the actual end frame
                                endFrame = ActualEndFrameExtractor(pageInString);

                                // Display controls
                                document.getElementById("pnlPlayerControls").style.display = "block";

                                // Reload audio source
                                var audioSrc = document.getElementById("audio").src;
                                document.getElementById("audio").src = audioSrc;
                                document.getElementById("progressBar").style.width = "0%";
                                if (audioStartSecond > 0) {
                                    document.getElementById("audio").currentTime = audioStartSecond;
                                }

                                // Preload frames
                                InitialPreload();
                            }
                            else if (pageInString.search("error") !== -1) {
                                window.location.replace("Error.aspx?id=301");
                            }
                            else {
                                VideoProcessingProgressWorker(pageInString);
                            }
                        }
                    }
                }
                xhr.open('GET', '<%= CheckerAddress %>');
                xhr.send();
            }

            function InitialPreload() {
                document.getElementById("preloadFrame1").src = videoURL + "/" + startFrame + ".jpg";
                document.getElementById("preloadFrame2").src = videoURL + "/" + ((startFrame + 1) + frameIncrementValue) + ".jpg";
                document.getElementById("preloadFrame3").src = videoURL + "/" + ((startFrame + 2) + frameIncrementValue) + ".jpg";
                document.getElementById("preloadFrame4").src = videoURL + "/" + ((startFrame + 3) + frameIncrementValue) + ".jpg";
                document.getElementById("preloadFrame5").src = videoURL + "/" + ((startFrame + 4) + frameIncrementValue) + ".jpg";

                if (processProgressBar) {
                    document.getElementById("progressBar").style.display = "block";
                }
                else {
                    document.getElementById("progressBar").style.display = "none";
                }

                if (processTime) {
                    document.getElementById("time").style.display = "block";
                }
                else {
                    document.getElementById("time").style.display = "none";
                }

                document.getElementById("frame1").src = document.getElementById("preloadFrame1").src;
            }

            function NextFramePreloadWorker() {
                document.getElementById("preloadFrame" + currentUsedPreloadFrame).src = videoURL + "/" + ((currentDisplayedFrame + 5) + frameIncrementValue) + ".jpg";
            }

            function framePositionCorretor() {
                // Function to correct frame position based on audio position
                currentDisplayedFrame = parseInt(document.getElementById("audio").currentTime * videoFrameRate);
            }

            // Progressbar processing
            function ProgressBarWorker() {
                document.getElementById("progressBar").style.width = ((Math.floor(document.getElementById("audio").currentTime) / Math.floor(document.getElementById("audio").duration)) * 100) + "%";
            }

            // Time processing
            function TimeWorker(duration) {
                duration = Number(duration);

                var h = Math.floor(duration / 3600);
                var m = Math.floor(duration % 3600 / 60);
                var s = Math.floor(duration % 3600 % 60);

                var durationInString = "";
                if (h.toString().length < 2) {
                    durationInString += "0" + h.toString() + ":";
                }
                else {
                    durationInString += h.toString() + ":";
                }
                if (m.toString().length < 2) {
                    durationInString += "0" + m.toString() + ":";
                }
                else {
                    durationInString += m.toString() + ":";
                }
                if (s.toString().length < 2) {
                    durationInString += "0" + s.toString();
                }
                else {
                    durationInString += s.toString();
                }
                return durationInString;
            }

            // Main function
            if (typeof Worker !== 'unidentified') {
                checkerID = setInterval(processChecker, 1000);
            }
            else {
                window.location.replace("Error.aspx?id=1");
                document.getElementById("pnlVideo").style.visibility = "hidden";
                document.getElementById("pnlError").style.visibility = "visible";
                document.body.style = "background-image:url('http://toshiba/Sources/Images/other/anime-poker-face-2.png'); background-attachment:fixed; background-repeat:no-repeat; background-position:left bottom; background-size:30%;";
            }

            function SequencePlayer() {
                document.getElementById("frame1").src = document.getElementById("preloadFrame" + currentUsedPreloadFrame).src;

                if (frameHoldValue === 0 || currentFrameHoldValue === frameHoldValue) {

                    NextFramePreloadWorker();

                    frameCounter++;
                    if (frameCounter === parseInt(videoFrameRate)) {
                        if (frameIncrementValue === 0 && frameHoldValue === 0) {
                            framePositionCorretor();
                        }
                        frameCounter = 0;
                    }

                    currentUsedPreloadFrame++;
                    if (currentUsedPreloadFrame == 6) {
                        currentUsedPreloadFrame = 1;
                    }
                    else if (currentUsedPreloadFrame == 0) {
                        currentUsedPreloadFrame = 5;
                    }
                    currentDisplayedFrame = (currentDisplayedFrame + 1) + frameIncrementValue;
                    if (currentDisplayedFrame === endFrame) {
                        clearInterval(animationId);
                        currentDisplayedFrame = 0;
                        isVideoPlaying = false;
                    }

                    if (frameHoldValue > 0) {
                        currentFrameHoldValue = 0;
                    }
                }
                else {
                    currentFrameHoldValue++;
                }
                //frameCounter++;
                //if (frameCounter === parseInt(videoFrameRate)) {
                //    framePositionCorretor();
                //    frameCounter = 0;
                    
                //}
            }

            // Player functions
            function Play() {
                if (frameHoldValue > 0 || frameIncrementValue > 0) {
                    frameIncrementValue = 0;
                    frameHoldValue = 0;
                }
                if (!isVideoPlaying && (frameHoldValue === 0 || frameIncrementValue === 0)) {
                    isVideoPlaying = true;
                    if (frameIncrementValue === 0) {
                        document.getElementById("audio").play();
                    }
                    animationId = setInterval(SequencePlayer, (1000 / videoFrameRate));
                }
                else {
                    isVideoPlaying = false;
                    clearInterval(animationId);
                    document.getElementById("audio").pause();
                }
                //window.location.replace("Error.aspx?id=96");
                //console.log("Play button is pressed");
            }

            function FastForward() {
                //window.location.replace("Error.aspx?id=96");
                console.log("Fast forward button is pressed");
                if (frameHoldValue > 0) {
                    frameHoldValue--;
                }
                else {
                    frameIncrementValue++;
                }
                if (frameIncrementValue === 0 && isVideoPlaying === true) {
                    document.getElementById("audio").stop();
                    document.getElementById("audio").currentTime = currentDisplayedFrame * videoFrameRate;
                    document.getElementById("audio").play();
                    alert("Playing Audio after clicking fast forward button is still under development");
                }
                else {
                    document.getElementById("audio").pause();
                }
            }

            function SlowDown() {
                //window.location.replace("Error.aspx?id=96");
                console.log("Reverse button is pressed");
                if (frameIncrementValue > 0) {
                    frameIncrementValue--;
                }
                else {
                    frameHoldValue++;
                }
                if (frameIncrementValue === 0 && frameHoldValue === 0 && isVideoPlaying === true) {

                    //document.getElementById("audio").currentTime = currentDisplayedFrame * videoFrameRate; // still buggy
                    document.getElementById("audio").stop();
                    document.getElementById("audio").currentTime = currentDisplayedFrame * videoFrameRate;
                    document.getElementById("audio").play();
                    alert("Playing Audio after clicking reverse button is still under development");
                }
                else {
                    document.getElementById("audio").pause();
                }
            }

            //#region Javascript button events
            var btnPlay = document.getElementById("btnControlPlay").onclick = function () { Play(); };
            var btnFastForward = document.getElementById("btnControlFastForward").onclick = function () { FastForward(); };
            var btnReverse = document.getElementById("btnControlReverse").onclick = function () { SlowDown(); };
            //btnPlay.addEventListener("click", Play);
            //#endregion

            //#region other events
            var timeEvent = document.getElementById("audio").ontimeupdate = function () {
                if (processProgressBar) {
                    ProgressBarWorker();
                    }
                if (processTime) {
                    document.getElementById("time").innerHTML = TimeWorker(this.currentTime);
                }
                //document.getElementById('time').innerHTML = Math.floor(this.currentTime) + ' / ' + Math.floor(this.duration);
            };
            //#endregion other events
        </script>
        <noscript>
            <style>html{display:none;}</style>
            <meta http-equiv="refresh" content='0.0;url=error.aspx?id=901&message=<%= userRequestedURL %>' />
        </noscript>
    </form>
</body>
</html>
