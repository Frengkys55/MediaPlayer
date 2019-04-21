<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MediaPlayer.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error.....</title>
    
    <link href="Sources/CSS/W3S/w3.css" rel="stylesheet" />
    <link href="Sources/CSS/Custom.css" rel="stylesheet" />
    <style>
        body{
            background-image:url('<%= errorImgURL %>');
            background-attachment:fixed;
            background-repeat:no-repeat;
            background-position:left bottom;
            background-size:30%;
        }
        .Position-Screen-Middle {
            position: absolute;
            margin: auto;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            width: 100%;
            height: 180px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Position-Screen-Middle w3-display-container">
            <div class="w3-display-middle">
                <h1>
                    <asp:Label
                        ID="lbError"
                        runat="server"
                        Text="Seems like there's an error happening on the server....">
                    </asp:Label>
                </h1>
            </div>
        </div>
    </form>
</body>
</html>
