<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="MediaPlayer.Help" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Sources/CSS/W3S/w3.css" />
    <link rel="stylesheet" href="Sources/CSS/Custom.css" />
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
