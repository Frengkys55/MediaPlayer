<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MediaPlayer.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error.....</title>
    <style>
        body{
            background-image:url('<%= errorImgURL %>');
            background-attachment:fixed;
            background-repeat:no-repeat;
            background-position:left bottom;
            background-size:30%;
        }
    </style>
    <link href="Sources/CSS/W3S/w3.css" rel="stylesheet" />
    <link href="Sources/CSS/Custom.css" rel="stylesheet" />

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
