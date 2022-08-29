
var c2xProjectReport = null;
(function () {
    var C2XFunctions = function () {
    };

   
    var _countMale = 0;
    var _countFemale = 0;
    var _hiddenTotalMaleParticipantID = "";
    var _hiddenTotalFemaleParticipantID = "";   

    var _hiddenParticipantID = "";
    var _textBoxParticipantTargetGroupID = "";
    var _participantGridID = "";

    var _participantTargetGroupEtcBlock = "";
    var _textBoxParticipantTargetGroupEtcCreate = "";
    var _participantGenderCreate = "";
    var _participantIdCardCreate = "";
    var _participantFirstNameCreate = "";
    var _participantLastNameCreate = "";
    var _participantIsCrippleCreate = "";
    var _btnAddParticipant = "";

    var _projectTargetEtcEditBlock = "TargetOtherNameEditBlock";
    var _textBoxProjectTargetEditID = "TextBoxProjectTargetEdit";
    var _textBoxProjectTargetEtcEdit = "TargetOtherNameEdit";
    var _participantGenderEdit = "ParticipantGenderEdit";
    var _participantIdCardEdit = "ParticipantIdCardEdit";
    var _participantFirstNameEdit = "ParticipantFirstNameEdit";
    var _participantLastNameEdit = "ParticipantLastNameEdit";
    var _participantIsCrippleEdit = "IsCrippleEdit";
   
    var _requiredTargetGroupMsg = "";
    var _requiredTargetGroupEtcMsg = "";
    var _requiredTargetGroupDupMsg = "";
    var _requiredFirstNameMsg = "";
    var _requiredLastNameMsg = "";
    var _requiredIdCardMsg = "";
    var _idCardInvalidMsg = "";
    var _idCardDupMsg = "";


    var _targetGroupEtcValueID = "";
    var _targetGroupList = null;
    var _genderList = null;

    var _currentEditItem = null;
    var _columnTitle = {};
    var _isViewMode = false;
    var _projectID = null;

    var _targetEtcLsit = [];
    var _isCrippleList = [];

    var _isRequiredData = false;
  
    C2XFunctions.prototype.RequiredFirstNameMsg = _requiredFirstNameMsg;
    C2XFunctions.prototype.RequiredLastNameMsg = _requiredLastNameMsg;

    C2XFunctions.prototype.config = function (config) {
        _hiddenTotalMaleParticipantID = config.HiddenTotalMaleParticipantID;
        _hiddenTotalFemaleParticipantID = config.HiddenTotalFemaleParticipantID;

        _hiddenParticipantID = config.HiddenParticipantID;
        _textBoxParticipantTargetGroupID = config.TextBoxParticipantTargetGroupID;
        _participantGridID = config.ParticipantGridID;

        _participantTargetGroupEtcBlock = config.ParticipantTargetGroupEtcBlockID;
        _textBoxParticipantTargetGroupEtcCreate = config.TextBoxParticipantTargetGroupEtcCreate;
        _participantGenderCreate = config.ParticipantGenderCreate;
        _participantFirstNameCreate = config.ParticipantFirstNameCreate;
        _participantLastNameCreate = config.ParticipantLastNameCreate;
        _participantIdCardCreate = config.ParticipantIdCardCreate;
        _participantIsCrippleCreate = config.ParticipantIsCrippleCreate;

        _btnAddParticipant = config.BtnAddParticipant;

        _targetGroupEtcValueID = config.TargetGroupEtcValueID;
        _targetGroupList = config.TargetGroupList;
        _genderList = config.GenderList;

        _isCrippleList = config.IsCrippleList;

        _requiredTargetGroupMsg = config.RequiredTargetGroupMsg;
        _requiredTargetGroupEtcMsg = config.RequiredTargetGroupEtcMsg;       
        _requiredTargetGroupDupMsg = config.RequiredTargetGroupDupMsg;
        _requiredFirstNameMsg = config.RequiredFirstNameMsg;
        _requiredLastNameMsg = config.RequiredLastNameMsg;
        _requiredIdCardMsg = config.RequiredIdCardMsg;
        _idCardInvalidMsg = config.IdCardInvalidMsg;
        _idCardDupMsg = config.IdCardDupMsg;

        _columnTitle = config.ColumnTitle,
        _isViewMode = config.IsView,
        _projectID = config.ProjectID;

        _isRequiredData = config.IsRequiredData;

    };


    C2XFunctions.prototype.createDdlProjectTargetGroup = function () {
        $("#" + _textBoxParticipantTargetGroupID).kendoComboBox({
            dataTextField: "TargetDesc",
            dataValueField: "TargetDesc",
            filter: "contains",
            suggest: true,
            index: 1,
            change: c2xProjectReport.onDdlParticipantTargetGroupCreateChange
        });

        if (_targetGroupList != null) {
            var ddl = $("#" + _textBoxParticipantTargetGroupID).data('kendoComboBox');
            if (ddl != null) {
                ddl.dataSource.data(_targetGroupList);
                //ddl.dataSource.data(getActiveTargetGroupList());
            }
        }

       
    };

    C2XFunctions.prototype.createDdlGender = function () {
        $("#" + _participantGenderCreate).kendoDropDownList({
            dataSource: _genderList,
            dataTextField: "Text",
            dataValueField: "Value",
            index: 0,   
            
        });       
    };

    C2XFunctions.prototype.createDdlIsCripple = function () {
        $("#" + _participantIsCrippleCreate).kendoDropDownList({
            dataSource: _isCrippleList,
            dataTextField: "Text",
            dataValueField: "Value",
            index: 0,
            change: c2xProjectReport.onDdlIsCrippleCreateChange
        });
    };

    C2XFunctions.prototype.createGridParticipant = function () {

        var columns = [
                { field: 'No', title: _columnTitle.No, width: '50px', sortable: false, attributes: { class: "participant-no"  }},
                {

                    field: 'FirstName', title: _columnTitle.FirstName, width: '135px', template: "#=data.FirstName#",
                    editor: c2xProjectReport.createTextBoxFirstNameEditor,
                    
                },
                {
                    field: 'LastName', title: _columnTitle.LastName, width: '135px', template: "#=data.LastName#",
                    editor: c2xProjectReport.createTextBoxLastNameEditor
                },
                {
                    field: 'IDCardNo', title: _columnTitle.IDCardNo, width: '150px', template: "#=c2xProjectReport.citizenFormat(data.IDCardNo, '-')#",
                    editor: c2xProjectReport.createTextBoxIDCardNoEditor,
                    attributes: { class: "text-center" }
                },
                {
                    field: 'DdlGender', title: _columnTitle.Gender, width: '90px', template: "#=data.GenderName#",
                    editor: c2xProjectReport.createGenderEditor,
                    sortable: false,
                    attributes: { class: "text-center" }
                },

                {
                    field: 'DdlIsCripple', title: _columnTitle.IsCripple, width: '90px', template: "#=data.IsCrippleDesc#",
                    editor: c2xProjectReport.createIsCrippleEditor,
                    sortable: false,
                    attributes: { class: "text-center" }
                },

                {
                    field: 'DdlTargetGroup', title: _columnTitle.TargetGroupID, width: '250px', template: "#=((data.TargetGroupNameDesc != null) && (data.TargetGroupNameDesc != ''))? data.TargetGroupNameDesc : '-'#",
                    editor: c2xProjectReport.createTargetNameEditor
                }, 
                {
                    field: 'CheckData', title: "<img src='../Images/icon/about.png' title='ตรวจสอบกับฐานข้อมูลทะเบียนกลาง'></img>", width: '10px',
                    template: "#=c2xProjectReport.getCheckImage(data.CheckDESResult)#", // "<img src='../Images/icon/checked.gif'></img>",
                    //editor: c2xProjectReport.createLableStatus,
                    sortable: false,
                    attributes: { class: "text-center" }
                },
        ];
        if (!_isViewMode) {
            columns.push({
                command: [
                    { name: 'edit', text: { edit: '', cancel: '', update: '' } },
                    { name: 'destroy', text: '' }
                ], title: '&nbsp;', width: '65px'
            });
        }

        $('#' + _participantGridID).kendoGrid({
            autoBind: false,
            dataSource: {
                data: [],
                schema: {
                    model: {
                        id: "UID",
                        fields: {
                            UID: { editable: false, nullable: false },
                            No: { type: "number", editable: false, nullable: true },
                            ProjectParticipantID: { type: "number", editable: false, nullable: true },
                            FirstName: { validation: { required: true } },
                            LastName: { validation: { required: true } },
                            IDCardNo: { validation: { required: true } },
                            Gender: { validation: { required: true } },
                            GenderName: { validation: { required: true } },
                            DdlGender: { nullable: true, validation: { required: false } },
                            ProjectTargetGroupID: { type: "number", editable: false, nullable: true },
                            TargetGroupID: { type: "number", editable: false, nullable: true },
                            TargetGroupName: { validation: { required: true } },
                            TargetGroupNameDesc: { type: "string" },
                            TargetGroupCode: { type: "string" },
                            TargetGroupEtc: { type: "string" },
                            DdlTargetGroup: { nullable: true, validation: { required: false } },
                            IsCripple: { validation: { required: true } },
                            IsCrippleDesc: { validation: { required: true } },
                            DdlIsCripple: { nullable: true, validation: { required: false } },
                            CheckData: {editable:false},
                        }
                    }
                },
                pageSize: 20
            },
            pageable: {
                pageSize: 20,
                previousNext: false,
                numeric: true,
                messages: {
                    display: "รวม : {2} รายการ"
                }
                
            },
            columns: columns,
            sortable: true,
            editable: {
                mode: "inline",
                confirmation: false,
            },
            scrollable: false,
            edit: c2xProjectReport.editRowParticipantGroup,
            remove: c2xProjectReport.destroyParticipant,
            save: c2xProjectReport.updateParticipant,
            cancel: c2xProjectReport.cancelRowParticipant,
            dataBound: function (e) { c2xProjectReport.renderRowNumber(e); c2x.updateCommandText(e, _participantGridID); },
           
        });

        c2xProjectReport.bindDataProjectTargetGroup();
        
        //total participant
        var totalMale = $("#" + _hiddenTotalMaleParticipantID).val();
        var totalFemale = $("#" + _hiddenTotalFemaleParticipantID).val();
        _countMale = parseInt(totalMale, 10);
        _countFemale = parseInt(totalFemale, 10);

        //id card
        $("#" + _participantIdCardCreate).kendoMaskedTextBox({
            mask: "0-0000-00000-00-0",
            clearPromptChar: true
        });

        //txt create
        $("#" + _participantFirstNameCreate).keydown(function (e) { c2xProjectReport.onTextBoxKeyUp(e, true); });
        $("#" + _participantLastNameCreate).keydown(function (e) { c2xProjectReport.onTextBoxKeyUp(e, true); });
        $("#" + _participantIdCardCreate).keydown(function (e) { c2xProjectReport.onTextBoxKeyUp(e, true); });
        $("#" + _textBoxParticipantTargetGroupEtcCreate).keydown(function (e) { c2xProjectReport.onTextBoxKeyUp(e, true); });

       
        if (_isViewMode) {
            $(".k-grid-pager").addClass("k-grid-pager-width");
        }
    };
    C2XFunctions.prototype.getCheckImage = function (d) {
       // console.log('=====> ' + d);
       
        if (d == undefined) {
            return "";
        }
        if (d != "") {
            return "<img src='../Images/icon/cross_icon.gif' title='" + d + "'></img>";
          
        } else {
            return "<img src='../Images/icon/checked.gif'></img>";
        }
    };
    C2XFunctions.prototype.bindDataProjectTargetGroup = function () {
       
        var hidd = $('#' + _hiddenParticipantID);
        if (hidd.length > 0) {
            var tgValue = $(hidd).val();
            var jsonTgs = (tgValue != "") ? ($.parseJSON(tgValue)) : null;

            if ((jsonTgs != null) && (jsonTgs.length > 0)) {
                $("#" + _participantGridID).show();

                var grid = $("#" + _participantGridID).data("kendoGrid");
                grid.dataSource.data(jsonTgs);
            } else {
                $("#" + _participantGridID).hide();
            }
        }

    };

    C2XFunctions.prototype.getProjectTargetGroup = function () {
        var hidd = $('#' + _hiddenParticipantID);
        var data = [];
        if (hidd.length > 0) {
            var tgValue = $(hidd).val();
            data = (tgValue != "") ? ($.parseJSON(tgValue)) : [];

            if ((data != null) && (data.length > 0)) {
                $("#" + _participantGridID).show();
            } else {
                $("#" + _participantGridID).hide();
            }
        }

        return data;

    };

    C2XFunctions.prototype.createRowParticipant = function (e) {

       
        var uid = guid();
        var validateConfig = {
            UID: uid,
            ProjectParticipantID: null,
            DdlTargetID: _textBoxParticipantTargetGroupID,
            TxtEtcID: _textBoxParticipantTargetGroupEtcCreate,
            TxtFirstName: _participantFirstNameCreate,
            TxtLastName: _participantLastNameCreate,
            TxtIdCard: _participantIdCardCreate,
            DdlGender: _participantGenderCreate,
            DdlIsCripple: _participantIsCrippleCreate,
            ValidatorTargetDupID: 'ValidateParticipantTargetGroupEtcDupCreate'
        };
              

        var isValid = validateParticipant(validateConfig);

        if (isValid) {
            var grid = $('#' + _participantGridID).data("kendoGrid");
            if ((grid != 'undefined') && (grid != null)) {
                newItem = getProjectTargetModel(true);
                if (newItem.Gender == "M") {
                    _countMale++;
                } else {
                    _countFemale++;
                }

                showTotalPaticipant();

                grid.dataSource.add(newItem);
                grid.saveChanges();
                var dataItems = grid.dataSource.data();
                var jsonText = kendo.stringify(dataItems);
                $("#" + _hiddenParticipantID).val(jsonText);
                $('#' + _participantGridID).show();
            }

            
            rebindDdlTargetGroup();
            $("#" + _participantFirstNameCreate).focus();
            c2xProjectReport.cancelCreateRowParticipant();
        }        
        
      
        return false;
    };
    C2XFunctions.prototype.createRowParticipantAuto = function (obj) {


        var uid = guid();
        var validateConfig = {
            UID: uid,
            ProjectParticipantID: null,
            DdlTargetID: _textBoxParticipantTargetGroupID,
            TxtEtcID: _textBoxParticipantTargetGroupEtcCreate,
            TxtFirstName: _participantFirstNameCreate,
            TxtLastName: _participantLastNameCreate,
            TxtIdCard: _participantIdCardCreate,
            DdlGender: _participantGenderCreate,
            DdlIsCripple: _participantIsCrippleCreate,
            ValidatorTargetDupID: 'ValidateParticipantTargetGroupEtcDupCreate'
        };


        var isValid = validateParticipantAuto(validateConfig);
        if (isValid != "") {
            return isValid;
        }
        //if (isValid) {
            var grid = $('#' + _participantGridID).data("kendoGrid");
            if ((grid != 'undefined') && (grid != null)) {
                getProjectTargetModel(true);
                newItem = getProjectTargetModel(true);
              
                if (newItem.Gender == "M") {
                    _countMale++;
                } else {
                    _countFemale++;
                }

                showTotalPaticipant();

                grid.dataSource.add(newItem);
                grid.saveChanges();
                var dataItems = grid.dataSource.data();
                var jsonText = kendo.stringify(dataItems);
                $("#" + _hiddenParticipantID).val(jsonText);
                $('#' + _participantGridID).show();
            }


            rebindDdlTargetGroup();
            $("#" + _participantFirstNameCreate).focus();
            c2xProjectReport.cancelCreateRowParticipant();
        //}


        return "";
    };
    function getProjectTargetModelAuto(o) {

        var ddlID = _textBoxParticipantTargetGroupID;
        var txtEtcID = _textBoxParticipantTargetGroupEtcCreate;
        var txtFirstName = _participantFirstNameCreate;
        var txtLastName = _participantLastNameCreate;
        var txtIdCard = _participantIdCardCreate;
        var ddlGender = _participantGenderCreate;
        var ddlIsCripple = _participantIsCrippleCreate;

        //var no = getParticipantNo(isCreate);
        var uid = guid();
        var participantID = null;


        var targetDdl = $("#" + ddlID).data("kendoComboBox");
        var targetSelectedIndex = targetDdl.select();
        var targetID = 0;
        var targetName = "";
        var targetOtherName = "";
        var targetDesc = "";
        var projectTargetID = 0;
        var tmpProjectTargetID = "";
        if (targetSelectedIndex >= 0) {
            var selectedItem = targetDdl.dataItem(targetSelectedIndex);


            projectTargetID = selectedItem.ProjectTargetID;
            targetID = selectedItem.TargetID;
            targetName = selectedItem.TargetName;
            targetDesc = targetName;

            if ((targetID == _targetGroupEtcValueID) && ((projectTargetID == null) || (projectTargetID == 0))) {
                targetOtherName = $("#" + txtEtcID).val();
                targetOtherName = $.trim(targetOtherName);
                targetDesc = targetOtherName;
            } else if (targetID == _targetGroupEtcValueID) {
                targetOtherName = selectedItem.TargetEtc;
                targetDesc = selectedItem.TargetEtc;
            }
        }

        var genderKey = '', genderText = '';
        var genderDdl = $("#" + ddlGender).data("kendoDropDownList");
      
        var genderIndex = genderDdl.select(function (s) {
            return s.Text === o.เพศ;
        });
       
        if (genderDdl.selectedIndex >= 0) {
            var selectedItem = genderDdl.dataItem(genderDdl.selectedIndex);
            genderKey = selectedItem.Value;
            genderText = selectedItem.Text;
        } else {
            return { error: "ข้อมูล 'เพศ' ไม่ถูกต้อง", item: null };
        }

        var isCrippleKey = '', isCrippleText = '';
        var isCrippleDdl = $("#" + ddlIsCripple).data("kendoDropDownList");
        var isCrippleIndex = isCrippleDdl.select();
        if (isCrippleIndex >= 0) {
            var selectedItem = isCrippleDdl.dataItem(isCrippleIndex);
            isCrippleKey = selectedItem.Value;
            isCrippleText = selectedItem.Text;
        }

        //var firstName = $('#' + txtFirstName).val();
        //firstName = $.trim(firstName);

        //var lastName = $('#' + txtLastName).val();
        //lastName = $.trim(lastName);

        var cardNo = "";
        if (o.เลขประจำตัวประชาชน) { cardNo = o.เลขประจำตัวประชาชน; }

        cardNo = cardNo.replace(/[-_]/g, '');

        var newItem = {
            UID: uid,
            No: 0,
            ProjectParticipantID: participantID,
            TempProjectTargetGroupID: tmpProjectTargetID,
            ProjectTargetGroupID: projectTargetID,
            TargetGroupID: targetID,
            TargetGroupName: o.targetName,
            TargetGroupNameDesc: targetDesc,
            TargetGroupEtc: targetOtherName,
            TargetGroupCode: null,
            DdlTargetGroup: { ProjectTargetID: projectTargetID, TargetID: targetID, TargetName: targetName, TargetEtc: targetOtherName, TargetDesc: targetDesc },
            DdlGender: { Value: genderKey, Text: genderText },
            Gender: genderKey,
            GenderName: genderText,
            IsCripple: isCrippleKey,
            IsCrippleDesc: isCrippleText,
            DdlIsCripple: { Value: isCrippleKey, Text: isCrippleText },
            FirstName: o.ชื่อ,
            LastName: o.นามสกุล,
            IDCardNo: cardNo,
            CheckDESResult: null,
        };

        return { error: "", item: newItem };
    }

    C2XFunctions.prototype.updateParticipant = function (e) {
       
        var uid;
        var participantID;
        if (typeof (e) != 'undefined') {
            uid = e.model.UID;
            participantID = e.model.ProjectParticipantID;
        } else {
            uid = _currentEditItem.UID;
            participantID = _currentEditItem.ProjectParticipantID;
        }

        var validateConfig = {
            UID: uid,
            ProjectParticipantID: participantID,
            DdlTargetID: _textBoxProjectTargetEditID,
            TxtEtcID: _textBoxProjectTargetEtcEdit,
            TxtFirstName: _participantFirstNameEdit,
            TxtLastName: _participantLastNameEdit,
            TxtIdCard: _participantIdCardEdit,
            DdlGender: _participantGenderEdit,
            DdlIsCripple: _participantIsCrippleEdit,
            ValidatorTargetDupID: 'ValidateProjectTargetGroupDupEdit'
        };

        var isValid = validateParticipant(validateConfig);
        var grid = $('#' + _participantGridID).data("kendoGrid");
        if (isValid) {
            var newItem = getProjectTargetModel(false);
            
            if ((_currentEditItem.Gender != newItem.Gender) && (newItem.Gender == "M")) {                
                _countMale++;
                _countFemale--;
            } else if (_currentEditItem.Gender != newItem.Gender) {
                _countFemale++;
                _countMale--;
            }

            showTotalPaticipant();

            var projectTargetGroupText = $('#' + _hiddenParticipantID).val();
            var item;
            if (projectTargetGroupText.length > 0) {
                var data = $.parseJSON(projectTargetGroupText);
                for (var i = 0; i < data.length; i++) {
                    item = data[i];
                    if (((uid != null) && (item.UID == uid)) || ((participantID != null) && participantID == item.ProjectParticipantID)) {
                        data[i] = newItem;
                    }
                }

                var newValue = kendo.stringify(data);
                $('#' + _hiddenParticipantID).val(newValue);
            }

            rebindDdlTargetGroup();


            grid.dataSource.data(data);
            grid.saveChanges();

            disableCreateForm(false);
        } else if (typeof (e) != 'undefined') {
            grid.one("dataBinding", function (args) {
                args.preventDefault();//cancel grid rebind if error occurs  
            });

        }

    };

    C2XFunctions.prototype.destroyParticipant = function (e) {
        var uid = e.model.UID;
        var id = e.model.ProjectParticipantID;
        var projectTargetGroupText = $('#' + _hiddenParticipantID).val();
        if (e.model.Gender == "M") {
            _countMale--;
        } else {
            _countFemale--;
        }
        showTotalPaticipant();
        if (projectTargetGroupText.length > 0) {
            var data = $.parseJSON(projectTargetGroupText);
            data = $.grep(data, function (item) {
                return ((uid != null) && (item.UID !== uid)) || (item.ProjectParticipantID != id);
            });

            var newValue = kendo.stringify(data);
            $('#' + _hiddenParticipantID).val(newValue);

            if (data.length == 0) {
                $("#" + _participantGridID).hide();
            }

        }

    };

    C2XFunctions.prototype.cancelCreateRowParticipant = function () {
        clearCreateForm();
        clearValidatorCreateForm();
        return false;

    };

    C2XFunctions.prototype.editRowParticipantGroup = function (e) {
        c2x.updateCommandText(e);
        disableCreateForm(true);

        _currentEditItem = c2x.clone(e.model);

        var projectTargetID = _currentEditItem.ProjectTargetGroupID;
        var targetID = _currentEditItem.TargetGroupID;
        //var targetList = getActiveTargetGroupList(projectTargetID + "_" + targetID, _currentEditItem.TargetGroupNameDesc);

        $('#' + _textBoxProjectTargetEditID).kendoComboBox({
            dataTextField: "TargetDesc",
            dataValueField: "TargetDesc",
            filter: "contains",
            suggest: true,
            index: 1,
            change: c2xProjectReport.onDdlProjectTargetEditChange,
            dataSource: {
                data: _targetGroupList
            }
        });


        $('#' + _participantGenderEdit).kendoDropDownList({
            dataTextField: "Text",
            dataValueField: "Value",
            index: 0,
            dataSource: {
                data: _genderList
            }
        });


        $('#' + _participantIsCrippleEdit).kendoDropDownList({
            dataTextField: "Text",
            dataValueField: "Value",
            index: 0,
            change: c2xProjectReport.onDdlIsCrippleEditChange,
            dataSource: {
                data: _isCrippleList
            }
        });


        $("#" + _participantIdCardEdit).kendoMaskedTextBox({
            mask: "0-0000-00000-00-0",
            clearPromptChar: true
        });

        $("#" + _participantFirstNameEdit).keydown(function (e) { c2xProjectReport.onTextBoxKeyUp(e, false); });
        $("#" + _participantLastNameEdit).keydown(function (e) { c2xProjectReport.onTextBoxKeyUp(e, false); });
        $("#" + _participantIdCardEdit).keydown(function (e) { c2xProjectReport.onTextBoxKeyUp(e, false); });
        $("#" + _textBoxProjectTargetEtcEdit).keydown(function (e) { c2xProjectReport.onTextBoxKeyUp(e, false); });
        
    };

    C2XFunctions.prototype.cancelRowParticipant = function (e) {
        var grid = $("#" + _participantGridID).data("kendoGrid");
        _currentEditItem = null;
        //grid.cancelRow();
        disableCreateForm(false);
        c2xProjectReport.bindDataProjectTargetGroup();
    };
    C2XFunctions.prototype.onDdlIsCrippleCreateChange = function (e) {
         var value = this.value();
         var ddl = $("#" + _textBoxParticipantTargetGroupID).data('kendoComboBox');
         if (value > "1") {
             ddl.wrapper.hide();
             ddl.select(-1);
         }else
         {
             ddl.wrapper.show();
         }
    }
    C2XFunctions.prototype.onDdlParticipantTargetGroupCreateChange = function (e) {
        var value = this.value();
        
        clearValidatorCreateForm();
        $('#' + _participantTargetGroupEtcBlock).hide();
        if ((value != 'undefined') && (value != "")) {
            var seletedIndex = this.selectedIndex;
            if (seletedIndex >= 0) {
                var item = this.dataItem(seletedIndex);
                var projectTargetID = item.ProjectTargetID;
                var targetID = item.TargetID

                if (targetID == _targetGroupEtcValueID) {
                    $("#" + _textBoxParticipantTargetGroupEtcCreate).val(item.TargetEtc);

                    if ((item.TargetEtc == null) || (item.TargetEtc == "")) {
                        $('#' + _participantTargetGroupEtcBlock).show();
                        $("#" + _textBoxParticipantTargetGroupEtcCreate).focus();
                    }
                }
            }            
        }
    };
    C2XFunctions.prototype.onDdlIsCrippleEditChange = function (e) {
        var value = this.value();
        var ddl = $("#" + _textBoxProjectTargetEditID).data('kendoComboBox');
        if (value > "1") {
            ddl.wrapper.hide();
            ddl.select(-1);
        } else {
            ddl.wrapper.show();
        }
    }
    C2XFunctions.prototype.onDdlProjectTargetEditChange = function (e) {
        var value = this.value();
        clearValidatorCreateForm();
        clearValidatorEditForm();

        $('#' + _projectTargetEtcEditBlock).hide();

        var grid = $("#" + _participantGridID).data("kendoGrid");
        if ((value != 'undefined') && (value != "") && (grid != null)) {
            var seletedIndex = this.selectedIndex;
            if (seletedIndex >= 0) {
                var item = this.dataItem(seletedIndex);
                var projectTargetID = item.ProjectTargetID;
                var targetID = item.TargetID;

                if ((targetID == _targetGroupEtcValueID) && ((projectTargetID == null) || (projectTargetID == 0))) {
                    $('#' + _projectTargetEtcEditBlock).show();
                    $("#" + _textBoxProjectTargetEtcEdit).val(item.TargetEtc);
                    $("#" + _textBoxProjectTargetEtcEdit).focus();
                }

            }
           
        }
    };

    C2XFunctions.prototype.createTargetNameEditor = function (container, options) {
        
        var ddlTarget = options.model.DdlTargetGroup;
        var projectTargetID = ddlTarget.ProjectTargetID;
        var targetID = ddlTarget.TargetID;        

        var descStyle = "none";
        var textField = "TargetName";

        if ((targetID == _targetGroupEtcValueID) && ((projectTargetID == null) || (projectTargetID == 0))) {
            descStyle = "block";
            textField = "TargetName";
        } else if (targetID == _targetGroupEtcValueID) {
            textField = "TargetEtc";
        }

        //console.log('TargetID = ' + targetID + ", ProjectTargetID = " + projectTargetID + ", Text field = "+ textField);
        //console.log(options.model.DdlTargetGroup);

        var input = '<input class="ddl-participant-target-group" data-text-field="' + textField + '" id="' + _textBoxProjectTargetEditID + '" data-value-field="' + textField  + '" data-bind="value:' + options.field + '"  style="width:100%" />';
        var validator = '<span class="participant-validate error-text"' +
                        ' id="ValidateParticipantTargetGroupEdit" runat="server" data-val-validationgroup="SaveParticipant" data-val-controltovalidate="' + _textBoxProjectTargetEtcEdit + '" style="display:none;">' +
                        _requiredTargetGroupMsg + '</span>';
        var etcInput = '<div id="' + _projectTargetEtcEditBlock + '" style="display:' + descStyle + '; margin-top:7px;"><input id="' +
                        _textBoxProjectTargetEtcEdit + '" class="form-control project-participant-group-etc" data-bind="value:TargetGroupEtc" maxlength="1333"  /></div>';
        var validatorDup = '<span class="participant-validate error-text"' +
                        ' id="ValidateProjectTargetGroupDupEdit" runat="server" data-val-validationgroup="SaveParticipant" data-val-controltovalidate="' + _textBoxProjectTargetEtcEdit + '" style="display:none;">' +
                         _requiredTargetGroupDupMsg + '</span>';

       
        $(input).appendTo(container);
        $(validator).appendTo(container);
        $(etcInput).appendTo(container);
        $(validatorDup).appendTo(container);      
        
    }

    C2XFunctions.prototype.onTextBoxKeyUp = function (e, isCreate) {
        var theEvent = e || window.event;        
        if (theEvent.keyCode === 13) {
           
            if (isCreate) {
                c2xProjectReport.createRowParticipant();
            }else{
                c2xProjectReport.updateParticipant();
            }
            theEvent.returnValue = false;
        }
    };

    C2XFunctions.prototype.createGenderEditor = function (container, options) {       

        var html = '<input class="ddl-participant-target-group" data-text-field="Text" id="ParticipantGenderEdit" data-value-field="Value" data-bind="value:' + options.field + '" style="width:100%" />';

        $(html).appendTo(container);
    }

    C2XFunctions.prototype.createIsCrippleEditor = function (container, options) {
        var html = '<input class="ddl-participant-iscripple" data-text-field="Text" id="IsCrippleEdit" data-value-field="Value" data-bind="value:' + options.field + '" style="width:100%" />';

        $(html).appendTo(container);
    }

    

    C2XFunctions.prototype.createTextBoxFirstNameEditor = function (container, options) {
       
        var input = '<input id="ParticipantFirstNameEdit"  class="form-control" data-bind="value:' + options.field + '" />';
        var validator = '<span class="participant-validate error-text" id="ValidateParticipantFirstNameEdit" data-val-validationgroup="SaveParticipant" data-val-controltovalidate="ParticipantFirstNameEdit" style="display:none;">'+ c2xProjectReport.RequiredFirstNameMsg+'</span>';
        
        $(input).appendTo(container);
        $(validator).appendTo(container);
        
    }

    C2XFunctions.prototype.createLableStatus = function (container, options) {

        //var input = '<input id="ParticipantFirstNameEdit"  class="form-control" data-bind="value:' + options.field + '" />';
       // var validator = '<span class="participant-validate error-text" id="ValidateParticipantFirstNameEdit" data-val-validationgroup="SaveParticipant" data-val-controltovalidate="ParticipantFirstNameEdit" style="display:none;">' + c2xProjectReport.RequiredFirstNameMsg + '</span>';
        var img = '<img id="IMGStatus" src="../Images/icon/checked.gif" ></img>'
        $(img).appendTo(container);
       // $(validator).appendTo(container);

    }
    C2XFunctions.prototype.createTextBoxLastNameEditor = function (container, options) {

        var input = '<input id="ParticipantLastNameEdit"  class="form-control" data-bind="value:' + options.field + '"  />';
        var validator = '<span class="participant-validate error-text" id="ValidateParticipantLastNameEdit" data-val-validationgroup="SaveParticipant" data-val-controltovalidate="ParticipantLastNameEdit" style="display:none;">' + c2xProjectReport.RequiredLastNameMsg + '</span>';

        $(input).appendTo(container);
        $(validator).appendTo(container);

    }


    C2XFunctions.prototype.createTextBoxIDCardNoEditor = function (container, options) {
        
        var input = '<input id="ParticipantIdCardEdit"  class="form-control" data-bind="value:' + options.field + '" />';
        var validator = '<span class="participant-validate error-text" id="ValidateParticipantIdCardEdit" data-val-validationgroup="SaveParticipant" data-val-controltovalidate="ParticipantIdCardEdit" style="display:none;"></span>';

        $(input).appendTo(container);
        $(validator).appendTo(container);        
    }

    C2XFunctions.prototype.citizenFormat = function (text, nullValue) {
        var format = ((typeof(nullValue) != 'undefined') && (nullValue != null))? nullValue : "";
        if ((text != null) && (text != "")) {
            text = text + "";

            text = text.substring(0, 1) + "-" + text.substring(1, 5) + "-" + text.substring(5, 10) + "-" + text.substring(10, 12) + "-" + text.substring(12, 13);

            format = text;
        }
        
        return format;
    }

    C2XFunctions.prototype.renderRowNumber = function (e) {
        
        var pager = e.sender.pager;
        var pageIndex = (pager.page() - 1);
        var pageSize = pager.pageSize();
        var rowNumber = (pageIndex * pageSize);

        $(".participant-no").each(function(index, item){
            $(this).text(++rowNumber);
        });       
    }

    function validateParticipant(config) {

        var targetDdl = $("#" + config.DdlTargetID).data("kendoComboBox");
        var targetSelectedIndex = targetDdl.select();
        var isValid = true;
        var validator;
        var controltovalidate;
        var temp = [];


        // check participant name
        var firstName = $("#" + config.TxtFirstName).val();
        var lastName = $("#" + config.TxtLastName).val();

        firstName = $.trim(firstName);
        lastName = $.trim(lastName);

        if (firstName == "") {

            temp = (config.TxtFirstName).split("_");
            controltovalidate = temp[temp.length - 1];
            validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
            $(validator).show();
            if (isValid) {
                $("#" + config.TxtFirstName).focus();
            }

            isValid = false;
        }

        if (lastName == "") {

            temp = (config.TxtLastName).split("_");
            controltovalidate = temp[temp.length - 1];
            validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
            $(validator).show();

            if (isValid) {
                $("#" + config.TxtLastName).focus();
            }
            isValid = false;
        }

        // check id card no
        var ckIdCardDup = true;
        var idCardNo = $("#" + config.TxtIdCard).val();
        idCardNo = idCardNo.replace(/[-_]+/g, "");
        if ((idCardNo == "") && (_isRequiredData)) {

            temp = (config.TxtIdCard).split("_");
            controltovalidate = temp[temp.length - 1];
            validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
            $(validator).text(_requiredIdCardMsg);
            $(validator).show();
            if (isValid) {
                $("#" + config.TxtIdCard).focus();
            }
            isValid = false;

        } else if (idCardNo != "") {
            // check format
            var isIdCardValid = c2x.ValidateCitizenId(idCardNo);
            if (!isIdCardValid) {

                temp = (config.TxtIdCard).split("_");
                controltovalidate = temp[temp.length - 1];
                validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
                $(validator).text(_idCardInvalidMsg);
                $(validator).show();

                if (isValid) {
                    $("#" + config.TxtIdCard).focus();
                }

                isValid = false;

            } else if (isIdCardValid) {
                // check dup               
                ckIdCardDup = checkIdCardNoDup(config.UID, config.ProjectParticipantID, idCardNo);
                if (!ckIdCardDup) {
                    temp = (config.TxtIdCard).split("_");
                    controltovalidate = temp[temp.length - 1];
                    validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
                    $(validator).text(_idCardDupMsg);
                    $(validator).show();

                    if (isValid) {
                        $("#" + config.TxtIdCard).focus();
                    }
                }
            }
        }


        // check target group
        if (targetSelectedIndex >= 0) {
            var selectedItem = targetDdl.dataItem(targetSelectedIndex);
            var targetSelectedID = selectedItem.TargetID;
            var projectTargetID = selectedItem.ProjectTargetID;

            if ((targetSelectedID == _targetGroupEtcValueID) && ((projectTargetID == null) || (projectTargetID == 0))) {
                var etcValue = $("#" + config.TxtEtcID).val();
                etcValue = $.trim(etcValue);
                if (etcValue == "") {

                    temp = (config.TxtEtcID).split("_");
                    controltovalidate = temp[temp.length - 1];
                    validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
                    $(validator).show();

                    if (isValid) {
                        $(config.TxtEtcID).focus();
                    }

                    isValid = false;

                } else {
                    var isEtcDup = checkProjectTargetGroupEtcDup(selectedItem, etcValue);

                    if (!isEtcDup) {
                        temp = (config.ValidatorTargetDupID).split("_");
                        controltovalidate = temp[temp.length - 1];
                        validator = $("span[id='" + config.ValidatorTargetDupID + "']").get(0);
                        $(validator).show();


                        if (isValid) {
                            $(config.TxtEtcID).focus();
                        }
                    }
                    isValid = isEtcDup;
                }
            }
        } else {
            if (!targetDdl.wrapper.is(":visible")) {
                isValid = true;
            } else {
                temp = (config.DdlTargetID).split("_");
                controltovalidate = temp[temp.length - 1];
                validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
                $(validator).show();

                if (isValid) {
                    targetDdl.focus();
                }

                isValid = false;
            }

        }


        return isValid && ckIdCardDup;
    }
    
    function validateParticipantAuto(config) {

            var targetDdl = $("#" +config.DdlTargetID).data("kendoComboBox");
            var targetSelectedIndex = targetDdl.select();
          var isValid = true;
            var validator;
            var controltovalidate;
          var temp =[];


            // check participant name
          var firstName = $("#" +config.TxtFirstName).val();
          var lastName = $("#" +config.TxtLastName).val();

          firstName = $.trim(firstName);
          lastName = $.trim(lastName);

          if(firstName == "") {

              //temp = (config.TxtFirstName).split("_");
              //controltovalidate = temp[temp.length - 1];
              // validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
              // $(validator).show();
              //if (isValid) {
                    //    $("#" + config.TxtFirstName).focus();
                        //}           
                return "ไม่พบข้อมูลชื่อ";
                    isValid = false;
                    }

                if (lastName == "") {

                        //temp = (config.TxtLastName).split("_");
                            //controltovalidate = temp[temp.length - 1];
        //validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        //$(validator).show();

        //if (isValid) {
        //    $("#" + config.TxtLastName).focus();
            //}
        return "ไม่พบข้อมูลนามสกุล";
        isValid = false;
            }

            // check id card no
            var ckIdCardDup = true;
            var idCardNo = $("#" + config.TxtIdCard).val();
            idCardNo = idCardNo.replace(/[-_]+/g, "");
            if ((idCardNo.trim() == "")) {

                //temp = (config.TxtIdCard).split("_");
            //controltovalidate = temp[temp.length - 1];
                //validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);           
                    //$(validator).text(_requiredIdCardMsg);
                        //$(validator).show();
                //if (isValid) {
                        //    $("#" + config.TxtIdCard).focus();
                        //}
                        return "ไม่พบข้อมูลบัตรประชาชน";
                isValid = false;

            } else if (idCardNo != "") {
                // check format
                var isIdCardValid = c2x.ValidateCitizenId(idCardNo.trim());
                if (!isIdCardValid) {

                   //temp = (config.TxtIdCard).split("_");
                    //controltovalidate = temp[temp.length - 1];
                   //validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
                       //$(validator).text(_idCardInvalidMsg);
                       //$(validator).show();

                    //if(isValid){
                       //    $("#" + config.TxtIdCard).focus();
                       //}
                   return "ข้อมูลบัตรประชาชน ไม่ถูกต้อง";
                    isValid = false;

                } else if (isIdCardValid) {
                   // check dup               
                  ckIdCardDup = checkIdCardNoDup(config.UID, config.ProjectParticipantID, idCardNo);
                    if (!ckIdCardDup) {
                      //temp = (config.TxtIdCard).split("_");
                      //controltovalidate = temp[temp.length - 1];
                      //validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
                          //$(validator).text(_idCardDupMsg);
                          //$(validator).show();

                          //if (isValid) {
                              //    $("#" + config.TxtIdCard).focus();
                              //}          
                              return "บัตรประชาชนซ้ำ";
                              }
                              }
                              }


        // check target group
        if (targetSelectedIndex >= 0) {
            var selectedItem = targetDdl.dataItem(targetSelectedIndex);
            var targetSelectedID = selectedItem.TargetID;
            var projectTargetID = selectedItem.ProjectTargetID;

            if ((targetSelectedID == _targetGroupEtcValueID) && ((projectTargetID == null) || (projectTargetID == 0))) {
                var etcValue = $("#" + config.TxtEtcID).val();
                etcValue = $.trim(etcValue);
                if (etcValue == "") {

                    temp = (config.TxtEtcID).split("_");
                    controltovalidate = temp[temp.length - 1];
                    validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
                    $(validator).show();

                    if (isValid) {
                        $(config.TxtEtcID).focus();
                    }

                    isValid = false;

                } else {
                    var isEtcDup = checkProjectTargetGroupEtcDup(selectedItem, etcValue);

                    if (!isEtcDup) {
                        temp = (config.ValidatorTargetDupID).split("_");
                        controltovalidate = temp[temp.length - 1];
                        validator = $("span[id='" + config.ValidatorTargetDupID + "']").get(0);
                        $(validator).show();


                        if (isValid) {
                            $(config.TxtEtcID).focus();
                        }
                    }
                    isValid = isEtcDup;
                }
            }
        } else {
            if (!targetDdl.wrapper.is(":visible")) {
                return "";
                isValid = true;
            } else {
                return "ไมพบข้อมูลกลุ่มเป้าหมาย";
                isValid = false;
            }

        }


        return "";
    }

    function checkProjectTargetGroupEtcDup(selectedItem, targetEtc) {
        var isValid = true;
        var isFound = false;
     
        var targetName = selectedItem.TargetName;

        var targetID = selectedItem.TargetID;
        var keyID = selectedItem.TargetDesc;
        if (targetID == _targetGroupEtcValueID) {
            var item; 
            for (var i = 0; i < _targetGroupList.length; i++) {
                item = _targetGroupList[i];
                if ((targetEtc == item.TargetEtc) && (keyID != item.TargetDesc)) {
                    isValid = false;
                    break;
                } else if (targetEtc == item.TargetEtc) {
                    isFound = true;
                }
            }

            if ((!isFound) && isValid) {
                var insertIndex = _targetGroupList.length;
                _targetGroupList.splice(insertIndex - 1, 0, {                   
                    ProjectTargetID: selectedItem.ProjectTargetID,
                    TargetID: targetID,
                    TargetName: targetName,
                    TargetEtc: targetEtc,
                    TargetDesc: targetEtc
                });              
            }
        }

        return isValid;
    }

    function checkIdCardNoDup(uid, projectParticipantID, idCard) {
        var isValid = true;
        var text = $('#' + _hiddenParticipantID).val();
        if (text.length > 0) {
            var data = $.parseJSON(text);
            var item;
            for (var i = 0; i < data.length; i++) {
                item = data[i];
                if ((item.IDCardNo == idCard) && (((uid != null) && (item.UID != uid)) || (projectParticipantID != item.ProjectParticipantID))) {
                    isValid = false;
                    break;
                }
            }
        }

        return isValid;
    }

    function rebindDdlTargetGroup() {
        var targetDdl = $("#" + _textBoxParticipantTargetGroupID).data("kendoComboBox");        
        targetDdl.dataSource.data(_targetGroupList);
    }

    function clearValidatorCreateForm() {

        var temp, controltovalidate, validator;

        //clear required TextBoxProjectTarget
        temp = (_textBoxParticipantTargetGroupID).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear duplicate TextBoxProjectTarget        
        validator = $("span[id='ValidateParticipantTargetGroupEtcDupCreate']").get(0);
        $(validator).hide();

        //clear required TextBoxProjectTargetEtc
        temp = (_textBoxParticipantTargetGroupEtcCreate).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear required TextBoxParticipantFirstName
        temp = (_participantFirstNameCreate).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear required TextBoxParticipantLastName
        temp = (_participantLastNameCreate).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear required TextBoxParticipantIDCardNo
        temp = (_participantIdCardCreate).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();
    }

    function clearValidatorEditForm() {

        var temp, controltovalidate, validator;

        //clear required TextBoxProjectTarget       
        validator = $("span[id='ValidateParticipantTargetGroupEdit']").get(0);
        $(validator).hide();

        //clear duplicate TextBoxProjectTarget        
        validator = $("span[id='ValidateProjectTargetGroupDupEdit']").get(0);
        $(validator).hide();

        //clear required TextBoxProjectTargetEtc
        temp = (_textBoxProjectTargetEtcEdit).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear required TextBoxParticipantFirstName
        temp = (_participantFirstNameEdit).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear required TextBoxParticipantLastName
        temp = (_participantLastNameEdit).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear required TextBoxParticipantIDCardNo
        temp = (_participantIdCardEdit).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();
    }

    function clearCreateForm() {
        var targetDdl = $("#" + _textBoxParticipantTargetGroupID).data("kendoComboBox");
        targetDdl.value("");

        var genderDdl = $("#" + _participantGenderCreate).data("kendoDropDownList");
        genderDdl.select(0);

        $("#" + _participantFirstNameCreate).val("");
        $("#" + _participantLastNameCreate).val("");
        $("#" + _participantIdCardCreate).val("");
        $("#" + _textBoxParticipantTargetGroupEtcCreate).val("");
       
        $('#' + _participantTargetGroupEtcBlock).hide();
    }

    //function getActiveTargetGroupList(selectedID, selectedText) {
    //    var list = [];
    //    var tgSelectedIDList = [];
    //    var item;
    //    var targetID;

    //    var selectedId = (typeof (selectedID) != 'undefined') ? selectedID : 0;

    //    if (selectedId > 0) {
    //        targetID = selectedId.split("_")[1];
    //        if (targetID == _targetGroupEtcValueID) {
    //            arrIndex = $.inArray(selectedText, _targetEtcLsit);

    //            if (arrIndex < 0) {
    //                var keyID = guid() + "_" + _targetGroupEtcValueID;
    //                item = { KeyID: keyID, TargetDesc: selectedText };
    //                //_targetGroupList.push(item);
    //                _targetEtcLsit.push(selectedText);
    //            }
    //        }
    //    } else {

    //    }

    //    return _targetGroupList;
    //}

    function getParticipantNo(isCreate) {
        var no;
        if (isCreate) {
            var text = $('#' + _hiddenParticipantID).val();
            var participants = (text != "") ? ($.parseJSON(text)) : null;
            if (participants != null) {
                no = (participants.length + 1);
            }
        } else {
            no = _currentEditItem.No;
        }        

        return no;
    }

    function getProjectTargetModel(isCreate) {
        
        var ddlID = _textBoxParticipantTargetGroupID;
        var txtEtcID = _textBoxParticipantTargetGroupEtcCreate;
        var txtFirstName = _participantFirstNameCreate;
        var txtLastName = _participantLastNameCreate;
        var txtIdCard = _participantIdCardCreate;
        var ddlGender = _participantGenderCreate;
        var ddlIsCripple = _participantIsCrippleCreate;

        //var no = getParticipantNo(isCreate);
        var uid = guid();
        var participantID = null;
        if (!isCreate) {
            ddlID = _textBoxProjectTargetEditID;
            txtEtcID = _textBoxProjectTargetEtcEdit;
            txtFirstName = _participantFirstNameEdit;
            txtLastName = _participantLastNameEdit;
            txtIdCard = _participantIdCardEdit;
            ddlGender = _participantGenderEdit;
            ddlIsCripple = _participantIsCrippleEdit;
            uid = _currentEditItem.UID;
            participantID = _currentEditItem.ProjectParticipantID;
        } 

        var targetDdl = $("#" + ddlID).data("kendoComboBox");
        var targetSelectedIndex = targetDdl.select();
        var targetID = 0;
        var targetName = "";
        var targetOtherName = "";
        var targetDesc = "";      
        var projectTargetID = 0;
        var tmpProjectTargetID = "";
        if (targetSelectedIndex >= 0) {
            var selectedItem = targetDdl.dataItem(targetSelectedIndex);          

           
            projectTargetID = selectedItem.ProjectTargetID;
            targetID = selectedItem.TargetID;
            targetName = selectedItem.TargetName;
            targetDesc = targetName;

            if ((targetID == _targetGroupEtcValueID) && ((projectTargetID == null) || (projectTargetID == 0))) {
                targetOtherName = $("#" + txtEtcID).val();
                targetOtherName = $.trim(targetOtherName);
                targetDesc = targetOtherName;
            } else if (targetID == _targetGroupEtcValueID) {
                targetOtherName = selectedItem.TargetEtc;
                targetDesc = selectedItem.TargetEtc;
            }
        }

        var genderKey = '', genderText = '';
        var genderDdl = $("#" + ddlGender).data("kendoDropDownList");
        var genderIndex = genderDdl.select();
        if (genderIndex >= 0) {
            var selectedItem = genderDdl.dataItem(genderIndex);
            genderKey = selectedItem.Value;
            genderText = selectedItem.Text;
        }

        var isCrippleKey = '', isCrippleText = '';
        var isCrippleDdl = $("#" + ddlIsCripple).data("kendoDropDownList");
        var isCrippleIndex = isCrippleDdl.select();
        if (isCrippleIndex >= 0) {
            var selectedItem = isCrippleDdl.dataItem(isCrippleIndex);
            isCrippleKey = selectedItem.Value;
            isCrippleText = selectedItem.Text;
        }

        var firstName = $('#' + txtFirstName).val();
        firstName = $.trim(firstName);

        var lastName = $('#' + txtLastName).val();
        lastName = $.trim(lastName);

        var cardNo = $('#' + txtIdCard).val();
        cardNo = cardNo.replace(/[-_]/g, '');       

        var newItem = {
            UID: uid,
            No:0,           
            ProjectParticipantID: participantID,
            TempProjectTargetGroupID: tmpProjectTargetID,
            ProjectTargetGroupID:projectTargetID,
            TargetGroupID: targetID,
            TargetGroupName: targetName,
            TargetGroupNameDesc: targetDesc,
            TargetGroupEtc: targetOtherName,
            TargetGroupCode:null,
            DdlTargetGroup: { ProjectTargetID: projectTargetID, TargetID: targetID, TargetName: targetName, TargetEtc: targetOtherName, TargetDesc: targetDesc },
            DdlGender: { Value: genderKey, Text: genderText },
            Gender: genderKey,
            GenderName: genderText,
            IsCripple: isCrippleKey,
            IsCrippleDesc: isCrippleText,
            DdlIsCripple:{Value: isCrippleKey, Text: isCrippleText},
            FirstName: firstName,
            LastName: lastName,
            IDCardNo: cardNo,
            CheckDESResult: null,
        };

        return newItem;
    }

    

    function disableCreateForm(flag) {
        clearCreateForm();
        clearValidatorCreateForm();
        var targetDdl = $("#" + _textBoxParticipantTargetGroupID).data("kendoComboBox");
        targetDdl.enable(!flag);

        var genderDdl = $("#" + _participantGenderCreate).data("kendoDropDownList");
        genderDdl.enable(!flag);

        var isCrippleDdl = $("#" + _participantIsCrippleCreate).data("kendoDropDownList");
        isCrippleDdl.enable(!flag);
                
        if (flag) {
            $("#" + _participantFirstNameCreate).attr("disabled", "disabled");
            $("#" + _participantLastNameCreate).attr("disabled", "disabled");
            $("#" + _participantIdCardCreate).attr("disabled", "disabled");
            $("#" + _btnAddParticipant).attr("disabled", "disabled");
        } else {
            $("#" + _participantFirstNameCreate).removeAttr("disabled");
            $("#" + _participantLastNameCreate).removeAttr("disabled");
            $("#" + _participantIdCardCreate).removeAttr("disabled");
            $("#" + _btnAddParticipant).removeAttr("disabled");            
        }

    }

    function showTotalPaticipant() {
        _countMale = (_countMale > 0) ? _countMale : 0;
        _countFemale = (_countFemale > 0) ? _countFemale : 0;

        $("#" + _hiddenTotalMaleParticipantID).val(_countMale);
        $("#" + _hiddenTotalFemaleParticipantID).val(_countFemale);

        $(".total-male-participant").text(kendo.toString(_countMale, "n0"));
        $(".total-female-participant").text(kendo.toString(_countFemale, "n0"));
        $(".total-total-participant").text(kendo.toString((_countMale + _countFemale), "n0"));
    }

    function guid() {
        function g() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }

        return (g() + g() + "-" + g() + "-" + g() + "-" + g() + "-" + g() + g() + g()).toUpperCase();
    }


    c2xProjectReport = new C2XFunctions();
})();


