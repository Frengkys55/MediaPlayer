<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="MediaPlayer.Player" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%= videoFileName.Replace("%20", " ") %> - MediaPlayer</title>
    <link rel="stylesheet" href="Sources/CSS/W3S/w3.css" />
    <link rel="stylesheet" href="Sources/CSS/Custom.css" />
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
                    ImageUrl="Sources/Images/MediaPlayer2Small.png"
                    Height="18px"
                    CssClass="buttonChild"
                    OnClick="btnHome_Click "/>
            </div>
            <div class="w3-bar-item w3-right w3-red">Experimental</div>
            
        </div>

        <div class="w3-panel" id="pnlVideo">
            <div class="w3-container w3-border w3-border-theme w3-center">
                <canvas
                    runat="server"
                    id="playerWindow"
                    class=" w3-border
                    w3-border-theme"
                    style="display:none">
                </canvas>
                <img
                    id="frame1"
                    alt="Player window"
                    src="http://toshiba/Sources/Images/Under_Construction/UnderConstruction.png"
                    height="480px"
                    style="display:normal; text-align:center" />
                <audio id="audio" src='<%= VideoURL %>/audio.mp3' hidden="hidden"></audio>
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
            <div class="w3-white">
                <div class="w3-theme" style="width: 50%; height: 5px"></div>
            </div>
            <div id="pnlPlayerControls" class="w3-container w3-theme-d5" style="display:none">
                <div class="w3-bar">
                    <div id="btnControlPlay" class="w3-bar-item w3-button w3-hover-white buttonHover"><img id="btnInnerControlPlay" class="buttonChild" src="Sources/Images/Controls/PlayW.png" alt="MediaPlayer" style="height:20px" /></div>
                    <div id="btnControlReverse" class="w3-bar-item w3-button w3-hover-white buttonHover"><img class="buttonChild" src="Sources/Images/Controls/BackwardW.png" alt="MediaPlayer" style="height:20px" /></div>
                    <div id="btnControlFastForward" class="w3-bar-item w3-button w3-hover-white buttonHover"><img class="buttonChild" src="Sources/Images/Controls/ForwardW.png" alt="MediaPlayer" style="height:20px" /></div>
                    <div class="w3-bar-item w3-right">0:00:00</div>
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
            var frameCounter = 0;
            //#endregion Video settings

            function processChecker() {
                console.log("checking");
                var startTag = "<value>";
                var endTag = "</value>";

                var pageInString;
                var result;

                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4) {
                        if (xhr.status == 200) {

                            // Write page to object
                            pageInString = xhr.responseText;
                            // Process object
                            var x = pageInString.indexOf(startTag);
                            //x = pageInString.indexOf(">", x);

                            x = pageInString.search("success");
                            var y = pageInString.lastIndexOf(endTag);

                            //console.log(pageInString.split(x + 1, y));

                            if (x !== -1) {
                                clearInterval(checkerID);
                                endFrame = ActualEndFrameExtractor(pageInString);

                                document.getElementById("pnlPlayerControls").style.display = "block";

                                // Reload audio source
                                var audioSrc = document.getElementById("audio").src;
                                document.getElementById("audio").src = audioSrc;
                                if (audioStartSecond > 0) {
                                    document.getElementById("audio").currentTime = audioStartSecond;
                                }
                                InitialPreload();
                            }
                        }
                    }
                }
                xhr.open('GET', '<%= CheckerAddress %>');
                xhr.send();
            }

            // Checker information extractor
            function ActualEndFrameExtractor(information) {
                var extractedEndFrame;
                var temp = information.split("|");
                extractedEndFrame = parseInt(temp[1].substring(0, temp[1].search("<")));
                console.log(extractedEndFrame);
                return extractedEndFrame;
            }

            function InitialPreload() {
                document.getElementById("preloadFrame1").src = videoURL + "/" + startFrame + ".jpg";
                document.getElementById("preloadFrame2").src = videoURL + "/" + (startFrame + 1) + ".jpg";
                document.getElementById("preloadFrame3").src = videoURL + "/" + (startFrame + 2) + ".jpg";
                document.getElementById("preloadFrame4").src = videoURL + "/" + (startFrame + 3) + ".jpg";
                document.getElementById("preloadFrame5").src = videoURL + "/" + (startFrame + 4) + ".jpg";
            }

            function NextFramePreloadWorker() {
                document.getElementById("preloadFrame" + currentUsedPreloadFrame).src = videoURL + "/" + (currentDisplayedFrame + 5 + ".jpg");
            }

            function framePositionCorretor() {
                // Function to correct frame position from audio position
                currentDisplayedFrame = parseInt(document.getElementById("audio").currentTime * videoFrameRate);
                console.log("Corrector executed");
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
                NextFramePreloadWorker();
                currentUsedPreloadFrame++;
                if (currentUsedPreloadFrame == 6) {
                    currentUsedPreloadFrame = 1;
                }
                else if (currentUsedPreloadFrame == 0) {
                    currentUsedPreloadFrame = 5;
                }
                currentDisplayedFrame++;
                if (currentDisplayedFrame === endFrame) {
                    clearInterval(animationId);
                    
                }
                //frameCounter++;
                //if (frameCounter === 30) {
                //    framePositionCorretor();
                //    frameCounter = 0;
                //}


            }

            // Player functions
            function Play() {
                if (!isVideoPlaying) {
                    document.getElementById("audio").play();
                    animationId = setInterval(SequencePlayer, (1000 / videoFrameRate));
                }
                //window.location.replace("Error.aspx?id=96");
                //console.log("Play button is pressed");
            }

            function FastForward() {
                window.location.replace("Error.aspx?id=96");
                console.log("Fast forward button is pressed");
            }

            function Reverse() {
                window.location.replace("Error.aspx?id=96");
                console.log("Reverse button is pressed");
            }

            //#region Javascript button events
            var btnPlay = document.getElementById("btnControlPlay").onclick = function () { Play(); };
            var btnFastForward = document.getElementById("btnControlFastForward").onclick = function () { FastForward(); };
            var btnReverse = document.getElementById("btnControlReverse").onclick = function () { Reverse(); };
            //btnPlay.addEventListener("click", Play);
            //#endregion
        </script>
    </form>
</body>
</html>
