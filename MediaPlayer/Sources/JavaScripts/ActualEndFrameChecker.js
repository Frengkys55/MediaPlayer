// This file is containing function for checking
// actual end frame of the processed file.

//self.addEventListener('message', function (e) {

//    //Required variable reclaration
//    var soapRequest = new XMLHttpRequest();
//    var receivedValue = 0;
//    var requestString = '<s: Envelope xmlns: s="http://schemas.xmlsoap.org/soap/envelope/">';
//    requestString += "<s: Header>"
//    requestString += '<Action s: mustUnderstand="1" xmlns="http://schemas.microsoft.com/ws/2005/05/addressing/none">http://tempuri.org/IService1/ProcessVideo2</Action>'
//    requestString += "</s: Header >";
//    requestString += "<s: Body>";
//    requestString += '<CheckEndFrame xmlns="http://tempuri.org/">'
//    requestString += "<videoURL>" + e.data + "</videoURL>"
//    requestString += "</CheckEndFrame>";
//    requestString += "</s: Body > ";
//    requestString += "</s: Envelope >";

//    // XMLHttpRequest preparation
//    soapRequest.open('POST', 'http://localhost:8003/Service1.svc', true);
    

//    // Send request
//    soapRequest.onreadystatechange = function () {
//        console.log("State changed");
//        console.log(soapRequest.response);

//    }

//    soapRequest.setRequestHeader('Content-Type', 'text/xml');
//    // Process received request
//    soapRequest.send(requestString);
//    // Cleanup objects

//    // return received value
//    self.postMessage(receivedValue);
//});

self.addEventListener('message', function (e) {
    var pageInString;
    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4) {
            if (xhr.status == 200) {
                pageInString = xhr.responseText;
                self.postMessage(xhr.responseText);
            }
        }
    }
    xhr.open('GET', 'http://localhost/MediaPlayer/Player.aspx');

});