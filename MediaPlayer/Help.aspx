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
            <div>
                Tambah "res=720" dan spasi didepan url untuk mengganti resolusi
            </div>
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
                <li>Video sudah dapat ditonton</li>
            </ol>
        </asp:Panel>
        <asp:Panel
            ID="pnlCommands"
            runat="server"
            CssClass="w3-container">
            <h4>Perinta-perintah</h4>
            <div class="w3-text-theme">
                Format penggunaan: perintah perintah_lainnya url video
            </div>
            <div>
                Command info
            </div>
            <ol>
                <li>res= = Atur resolusi video (contoh: res=720)</li>
                <li>help = Buka halaman bantuan</li>
                <li>settings = Buka halaman Pengaturan</li>
            </ol>
            <div>
                Debuging info
            </div>
            <ol>
                <li>sample = Muat contoh file</li>
                <li>error = (Debug) Buka halaman Error</li>
                <li>player = (Debug) Buka (paksa) halaman Player</li>
                <li>checker = (Debug) Buka halaman Checker.aspx</li>
            </ol>
            <div class="w3-text-red">
                Catatan: Perintah yang dimasukkan akan secara otomatis mengganti pengaturan yang telah tersimpan (untuk akun ini)
            </div>
        </asp:Panel>
        <asp:Panel
            ID="pnlWarning"
            runat="server"
            CssClass="w3-container">
            Catatan:<br />
            Pastikan ada terdapat ruang memori yang  cukup untuk menyimpan gambar (300).
            <br />
            Untuk saat ini, aplikasi akan menggunakan fungsi &ldquo;EnablePreload&rdquo; untuk membuat proses pemutaran video menjadi lebih baik.
            <br />
            Karena fungsi &ldquo;Enable Preload&rdquo; dihidupkan, aplikasi mungkin akan menggunakan banyak memori untuk memuat gambar.
        </asp:Panel>
        <div class="w3-container w3-panel w3-red w3-text-white">
            Catatan:
            <br />
            Untuk sekarang video hanya dapat diputar dengan resolusi awalan, yaitu 854x480.
        </div>
    </form>
</body>
</html>
