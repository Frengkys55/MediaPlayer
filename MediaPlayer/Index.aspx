<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MediaPlayer.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href=<%= W3CSSLocation %> />
    <%--<link rel="stylesheet" href=<%= customCSSLocation %> />--%>
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
                                    Text="v"
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
                        <div class="w3-col w3-container w3-rest Button-Even-Spacing">
                            <div class="w3-padding">
                                <asp:DropDownList
                                    ID="lstPlayingSpeed"
                                    runat="server"
                                    CssClass="w3-input w3-border w3-border-theme w3-bar-item"
                                    Width="120px"
                                    ToolTip="Play speed">
                                    <asp:ListItem Value="0">
                                    Speed
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">
                                    2x
                                    </asp:ListItem>
                                    <asp:ListItem Value="4">
                                    4x
                                    </asp:ListItem>
                                    <asp:ListItem Value="8">
                                    8x
                                    </asp:ListItem>
                                    <asp:ListItem Value="16">
                                    16x
                                    </asp:ListItem>
                                    <asp:ListItem Value="32">
                                    32x
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="w3-padding">
                                <asp:TextBox
                                    ID="txtCustomPlayTime"
                                    runat="server"
                                    CssClass="w3-input w3-border w3-border-theme w3-bar-item"
                                    placeholder="0:00:00"
                                    Width="120px"
                                    ToolTip="Play position">
                                </asp:TextBox>
                            </div>
                            <div class="w3-padding">
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
                background: url('<%= backgroundImage0 %>') no-repeat center center fixed; 
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
