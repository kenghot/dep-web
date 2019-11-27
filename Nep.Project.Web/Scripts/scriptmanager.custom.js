//custom filter combox
Sys.Application.add_load(function () {
    if ((typeof (Sys.Extended) != "undefined") && (typeof (Sys.Extended.UI.ComboBox) != "undefined")) {

        //Sys.Extended.UI.ComboBox.prototype._getOptionListHeight = function () {
        //    // gets the height of an individual option list item
        //    if (this._optionListItemHeight == null && this.get_optionListControl().scrollHeight > 0) {
        //        this._optionListItemHeight = (this.get_optionListControl().scrollHeight / this._optionListItems.length);
        //    } else if (Sys.Browser.agent === Sys.Browser.InternetExplorer && Sys.Browser.version < 7
        //        && Math.round(this.get_optionListControl().scrollHeight / this._optionListItems.length) < this._optionListItemHeight) {
        //        this._optionListItemHeight = (this.get_optionListControl().scrollHeight / this._optionListItems.length);
        //    }
        //    return this._optionListItemHeight;

        //    //// gets the total height of the option list 
        //    //var buttonControlTop = $(this._buttonControl).offset().top;
        //    //var screenRelativeTop = buttonControlTop - (window.scrollY ||
        //    //                                window.pageYOffset || document.body.scrollTop);
            
        //    //var wHeight = window.screen.availHeight;
        //    //if (buttonControlTop > wHeight) {
        //    //    this._optionListHeight = wHeight - screenRelativeTop;
        //    //    //this._optionListHeight = 200;
        //    //}


        //    //console.log(this._optionListHeight);

        //    //if (this._optionListHeight == null || (this._getOptionListItemHeight() * this._optionListItems.length) < this._optionListHeight) {
        //    //    // multiply the height of one item by the total number of items
        //    //    console.log('-----');
        //    //    console.log(this._getOptionListItemHeight());
        //    //    console.log(this._optionListItems.length);
        //    //    this._optionListHeight = this._getOptionListItemHeight() * this._optionListItems.length;
        //    //}

        //    //// protect against negative dimensions
        //    //if (this._optionListHeight < 0) {
        //    //    this._optionListHeight = 0;
        //    //}

        //    //return this._optionListHeight;
        //};
        
        
        Sys.Extended.UI.ComboBox.prototype._ensureHighlightedIndex = function () {

            var textBoxValue = this.get_textBoxControl().value;           
            var isFound = isTextMatch(textBoxValue, this._optionListItems);

            if (textBoxValue == "") {              
                this.set_selectedIndex(-1);
            }

            if (isFound) {
                var children = this.get_optionListControl().childNodes;
                for (var i = 0; i < this._optionListItems.length; i++) {
                    children[i].style.display = "list-item";
                }
                return;
            } else if (this._highlightedIndex != null && this._highlightedIndex >= 0
                && this._isExactMatch(this._optionListItems[this._highlightedIndex].text, textBoxValue)) {
                return;
            }

            // need to find the correct index
            var firstMatch = -1;
            var ensured = false;
            var children = this.get_optionListControl().childNodes;


            for (var i = 0; i < this._optionListItems.length; i++) {
                var itemText = this._optionListItems[i].text;
                children[i].style.display = isContainsMatch(itemText, textBoxValue) ? "list-item" : "none";

                if (!ensured && this._isExactMatch(itemText, textBoxValue)) {

                    this._highlightListItem(i, true);
                    ensured = true;
                }
                    // if in DropDownList mode, save first match.
                else if (!ensured && firstMatch < 0 && this._highlightSuggestedItem) {
                    if (isContainsMatch(itemText, textBoxValue)) { //_isPrefixMatch
                        firstMatch = i;
                    }
                }
            }

            if (!ensured) {
                this._highlightListItem(firstMatch, true);
            }
        };

        Sys.Extended.UI.ComboBox.prototype._handleArrowKey = function (e) {
            // when shift key is pressed, ignore arrow codes
            if (e.shiftKey == true) {
                return null;
            }

            var code = this._getKeyboardCode(e);

            // cycle through items when up & down arrow keys are used
            if (code == Sys.UI.Key.up || code == Sys.UI.Key.down) {
                if (this._popupBehavior._visible) {
                    var vector = code - 39;
                    var displayStyle = "";
                    var highlightedIndex = this._highlightedIndex;
                    var newIndex = highlightedIndex;
                    //console.log('vector: ' + vector);
                    if ((vector == -1 && this._highlightedIndex > 0) || (vector == 1 && this._highlightedIndex < this._optionListItems.length - 1)) {
                        var children = this.get_optionListControl().childNodes;
                        if (vector == -1) {
                            for (var i = (this._optionListItems.length - 1) ; i >= 0; i--) {
                                if ((children[i].style.display == "list-item") && (i < highlightedIndex)) {
                                    newIndex = i;
                                    break;
                                }
                            }
                        } else {
                            for (var i = 0 ; i < this._optionListItems.length; i++) {
                                if ((children[i].style.display == "list-item") && (i > highlightedIndex)) {
                                    newIndex = i;
                                    break;
                                }
                            }
                        }

                        //var newIndex = (this._highlightedIndex + vector);
                        this.get_textBoxControl().value = this._optionListItems[newIndex].text;
                        this._highlightListItem(newIndex, true);
                        this.set_selectedIndex(newIndex);
                        this._ensureScrollTop();
                    }
                }
                else {
                    this._popupBehavior.show();
                }

                if (e.type == 'keypress') {
                    e.preventDefault();
                    e.stopPropagation();
                    return false;
                }
                return true;
            }
            return null;

        };
    }

});


//if (typeof (Sys.Extended.UI.CalendarBehavior) != 'undefined') {
//    Sys.Extended.UI.CalendarBehavior.prototype.set_selectedDate = function (value) {
//        if (value && (String.isInstanceOfType(value)) && (value.length != 0)) {            
//            value = parseToDateTimeFormat(value);
//            value = new Date(value);
           
//        }

//        if (this._selectedDate != value) {
//            this._selectedDate = value;
//            this._selectedDateChanging = true;
//            var text = "";
//            if (value) {
//                text = this._convertToLocal(value).localeFormat(this._format);

//                // If we don't clear the time then we transfer the time from the
//                // textbox to the selected value
//                if (!this._clearTime) {
//                    var tbvalue = this._textbox.get_Value();
//                    if (tbvalue) {
//                        tbvalue = this._parseTextValue(tbvalue);
//                    }
//                    if (tbvalue) {
//                        if (value != tbvalue.getDateOnly()) {
//                            // Transfer time from textbox to selected value
//                            value.setUTCHours(tbvalue.getUTCHours());
//                            value.setUTCMinutes(tbvalue.getUTCMinutes());
//                            value.setUTCMilliseconds(tbvalue.getUTCMilliseconds());

//                            text = this._convertToLocal(value).localeFormat(this._format);
//                        }
//                    }
//                }

//            }
//            if (text != this._textbox.get_Value()) {
//                this._textbox.set_Value(text);
//                this._fireChanged();
//            }
//            this._selectedDateChanging = false;
//            this.invalidate();
//            this.raisePropertyChanged("selectedDate");
//        }
//    };
//}

function parseToDateTimeFormat(text) {
    if (text.length > 10) {
        text = text.replace(" ", "T");
        text = text + ".000Z";
    } else {
        text = text + "T00:00:00.000Z";
    }   
    return text;
}


