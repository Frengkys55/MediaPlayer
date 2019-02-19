//onconnect = function (e) {
//    var port = e.ports[0];

//    port.onmessage = function (e) {
//        var workerResult = 'Result: ' + (e.data[0] * e.data[1]);
//        port.postMessage(workerResult);
//    }
//}

// FrameWorker - How it works
// 1. Read the processed position from main thread
// 2. Create 300 new objects of images
// 3. Create 300 new objects for image downloader object
// 4. Initialize each objects
// 5. Create event listener for 300 image downloader objects
// 6. Send message to download images
//
// Note: I'm doing this with C# and C++ programming in mind and not Java, so there
// is might be some errors happening
// Also this is the first time i used the var keyword a lot!


self.addEventListener('message', function (processedPosition) {
    var currentProcessedFrameNumber = processedPosition;
    var currentImagePosition = 0;
    
    // Preparing images array object
    var downloadedImages = new Array(300);

    // Preparing downloaders array object
    var downloadWorker = new Array(downloadedImages.length);

    // Preparing downloaders status array
    var isDownloadCompleted = new Array(downloadedImages.length);

    // Initializing each image save location
    for (var i = 0; i < downloadedImages.length; i++) {
        downloadedImages[i] = new Image();
    }

    // Initializing each worker
    for (var i = 0; i < downloadedImages.length; i++) {
        downloadWorker[i] = new Worker('FileDownloader.js');
        downloadWorker[i].addEventListener('message', function (downloadedImage) {
            downloadedImages[i] = downloadedImage.data;
            isDownloadCompleted[i] = true;
        })
    }

    // Initializing each worker status
    for (var i = 0; i < isDownloadCompleted.length; i++) {
        isDownloadCompleted[i] = false;
    }

    // Begin download
    for (var i = currentProcessedFrameNumber + 1; i < i + 300; i++) {
        downloadWorker[currentImagePosition].postMessage(i);
        currentImagePosition++;
    }

    // Check for all images completion
    while (true) {
        var isAllTrue = false;
        for (var i = 0; i < isDownloadCompleted.length; i++) {
            if (isDownloadCompleted[i] == false) {
                break;
            }
            if (i == isDownloadCompleted.length) {
                isAllTrue = true;
            }
        }
        if (isAllTrue) {
            break;
        }
    }

    // Cleanup
    delete downloadWorker;
    delete isDownloadCompleted;

    // Report
    self.postMessage(downloadedImages);
}, false);