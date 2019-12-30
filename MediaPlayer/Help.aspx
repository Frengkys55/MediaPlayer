<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="MediaPlayer.Help" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
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
    <title>Help - MediaPlayer</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="w3-container">
            <div class="w3-row w3-padding-small">
                <div class="w3-col" style="width:200px;">
                    <asp:ImageButton
                        ID="imgLogo"
                        runat="server"
                        ImageUrl="~/Sources/Images/MediaPlayer(Small).png"
                        CssClass="w3-hide-small"
                        OnClick="imgLogo_Click" />
                </div>
                <div class="w3-rest">
                    <h2>Help</h2>
                </div>
            </div>
        </div>
        <br />
        <div class="w3-container w3-card w3-padding">
            <p>MediaPlayer merupakan sebuah aplikasi berbasis web yang memungkinkan untuk memutar video dengan berbagai format yang tidak didukung atau tidak didukung sepenuhnya (seperti jenis video RealMedia dan HEVC) dengan menggunakan browser.</p>
        </div>
        <asp:Panel
            ID="pnlPlayFromURL"
            runat="server"
            CssClass="w3-container">
            <h4>Pemutaran video dari URL</h4>
            <ol>
                <li>Masuk ke halaman utama media player <a href="Index.aspx">disini</a></li>
                <li>Jika tulisan "Use URL" tidak terpilih, tekan "Use URL"</li>
                <li>Salin URL video yang diinginkan dari situs yang menyimpannya</li>
                <li>Tekan tombol "Play Video"</li>
                <li>Tunggu hingga halaman dialihkan</li>
                <li>Video sudah dapat ditonton</li>
            </ol>
        </asp:Panel>
        <asp:Panel
            ID="pnlPlayFromFile"
            runat="server"
            CssClass="w3-container">
            <h4>Pemutaran video dari file</h4>
            <ol>
                <li>Masuk ke halaman utama media player <a href="Index.aspx">disini</a></li>
                <li>Jika tulisan "Use file" tidak terpilih, tekan "Use file"</li>
                <li>Unggah video yang diinginkan</li>
                <li>Tekan tombol "Upload"</li>
                <li>Tunggu hingga halaman dialihkan</li>
                <li>Tunggu hingga tombol kendali video muncul dan video siap diputar</li>
            </ol>
        </asp:Panel>
        <asp:Panel
            ID="pnlCommands"
            runat="server"
            CssClass="w3-container">
            <h4>Perintah-perintah (Experimental)</h4>
            <div class="w3-text-theme">
                Format penggunaan: url_video perintah perintah_lainnya 
            </div>
            <div>
                Command info
            </div>
            <ol>
                <li>help = Buka halaman bantuan</li>
                <li>settings = Buka halaman Pengaturan</li>
            </ol>
            <div>
                Debuging info
            </div>
            <ol>
                <li>sample = Muat contoh file (<a href="https://www.youtube.com/watch?v=tyneiz9FRMw" target="_blank">Akari ga yattekita zo ~tsu</a>)</li>
                <li>error = (Debug) Buka halaman Error.aspx</li>
                <li>player = (Debug) Buka (paksa) halaman Player.aspx</li>
                <li>checker = (Debug) Buka halaman Checker.aspx</li>
            </ol>
        </asp:Panel>
    </form>
</body>
</html>
