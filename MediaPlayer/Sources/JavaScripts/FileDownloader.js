self.addEventListener('message', function (downloadedImage) {
    var img = new Image();
    img.src = downloadedImage;
    self.postMessage(img);
})