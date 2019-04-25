﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Player.aspx.cs" Inherits="MediaPlayer.Player" %>

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
            <div class="w3-bar-item w3-right w3-hide-small">
                <img src="Sources/Images/MediaPlayer2Small.png" alt="MediaPlayer" height="18px" />
            </div>
            <div class="w3-bar-item w3-right w3-red">Experimental</div>
            
        </div>

        <div class="w3-panel" id="pnlVideo">
            <div class="w3-container w3-border w3-border-theme w3-center">
                <%--<canvas
                    runat="server"
                    id="playerWindow"
                    class=" w3-border
                    w3-border-theme"
                    style="visibility:hidden">
                </canvas>
                 --%>
                <img
                    id="frame1"
                    alt="Player window"
                    src="http://toshiba/Sources/Images/Under_Construction/UnderConstruction.png"
                    height="480px"
                    style="display:normal; text-align:center" />
                <!--For double buffer coniguration (experimental)-->
                <img
                    id="frame2"
                    alt="Player window"
                    src="http://toshiba/Sources/Images/Under_Construction/UnderConstruction.png"
                    style="display:none"
                    class="w3-center" />
                <!--For triple buffer configuration (not yet supported)-->
                <img
                    id="frame3"
                    alt="Player window"
                    src="http://toshiba/Sources/Images/Under_Construction/UnderConstruction.png"
                    style="display:none;" />
            </div>
            <div class="w3-white">
                <div class="w3-theme" style="width: 50%; height: 5px"></div>
            </div>
            <div class="w3-container w3-theme-d5">
                <div class="w3-bar">
                    <button class="w3-bar-item w3-button w3-hover-white buttonHover" onclick="Play"><img class="buttonChild" src="Sources/Images/Controls/PlayW.png" alt="MediaPlayer" height="20px" /></button>
                    <button class="w3-bar-item w3-button w3-hover-white buttonHover" onclick="Play"><img class="buttonChild" src="Sources/Images/Controls/BackwardW.png" alt="MediaPlayer" height="20px" /></button>
                    <button class="w3-bar-item w3-button w3-hover-white buttonHover" onclick="Play"><img class="buttonChild" src="Sources/Images/Controls/ForwardW.png" alt="MediaPlayer" height="20px" /></button>
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
                <audio id="audio" src='<%= VideoURL %>/audio.mp3'></audio>
            </div>
        </div>

        <script>
            function extractor() {
                var startTag = "<return>";
                var endTag = "</return>";
                
                var pageInString;
                var result;

                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4) {
                        if (xhr.status == 200) {

                            // Write page to object
                            pageInString = xhr.responseText;
                            console.log(pageInString);
                            // Process object
                            var x = pageInString.indexOf(startTag);
                            x = pageInString.indexOf(">", x);

                            var y = pageInString.lastIndexOf(endTag);
                            
                            console.log(pageInString.split(x + 1, y));
                        }
                    }
                }
                xhr.open('GET', 'http://192.168.1.245/MediaPlayer/Checker.aspx?mode=sample');
                xhr.send();
            }

            var videoURL = '<%= VideoURL %>';

            if (typeof Worker !== 'unidentified') {
                extractor();

                var EndFrameCheckerWorker = new Worker('Sources/JavaScripts/ActualEndFrameChecker.js');
                EndFrameCheckerWorker.addEventListener('message', function (e) {
                    console.log(e.data);
                });

                EndFrameCheckerWorker.postMessage(videoURL);
            }
            else {
                document.getElementById("pnlVideo").style.visibility = "hidden";
                document.getElementById("pnlError").style.visibility = "visible";
                document.body.style = "background-image:url('http://toshiba/Sources/Images/other/anime-poker-face-2.png'); background-attachment:fixed; background-repeat:no-repeat; background-position:left bottom; background-size:30%;";
            }


            // Button functions
            function Play() {
                alert("Player");
            }
        </script>

        <%--<script>

            var buffer1 = new Array(300);
            var buffer2 = new Array(300);

            

            if (typeof Worker !== 'undefined') {
                var worker = new Worker('Sources/JavaScripts/FrameWorker.js');

                worker.addEventListener('message', function (processedPosiotion) {
                    console.log("Worker said: ", processedPosiotion.data);
                }, false);

                worker.postMessage('Hello world');
                worker.postMessage('Hello world 2');
            }
            else {
                document.getElementById("pnlVideo").style.visibility = "hidden";
                document.getElementById("pnlError").style.visibility = "visible";
                document.body.style = "background-image:url('http://toshiba/Sources/Images/other/anime-poker-face-2.png'); background-attachment:fixed; background-repeat:no-repeat; background-position:left bottom; background-size:30%;";
            }
        </script>--%>

        <%--<script>
            
            var frame1 = document.getElementById("frame1");
            var framePosition = 1;
            var VideoURL = '<%= VideoURL %>';
            var isPlaying = false;
            var isReady = false;
            var totalFrame = <%= videoTotalFrame %>;
            var middleFrame = <%= middleFrame %>;
            var frameSpeed = <%= videoFrameRate %>;
            var loadingFrame = 2000;

            setInterval(function () {
                if (isReady) {
                    frame1.src = VideoURL + "/" + framePosition + ".jpg";
                    if (framePosition != totalFrame) {
                        framePosition++;
                    }
                    if (!isPlaying && framePosition == 3) {
                        document.getElementById("audio").play();
                        isPlaying = true;
                    }
                }
                else {
                    if (doesFileExist(VideoURL + "/" + middleFrame.toString() + ".jpg")) {
                        isReady = true;
                    }
                    else {
                        isReady = false;
                    }
                }
            }, (1000 / frameSpeed));
            function doesFileExist(urlToFile){
                var xhr = new XMLHttpRequest();
                xhr.open('HEAD', urlToFile, false);
                xhr.send();

                if (xhr.status == "404") {
                    console.log("File doesn't exist");
                    return false;
                } else {
                    console.log("File exists");
                    return true;
                }
            }

            window.requestAnimationFrame()

        </script>--%>
        <script>
            <%--var counter = 0;
            var waitTime = 1000 / <%= videoFrameRate %>;
            var frameBufferNumber = 1;
            var imageBufferNumber = 1;
            var img1 = new Image;
            var img2 = new Image;
            var myCanvas;
            var ctx;
            var isBeginning = true;--%>
            <%--setInterval(function () {
                
                //// Swap image buffer
                //if (imageBufferNumber == 1) {
                //    imageBufferNumber = 2;
                //}
                //else {
                //    imageBufferNumber = 1;
                //}

                // Working frame
                if (frameBufferNumber == 0) {
                    myCanvas = document.getElementById('frame2');
                }
                else {
                    myCanvas = document.getElementById('frame1');
                }

                // Set working canvas
                ctx = myCanvas.getContext('2d');

                // Working images
                if (imageBufferNumber == 1) {
                    img2.onload = function () {
                        ctx.drawImage(img2, 0, 0, img2.width, img2.height, 0, 0, myCanvas.width, myCanvas.height);
                    };

                    // Load frame
                    img2.src = '<%= VideoURL %>' + counter + '.jpg';

                }
                else {
                    img1.onload = function () {
                        ctx.drawImage(img1, 0, 0, img1.width, img1.height, 0, 0, myCanvas.width, myCanvas.height);
                    };

                    // Load frame
                    img1.src = '<%= VideoURL %>' + counter + '.jpg';
                }

                counter++;
                if (counter > <%= videoTotalFrame %>) counter = 0;

                // I set the interval to 100 ms, increase or decrease as per your need.
                // If you don't want loop use clearTimeout()

                document.getElementById("text").innerText = imageBufferNumber;
                // Swap image buffer
                if (imageBufferNumber == 1) {
                    imageBufferNumber = 2;
                }
                else {
                    imageBufferNumber = 1;
                }
            }, waitTime);--%>

            //setInterval(SwapFrame(), waitTime);
            <%--var canvas;
            var img;
            var frameRate = 1000 / 30;
            var totalFrames = <%= videoTotalFrame %>;
            var VideoURL = '<%= VideoURL %>';
            var FrameURL;
            var framePosition = 1;
            var ctx;
            var requestAnimationFrame = window.requestAnimationFrame ||
                                        window.webkitRequestAnimationFrame ||
                                        window.msRequestAnimationFrame ||
                                        window.mozRequestAnimationFrame;
            
            function ProcessFrame() {
                documemt.getElementById("text").innerText = "Button pressed";
                canvas = document.getElementById("frame");
                ctx = canvas.getContext('2d');
                img = new Image();

                img.onload = function () {
                    FrameURL = VideoURL + "/" + framePosition + ".jpg";
                    ctx.drawImage(img, 0, 0, img.width, img.height, 0, 0, canvas.width, canvas.height);
                };
                img.src = VideoURL + "/" + framePosition + ".jpg";
                console.log(img.src);

                if (framePosition != totalFrames) {
                    framePosition++;
                }
                
      
                requestAnimationFrame(ProcessFrame);
            }--%>
        </script>

        <script type="text/javascript">
            // Player settings
            
            /*
             * Frame buffer mode
             * 0 = Single buffer
             * 1 = Double buffer (Experimental)
             * 3 = Triple buffer (Not yet supported)
             */
            var frameBufferMode = 0;
            var currentBuffer = 0;
            var nextBuffer = 0;

            /*
             * Frame rate override
             * 0 = No override
             * 1 = 24 fps
             * 2 = 30 fps
             * 3 = 60 fps
             * 4 = 120 fps
             */
            var framerateOverride = 0;

            /*
             * Frame preload
             * 0 = Enable preload
             * 1 = Disable preload
             */
            var framePreload = true;

            /*
             * Player mode
             * 0 = Using setInterval()
             * 1 = Using requestAnimationFrame();
             */
            var playMode = 0;
            var animationId;

            // Video settings
            var videoLocation;
            var audioLocation;
            var videoFrameRate = 0;
            var currentWorkingFrame;
            var startFrame;
            var endFrame;
            var videoWidth;
            var videoHeight;
            var audioLevel;

            // Application functions ]
            //--------------------------------------------
            // Video playing function
            function ProcessVideo() {
                
                if (frameBufferMode === 0) {
                    
                }
                else if (frameBufferMode === 1) {

                }
                else if (frameBufferMode === 2) {

                }

                // Switch buffer

            }

            // Frame switcher (If enabling multiple buffer)
            function SwitchBuffer() {
                if (currentBuffer === 0) { // Single buffer
                    currentBuffer = 1;
                }
                else if (currentBuffer === 1) { // Double buffer
                    currentBuffer = 2;
                }
                else if (currentBuffer === 2) { // Triple buffer (Not yet supported)
                    currentBuffer = 0;
                }
            }


            // Player function
            function Play() {
                animationId = requestAnimationFrame(ProcessVideo);
            }

            function Pause() {
                if (playMode === 1) {
                    cancelAnimationFrame(id);
                }
            }
            function Stop() {
                cancelAnimationFrame(id);
                currentWorkingFrame = 0;
            }
        </script>
    </form>
</body>
</html>
