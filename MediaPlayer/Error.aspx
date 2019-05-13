﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MediaPlayer.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error.....</title>
    
    <link href=<%= W3CSSLocation %> rel="stylesheet" />
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
        body{
            background-image:url('<%= errorImgURL %>');
            background-attachment:fixed;
            background-repeat:no-repeat;
            background-position:<%= backgroundPosition %> bottom;
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
