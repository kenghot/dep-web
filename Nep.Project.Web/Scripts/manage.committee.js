
var c2xCommittee = null;
(function () {
    var C2XFunctions = function () {
    };

    var _checkDupMsg = "";
    var _requiredFirstnameMsg = "";
    var _requiredLastnameMsg = "";
    var _requiredPositionMsg = "";
    var _requiredPositionCodeMsg = "";

    var _isViewMode = false; 
    var _columnTitle = {};

    var _committeeGridID = "";
    var _officerGridID = "";

    var _hiddOganizationID = "";
    var _hiddenCommitteeDataID = "";
    var _hiddenOfficerDataID = "";

    var _txtMainCommitteeFirstNameID = "";
    var _txtMainCommitteeLastNameID = "";
    var _cbbCommitteePosition = "";
    
    var _btnSaveCommitteeID = "";
    var _btnCancelCommitteeID = "";
    var _txtCommitteeFirstNameID = "";
    var _txtCommitteeLastNameID = "";
    var _txtCommitteePositionID = "";
    var _txtCommitteePositionCodeID = "";
    var _txtEditCommitteeFirstNameID = 'TxtEditCommitteeFirstName';
    var _txtEditCommitteeLastNameID = 'TxtEditCommitteeLastName';
    var _cbbEditCommitteePositionID = 'CBBEditCommitteePosition';
    var _txtEditCommitteePositionID = 'TxtEditCommitteePosition';
    
    var _btnSaveOfficerID = "";
    var _btnCancelOfficerID = "";
    var _txtOfficerFirstNameID = "";
    var _txtOfficerLastNameID = "";
    var _txtOfficerPositionID = "";
    var _txtEditOfficerFirstNameID = 'TxtEditOfficerFirstName';
    var _txtEditOfficerLastNameID = 'TxtEditOfficerLastName';
    var _txtEditOfficerPositionID = 'TxtEditOfficerPosition';

    var _currentCommitteeEditItem = null
    var _currentOfficerEditItem = null;

    C2XFunctions.prototype.committeeConfig = function (config) {
        _checkDupMsg = config.CheckDupMsg;
        _requiredFirstnameMsg = config.RequiredFirstnameMsg;
        _requiredLastnameMsg = config.RequiredLastnameMsg;
        _requiredPositionMsg = config.RequiredPositionMsg;
        _requiredPositionCodeMsg = config.RequiredPositionCodeMsg;

        _hiddOganizationID = config.HiddenOganizationID;

        _columnTitle = config.ColumnTitle;
        _isViewMode = config.IsView;

        _committeeGridID = config.CommitteeGridID;
        _officerGridID = config.OfficerGridID;

        _txtMainCommitteeFirstNameID = config.TxtMainCommitteeFirstNameID;
        _txtMainCommitteeLastNameID = config.TxtMainCommitteeLastNameID;
        _cbbCommitteePosition = config.CBBMainCommitteePosition;

        _hiddenCommitteeDataID = config.HiddenCommitteeDataID;
        _hiddenOfficerDataID = config.HiddenOfficerDataID;

        _btnSaveCommitteeID = config.BtnSaveCommitteeID;
        _btnCancelCommitteeID = config.BtnCancelCommitteeID;
        _txtCommitteeFirstNameID = config.TxtCommitteeFirstNameID;
        _txtCommitteeLastNameID = config.TxtCommitteeLastNameID;
        _txtCommitteePositionID = config.TxtCommitteePositionID;
     

        _btnSaveOfficerID = config.BtnSaveOfficerID;
        _btnCancelOfficerID = config.BtnCancelOfficerID;
        _hiddOfficerUidID = config.HiddOfficerUidID;
        _txtOfficerFirstNameID = config.TxtOfficerFirstNameID;
        _txtOfficerLastNameID = config.TxtOfficerLastNameID;
        _txtOfficerPositionID = config.TxtOfficerPositionID;
               
       
        bindCreateCommitteeRowEvent(_committeeGridID);
        bindCreateOfficerRowEvent(_officerGridID);
               

    };
            
    C2XFunctions.prototype.createGridCommittee = function (gridID) {
        var validateGroup = "";
        var firstNameEditor, lastNameEditor, positionEditor;

        if (gridID == _committeeGridID) {          
            validateGroup = "SaveCommitttee";            
            firstNameEditor = c2xCommittee.createCommitteeTextBoxFirstnameEditor;
            lastNameEditor = c2xCommittee.createCommitteeTextBoxEditor;
            posCodeEditor = c2xCommittee.createPositionEditor;
            positionEditor = c2xCommittee.createCommitteeTextBoxEditor;
        } else {          
            validateGroup = "SaveOfficer";          
            firstNameEditor = c2xCommittee.createOfficerTextBoxFirstnameEditor;
            lastNameEditor = c2xCommittee.createOfficerTextBoxEditor;
            positionEditor = c2xCommittee.createOfficerTextBoxEditor;
        }
        //kenghot
        if (gridID == _committeeGridID) {
            var columns = [
                   { field: 'No', title: _columnTitle.No, width: '50px;' },
                   { field: 'MemberName', title: _columnTitle.FirstName, width: '220px', editor: firstNameEditor },
                   { field: 'MemberSurname', title: _columnTitle.LastName, width: '220px', editor: lastNameEditor },
                   { field: 'PositionName', title: _columnTitle.PositionCode, width: '220px', editor: posCodeEditor },
                   { field: 'MemberPosition', title: _columnTitle.Position, width: '220px', editor: positionEditor },

            ];
        }
        else {
            var columns = [
                   { field: 'No', title: _columnTitle.No, width: '50px;' },
                   { field: 'MemberName', title: _columnTitle.FirstName, width: '220px', editor: firstNameEditor },
                   { field: 'MemberSurname', title: _columnTitle.LastName, width: '220px', editor: lastNameEditor },
                  // { field: 'MemberPosition', title: _columnTitle.Position, width: '220px', editor: positionEditor },
            ];
        }
        if (!_isViewMode) {
            columns.push({
                command: [
                    { name: 'edit', text: { edit: '', cancel: '', update: '' } },
                    { name: 'destroy', text: '' }
                ], title: '&nbsp;', width: '65px'
            });
        }

        $('#' + gridID).kendoGrid({
            autoBind: false,
            dataSource: {
                data: [],
                schema: {
                    model: {
                        id: "UID",
                        fields: {
                            UID: { editable: false, nullable: false },
                            OrganizationID: { type:"number", editable: false, nullable: false },
                            No: {type:"number", editable: false, nullable: false },
                            CommitteePosition: { type: "string", editable: false},
                            MemberName: {type:"string"},
                            MemberSurname: { type: "string" },
                            PositionName: { type: "string" },
                            PositionCode: {type:"string"},
                            MemberPosition: { type: "string" },
                        }
                    }
                },
                pageSize: Number.MAX_VALUE
            },
            pageable: false,
            columns: columns,
            editable: {
                mode: "inline",
                confirmation: false,
            },
            scrollable: false,
            edit: function (e) { c2xCommittee.editRowCommittee(e, gridID) },
            remove: function (e) { c2xCommittee.destroyCommittee(e, gridID) },
            save: function (e) { c2xCommittee.updateCommittee(e, gridID) },
            cancel: function (e) { c2xCommittee.cancelRowCommittee(e, gridID) },
            dataBound: function (e) { c2x.updateCommandText(e, gridID); }
        });



        c2xCommittee.bindDataCommittee(gridID);

    };

    C2XFunctions.prototype.createRowCommittee = function (gridID) {
        
        var uid = guid();
        var validateConfig = {};
        var validateGroup = "";
        var gridContainerID = "";
        if (gridID == _committeeGridID) {
           
            validateConfig = {
                UID: uid,
                TxtMemberNameID: _txtCommitteeFirstNameID,
                TxtMemberSurnameID: _txtCommitteeLastNameID,
                CBBMainCommitteePosition: _cbbCommitteePosition,
                TxtMemberPositionID:_txtCommitteePositionID,              
                ValiatorFirstnameRequiredClientID: "ValidateRequiredCommitteeFirstname",
                ValiatorLastnameRequiredClientID: "ValidateRequiredCommitteeLastname",
                ValiatorPositionnameRequiredClientID: "ValidateRequiredCommitteePosition",
                ValiatorPositionCodeRequiredClientID: "ValidateRequiredCommitteePositionCode",
                ValiatorFirstnameDupClientID: "ValidateDupCommitteeFirstname",
            };
            validateGroup = "SaveCommittee";
            gridContainerID = "CommitteeGridContainer";
        } else {
            validateConfig = {
                UID: uid,
                TxtMemberNameID: _txtOfficerFirstNameID,
                TxtMemberSurnameID: _txtOfficerLastNameID,
                TxtMemberPositionID: _txtOfficerPositionID,               
                ValiatorFirstnameRequiredClientID: "ValidateRequiredOfficerFirstname",
                ValiatorLastnameRequiredClientID: "ValidateRequiredOfficerLastname",
                ValiatorPositionnameRequiredClientID: "ValidateRequiredOfficerPosition",
                ValiatorFirstnameDupClientID: "ValidateDupOfficerFirstname",
            };
            validateGroup = "SaveOfficer";
            gridContainerID = "OfficerGridContainer";
        }       
        clearValidatorCreateForm(validateGroup);
        var isValid = validateCommittee(validateConfig, validateGroup);

        var hiddenDataID = "";
        var textBoxFocus = "";
        if (gridID == _committeeGridID) {
            hiddenDataID = _hiddenCommitteeDataID;
            textBoxFocus = _txtCommitteeFirstNameID;
        } else {
            hiddenDataID = _hiddenOfficerDataID;
            textBoxFocus = _txtOfficerFirstNameID;
        }

        if (isValid) {
            var grid = $('#' + gridID).data("kendoGrid");
            if ((grid != 'undefined') && (grid != null)) {
                newItem = getCommitteeModel(true, validateGroup);
                grid.dataSource.add(newItem);
                grid.saveChanges();
                var dataItems = grid.dataSource.data();
                var jsonText = kendo.stringify(dataItems);
                
                $("#" + hiddenDataID).val(jsonText);
                $('#' + gridID).show();
                $('#' + gridContainerID).show();
            }

            c2xCommittee.cancelRowCommittee(null, gridID);            
        }

        

        $("#" + textBoxFocus).focus();

        return false;
    };

    C2XFunctions.prototype.updateCommittee = function (e, gridID) {
        var currentEdit = (gridID == _committeeGridID)? _currentCommitteeEditItem : _currentOfficerEditItem;
        var uid = ((typeof (e) != 'undefined') && (e != null)) ? e.model.UID : currentEdit.UID;
        var validateConfig, validateGroup, hiddDataID;
        if (gridID == _committeeGridID) {
            validateConfig = {
                UID: uid,
                TxtMemberNameID: _txtEditCommitteeFirstNameID,
                TxtMemberSurnameID: _txtEditCommitteeLastNameID,
                TxtMemberPositionID: _txtEditCommitteePositionID,
                CBBMainCommitteePosition: _cbbEditCommitteePositionID,
                ValiatorFirstnameRequiredClientID: "ValidateRequiredCommitteeFirstnameEdit",
                ValiatorLastnameRequiredClientID: "ValidateRequiredCommitteeLastnameEdit",
                ValiatorPositionnameRequiredClientID: "ValidateRequiredCommitteePositionEdit",
                ValiatorPositionCodeRequiredClientID: "ValidateRequiredCommitteePositionCodeEdit",
                ValiatorFirstnameDupClientID: "ValidateDupCommitteeFirstnameEdit",
            };

            validateGroup = "SaveCommittee";
            hiddDataID = _hiddenCommitteeDataID;

        } else {
            validateConfig = {
                UID: uid,
                TxtMemberNameID: _txtEditOfficerFirstNameID,
                TxtMemberSurnameID: _txtEditOfficerLastNameID,
                TxtMemberPositionID: _txtEditOfficerPositionID,
                ValiatorFirstnameRequiredClientID: "ValidateRequiredOfficerFirstnameEdit",
                ValiatorLastnameRequiredClientID: "ValidateRequiredOfficerLastnameEdit",
                ValiatorPositionnameRequiredClientID: "ValidateRequiredOfficerPositionEdit",
                ValiatorFirstnameDupClientID: "ValidateDupOfficerFirstnameEdit",
            };
            validateGroup = "SaveOfficer";
            hiddDataID = _hiddenOfficerDataID;
        }      

        clearValidatorEditForm(validateGroup);
        var isValid = validateCommittee(validateConfig, validateGroup);
        var grid = $('#' + gridID).data("kendoGrid");
        if (isValid) {
            var newItem = getCommitteeModel(false, validateGroup);
            var jsonText = $("#" + hiddDataID).val();
            var item;
            if (jsonText.length > 0) {
                var data = $.parseJSON(jsonText);
                for (var i = 0; i < data.length; i++) {
                    item = data[i];
                    if (item.UID == uid) {
                        data[i] = newItem;
                    }
                }

                var newValue = kendo.stringify(data);
                $('#' + hiddDataID).val(newValue);
            }

            grid.dataSource.data(data);
            grid.saveChanges();

            

            disableCreateForm(false, validateGroup);
        } else if (typeof (e) != 'undefined') {
            grid.one("dataBinding", function (args) {
                args.preventDefault();//cancel grid rebind if error occurs  
            });

        }

    };

    C2XFunctions.prototype.destroyCommittee = function (e, gridID) {
        var uid = e.model.UID;
        var hiddDataID = (gridID == _committeeGridID) ? _hiddenCommitteeDataID : _hiddenOfficerDataID;
        var gridContainerID = (gridID == _committeeGridID) ? "CommitteeGridContainer" : "OfficerGridContainer";
        var jsonText = $('#' + hiddDataID).val();
        if (jsonText.length > 0) {
            var data = $.parseJSON(jsonText);
            data = $.grep(data, function (item) {
                return item.UID !== uid;
            });

            //reorder          
            for (var i = 0; i < data.length; i++) {
                data[i].No = (i + 1);
            }            
           
            var grid = $("#" + gridID).data("kendoGrid");
            grid.dataSource.data(data);
            if (data.length == 0) {
                $('#' + hiddDataID).val("");
                $("#" + gridID).hide();
                $("#" + gridContainerID).hide();
            } else {
                var newValue = kendo.stringify(data);
                $('#' + hiddDataID).val(newValue);
            }
        }

    };

    C2XFunctions.prototype.cancelCreateRowCommittee = function (validateGroup) {
        clearCreateForm(validateGroup);
        clearValidatorCreateForm(validateGroup);
        return false;

    };

    C2XFunctions.prototype.editRowCommittee = function (e, gridID) {
        var validateGroup = (gridID == _committeeGridID)? "SaveCommittee" : "SaveOfficer";
        c2x.updateCommandText(e, gridID);

        disableCreateForm(true, validateGroup);
        if (gridID == _committeeGridID) {
            _currentCommitteeEditItem = c2x.clone(e.model);
        } else {
            _currentOfficerEditItem = c2x.clone(e.model);
        }

        if (gridID == _committeeGridID) {
            bindEditCommitteeRowEvent(gridID);
        } else {
            bindEditOfficerRowEvent(gridID);
        }     

    };

    C2XFunctions.prototype.cancelRowCommittee = function (e, gridID) {
        var validateGroup = (gridID == _committeeGridID) ? "SaveCommittee" : "SaveOfficer";
        var grid = $("#" + gridID).data("kendoGrid");
        if (gridID == _committeeGridID) {
            _currentCommitteeEditItem = null;
        } else {
            _currentOfficerEditItem = null;
        }
       
        grid.cancelRow();
        disableCreateForm(false, validateGroup);
        c2xCommittee.bindDataCommittee(gridID);
    };

    C2XFunctions.prototype.bindDataCommittee = function (gridID) {
        var hidd = (gridID == _committeeGridID) ? $('#' + _hiddenCommitteeDataID) : $('#' + _hiddenOfficerDataID);
        var gridContainerID = (gridID == _committeeGridID) ? "CommitteeGridContainer" : "OfficerGridContainer";
        if (hidd.length > 0) {
            var tgValue = $(hidd).val();
            var jsonTgs = (tgValue != "") ? ($.parseJSON(tgValue)) : null;

            if ((jsonTgs != null) && (jsonTgs.length > 0)) {
                $("#" + gridID).show();
                $("#" + gridContainerID).show();

                var grid = $("#" + gridID).data("kendoGrid");
                grid.dataSource.data(jsonTgs);
            } else {
                $("#" + gridID).hide();
                $("#" + gridContainerID).hide();
            }
        }

    };

    C2XFunctions.prototype.createCommitteeTextBoxEditor = function (container, options) {
        var controlId = (options.field == "MemberSurname") ? _txtEditCommitteeLastNameID : _txtEditCommitteePositionID;
        var validateClientID = (options.field == "MemberSurname") ? "ValidateRequiredCommitteeLastnameEdit" : "ValidateRequiredCommitteePositionEdit";
        var msg = (options.field == "MemberSurname") ? _requiredLastnameMsg : _requiredPositionMsg;
        var html = '<input id="' + controlId + '"  class="form-control" data-bind="value:' + options.field + '" />' +
                       '<span class="committee-validate error-text" client-id="'+ validateClientID +'"' + 
                             ' id="Validate' + controlId + '" data-val-validationgroup="SaveCommittee" data-val-controltovalidate="' +
                             controlId + '" style="display:none;">' + msg + '</span>';
        $(html).appendTo(container);      

    };
    C2XFunctions.prototype.createPositionEditor = function (container, options) {
        var cbb = $('#' + _cbbCommitteePosition);
        var controlId = _cbbEditCommitteePositionID;
        //var controlId = (options.field == "PositionName") ? _txtEditCommitteeLastNameID : _txtEditCommitteePositionID;
        //var validateClientID = (options.field == "PositionName") ? "ValidateRequiredCommitteeLastnameEdit" : "ValidateRequiredCommitteePositionEdit";
        //var msg = (options.field == "PositionName") ? _requiredLastnameMsg : _requiredPositionMsg;
       // var newname = 'CommitteeControl_ComboBoxPosition' + Date.now();
       
        //var html = cbb[0].outerHTML.replace('CommitteeControl_ComboBoxPosition', newname);
        var validateClientID = "ValidateRequiredCommitteePositionCode";
        var html = '<select class="form-control" data-bind="value:PositionCode" id="' + controlId + '" style="height:30px;width:100%;">' + cbb[0].innerHTML + '</select>'
        '<span class="committee-validate error-text" client-id="' + validateClientID + '"' +
                            ' id="Validate' + controlId + '" data-val-validationgroup="SaveCommittee" data-val-controltovalidate="' +
                            controlId + '" style="display:none;">' + _requiredPositionCodeMsg + '</span>';
        //html = html.replace('<select ', '<select class="form-control" data-bind="value:PositionCode" ');
        $(html).appendTo(container);
        //var newcbb = _cbbCommitteePosition.replace('CommitteeControl_ComboBoxPosition', newname);
        //var cbb = $('#' + newcbb  );
        //var selectCode = options.model.PositionCode;
       // cbb.val(selectCode);
    };
    C2XFunctions.prototype.createOfficerTextBoxEditor = function (container, options) {
        
        var controlId = (options.field == "MemberSurname") ? _txtEditOfficerLastNameID : _txtEditOfficerPositionID;
        var validateClientID = (options.field == "MemberSurname") ? "ValidateRequiredOfficerLastnameEdit" : "ValidateRequiredOfficerPositionEdit";
        var msg = (options.field == "MemberSurname") ? _requiredLastnameMsg : _requiredPositionMsg;
        var html = '<input id="' + controlId + '"  class="form-control" data-bind="value:' + options.field + '" />' +
                       '<span class="committee-validate error-text" client-id="' + validateClientID + '"' +
                             ' id="Validate' + controlId + '" data-val-validationgroup="SaveCommittee" data-val-controltovalidate="' +
                             controlId + '" style="display:none;">' + msg + '</span>';
        $(html).appendTo(container);

    };

    C2XFunctions.prototype.createCommitteeTextBoxFirstnameEditor = function (container, options) {
        var id = _txtEditCommitteeFirstNameID;
        var dupValidator = "ValidateDupCommitteeFirstnameEdit";
        var requiredValidator = "ValidateRequiredCommitteeFirstnameEdit";
        
        var html = '<input id="'+ id +'"  class="form-control" data-bind="value:' + options.field + '" />' +
                   '<span class="committee-validate error-text"' + ' client-id="' + requiredValidator + '"' +
                      ' id="Validate' + id + '" runat="server" data-val-validationgroup="SaveCommittee" data-val-controltovalidate="' +
                             id + '" style="display:none;">' + _requiredFirstnameMsg + '</span>' +
                   '<span class="committee-validate error-text"' + ' client-id="' + dupValidator + '"' +
                      ' id="ValidateDup' + id + '" runat="server" data-val-validationgroup="SaveCommittee"  data-val-controltovalidate="' +
                             id + '" style="display:none;">' + _checkDupMsg + '</span>';
        
        $(html).appendTo(container);
    };

    C2XFunctions.prototype.createOfficerTextBoxFirstnameEditor = function (container, options) {
        var id = _txtEditOfficerFirstNameID;
        var dupValidator = "ValidateDupOfficerFirstnameEdit";
        var requiredValidator = "ValidateRequiredOfficerFirstnameEdit";
        var html = '<input id="' + id + '"  class="form-control" data-bind="value:' + options.field + '" />' +
                   '<span class="committee-validate error-text"' + ' client-id="' + requiredValidator + '"' +
                      ' id="Validate' + id + '" runat="server" data-val-validationgroup="SaveCommittee" data-val-controltovalidate="' +
                             id + '" style="display:none;">' + _requiredFirstnameMsg + '</span>' +
                   '<span class="committee-validate error-text"' + ' client-id="' + dupValidator + '"' +
                      ' id="ValidateDup' + id + '" runat="server" data-val-validationgroup="SaveCommittee"  data-val-controltovalidate="' +
                             id + '" style="display:none;">' + _checkDupMsg + '</span>';
      
        $(html).appendTo(container);
    };


    C2XFunctions.prototype.bindDataCommittee = function (gridID) {
        var hidd = (gridID == _committeeGridID) ? $('#' + _hiddenCommitteeDataID) : $('#' + _hiddenOfficerDataID);
        var gridContainerID = (gridID == _committeeGridID) ? "CommitteeGridContainer" : "OfficerGridContainer";

        if (hidd.length > 0) {
            var textJson = $(hidd).val();
            var dataItems = (textJson != "") ? ($.parseJSON(textJson)) : null;

            if ((dataItems != null) && (dataItems.length > 0)) {
                $("#" + gridID).show();
                $("#" + gridContainerID).show();


                var grid = $("#" + gridID).data("kendoGrid");
                grid.dataSource.data(dataItems);
            } else {
                $("#" + gridID).hide();
                $("#" + gridContainerID).hide();
            }
        }

    };

    C2XFunctions.prototype.validateDupHeadCommittee = function () {
        var isValid = true;
        var validator = null;
        //Firstname
        var firstname = $("#" + _txtMainCommitteeFirstNameID).val();
        firstname = ($.trim(firstname)).toLowerCase();

        //Lastname
        var lastname = $("#" + _txtMainCommitteeLastNameID).val();
        lastname = ($.trim(lastname)).toLowerCase();

        if ((firstname != "") && (lastname != null)) {
            // check committee
            var jsonText;
            var committeeList = null;
            var item;
            jsonText = $("#" + _hiddenCommitteeDataID).val();
            if (isValid && (jsonText != "")) {
                // check committee name
                committeeList = $.parseJSON(jsonText);
                committeeList = $.grep(committeeList, function (item) {
                    return ((item.MemberName).toLowerCase() == firstname) && ((item.MemberSurname).toLowerCase() == lastname);
                });

                if (committeeList.length > 0) {
                    isValid = false;
                    validator = $("[client-id='CheckDupCommitteeFirstname']").get(0);
                    $(validator).css("visibility", "visible");
                }
            }

            // check officer
            jsonText = $("#" + _hiddenOfficerDataID).val();
            if (isValid && (jsonText != "")) {
                committeeList = $.parseJSON(jsonText);
                committeeList = $.grep(committeeList, function (item) {
                    return ((item.MemberName).toLowerCase() == firstname) && ((item.MemberSurname).toLowerCase() == lastname);
                });

                if (committeeList.length > 0) {
                    isValid = false;
                    validator = $("[client-id='CheckDupCommitteeFirstname']").get(0);
                    $(validator).css("visibility", "visible");
                }
            }
        }

        if (isValid) {
            validator = $("[client-id='CheckDupCommitteeFirstname']").get(0);
            $(validator).css("visibility", "hidden");
        }
       

        return isValid;
    };
    
    function validateCommittee(config, validateGroup) {
        var isValid = true;
        var validator;
        var uid = config.UID;
        var txtFirstnameID = config.TxtMemberNameID;
        var txtLastnameID = config.TxtMemberSurnameID;
        var txtPositionID = config.TxtMemberPositionID;
        var txtPositionCodeID = config.CBBMainCommitteePosition;

        //Firstname
        var firstname = $("#" + txtFirstnameID).val();
        firstname = ($.trim(firstname)).toLowerCase();
        if (firstname == "") {
            isValid = false;           
            validator = $("[client-id='"+ config.ValiatorFirstnameRequiredClientID +"']").get(0);
            $(validator).show();
        }

        //Lastname
        var lastname = $("#" + txtLastnameID).val();
        lastname = ($.trim(lastname)).toLowerCase();
        if (lastname == "") {
            isValid = false;
           
            validator = $("[client-id='" + config.ValiatorLastnameRequiredClientID + "']").get(0);
            $(validator).show();
        }
        // Position code
        var postionCode = $("#" + txtPositionCodeID + " :selected").val();
        postionCode = $.trim(postionCode);
        if (postionCode == "" && validateGroup == "SaveCommittee") {
            isValid = false;

            validator = $("[client-id='" + config.ValiatorPositionCodeRequiredClientID + "']").get(0);
            $(validator).show();
        }

        //Position
        var position = $("#" + txtPositionID).val();
        position = $.trim(position);
        if (postionCode != "99") {
            $("#" + txtPositionID).val("");
        }
        if (postionCode == "99" && position == "") {
            isValid = false;
                       
            validator = $("[client-id='" + config.ValiatorPositionnameRequiredClientID + "']").get(0);
            $(validator).show();
        }

        //CheckDup
        if (isValid) {
            // check head committee
            var mainCommitteeFirstname = $("#" + _txtMainCommitteeFirstNameID).val();
            mainCommitteeFirstname = ($.trim(mainCommitteeFirstname)).toLowerCase();

            var mainCommitterLastname = $("#" + _txtMainCommitteeLastNameID).val();
            mainCommitterLastname = ($.trim(mainCommitterLastname)).toLowerCase();

            if ((firstname == mainCommitteeFirstname) && (lastname == mainCommitterLastname)) {
                isValid = false;
                validator = $("[client-id='" + config.ValiatorFirstnameDupClientID + "']").get(0);
                $(validator).show();
            }

            // check committee
            var jsonText;
            var committeeList = null;
            var item;
            jsonText = $("#" + _hiddenCommitteeDataID).val();
            if (isValid && (jsonText != "")) {
                // check committee name
                committeeList = $.parseJSON(jsonText);
                committeeList = $.grep(committeeList, function (item) {
                    return (((item.MemberName).toLowerCase() == firstname) && ((item.MemberSurname).toLowerCase() == lastname) && (item.UID != uid)) ;
                });

                if (committeeList.length > 0) {
                    isValid = false;
                    validator = $("[client-id='" + config.ValiatorFirstnameDupClientID + "']").get(0);
                    $(validator).show();
                }
            }

            // check officer
            jsonText = $("#" + _hiddenOfficerDataID).val();
            if (isValid && (jsonText != "")) {
                committeeList = $.parseJSON(jsonText);
                committeeList = $.grep(committeeList, function (item) {
                    return (((item.MemberName).toLowerCase() == firstname) && ((item.MemberSurname).toLowerCase() == lastname) && (item.UID != uid));
                });

                if (committeeList.length > 0) {
                    isValid = false;
                    validator = $("[client-id='" + config.ValiatorFirstnameDupClientID + "']").get(0);
                    $(validator).show();
                }
            }
           
        }
        

        return isValid;
    }

    function clearValidatorCreateForm(validateGroup) {

        var validator;

        if (validateGroup == "SaveCommittee") {
            validator = $("span[client-id='ValidateRequiredCommitteeFirstname']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredCommitteeLastname']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredCommitteePosition']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredCommitteePositionCode']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateDupCommitteeFirstname']").get(0);
            $(validator).hide();
        } else {
            validator = $("span[client-id='ValidateRequiredOfficerFirstname']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredOfficerLastname']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredOfficerPosition']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateDupOfficerFirstname']").get(0);
            $(validator).hide();
        }

        validator = $("span[client-id='CustomValidatorCommittee']").get(0);
        $(validator).hide();

    }

    function clearValidatorEditForm(validateGroup) {

        var validator;

        if (validateGroup == "SaveCommittee") {
            validator = $("span[client-id='ValidateRequiredCommitteeFirstnameEdit']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredCommitteeLastnameEdit']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredCommitteePositionEdit']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateDupCommitteeFirstnameEdit']").get(0);
            $(validator).hide();
        } else {
            validator = $("span[client-id='ValidateRequiredOfficerFirstnameEdit']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredOfficerLastnameEdit']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateRequiredOfficerPositionEdit']").get(0);
            $(validator).hide();

            validator = $("span[client-id='ValidateDupOfficerFirstnameEdit']").get(0);
            $(validator).hide();
        }

    }

    function clearCreateForm(validateGroup) {     

        if (validateGroup == "SaveCommittee") {
            $("#" + _txtCommitteeFirstNameID).val("");
            $("#" + _txtCommitteeLastNameID).val("");
            $("#" + _txtCommitteePositionID).val("");          
        } else {
            $("#" + _txtOfficerFirstNameID).val("");
            $("#" + _txtOfficerLastNameID).val("");
            $("#" + _txtOfficerPositionID).val("");
        }
       
    }

    function getCommitteeModel(isCreate, validateGroup) {
        var hiddDataID = "", txtFirstNameID = "", txtLastNameID = "", txtPositionID = "", txtPositionCodeID="", committeePosition = "", uid = "";
        var orgID = $("#" + _hiddOganizationID).val();
       
        if (isCreate && (validateGroup == "SaveCommittee")) {
            hiddDataID = _hiddenCommitteeDataID;
            txtFirstNameID = _txtCommitteeFirstNameID;
            txtLastNameID = _txtCommitteeLastNameID;
            txtPositionCodeID = _cbbCommitteePosition;
            txtPositionID = _txtCommitteePositionID;
            committeePosition = "2";
            uid = guid();

        }else if(isCreate && (validateGroup == "SaveOfficer")){
            hiddDataID = _hiddenOfficerDataID;
            txtFirstNameID = _txtOfficerFirstNameID;
            txtLastNameID = _txtOfficerLastNameID;
            txtPositionID = _txtOfficerPositionID;
            committeePosition = "3";
            uid = guid();

        } else if ((!isCreate) && (validateGroup == "SaveCommittee")) {
            hiddDataID = _hiddenCommitteeDataID;
            txtFirstNameID = _txtEditCommitteeFirstNameID;
            txtLastNameID = _txtEditCommitteeLastNameID;
            txtPositionID = _txtEditCommitteePositionID;
            txtPositionCodeID = _cbbEditCommitteePositionID;
            committeePosition = "2";
            uid = _currentCommitteeEditItem.UID;

        } else if (validateGroup == "SaveOfficer") {
            hiddDataID = _hiddenOfficerDataID;
            txtFirstNameID = _txtEditOfficerFirstNameID;
            txtLastNameID = _txtEditOfficerLastNameID;
            txtPositionID = _txtEditOfficerPositionID;
            committeePosition = "3";
            uid = _currentOfficerEditItem.UID;
        }
        var PositionCode = PositionName = "";
        if (txtPositionCodeID != "")
        {
            var cbb = $('#' + txtPositionCodeID + ' :selected');
            

            if (cbb.length > 0)
            {
                PositionCode = cbb.val();
                PositionName = cbb.text();
            }
        }
      
        var txtFirstName = $("#" + txtFirstNameID).val();
        txtFirstName = $.trim(txtFirstName);

        var txtLastName = $("#" + txtLastNameID).val();
        txtLastName = $.trim(txtLastName);

        var txtPosition = $("#" + txtPositionID).val();
        txtPosition = $.trim(txtPosition);

        var jsonText = $("#" + hiddDataID).val();
        var dataItems = (jsonText != "") ? $.parseJSON(jsonText) : null;
        var newItem = null;
        if (dataItems != null) {
            for (var i = 0; i < dataItems.length; i++) {
                if (dataItems[i].UID == uid) {
                    newItem = dataItems[i];
                    newItem.MemberName = txtFirstName;
                    newItem.MemberSurname = txtLastName;
                    newItem.MemberPosition = txtPosition;
                    //test
                    newItem.PositionCode = PositionCode;
                    newItem.PositionName = PositionName;
                }
            }
        }

        if (newItem == null) {
            var no = (dataItems == null) ? 1 : (dataItems.length + 1);
            newItem = {
                UID: uid,
                OrganizationID: orgID,
                No: no,
                CommitteePosition: committeePosition,
                MemberName: txtFirstName,
                MemberSurname: txtLastName,
                MemberPosition: txtPosition,
                PositionCode: PositionCode,
                PositionName: PositionName,
            };
        }

        return newItem;
    }

    
    function disableCreateForm(flag, validateGroup) {
        clearCreateForm(validateGroup);
        clearValidatorCreateForm(validateGroup);
        //var btn = $("#" + _cbbCommitteePosition).find("button")
        if (flag && (validateGroup == "SaveCommittee")) {
            //btn[0].disabled = true;
            $("#" + _cbbCommitteePosition).attr("disabled", "disabled");
            $("#" + _txtCommitteeFirstNameID).attr("disabled", "disabled");
            $("#" + _txtCommitteeLastNameID).attr("disabled", "disabled");
            $("#" + _txtCommitteePositionID).attr("disabled", "disabled");
            $("#" + _btnSaveCommitteeID).attr("disabled", "disabled");
            $("#" + _btnCancelCommitteeID).attr("disabled", "disabled");
        } else if (flag && (validateGroup == "SaveOfficer")) {
            $("#" + _txtOfficerFirstNameID).attr("disabled", "disabled");
            $("#" + _txtOfficerLastNameID).attr("disabled", "disabled");
            $("#" + _txtOfficerPositionID).attr("disabled", "disabled");
            $("#" + _btnSaveOfficerID).attr("disabled", "disabled");
            $("#" + _btnCancelOfficerID).attr("disabled", "disabled");
        } else if ((validateGroup == "SaveCommittee")) {
            //btn[0].disabled = false
            $("#" + _cbbCommitteePosition).removeAttr("disabled");
            $("#" + _txtCommitteeFirstNameID).removeAttr("disabled");
            $("#" + _txtCommitteeLastNameID).removeAttr( "disabled");
            $("#" + _txtCommitteePositionID).removeAttr("disabled");
            $("#" + _btnSaveCommitteeID).removeAttr("disabled");
            $("#" + _btnCancelCommitteeID).removeAttr("disabled");
        }else {
            $("#" + _txtOfficerFirstNameID).removeAttr("disabled");
            $("#" + _txtOfficerLastNameID).removeAttr("disabled");
            $("#" + _txtOfficerPositionID).removeAttr("disabled");
            $("#" + _btnSaveOfficerID).removeAttr("disabled");
            $("#" + _btnCancelOfficerID).removeAttr("disabled");
        }
    }

    function guid() {
        function g() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }

        return (g() + g() + "-" + g() + "-" + g() + "-" + g() + "-" + g() + g() + g()).toUpperCase();
    }

    function bindCreateCommitteeRowEvent(gridID) {
        $('#'+ _txtCommitteeFirstNameID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.createRowCommittee(gridID);
            }
        });
        $('#' + _txtCommitteeLastNameID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.createRowCommittee(gridID);
            }
        });
        $('#' + _txtCommitteePositionID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.createRowCommittee(gridID);
            }
        });
    }

    function bindEditCommitteeRowEvent(gridID) {
        $('#' + _txtEditCommitteeFirstNameID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.updateCommittee(null, gridID);
            }
        });
        $('#' + _txtEditCommitteeLastNameID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.updateCommittee(null, gridID);
            }
        });
        $('#' + _txtEditCommitteePositionID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.updateCommittee(null, gridID);
            }
        });
    }

    function bindCreateOfficerRowEvent(gridID) {
        $('#' + _txtOfficerFirstNameID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.createRowCommittee(gridID);
            }
        });

        $('#' + _txtOfficerLastNameID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.createRowCommittee(gridID);
            }
        });

        $('#' + _txtOfficerPositionID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.createRowCommittee(gridID);
            }
        });
    }

    function bindEditOfficerRowEvent(gridID) {
        $('#' + _txtEditOfficerFirstNameID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.updateCommittee(null, gridID);
            }
        });
        $('#' + _txtEditOfficerLastNameID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.updateCommittee(null, gridID);
            }
        });
        $('#' + _txtEditOfficerPositionID).keypress(function (event) {
            if (event.keyCode == 13) {
                c2xCommittee.updateCommittee(null, gridID);
            }
        });
    }
    


    c2xCommittee = new C2XFunctions();
})();


