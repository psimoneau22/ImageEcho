var tradeSumoUtils = (function () {
    
    function dataURLToFile(dataURL, filename) {
        var BASE64_MARKER = ';base64,';
        if (dataURL.indexOf(BASE64_MARKER) == -1) {
            var parts = dataURL.split(',');
            var contentType = parts[0].split(':')[1];
            var raw = decodeURIComponent(parts[1]);

            return new Blob([raw], { type: contentType });
        }

        var parts = dataURL.split(BASE64_MARKER);
        var contentType = parts[0].split(':')[1];
        var raw = window.atob(parts[1]);
        var uInt8Array = binaryToArray(raw);

        return new File([uInt8Array], filename, { type: contentType });
    };

    function binaryToArray(binaryData) {
        var uInt8Array = new Uint8Array(binaryData.length);
        for (var i = 0; i < binaryData.length; ++i) {
            uInt8Array[i] = binaryData.charCodeAt(i);
        }
        return uInt8Array;
    }

    // quality factor is a number from 0 to 1     
    function resizeImage(imageFile, settings, callback) {

        var defaults = {
            maxWidth: 800,
            maxHeight: 600,
            qualityFactor: .5
        }
        var options = $.extend({}, defaults, settings);        

        var imageObj = new Image();
        imageObj.onload = function (tt) {

            var canvas = document.createElement('canvas');
            canvas.width = imageObj.width;
            canvas.height = imageObj.height;

            var context = canvas.getContext('2d');
            context.clearRect(0, 0, canvas.width, canvas.height);

            var width = imageObj.width;
            var height = imageObj.height;

            if (width > height) {
                if (width > options.maxWidth) {
                    height *= options.maxWidth / width;
                    width = options.maxWidth;
                }
            }
            else {
                if (height > options.maxHeight) {
                    width *= options.maxHeight / height;
                    height = options.maxHeight;
                }
            }
            canvas.width = width;
            canvas.height = height;
            context.drawImage(imageObj, 0, 0, width, height);

            callback(dataURLToFile(canvas.toDataURL("image/jpeg", options.qualityFactor), "optimized_" + imageFile.name));
        }
        imageObj.src = window.URL.createObjectURL(imageFile);
    }

    return {
        resizeImage: resizeImage
    };
})();