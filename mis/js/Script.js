function validateusername(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 32)
        return false;
    return true;
}
function validatename(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
        return false;
    }
    return true;
}

// onkeypress="return validateNum(event)"
function validateNum(evt) {
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
//onkeypress="return alpha(event);"	
function alpha(e) {
    var k;
    document.all ? k = e.keyCode : k = e.which;
    return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || k == 32 || (k >= 48 && k <= 57));
}
// onkeypress="return validateDec(this,event)"
function validateDec(el, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    var number = el.value.split('.');
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    //just one dot (thanks ddlab)
    if (number.length > 1 && charCode == 46) {
        return false;
    }
    //get the carat position
    var caratPos = getSelectionStart(el);
    var dotPos = el.value.indexOf(".");
    if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
        return false;
    }
    return true;
}
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}
// onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*XLS*XLSX', this),ValidateFileSize(this,1024*1024*1)"
function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
    //ex---------------
    //maxLengthFileName=50;
    //validFileFormaString=JPG*JPEG*PDF*DOCX
    //uploadControlId=upSaveBill
    //ex---------------
    var msg = '';
    if (document.getElementById(that.id).value != '') {
        var size = document.getElementById(that.id);

        var fileName = document.getElementById(that.id).value;
        var lengthFileName = parseInt(document.getElementById(that.id).value.length)

        var fileExtacntionArray = new Array();
        fileExtacntionArray = fileName.split('.');

        if (fileExtacntionArray.length == 2) {

            var fileExtacntion = fileExtacntionArray[fileExtacntionArray.length - 1];


            if (lengthFileName >= parseInt(maxLengthFileName) + parseInt(1)) {
                msg += '- File name should be less than ' + maxLengthFileName + ' characters. \n';
            }
            for (i = 0; i <= (fileName.length - 1) ; i++) {
                var charFileName = '';

                charFileName = fileName.substring(i, i + 1);

                if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                    msg += '- Special character not allowed in file name. \n';
                    break;
                }

            }
            var isFileFormatCorrect = false;
            var strValidFormates = '';

            if (validFileFormaString != "") {

                var fileFormatArray = new Array();
                fileFormatArray = validFileFormaString.split('*');

                for (var j = 0; j < fileFormatArray.length; j++) {
                    if (fileFormatArray[j].toUpperCase() == fileExtacntion.toUpperCase()) {
                        isFileFormatCorrect = true;
                    }

                    if (j == fileFormatArray.length - 1) {
                        strValidFormates += '.' + fileFormatArray[j].toLowerCase();

                    }
                    else {
                        strValidFormates += '.' + fileFormatArray[j].toLowerCase() + '/';

                    }
                }

                if (isFileFormatCorrect == false) {
                    msg += '- File format is not correct (only ' + strValidFormates + ').\n';
                }
            }

        }
        else {
            msg += '- File name is incorrect';
        }
        if (msg != '') {
            document.getElementById(that.id).value = "";
            alert(msg);
            return false;
        }
        else {
            return true;
        }

    }
}


function ValidateFileSize(a, size) {
    // 1 kb =(size=1024) 
    // 1 mb =(size=1024 * 1024 * 1) 

    var uploadcontrol = document.getElementById(a.id);
    if (uploadcontrol.files[0].size > size) {
        alert('File size should not greater than' + size / 1024 + ' kb.');
        document.getElementById(a.id).value = '';
        return false;
    }
    else {
        return true;
    }
}
// onblur="return checkvalue(this);"
function checkvalue(id) {
    id.value = id.value.trim();
    if (isNaN(id.value)) {
        id.value = '';
        alert('Please enter correct value !')
    }
}