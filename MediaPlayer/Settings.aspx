<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="MediaPlayer.SiteSettings" %>

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
    <title>Settings - MediaPlayer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="w3-container w3-border-right w3-border-theme w3-card" style="width: 40%">
                <h3>
                    Settings (Experimental)
                </h3>
                <asp:Panel
                    ID="pnlVideoResolution"
                    runat="server"
                    CssClass="w3-panel">
                    <header>
                        <h4>
                            Video resolution
                        </h4>
                    </header>
                    <div class="w3-container">
                        <div class="w3-text-red">

                        </div>
                        <asp:DropDownList
                            ID="lstVideoResolution"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme">
                            <asp:ListItem Value="0">
                                Original (bug)
                            </asp:ListItem>
                            <asp:ListItem Value="7" Enabled="false">
                                Other
                            </asp:ListItem>
                            <asp:ListItem Value="360">
                            360p
                            </asp:ListItem>
                            <asp:ListItem Value="480" Selected="True">
                            480p
                            </asp:ListItem>
                            <asp:ListItem Value="720">
                            720p
                            </asp:ListItem>
                            <asp:ListItem Value="1080">
                            1080p
                            </asp:ListItem>
                            <asp:ListItem Value="1440">
                            1440p
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlVideoFrameRate"
                    runat="server"
                    CssClass="w3-panel">
                    <header>
                        <h4>
                            Frame rate override
                        </h4>
                    </header>
                    <div class="w3-container">
                        <div class="w3-text-red">
                            
                        </div>
                        <asp:DropDownList
                            ID="lstFrameRate"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme">
                            <asp:ListItem Value="-1">
                                No override (bug)
                            </asp:ListItem>
                            <asp:ListItem Value="24">
                                24fps
                            </asp:ListItem>
                            <asp:ListItem Value="30" Selected="True">
                                30fps
                            </asp:ListItem>
                            <asp:ListItem Value="60">
                                60fps
                            </asp:ListItem>
                            <asp:ListItem Value="120">
                                120fps
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlFrameBuffer"
                    runat="server"
                    CssClass="w3-panel"
                    style="display:none">
                    <header>
                        <h4>
                            Buffer mode (Experimental)
                        </h4>
                    </header>
                    <div class="w3-container">
                        <div class="w3-text-red">
                            Note: For now the application will use single buffer mode. Double buffer mode is still has weird bugs need to be fixed.
                        </div>
                        <asp:DropDownList
                            ID="lstFrameBufferMode"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme">
                            <asp:ListItem Value="1" Selected="True">
                                Single buffer
                            </asp:ListItem>
                            <asp:ListItem Value="2" Enabled="false">
                                Double buffer (Experimental)
                            </asp:ListItem>
                            <asp:ListItem Value="3" Enabled="false">
                                Triple buffer (Not yet supported)
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlPreload"
                    runat="server"
                    CssClass="w3-panel">
                    <header>
                        <h4>
                            Preload frames (Experimental)
                        </h4>
                    </header>
                    <div class="w3-container">
                        <div class="w3-text-red">
                            
                        </div>
                        <asp:DropDownList
                            ID="lstFramePreload"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme">
                            <asp:ListItem Value="1" Selected="True">
                                Enable preload (Experimental)
                            </asp:ListItem>
                            <asp:ListItem Value="0" Enabled="false">
                                Disable preload
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlPlayMode"
                    runat="server"
                    CssClass="w3-panel">
                    <header>
                        <h4>
                            Play mode (Experimental)
                        </h4>
                    </header>
                    <div class="w3-container">
                        <asp:DropDownList
                            ID="lstPlayMode"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme">
                            <asp:ListItem Value="1" Selected="True">
                                Use setInterval
                            </asp:ListItem>
                            <asp:ListItem Value="2" Enabled="false">
                                Use requestAnimationFrame
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlSettingsConfirmation"
                    runat="server"
                    CssClass="w3-panel">
                    <asp:Button
                        ID="btnSaveSettings"
                        runat="server"
                        CssClass="  w3-button
                                    w3-white
                                    w3-border
                                    w3-border-theme
                                    w3-hover-theme"
                        Width="120px"
                        Text="Save"
                        OnClick="btnSaveSettings_Click" />
                    <asp:Button
                        ID="btnCancelSettings"
                        runat="server"
                        CssClass="  w3-button
                                    w3-white
                                    w3-border
                                    w3-border-theme
                                    w3-hover-theme"
                        Width="120px"
                        Text="Cancel"
                        OnClick="btnCancelSettings_Click" />
                </asp:Panel>
                
            </div>
        </div>
    </form>
</body>
</html>
