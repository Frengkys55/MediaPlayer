<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="MediaPlayer.SiteSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Sources/CSS/W3S/w3.css" rel="stylesheet" />
    <link href="Sources/CSS/Custom.css" rel="stylesheet" />
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
                            Video resolution (Experimental)
                        </h4>
                    </header>
                    <div class="w3-container">
                        <div class="w3-text-red">
                            Note: If the video resolution is lower, the player will not override it
                        </div>
                        <asp:DropDownList
                            ID="lstVideoResolution"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme">
                            <asp:ListItem>
                                Original
                            </asp:ListItem>
                            <asp:ListItem>
                            360p
                            </asp:ListItem>
                            <asp:ListItem Selected="True">
                            480p
                            </asp:ListItem>
                            <asp:ListItem>
                            720p
                            </asp:ListItem>
                            <asp:ListItem>
                            1080p
                            </asp:ListItem>
                            <asp:ListItem>
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
                            Frame rate override (Experimental)
                        </h4>
                    </header>
                    <div class="w3-container">
                        <div class="w3-text-red">
                            Note: The player will use the video frame speed if the speed is lower than the setting
                        </div>
                        <asp:DropDownList
                            ID="lstFrameRate"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme">
                            <asp:ListItem>
                                No override
                            </asp:ListItem>
                            <asp:ListItem>
                                24fps
                            </asp:ListItem>
                            <asp:ListItem Selected="True">
                                30fps
                            </asp:ListItem>
                            <asp:ListItem>
                                60fps
                            </asp:ListItem>
                            <asp:ListItem>
                                120fps
                            </asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <asp:Panel
                    ID="pnlFrameBuffer"
                    runat="server"
                    CssClass="w3-panel">
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
                            <asp:ListItem Selected="True">
                                Single buffer
                            </asp:ListItem>
                            <asp:ListItem>
                                Double buffer (Experimental)
                            </asp:ListItem>
                            <asp:ListItem>
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
                            Note: For now it will not preload frames, so make sure internet access is fast enough to handle real-time request.
                        </div>
                        <asp:DropDownList
                            ID="lstFramePreload"
                            runat="server"
                            CssClass="w3-input w3-border w3-border-theme">
                            <asp:ListItem Selected="True">
                                Enable preload (Experimental)
                            </asp:ListItem>
                            <asp:ListItem>
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
                            <asp:ListItem Selected="True">
                                Use setInterval
                            </asp:ListItem>
                            <asp:ListItem>
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
