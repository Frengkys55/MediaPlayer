<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MediaPlayer.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Sources/CSS/W3S/w3.css" />
    <link rel="stylesheet" href="Sources/CSS/Custom.css" />

    <style>
        .Position-Screen-Middle {
            position: absolute;
            margin: auto;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            width: 100%;
            height:<%= panelHeight %>;
        }
    </style>
    
    <title>Media Player</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="w3-panel">
            <asp:Image
                ID="imgLogo"
                runat="server"
                ImageUrl="~/Sources/Images/MediaPlayer2Small.png"
                Width="200px"
                CssClass="w3-hide-small" />
        </div>
        <div>
            <div class="Position-Screen-Middle w3-card-4 w3-white">
                <div class="">
                    <asp:Button
                        ID="btnUploadURL"
                        runat="server"
                        CssClass="  w3-button
                                    w3-hover-theme
                                    w3-theme"
                        Text="Use URL"
                        OnClick="btnUploadURL_Click" />
                    <asp:Button
                        ID="btnLoadUpload"
                        runat="server"
                        CssClass="  w3-button
                                    w3-white
                                    w3-hover-theme"
                        Text="Use file"
                        OnClick="btnLoadUpload_Click" />
                    <asp:Label
                        ID="lblHelp"
                        runat="server"
                        Text="Write &ldquo;help&rdquo; and press &ldquo;Play video&rdquo; if you are new to this site"
                        CssClass="w3-right w3-margin-right w3-hide-small">
                    </asp:Label>
                </div>
                <asp:Panel
                    ID="pnlURLPlay"
                    runat="server"
                    Visible="true">
                    <div class="w3-bar w3-panel">
                        <asp:TextBox
                            ID="txtURLSource"
                            runat="server"
                            placeholder="Type video URL here (default play in 480p)"
                            CssClass="  w3-input
                                        w3-border
                                        w3-border-theme">
                        </asp:TextBox>
                        <br />
                        <div class="w3-bar Button-Even-Spacing">
                            <div class="w3-padding">
                                <asp:Button
                                    ID="btnLoadURLVideo"
                                    runat="server"
                                    Text="Play Video"
                                    CssClass="  w3-button
                                            w3-white
                                            w3-border
                                            w3-border-theme
                                            w3-hover-theme
                                            w3-bar-item"
                                    Width="120px"
                                    OnClick="btnLoadURLVideo_Click" />
                            </div>
                            <div class="w3-padding w3-hide-small">
                                <asp:Button
                                    ID="btnURLReset"
                                    runat="server"
                                    Text="Reset"
                                    CssClass="  w3-button
                                                w3-white
                                                w3-hover-theme
                                                w3-border
                                                w3-border-theme
                                                w3-bar-item"
                                    Width="120px"
                                    OnClick="btnURLReset_Click" />
                            </div>
                            <div class="w3-padding w3-hide-small">
                                <asp:Button
                                    ID="btnSettings"
                                    runat="server"
                                    Text="v"
                                    CssClass="  w3-button
                                                w3-white
                                                w3-hover-theme
                                                w3-border
                                                w3-border-theme
                                                w3-bar-item"
                                    Width="120px"
                                    OnClick="btnSettings_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlUploadPlay"
                    runat="server"
                    Visible="false">
                    <div class="w3-bar w3-panel">
                        <asp:FileUpload
                            ID="uplVideo"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme" />
                        <br />
                        <div class="w3-bar Button-Even-Spacing">
                            <div class="w3-padding">
                                <asp:Button
                                    ID="btnUploadVideo"
                                    runat="server"
                                    CssClass="  w3-button
                                                w3-white
                                                w3-hover-theme
                                                w3-border
                                                w3-border-theme"
                                    Text="Upload"
                                    Width="120px"
                                    OnClick="btnUploadVideo_Click" />
                            </div>
                            <div class="w3-padding w3-hide-small">
                                <asp:Button
                                    ID="btnUploadReset"
                                    runat="server"
                                    Text="Reset"
                                    CssClass="  w3-button
                                                w3-white
                                                w3-hover-theme
                                                w3-border
                                                w3-border-theme"
                                    Width="120px"
                                    OnClick="btnUploadReset_Click" />
                            </div>
                            <div class="w3-padding w3-hide-small">
                                <asp:Button
                                    ID="btnUploadSettings"
                                    runat="server"
                                    Text="Settings"
                                    CssClass="  w3-button
                                                w3-white
                                                w3-hover-theme
                                                w3-border
                                                w3-border-theme"
                                    Width="120px"
                                    OnClick="btnUploadSettings_Click" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlPlaySettings"
                    runat="server"
                    Visible="false">
                    <asp:Panel
                        ID="pnlURLPlaySettings"
                        runat="server"
                        CssClass="w3-row">
                        <div class="w3-col s2 w3-right">
                            <asp:Button
                                ID="btnMoreSettings"
                                runat="server"
                                Text="More"
                                Width="120px"
                                CssClass="  w3-button
                                            w3-border
                                            w3-border-theme
                                            w3-white
                                            w3-hover-theme"
                                OnClick="btnMoreSettings_Click" />
                        </div>
                        <div class="w3-col w3-rest w3-red">

                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>
        </div>

        <div class="w3-container w3-top">
            <div class="w3-red w3-top w3-center" style="max-width:170px">
                Experimental
            </div>
        </div>

        <style>
            body{
                background: url('http://toshiba/Sources/Images/Other/HonkaiGakuen.jpg') no-repeat center center fixed; 
			    -webkit-background-size: cover;
			    -moz-background-size: cover;
			    -o-background-size: cover;
			    background-size: cover;
            }
            #bg{
                position:fixed !important;
                top:0;
                bottom:0;
                left:0;
                right:0;
                z-index:-20;
            }
        </style>
        <script src="http://toshiba/Sources/JavaScript/jquery.js" type="text/javascript"></script>
        <script type="text/javascript" src="http://toshiba/Sources/JavaScript/jquery.easing.1.3.js"></script>
        <script>
            var img1 = "url('" + '<%= backgroundImage1 %>' + "') no-repeat center center fixed";
            var img2 = "url('" + '<%= backgroundImage2 %>' + "') no-repeat center center fixed";
            var img3 = "url('" + '<%= backgroundImage3 %>' + "') no-repeat center center fixed";
            var img4 = "url('" + '<%= backgroundImage4 %>' + "') no-repeat center center fixed";
            var img5 = "url('" + '<%= backgroundImage5 %>' + "') no-repeat center center fixed";
            var img6 = "url('" + '<%= backgroundImage6 %>' + "') no-repeat center center fixed";
            var img7 = "url('" + '<%= backgroundImage7 %>' + "') no-repeat center center fixed";
            var bgNumber = 1;
            
            setInterval(function () {
                if (bgNumber == 1) {
                    document.body.style.background = img1;
                }
                else if (bgNumber == 2) {
                    document.body.style.background = img2;
                }
                else if (bgNumber == 3) {
                    document.body.style.background = img3;
                }
                else if (bgNumber == 4) {
                    document.body.style.background = img4;
                }
                else if (bgNumber == 5) {
                    document.body.style.background = img5;
                }
                else if (bgNumber == 6) {
                    document.body.style.background = img6;
                }
                else if (bgNumber == 7) {
                    document.body.style.background = img7;
                }

                bgNumber++;
                if (bgNumber == 8) {
                    bgNumber = 1;
                }
                document.body.style.webkitBackgroundSize = "cover";
                document.body.style.backgroundSize = "cover"
            }, 10000);
        </script>
    </form>
</body>
</html>
