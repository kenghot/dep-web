Number.prototype.round = function (p) {
    p = p || 10;
    return parseFloat(this.toFixed(p));
};


/**
    format : N and D
    example: 1234 -> N0 = 1,234, N2 = 1,234.00, #### = 1234, D8 = 00001234
*/
Number.prototype.format = function (format) {

    var num = parseFloat(this.toString());
    return kendo.toString(num, format);
};



var c2x = null;
(function () {
    var C2XFunctions = function () {
    };    

    C2XFunctions.prototype.clone = function (obj) {
        // Handle the 3 simple types, and null or undefined
        if (null == obj) return null;

        var objType = jQuery.type(obj);
        if (objType === "boolean") {
            return obj ? true : false;
        }

        if (objType === "number") {
            return Number(obj);
        }

        if (objType === "string") {
            return String(obj);
        }

        // Handle Date
        if (objType === "date") {
            var copy = new Date();
            copy.setTime(obj.getTime());
            return copy;
        }

        // Handle Array
        if (objType === "array") {
            var copy = [];
            for (var i = 0, len = obj.length; i < len; i++) {
                copy[i] = c2x.clone(obj[i]);
            }
            return copy;
        }

        // Handle Object
        if (objType === "object") {
            var copy = {};
            for (var attr in obj) {
                if (obj.hasOwnProperty(attr)) copy[attr] = c2x.clone(obj[attr]);
            }
            return copy;
        }

        if (objType === "function") {
            return null;
        }

        //return null;

        throw new Error("Unable to copy obj! Its type isn't supported.");
    };

    C2XFunctions.prototype.clearAutofill = function () {
        if (navigator.userAgent.toLowerCase().indexOf('chrome') >= 0) {
            $('input[autocomplete="off"]').each(function () {
                $(this).attr('autocomplete', 'false');               
            });
        }
    };
    

    C2XFunctions.prototype.closeAlert = function (el) {
        var alertBlock = $(el).parent().parent();
        $(alertBlock).empty();
        $(alertBlock).hide();
    };

    C2XFunctions.prototype.writeSummaryResult = function (resultMessages, errorMessage) {
        c2x.clearResultMsg();

        var resultMessageBlock = $('#resultMessage-block');
        resultMessageBlock.removeClass("show");
        resultMessageBlock.removeClass("hide");

        var alertTemplate;
        var container = document.createElement('div');
        var messages = [];
        if (resultMessages != null) {
            messages = resultMessages;
            resultMessageBlock.addClass("show");
            alertTemplate = kendo.template($("#AlertInfo").html());
        } else if (errorMessage != null) {
            messages = errorMessage;
            resultMessageBlock.addClass("show");
            alertTemplate = kendo.template($("#AlertDanger").html());
        }

        if ((messages != null) && (messages.length > 0)) {
            var resultHtml = "<ul>";
            if ($.isArray(messages)) {
                resultHtml = "<ul>";
                for (var i = 0; i < messages.length; i++) {
                    resultHtml += "<li>" + messages[i] + "</li>";
                }
                resultHtml += "</ul>";
            } else {
                resultHtml = messages;
            }
            

            resultMessageBlock.append(alertTemplate({ message: resultHtml }));
        } else if ((messages != null) && (typeof (messages.error) != 'undefined')) {
               

           resultMessageBlock.append(alertTemplate({ message: messages.error }));
        }

        scrollToAlertBlock();

    };

    C2XFunctions.prototype.showErrorCallBack = function (error) {
        var jsonObject, errorMessage;
        if (typeof (error.responseJSON) != 'undefined') { //system error (500, 404)
            jsonObject = error.responseJSON;
            errorMessage = jsonObject.error.message;
            c2x.writeSummaryResult(null, errorMessage);

        } else if (typeof (error.errors) != 'undefined') { //server custom error
            errorMessage = (typeof (error.errors.error) != "undefined") ? error.errors.error : error.errors;
            c2x.writeSummaryResult(null, errorMessage);
        } else if (typeof (error.error) != "undefined") {
            c2x.writeSummaryResult(null, error.error);
        }
    };
    
    C2XFunctions.prototype.clearResultMsg = function () {
        //var resultMessageBlock = $('#resultMessage-block');
        var resultMessageBlocks = $("div[id^='resultMessage-block']");
        if (resultMessageBlocks.length > 0) {
            $.each(resultMessageBlocks, function (index, item) {
                $(item).empty();
            });
        }
        //$('#resultMessage-block').empty();
    };

    C2XFunctions.prototype.displayRecordLog = function (config) {
        if ((typeof (config.createdDateFormat) != 'undefined') && (config.createdDateFormat.length > 1)) {
            $('#recordLog').removeClass("hide").addClass("show");
            $('#SystemInfomationUpdatedDate').text(config.updatedDateFormat);
            $('#SystemInfomationUpdatedBy').text(config.updatedByFullname);

            $('#SystemInfomationCreatedDate').text(config.createdDateFormat);
            $('#SystemInfomationCreatedBy').text(config.createdByFullname);
        }
    };

    C2XFunctions.prototype.writeTextMultiline = function (text) {
        var newText = "-";
        var regx = /(\r\n)|\n|\r/g;
        if ((text != null) && (text != "")) {
            newText = text.replace(regx, "<br \>");
        }
        return newText;
    };

    C2XFunctions.prototype.handleNumberTextBox = function (textBoxIDs) {
        var elID, el, numberFormat, min, max, text, format;
        var maxLength = 0;
      
        for (var i = 0; i < textBoxIDs.length; i++) {
            elID = textBoxIDs[i];
            el = $("#" + elID);
            text = $(el).val();
            format = $(el).attr("numberformat");
            text = toNumberFormat(text, format);
            $(el).val(text);

            $(el).attr("onkeypress", "c2x.onNumberTextBoxKeyPress(event)");
            $(el).attr("onkeyup", "c2x.onNumberTextBoxKeyUp(event)");
            $(el).attr("onfocus", "c2x.onNumberTextBoxFocus(this)");
            $(el).attr("onblur", "c2x.onNumberTextBoxBlur(this)");

        }
    };

    C2XFunctions.prototype.onNumberTextBoxKeyPress = function (e) {
        var theEvent = e || window.event;
        var elem;

        var key = theEvent.keyCode || theEvent.which;
        if ((key === 9) || (key === 8) || (key === 46)) {
            return true;
        }


        key = String.fromCharCode(key);
        var regex = /[0-9]|\./;
        
        if (e.srcElement) elem = e.srcElement;
        else if (e.target) elem = e.target;
        var value = elem.value;

        var countInvalid = (value.match(/\./g) || []).length;
        var countText = (value.match(/[^\d^\.]/g) || []).length;

        if (!regex.test(key)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
        } else if ((key == ".") && (countInvalid > 0)) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
        } else if (countText > 0) {
            theEvent.returnValue = false;
            if (theEvent.preventDefault) theEvent.preventDefault();
        }
    };

    C2XFunctions.prototype.onNumberTextBoxKeyUp = function (e) {
        var theEvent = e || window.event;
        var elem;
        if (e.srcElement) elem = e.srcElement;
        else if (e.target) elem = e.target;

        var value = elem.value;
        var countInvalid = (value.match(/[^\d^\.]/g) || []).length;
        var regx = /[^\d^\.]/g;
       
        if (countInvalid > 0) {
            theEvent.returnValue = false;
            elem.value = value.replace(regx, '');           
            if (theEvent.preventDefault) theEvent.preventDefault();
        }
    };

    C2XFunctions.prototype.onNumberTextBoxFocus = function (el) {
        var pos = getCursorPosition(el);
        var value = el.value;
        var commaPos = value.indexOf(",");
        value = el.value.replace(/,/g, '');

        pos = (pos > commaPos) ? (pos - 1) : pos;

        var countInvalid = (value.match(/[^\d^\.]/g) || []).length;

        el.value = ((value != "") && (countInvalid == 0)) ? parseFloat(value) : "";
        setCursorPosition(el, pos);
    };

    C2XFunctions.prototype.onNumberTextBoxBlur = function (el) {
        //var el = this;
        var value = el.value.replace(/[^\d^\.]/g, '');
        var min = $(el).attr("min");
        var max = $(el).attr("max");
        var numberFormat = $(el).attr("numberformat");
        var decimal = numberFormat.replace(/[^\d]/g, '');
        var format = "";
        

        if (value != "" && (!isNaN(value))) {
            min = (typeof (min) != 'undefined') ? parseFloat(min) : null;
            max = (typeof (max) != 'undefined') ? parseFloat(max) : null;
            value = parseFloat(value);

            if ((max != null) && (value > max)) {
                value = max;
            } else if ((min != null) && (value < min)) {
                value = min;
            }

            format = value.format(numberFormat);
        }

        $(el).val(format);
    };

    C2XFunctions.prototype.handleTextAreaMaxlength = function (textareaIDs) {
        var elID;
        var maxLength = 0;

        for (var i = 0; i < textareaIDs.length; i++) {
            elID = textareaIDs[i];
            maxLength = $("#" + elID).attr("maxlength");

            $("#" + elID).keypress(function (event) {
                var key = event.which;

                //all keys including return.
                if (key >= 33 || key == 13) {
                    var length = this.value.length;
                    if (length >= maxLength) {
                        event.preventDefault();
                    }
                }
            });
        }
    };

    C2XFunctions.prototype.onTextAreaKeypress = function (e) {        
        var maxLength = $(e.target).attr("maxlength");
        
        var key = event.which;

        //all keys including return.
        if (key >= 33 || key == 13) {
            var value = $(e.target).val();
            var length = value.length;
            if (length >= maxLength) {
                event.preventDefault();
            }
        }     

        
    };
       
    C2XFunctions.prototype.uploadFileSetup = function (settings) {
        var validateValidators = function () {
            if (Page_Validators === undefined || ValidatorValidate === undefined) {
                return;
            }
            for (var i = 0; i < Page_Validators.length; i++) {
                if (Page_Validators[i].controltovalidate == settings.controlID) {
                    ValidatorValidate(Page_Validators[i]);
                }
            }
        }

        var onFileUploadSelect = function (e) {
            c2x.clearResultMsg();
            clearError(e.sender.name);
            var files = e.files;
            var currentFiles = getAttachmentFilenames();
            var accepts = getAcceptFileUpload();

            // Check the extension of each file and abort the upload if it is not .jpg
            $.each(files, function () {
                var size = this.size;
                var name = this.name;
                var extension = (this.extension).toLowerCase();
                var isNameDup = ($.inArray(name, currentFiles) >= 0) ? true : false;
                var isAccept = ($.inArray(extension, accepts) >= 0) ? true : false;

                if (isAccept) {
                    if (isNameDup) {
                        writeError(e.sender.name, FILEUPLOAD_DUPLICATE);
                        //c2x.writeSummaryResult(null, FILEUPLOAD_DUPLICATE);
                        e.preventDefault();
                    } else if ((size != null) && (size > (4194304*2))) {
                        writeError(e.sender.name, FILEUPLOAD_MAXIMUM_EXCESS);
                        //c2x.writeSummaryResult(null, FILEUPLOAD_MAXIMUM_EXCESS);
                        e.preventDefault();
                    }
                } else {
                    var message = FILEUPLOAD_EXTENSION_INVALID.replace("{0}", accepts.join(", "));
                    writeError(e.sender.name, message);
                    //c2x.writeSummaryResult(null, message);
                    e.preventDefault();
                }
            });
        };


        var onFileUploadRemove = function (e) {
            c2x.clearResultMsg();
            e.data = { tempId: e.files[0].tempId };
        };

        var onFileUploadError = function (e) {
            if (typeof (e.XMLHttpRequest) != "undefined") {
                var message = e.XMLHttpRequest.responseText;
                writeError(e.sender.name, message);
                //c2x.writeSummaryResult(null, message);
            }
        };

        var onFileUploadSuccess = function (e) {
            var files = e.files;
            var response = e.response;
            if (e.operation == "upload") {
                files[0].tempId = response[0].tempId;
                var jsonData = [];
                var current = addedFileHiddenControl.val();
                if (current != '') {
                    jsonData = JSON.parse(current);
                }
                jsonData.push(files[0]);
                addedFileHiddenControl.val(JSON.stringify(jsonData));
                validateValidators();
                
            } else if (e.operation == "remove") {
                if (files[0].tempId != null) {
                    var jsonData = [];
                    var current = addedFileHiddenControl.val();
                    if (current != '') {
                        jsonData = JSON.parse(current);
                    }
                    jsonData = jsonData.filter(function (value) {
                        return value.tempId != files[0].tempId;
                    });
                    addedFileHiddenControl.val(JSON.stringify(jsonData));
                } else {
                    var jsonData = [];
                    var current = removedFileHiddenControl.val();
                    if (current != '') {
                        jsonData = JSON.parse(current);
                    }
                    jsonData.push(files[0]);
                    removedFileHiddenControl.val(JSON.stringify(jsonData));
                }
                validateValidators();
            }
        };

        var getAttachmentFilenames = function() {
            var filenames = [];            
                        
            //for (var i = 0; i < settings.existingFiles.length; i++) {
            //    filenames.push(settings.existingFiles[i].name);
            //}

            var currentFiles = getExistingFiles();
            for (var i = 0; i < currentFiles.length; i++) {
                filenames.push(currentFiles[i].name);
            }

            return filenames;
        }

        var getAcceptFileUpload = function() {
            var acceptFiles = [];
            var accept = fileControl.attr("accept");
            if (accept != null && accept != "") {
                accept = accept.replace(/ /g, "");
                acceptFiles = accept.split(",");
            }
            return acceptFiles;
        }

        var getExistingFiles = function () {
            var addedFiles = [];
            
            var current = addedFileHiddenControl.val();
            if (current != '') {
                addedFiles = JSON.parse(current);
            }

            var removedFiles = [];
            var removedFilesId = [];
            current = removedFileHiddenControl.val();
            if (current != '') {
                removedFiles = JSON.parse(current);
            }
            removedFiles.forEach(function (value) {
                removedFilesId.push(value.id);
            });

            var files = settings.existingFiles;

            files = files.filter(function (value) {
                return $.inArray(value.id, removedFilesId) == -1;
            });

            files = files.concat(addedFiles);
            return files;
        }

        var fileControl = $('#' + settings.fileUploadControlClientID);
        var addedFileHiddenControl = $('#' + settings.addedHiddenControlClientID);
        var removedFileHiddenControl = $('#' + settings.removedHiddenControlClientID);
        var attachmentTemplate ='<div class="file-wrapper">'+
                                    '<button type="button" class="k-upload-action"></button>' +
                                    '<span class="file-name">#=name#</span>' +                                    
                                    '<a href="javascript:void(0)" target="_blank" class="file-link" onclick="return c2x.openAttachment(this,\'' + settings.viewAttachmentPrefix + '\')">#=name#</a>' +
                                 '</div>';

        fileControl.kendoUpload({
            enabled: settings.enabled,
            multiple: settings.multiple,
            async: {
                saveUrl: './AttachmentHandler/Upload',
                removeUrl: './AttachmentHandler/Remove',
                autoUpload: true
            },
            files: getExistingFiles(),
            error: onFileUploadError,
            remove: onFileUploadRemove,
            select: onFileUploadSelect,
            success: onFileUploadSuccess,            
            template: kendo.template(attachmentTemplate),
            localization: {
                select: "เลือกไฟล์"
            },           
            //template: kendo.template(KENDO_FILEUPLOAD_TEMPLATE)
        });

        if (settings.startValidate){
            validateValidators();
        }

        var writeError = function (senderName, messages) {
            var fileUpload = $('[name="' + senderName + '"]').get();
            var fileUploadContainer = $(fileUpload).closest('div[class~="input-file-container"]');
            
            if (fileUploadContainer != null) {
                var errorTag = $(fileUploadContainer).find('span[class~="extension-validate"]');
                $(errorTag).text(messages);
                $(errorTag).attr("style", "visibility:visible");
            }
        };

        var clearError = function (senderName) {
            var fileUpload = $('[name="' + senderName + '"]').get();
            var fileUploadContainer = $(fileUpload).closest('div[class~="input-file-container"]');
            if (fileUploadContainer != null) {
                var errorTag = $(fileUploadContainer).find('span[class~="extension-validate"]');
                $(errorTag).empty();
                $(errorTag).attr("style", "visibility:visible");               
            }           
        };
    }

    C2XFunctions.prototype.validateRequiredComboboxWithPleaseSelectItem = function (oSrc, args) {
        var controlToValidate = oSrc.controltovalidate;
        var selectedValue = $find(controlToValidate).get_hiddenFieldControl().value;
        var isValid = false, selectedIndex;
        if ((selectedValue != "") && (!isNaN(selectedValue))) {
            selectedIndex = parseInt(selectedValue, 10);
            isValid = (selectedIndex > 0) ? true : false;
        }
        args.IsValid = isValid;
    }

    C2XFunctions.prototype.validateRequiredComboboxWithOutPleaseSelectItem = function (oSrc, args) {
        var controlToValidate = oSrc.controltovalidate;
        var selectedValue = $find(controlToValidate).get_hiddenFieldControl().value;
        var isValid = false, selectedIndex;
        if ((selectedValue != "") && (!isNaN(selectedValue))) {
            selectedIndex = parseInt(selectedValue, 10);
            isValid = (selectedIndex >= 0) ? true : false;
        }
        args.IsValid = isValid;
    }

    C2XFunctions.prototype.OnDatePickerYearShown = function(sender, args) {

        sender._switchMode("years", true);

        if (sender._yearsBody) {
            for (var i = 0; i < sender._yearsBody.rows.length; i++) {
                var row = sender._yearsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", c2x.DatePickerYearCall);
                }
            }
        }
    }

    C2XFunctions.prototype.OnDatePickerYearHide = function(sender, args) {

        sender._switchMode("years", true);
        if (sender._yearsBody) {
            for (var i = 0; i < sender._yearsBody.rows.length; i++) {
                var row = sender._yearsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", c2x.DatePickerYearCall);
                }
            }
        }
    }

    C2XFunctions.prototype.DatePickerYearCall = function (eventElement) {
        var target = eventElement.target;
        switch (target.mode) {
            case "year":
                var containerID = $(target).closest('div[class="nep-calendar"]').attr("id");
                var behaviorID = containerID.replace("_container", "");
                var cal = $find(behaviorID);
                cal._visibleDate = target.date;
                cal.set_selectedDate(target.date);
                cal._switchMonth(target.date);
                cal._blur.post(true);
                cal.raiseDateSelectionChanged();
                break;
        }
    }

    C2XFunctions.prototype.OnDatePickerMonthShown = function(sender, args) {

        sender._switchMode("months", true);

        if (sender._monthsBody) {
            for (var i = 0; i < sender._monthsBody.rows.length; i++) {
                var row = sender._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", c2x.DatePickerMonthCall);
                }
            }
        }
    }

    C2XFunctions.prototype.OnDatePickerMonthHide = function (sender, args) {

        sender._switchMode("months", true);
        if (sender._monthsBody) {
            for (var i = 0; i < sender._monthsBody.rows.length; i++) {
                var row = sender._monthsBody.rows[i];
                for (var j = 0; j < row.cells.length; j++) {
                    Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", c2x.DatePickerMonthCall);
                }
            }
        }
    }

    C2XFunctions.prototype.DatePickerMonthCall = function (eventElement) {
        var target = eventElement.target;        
        switch (target.mode) {
            case "month":
                var containerID = $(target).closest('div[class="nep-calendar"]').attr("id");
                var behaviorID = containerID.replace("_container", "");
                var cal = $find(behaviorID);
                cal._visibleDate = target.date;
                cal.set_selectedDate(target.date);
                cal._switchMonth(target.date);
                cal._blur.post(true);
                cal.raiseDateSelectionChanged();
                break;
        }        
    }

    C2XFunctions.prototype.updateCommandText = function (e, gridID) {
        if (typeof (gridID) != 'undefined') {
            // dataBound event
            var addButton = $('#' + gridID).find(".k-grid-add");
            $.each(addButton, function (index, value) {
                $(this).attr("title", ADD_BUTTON_NAME);
            });

            var editButton = $('#' + gridID).find(".k-grid-edit");
            $.each(editButton, function (index, value) {
                $(this).attr("title", EDIT_BUTTON_NAME);
            });

            var customEditButton = $('#' + gridID).find(".k-grid-custom-edit");
            $.each(customEditButton, function (index, value) {
                $(this).attr("title", EDIT_BUTTON_NAME);
            });

            var delButton = $('#' + gridID).find(".k-grid-delete");
            $.each(delButton, function (index, value) {
                $(this).attr("title", DELETE_BUTTON_NAME);
            });

            var customDelButton = $('#' + gridID).find(".k-grid-custom-delete");
            $.each(customDelButton, function (index, value) {
                $(this).attr("title", DELETE_BUTTON_NAME);
            });

        } else {
            //for edit event
            var update = $(e.container).parent().find(".k-grid-update");            
            //$(update).html('<span class="k-icon k-update"></span>');
            $(update).attr("title", SAVE_BUTTON_NAME);           

            var cancel = $(e.container).parent().find(".k-grid-cancel");           
            //$(cancel).html('<span class="k-icon k-cancel"></span>');
            $(cancel).attr("title", CANCEL_BUTTON_NAME);           
        }
    };

    function setCursorPosition(el, pos) {
        $(el).each(function (index, elem) {
            if (elem.setSelectionRange) {
                elem.setSelectionRange(pos, pos);
            } else if (elem.createTextRange) {
                var range = elem.createTextRange();
                range.collapse(true);
                range.moveEnd('character', pos);
                range.moveStart('character', pos);
                range.select();
            }
        });
        //return this;
    }

    function getCursorPosition(input) {
        //var input = this.get(0);
        if (!input) return;

        var t = input.value.replace(/[^\d^\.]/g, '');
        var start = 0;

        if ('selectionStart' in input) {
            // Standard-compliant browsers
            return input.selectionStart;
        } else if (document.selection) {
            // IE
            input.focus();
            var sel = document.selection.createRange();
            var selLen = document.selection.createRange().text.length;
            sel.moveStart('character', -input.value.length);
            return sel.text.length - selLen;
        }  

        return start;
    }

    function toNumberFormat(text, format) 
    {
        var n = ((text != null) && ($.isNumeric(text))) ? text.replace(/,/g, '') : null;
        var newText = (n != null) ? parseFloat(n).format(format) : "";
        return newText;
    }

    function scrollToAlertBlock() {
        var alertBlockPos = $('.alert').position();      
        if (alertBlockPos != null) {
            $("html,body").animate({ scrollTop: alertBlockPos.top }, 1000);
        }
               
    }
    
    C2XFunctions.prototype.ClientValidateCitizenIdValidator = function (source, arguments) {
        arguments.IsValid = c2x.ValidateCitizenId(arguments.Value);
    }

    C2XFunctions.prototype.ClientValidateCitizenIdRequiredValidator = function (source, arguments) {
        
        var str = arguments.Value;
        str = str.replace(/[-_]+/g, "");
        arguments.IsValid = (str != "");
    }

    C2XFunctions.prototype.ValidateCitizenId = function (text) {
        var isValid = false;
        var str = text;
        str = str.replace(/[-_]+/g, "");
        if (str != "") {
            if (!/^\d{13}$/.exec(str)) {
                isValid = false;
            } else {
                var sum = 0;
                var checkDigit = str.charCodeAt(12) - 48;
                for (i = 0; i <= 11; i++) {
                    digit = str.charCodeAt(i) - 48;
                    if (digit < 0 || digit > 9)
                        isValid = false;
                    sum += digit * (13 - i);
                }
                var calcDigit = 11 - (sum % 11);
                if (calcDigit >= 10)
                    calcDigit -= 10;

                isValid = calcDigit == checkDigit;
            }
        } else {
            isValid = true;
        }
        

        return isValid;
    }

    C2XFunctions.prototype.openAttachment = function (ele, prefixViewUrl) {        
        var url = "";
        var files = $(ele).parentsUntil('ul', 'li').data('files');
        var file = files[0];
        if (file.tempId != null) {
            url = "AttachmentHandler/View/Temp/" + file.tempId + '/' + encodeURIComponent(c2x.htmlDecode(file.name));
        } else {
            if (prefixViewUrl != '') {
                url = "AttachmentHandler/View/" + prefixViewUrl + "/" + file.id + '/' + encodeURIComponent(c2x.htmlDecode(file.name));
            }
        }
        if (url != '') {
            window.open(url, '_blank');
        }
        return false;
    }

    C2XFunctions.prototype.htmlDecode = function (value) {
        return $("<textarea/>").html(value).text();
    }

    C2XFunctions.prototype.htmlEncode = function(value) {
        return $('<textarea/>').text(value).html();
    }

    /*--Manage Popup--*/
    C2XFunctions.prototype.setDefaultFocusOnFormDialog = function(iframe) {
        $(document).ready(function () {
            $(iframe).contents().find(':input:text:enabled:first').focus();
        });
    }

    C2XFunctions.prototype.pageLoad = function (sender, arg) {
        if (!arg.get_isPartialLoad()) {
            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(InitializeRequest);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
        }
    };


    C2XFunctions.prototype.waitForIFrameDialog = function() {
        var dialog = $('.ui-dialog-content').get();
        var dialogLength = $(dialog).length;

        var currentDialog = $(dialog).get()[dialogLength - 1];
        var iframe = $(currentDialog).find('iframe').get()[0];

        if (typeof(iframe.readyState) != 'undefined' && ( iframe.readyState != "complete")) {
            setTimeout("c2x.waitForIFrameDialog();", 100);
        } else {

            c2x.setDefaultFocusOnFormDialog(iframe);
        }

    };

    C2XFunctions.prototype.openFormDialog = function(url, dialogTitle, options, alwaysReloadPage) {
        alwaysReloadPage = alwaysReloadPage || false;
        var targetUrl = url;

                     
        var opt = jQuery.extend({}, {
            closeOnEscape: false
            , open: function (event, ui) {
                window.top.jQuery(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
                window.top.jQuery(".ui-dialog", ui.dialog).addClass("NepProjectDialog");
               

                c2x.waitForIFrameDialog();
            }
            , url: targetUrl
            , title: dialogTitle
            , buttons: {}
            , width: 800, height: 600
            , position: { my: "center", at: "center", of: window }
            , close: function (event, ui) {
                if (d.attr("isRefresh") == "true") {
                    window.location.href = window.location.href;
                }
            }
            , modal: true
        }, options);
        var d = window.top.jQuery.FrameDialog.create(opt);

        return d;
    };


    C2XFunctions.prototype.closeFormDialog = function(isRefresh) {
        if (window.frameElement) {
            var target = jQuery.FrameDialog.current();
            if (!isRefresh) {
                // set default for refresh every close dialog action
                isRefresh = false;
            }

            target.attr("isRefresh", isRefresh);
            target.closeDialog();
        }
    };

    C2XFunctions.prototype.showLoader = function () {       
        var wH = $(window).height();
        var wW = $(window).width();
        var docH = $(document).height();
        var h = (docH > wH) ? docH : wH;
        var loader = $('#loader');
        var imgLoader = $('#ImageLoader');
        var imgTop = Math.max(0, (wH / 2) + $(window).scrollTop());
        var imgLeft = Math.max(0, (wW / 2) + $(window).scrollLeft());
        
        $(loader).height(h);       
        $(loader).show();

        $(imgLoader).css({ top: (imgTop - 48), left: (imgLeft- 48), position: 'absolute' });        
        $(imgLoader).show();
    }

    C2XFunctions.prototype.closeLoader = function () {        
        $('#loader').hide();
        $('#ImageLoader').hide();
    }

    C2XFunctions.prototype.createCombobox = function (setting) {
        var controlID = setting.ControlID;

        var headerTemplate = (typeof (setting.HeaderTemplate) != "undefined") ? setting.HeaderTemplate : null;
        var template = (typeof (setting.Template) != "undefined") ? setting.Template : null;      
        var enable = (typeof (setting.Enable) != "undefined") ? setting.Enable : true;
        var autoBind = (typeof (setting.AutoBind) != "undefined") ? setting.AutoBind : true;
      
        
        var ddl = $("#" + controlID).kendoComboBox({
            enable: enable,
            autoBind: autoBind,
            filter: "contains",
            placeholder: setting.Placeholder,
            dataTextField: setting.TextField,
            dataValueField: setting.ValueField,
            dataSource: {
                type: "json",
                serverFiltering: setting.ServerFiltering,
                transport: {                   
                    read: {
                        dataType: "json",
                        contentType: "application/x-www-form-urlencoded; charset=utf-8",
                        url: setting.ReadUrl,
                        type: "POST"
                    }
                },
                schema: {
                    data: "Data",
                    total: "TotalRow",
                },              
                requestEnd: c2x.onComboboxRequestEnd,
                error: c2x.onComboboxError
            },
            headerTemplate: headerTemplate,
            template: template,           
        }).data("kendoComboBox");

        if (typeof (setting.DataBound) != "undefined") {
            ddl.bind("dataBound", setting.DataBound);
        }

        if (typeof (setting.Select) != "undefined") {
            ddl.bind("select", setting.Select);
        }

        if (typeof (setting.Change) != "undefined") {
            ddl.bind("change", setting.Change);
        }

        if ((typeof (setting.Value) != "undefined") && (setting.Value != null) && (setting.Value != "")) {
            ddl.value(setting.Value);
        } 
       
    }

    C2XFunctions.prototype.validateComboBoxRequired = function(oSrc, args) {
        var ddlID = $(oSrc).attr("data-val-controltovalidate");
        var ddl = $("#" + ddlID).data("kendoComboBox");       
        var item = ddl.dataItem();
        args.IsValid = (item != null);
    }

    C2XFunctions.prototype.clearValidateComboBoxRequired = function (comboboxID) {
        var val = $('span[data-val-controltovalidate="' + comboboxID + '"]').get(0);
        if (val != null) {
            $(val).css("visibility", " hidden");
        }      
    }

    C2XFunctions.prototype.createLocalCombobox = function (setting) {
        var controlID = setting.ControlID;
        var headerTemplate = (typeof (setting.HeaderTemplate) != "undefined") ? setting.HeaderTemplate : null;
        var template = (typeof (setting.Template) != "undefined") ? setting.Template : null;
        var enable = (typeof (setting.Enable) != "undefined") ? setting.Enable : true;

        var ddl = $("#" + controlID).kendoComboBox({           
            enable: enable,            
            filter: "contains",
            placeholder: setting.Placeholder,
            dataTextField: setting.TextField,
            dataValueField: setting.ValueField,
            dataSource: {
                type: "json",
                serverFiltering: setting.ServerFiltering,
                data: setting.Data,
                schema: {
                    data: "Data",
                    total: "TotalRow",
                },                
                error: c2x.onComboboxError
            },
            headerTemplate: headerTemplate,
            template: template,            
        }).data("kendoComboBox");

        if (ddl != null) {

            if ((typeof (setting.Value) != "undefined") && (setting.Value != null) && (setting.Value != "")) {
                ddl.value(setting.Value);
            } else if ((typeof (setting.SelectedIndex) != "undefined")) {
                ddl.select(setting.SelectedIndex);
            }

            if (typeof (setting.Change) != "undefined") {
                ddl.bind("change", setting.Change);
            }
        }
        
    };

    C2XFunctions.prototype.createComboboxCustom = function (setting) {
        var controlID = setting.ControlID;

        var headerTemplate = (typeof (setting.HeaderTemplate) != "undefined") ? setting.HeaderTemplate : null;
        var template = (typeof (setting.Template) != "undefined") ? setting.Template : null;
        var enable = (typeof (setting.Enable) != "undefined") ? setting.Enable : true;
        var autoBind = (typeof (setting.AutoBind) != "undefined") ? setting.AutoBind : true;
        var param = (typeof (setting.Param) != "undefined") ? setting.Param : null;

        var ddl = $("#" + controlID).kendoComboBox({
            enable: enable,
            autoBind: autoBind,
            filter: "contains",
            placeholder: setting.Placeholder,
            dataTextField: setting.TextField,
            dataValueField: setting.ValueField,
            dataSource: {
                type: "json",
                serverFiltering: setting.ServerFiltering,              
                transport: {
                    read: {
                        dataType: "json",
                        contentType: "application/x-www-form-urlencoded; charset=utf-8",
                        url: setting.ReadUrl,
                        type: "POST",
                        data: param
                    }
                },
                schema: {
                    data: "Data",
                    total: "TotalRow",
                },

                requestEnd: c2x.onComboboxRequestEnd,

                error: c2x.onComboboxError
            },
            headerTemplate: headerTemplate,
            template: template,
        }).data("kendoComboBox");

        if (typeof (setting.DataBound) != "undefined") {
            ddl.bind("dataBound", setting.DataBound);
        }

        if (typeof (setting.Open) != "undefined") {
            ddl.bind("open", setting.Open);
        }

        if ((typeof (setting.Value) != "undefined") && (setting.Value != null) && (setting.Value != "")) {
            var selectedValue = setting.Value;
            if ((typeof (setting.ParentID) != "undefined")) {
                var ddlParent = $("#" + setting.ParentID).data("kendoComboBox");
                
                ddl.dataSource.read({ parentid: ddlParent.value() }).then(function () {
                    ddl.enable(true);
                    ddl.value(selectedValue);
                });
            }
        } else if (typeof (setting.ParentID) != "undefined") {
            var ddlParent = $("#" + setting.ParentID).data("kendoComboBox");
            if (ddlParent != null && (ddlParent.value() != "")) {
                ddl.dataSource.read({ parentid: ddlParent.value() }).then(function () {
                    ddl.enable(true);                   
                });
            }

        }


        if (typeof (setting.Change) != "undefined") {
            ddl.bind("change", setting.Change);
        }       

    }

    C2XFunctions.prototype.createVirtualCombobox = function (setting) {
        var controlID = setting.ControlID;

        var headerTemplate = (typeof (setting.HeaderTemplate) != "undefined") ? setting.HeaderTemplate : null;
        var template = (typeof (setting.Template) != "undefined") ? setting.Template : null;
        var enable = (typeof (setting.Enable) != "undefined") ? setting.Enable : true;
        var autoBind = (typeof (setting.AutoBind) != "undefined") ? setting.AutoBind : true;
        var param = (typeof (setting.Param) != "undefined") ? setting.Param : null;
        var ddlParentID = (typeof (setting.ParentID) != "undefined") ? setting.ParentID : null;

        var ddl = $("#" + controlID).kendoComboBox({           
            enable: enable,
            autoBind: autoBind,
            filter: "contains",
            placeholder: setting.Placeholder,
            dataTextField: setting.TextField,
            dataValueField: setting.ValueField,
            virtual: {
                itemHeight: 26,
                valueMapper: function (options) {
                    $.ajax({
                        url: setting.VirtualUrl,
                        type: "post",
                        dataType: "json",
                        data: c2x.virtualComboboxConvertValues(options.value, ddlParentID),
                        success: function (data) {                          
                            options.success(data);
                        }
                    })
                }
            },
            height: 260,
            dataSource: {
                type: "json",
                serverFiltering: true,
                pageSize: 40,
                serverPaging: true,
                transport: {
                    read: {
                        dataType: "json",
                        contentType: "application/x-www-form-urlencoded; charset=utf-8",
                        url: setting.ReadUrl,
                        type: "POST",
                        data: param
                    }
                },
                schema: {
                    data: "Data",
                    total: "TotalRow",
                },

                requestEnd: c2x.onComboboxRequestEnd,

                error: c2x.onComboboxError
            },
            value: setting.Value,
            headerTemplate: headerTemplate,
            template: template,
        }).data("kendoComboBox");

        var isReaded = false;

        if (typeof (setting.DataBound) != "undefined") {
            ddl.bind("dataBound", setting.DataBound);
        }

        if (typeof (setting.Open) != "undefined") {
            ddl.bind("open", setting.Open);
        }



        if (typeof (setting.Change) != "undefined") {
            ddl.bind("change", setting.Change);
        }
    }

    C2XFunctions.prototype.virtualComboboxConvertValues = function (value, ddlParentID) {
       
        var data = {};
        

        value = $.isArray(value) ? value : [value];

        for (var idx = 0; idx < value.length; idx++) {
            data["values[" + idx + "]"] = value[idx];
        }

        if (typeof (ddlParentID) != "undefined" && (ddlParentID != null)) {
            var ddl = $("#" + ddlParentID).data("kendoComboBox");
            var selectedItem = (ddl != null) ? ddl.dataItem() : null;
            if (selectedItem != null) {
                data["values[1]"] = selectedItem.Value;
            }
        }

        return data;
    };

    C2XFunctions.prototype.onComboboxError = function (e) {      

        if (typeof (e.errorThrown) != "undefined") {
            c2x.writeSummaryResult(null, e.message);
        }
    }

    C2XFunctions.prototype.onComboboxRequestEnd = function (e) {       
        if (e.response != null) {
            var result = e.response;
            if ((result.TotalRow == 0) && (result.IsCompleted)) {
                c2x.writeSummaryResult(result.Message, null);
            } else if (result.IsCompleted == false) {
                c2x.writeSummaryResult(null, result.Message);
            }
        }
    }

    C2XFunctions.prototype.onComboboxDataBound = function (ddlID, e) {        
        var dataSource = e.sender.dataSource;
        var total = dataSource.total();
        var container = $("#" + ddlID + "-list");
                
        if (total > 0) {
            var dataLength = dataSource.data().length;            
            
            if (total > dataLength) {
                var divContinue = $(container).find('[class="continue"]').get(0);
                
                if (divContinue == null) {
                    $(container).append("<div class='continue'>ผลการค้นหา " + dataLength + " รายการ จาก " + total + " รายการ</div>");
                }
            } else {
                $(container).find('[class="continue"]').remove();
            }
        } else {
            $(container).find('[class="continue"]').remove();
        }
    }

    C2XFunctions.prototype.onProvinceComboboxChange = function (districtID, subDistrictID, e) {       
        var item = e.sender.dataItem();
        var ddlDistrict = $("#" + districtID).data("kendoComboBox");
        var ddlSubDistrict = $("#" + subDistrictID).data("kendoComboBox");

        if (item != null) {
           
            var provinceID = parseInt(item.Value, 10);            
            var dataSource = ddlDistrict.dataSource;            

            ddlDistrict.value("");
            ddlDistrict.enable(true);           
            ddlDistrict.dataSource.read().then(function () {
                ddlDistrict.focus();
            });

            ddlSubDistrict.value("");
            ddlSubDistrict.enable(false);
        } else {
            

            ddlDistrict.value("");
            ddlDistrict.enable(false);

            ddlSubDistrict.value("");
            ddlSubDistrict.enable(false);
        }
        
    };

    C2XFunctions.prototype.onDistrictComboboxChange = function (subDistrictID, e) {
        var item = e.sender.dataItem();
        var ddlSubDistrict = $("#" + subDistrictID).data("kendoComboBox");
        if (item != null) {
            var districtID = parseInt(item.Value, 10);


            var dataSource = ddlSubDistrict.dataSource;

            ddlSubDistrict.value("");
            ddlSubDistrict.enable(true);
            ddlSubDistrict.dataSource.read().then(function () { ddlSubDistrict.focus(); });
        } else {
            ddlSubDistrict.value("");
            ddlSubDistrict.enable(false);
        }
    };   

    C2XFunctions.prototype.getProvinceComboboxParam = function (parentID) {
        var ddl = $("#" + parentID).data("kendoComboBox");
        var selectItem = ddl.dataItem();

        if (selectItem != null) {
            return { parentid: selectItem.Value };
        }
    };


    C2XFunctions.prototype.onCustomControlLoading = function () {
       
        var intervalID = setInterval(showLoading, 1000);
        function showLoading(buttonIds) {
            var hasLoadig = ($('.k-loading').length > 0);
                        
            if (hasLoadig) {
                c2x.showLoader();
            } else {
                c2x.closeLoader();
                if (typeof (intervalID) != "undefined") {
                    clearInterval(intervalID);
                }
            }
                        
        }
    };

    function validateCitizenID(text) {
        var isValid = false;
        var str = text;
        str = str.replace(/[-_]+/g, "");
        if (!/^\d{13}$/.exec(str)) {
            isValid = false;
        } else {
            var sum = 0;
            var checkDigit = str.charCodeAt(12) - 48;
            for (i = 0; i <= 11; i++) {
                digit = str.charCodeAt(i) - 48;
                if (digit < 0 || digit > 9)
                    isValid = false;
                sum += digit * (13 - i);
            }
            var calcDigit = 11 - (sum % 11);
            if (calcDigit >= 10)
                calcDigit -= 10;

            isValid = calcDigit == checkDigit;
        }

        return isValid;
    }
    

    c2x = new C2XFunctions();
})();




