
var c2xProjectInfo = null;
(function () {
    var C2XFunctions = function () {
    };

    var _hiddenProjectTargetGroupID = "";
    var _textBoxProjectTargetID = "";
    var _productTargetGroupGridID = "";

    var _projectTargetEtcCreateBlock = "";
    var _textBoxProjectTargetEtcCreate = "";
    var _textBoxProjectTargetAmountCreate = "";
    var _btnAddProjectTarget = "";

    var _projectTargetEtcEditBlock = "TargetOtherNameEditBlock";
    var _textBoxProjectTargetEditID = "TextBoxProjectTargetEdit";
    var _textBoxProjectTargetEtcEdit = "TargetOtherNameEdit";   
    var _textBoxProjectTargetAmountEdit = "TextBoxProjectTargetAmountEdit";

    var _requiredTargetGroupMsg = "";
    var _requiredTargetGroupEtcMsg = "";
    var _requiredTargetGroupAmountMsg = "";
    var _requiredTargetGroupDupMsg = "";

    var _targetGroupEtcValueID = "";
    var _targetGroupList = null;

    var _currentEditItem = null;
    var _columnTitle = {};
    var _isViewMode = false;
    var _projectID = null;
   

    C2XFunctions.prototype.targetGroupConfig = function (config) {
        _hiddenProjectTargetGroupID = config.HiddenFieldProjectTargetGroupID;
        _textBoxProjectTargetID = config.TextBoxProjectTargetID;
        _productTargetGroupGridID = config.ProductTargetGroupGridID;

        _projectTargetEtcCreateBlock = config.ProjectTargetEtcCreateBlockID;
        _textBoxProjectTargetEtcCreate = config.TextBoxProjectTargetEtcCreateID;        
        _textBoxProjectTargetAmountCreate = config.TextBoxProjectTargetAmountCreateID;
        _btnAddProjectTarget = config.BtnAddProjectTarget;

        _targetGroupEtcValueID = config.TargetGroupEtcValueID;
        _targetGroupList = config.TargetGroupList;

        _requiredTargetGroupMsg = config.RequiredTargetGroupMsg;
        _requiredTargetGroupEtcMsg = config.RequiredTargetGroupEtcMsg;
        _requiredTargetGroupAmountMsg = config.RequiredTargetGroupAmountMsg;
        _requiredTargetGroupDupMsg = config.RequiredTargetGroupDupMsg;

        _columnTitle = config.ColumnTitle,
        _isViewMode = config.IsView,
        _projectID = config.ProjectID;


        //txt create
        $("#" + _textBoxProjectTargetEtcCreate).keydown(function (e) { c2xProjectInfo.onTextBoxKeyUp(e, true); });
        $("#" + _textBoxProjectTargetAmountCreate).keydown(function (e) { c2xProjectInfo.onAmountNumberTextBoxKeyUp(e, true); });
    };
            

    C2XFunctions.prototype.createDdlProjectTargetGroup = function () {
        $("#" + _textBoxProjectTargetID).kendoComboBox({
            dataTextField: "LovName",
            dataValueField: "LovID",            
            filter: "contains",
            suggest: true,
            index: 1,
            change: c2xProjectInfo.onDdlProjectTargetCreateChange
        });

        if (_targetGroupList != null) {
            var ddl = $("#" + _textBoxProjectTargetID).data('kendoComboBox');
            if (ddl != null) {
                ddl.dataSource.data(getActiveTargetGroupList());
            }            
        }        
    };

    C2XFunctions.prototype.createGridProjectTargetGroup = function () {
        
        var columns = [
                { field: 'Target', title: _columnTitle.Target, width: '400px', editor: c2xProjectInfo.createTargetNameEditor, template: "#=data.TargetDesc#" },                
                { field: 'Amount', title: _columnTitle.Amount, width: '120px', format: "{0:n0}", editor: function (container, options) { c2xProjectInfo.createTextBoxEditor(container, options, _textBoxProjectTargetAmountEdit) }, attributes: { class: "text-right" } },
                
        ];
        if (!_isViewMode) {
            columns.push({
                        command: [
                            { name: 'edit', text: { edit: '', cancel: '', update: '' } },
                            { name: 'destroy', text: '' }
                        ], title: '&nbsp;', width: '65px'
                    });
        }

        $('#' + _productTargetGroupGridID).kendoGrid({
            autoBind: false,
            dataSource:{               
                data:[],
                schema: {
                    model: {
                        id: "UID",
                        fields: {
                            UID:{editable: false, nullable: false },
                            ProjectTargetID: { type: "number", editable: false, nullable: true },
                            ProjectID:{type:"number"},
                            TargetID: { type: "number", nullable:false },
                            TargetName: { nullable: true },
                            TargetOtherName: { nullable: true },
                            TargetDesc: { validation: { required: true} },
                            Target: { nullable: true, validation: { required: false} },
                            Amount: { type: "number", validation: { min: 1, max: 99999999, } }                           
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
            edit: c2xProjectInfo.editRowProjectTargetGroup,
            remove: c2xProjectInfo.destroyProjectTargetGroup,
            save:c2xProjectInfo.updateProjectTargetGroup,
            cancel: c2xProjectInfo.cancelRowProjectTargetGroup,
            dataBound: function (e) { c2xProjectInfo.showTotalTargetGroup(); c2x.updateCommandText(e, _productTargetGroupGridID); }
        });

        c2xProjectInfo.bindDataProjectTargetGroup();
        //createFirstRow();
        
    };

    C2XFunctions.prototype.bindDataProjectTargetGroup = function () {
        var hidd = $('#' + _hiddenProjectTargetGroupID);
        if (hidd.length > 0) {
            var tgValue = $(hidd).val();          
            var jsonTgs = (tgValue != "") ? ($.parseJSON(tgValue)) : null;


            if ((jsonTgs != null) && (jsonTgs.length > 0)) {
                $("#" + _productTargetGroupGridID).show();

                var grid = $("#" + _productTargetGroupGridID).data("kendoGrid");                
                grid.dataSource.data(jsonTgs);                
            } else {
                $("#" + _productTargetGroupGridID).hide();
            }
        }
             
    };

    C2XFunctions.prototype.getProjectTargetGroup = function(){
        var hidd = $('#' + _hiddenProjectTargetGroupID);        
        var data = [];
        if (hidd.length > 0) {
            var tgValue = $(hidd).val();            
            data = (tgValue != "") ? ($.parseJSON(tgValue)) : [];
           
            if ((data != null) && (data.length > 0)) {
                $("#" + _productTargetGroupGridID).show();
            } else {
                $("#" + _productTargetGroupGridID).hide();
            }
        }

        return data;
             
    };

    C2XFunctions.prototype.createRowProjectTargetGroup = function (e) {
        
        
        var uid = guid();
        var validateConfig = {
            UID: uid, 
            DdlTargetID: _textBoxProjectTargetID, 
            TxtEtcID: _textBoxProjectTargetEtcCreate,           
            TxtAmountID: _textBoxProjectTargetAmountCreate,
            ValidatorTargetDupID: 'ValidateProjectTargetGroupDupCreate'
        };

        var isValid = validateProjectTargetGroup(validateConfig);

        if (isValid) {
            newItem = getProjectTargetModel(true);

            var hiddText = $("#" + _hiddenProjectTargetGroupID).val();
            var currentData = (hiddText != "") ? $.parseJSON(hiddText) : [];
            currentData.push(newItem);
            var jsonText = kendo.stringify(currentData);
            $("#" + _hiddenProjectTargetGroupID).val(jsonText);

            var grid = $('#' + _productTargetGroupGridID).data("kendoGrid");
            if ((grid != 'undefined') && (grid != null)) {
                
                grid.dataSource.add(newItem);
                grid.saveChanges();               
                $('#' + _productTargetGroupGridID).show();                
            }          

            c2xProjectInfo.cancelCreateRowProjectTargetGroup();
            rebindDdlTargetGroup();           
        }

        $("#" + _textBoxProjectTargetID).data("kendoComboBox").focus();
        
        return false;
    };

    C2XFunctions.prototype.updateProjectTargetGroup = function (e) {
        var uid = (typeof(e) != 'undefined')? e.model.UID : _currentEditItem.UID;
        var validateConfig = {
            UID: uid,
            DdlTargetID: _textBoxProjectTargetEditID,
            TxtEtcID: _textBoxProjectTargetEtcEdit,          
            TxtAmountID: _textBoxProjectTargetAmountEdit,
            ValidatorTargetDupID: 'ValidateProjectTargetGroupDupEdit'
        };

        var isValid = validateProjectTargetGroup(validateConfig);
        var grid = $('#' + _productTargetGroupGridID).data("kendoGrid");
        if (isValid) {
            var newItem = getProjectTargetModel(false);
            var projectTargetGroupText = $('#' + _hiddenProjectTargetGroupID).val();
            var item;
            if (projectTargetGroupText.length > 0) {
                var data = $.parseJSON(projectTargetGroupText);
                for (var i = 0; i < data.length; i++) {
                    item = data[i];
                    if (item.UID == uid) {
                        data[i] = newItem;
                    }
                }

                var newValue = kendo.stringify(data);
                $('#' + _hiddenProjectTargetGroupID).val(newValue);
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

    C2XFunctions.prototype.destroyProjectTargetGroup = function (e) {
        var uid = e.model.UID;
        var projectTargetGroupText = $('#' + _hiddenProjectTargetGroupID).val();
        if (projectTargetGroupText.length > 0) {
            var data = $.parseJSON(projectTargetGroupText);
            data = $.grep(data, function (item) {
                return item.UID !== uid;
            });

            var newValue = kendo.stringify(data);
            $('#' + _hiddenProjectTargetGroupID).val(newValue);

            if (data.length == 0) {
                $("#" + _productTargetGroupGridID).hide();
            }
           
        }

    };

    C2XFunctions.prototype.cancelCreateRowProjectTargetGroup = function () {
        clearCreateForm();
        clearValidatorCreateForm();
        return false;

    };

    C2XFunctions.prototype.editRowProjectTargetGroup = function (e) {
        c2x.updateCommandText(e);
        disableCreateForm(true);

        _currentEditItem = c2x.clone(e.model);

        var targetID = _currentEditItem.TargetID;
        var targetList = getActiveTargetGroupList(targetID);

        $('#' + _textBoxProjectTargetEditID).kendoComboBox({
            dataTextField: "LovName",
            dataValueField: "LovID",
            filter: "contains",
            suggest: true,
            index: 1,
            change: c2xProjectInfo.onDdlProjectTargetEditChange,
            dataSource: {
                data: targetList
            }
        });        
    };

    C2XFunctions.prototype.cancelRowProjectTargetGroup = function (e) {
        var grid = $("#" + _productTargetGroupGridID).data("kendoGrid");
        _currentEditItem = null;
        grid.cancelRow();
        disableCreateForm(false);
        c2xProjectInfo.bindDataProjectTargetGroup();      
    };

    C2XFunctions.prototype.onDdlProjectTargetCreateChange = function (e) {
        var value = this.value();
        clearValidatorCreateForm();
        $('#' + _projectTargetEtcCreateBlock).hide();
        if ((value != 'undefined') && (value != "")) {
            value = parseInt(value, 10);
            if (value == _targetGroupEtcValueID) {
                $('#' + _projectTargetEtcCreateBlock).show();
                $("#" + _textBoxProjectTargetEtcCreate).focus();
            } 
        }        
    };

    C2XFunctions.prototype.onDdlProjectTargetEditChange = function (e) {
        var value = this.value();
        clearValidatorCreateForm();
        clearValidatorEditForm();

        $('#' + _projectTargetEtcEditBlock).hide();

        var grid = $("#" + _productTargetGroupGridID).data("kendoGrid");
        if ((value != 'undefined') && (value != "") && (grid != null)) {
            value = parseInt(value, 10);
            if (value == _targetGroupEtcValueID) {
                $('#' + _projectTargetEtcEditBlock).show();
                $("#" + _textBoxProjectTargetEtcEdit).focus();
            }          
        }
    };

    C2XFunctions.prototype.createTargetNameEditor = function (container, options) {
        var targetID = options.model.TargetID;
        var targetList = getActiveTargetGroupList(targetID);
        var descStyle = (targetID == _targetGroupEtcValueID) ? "block" : "none";

        var html = '<input class="ddl-project-target-group" data-text-field="LovName" id="' + _textBoxProjectTargetEditID + '" data-value-field="LovID" data-bind="value:' + options.field + '" />' +
                   '<span class="project-target-group-validate error-text"' + 
                        ' id="ValidateProjectTargetGroupEdit" runat="server" data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="' + _textBoxProjectTargetEditID + '" style="display:none;">' +
                        _requiredTargetGroupMsg + '</span>' +
                   '<div id="' + _projectTargetEtcEditBlock + '" style="display:' + descStyle + '; margin-top:7px;"><input id="' +
                        _textBoxProjectTargetEtcEdit + '" class="form-control project-target-group-etc" data-bind="value:TargetOtherName" onkeypress="c2xProjectInfo.onTextBoxKeyUp(event, false)" maxlength="1333"/></div>' +
                   '<span class="project-target-group-validate error-text"' +
                        ' id="ValidateProjectTargetGroupDupEdit" runat="server" data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="' + _textBoxProjectTargetEtcEdit + '" style="display:none;">' +
                         _requiredTargetGroupDupMsg + '</span>';

        $(html).appendTo(container);

       
    }

    C2XFunctions.prototype.createTextBoxEditor = function (container, options, controlId) {
        
        var html = '<input id="' + controlId + '"  class="form-control" numberformat="N0"  Min="1" Max="99999999"  data-bind="value:' + options.field + '"'+
            ' onkeypress="c2x.onNumberTextBoxKeyPress(event)" onkeyup="c2xProjectInfo.onAmountNumberTextBoxKeyUp(event, false)"' +
            ' onfocus="c2x.onNumberTextBoxFocus(this)" onblur="c2x.onNumberTextBoxBlur(this)" onchange="c2xProjectInfo.onProjectInfoTargetAmountChange(event, false)"/>' +
                       '<span class="project-target-group-validate error-text"' +
                             ' id="ValidateProjectTargetAmountEdit" runat="server" data-val-validationgroup="SaveProjectTargetGroup" data-val-controltovalidate="' + _textBoxProjectTargetAmountEdit + '" style="display:none;">' +
                             _requiredTargetGroupAmountMsg + '</span>';
            $(html).appendTo(container);

    }    

    C2XFunctions.prototype.onProjectInfoTargetAmountChange = function (e, isCreateFlag) {
        var elem;
        if (e.srcElement) elem = e.srcElement;
        else if (e.target) elem = e.target;
       
        var totalAmountID = (isCreateFlag) ? _textBoxProjectTargetAmountCreate : _textBoxProjectTargetAmountEdit;
      
        var totalAmountText = $('#' + totalAmountID).val();

        totalAmountText = totalAmountText.replace(/,/g, '');
        var amount = parseInt(((totalAmountText == '') ? 0 : totalAmountText), 10);
        amount = ((amount == 0) && (totalAmountText != '')) ? 1 : amount;

        amount = amount.format('N0');

        $('#' + totalAmountID).val(amount);

        //clear required TextBoxProjectTargetAmout
        var temp = (totalAmountID).split("_");
        var controltovalidate = temp[temp.length - 1];
        var validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();
    }

    C2XFunctions.prototype.onAmountNumberTextBoxKeyUp = function (e, isCreate) {       
        var theEvent = e || window.event;
        
        if (theEvent.keyCode === 13) {
            if (isCreate) {                
                c2xProjectInfo.createRowProjectTargetGroup();
            } else {
                c2xProjectInfo.updateProjectTargetGroup();
            }
            theEvent.returnValue = false;
        } else {
            c2x.onNumberTextBoxKeyUp(e);
        }
    };

    C2XFunctions.prototype.onTextBoxKeyUp = function (e, isCreate) {
        var theEvent = e || window.event;

       
        if (theEvent.keyCode === 13) {
            if (isCreate) {
                c2xProjectInfo.createRowProjectTargetGroup();
            } else {
                c2xProjectInfo.updateProjectTargetGroup();
            }
            theEvent.returnValue = false;
        } 
    };

    C2XFunctions.prototype.showTotalTargetGroup = function() {
        var totalAmount = 0, amount = 0;
        var text = $('#' + _hiddenProjectTargetGroupID).val();
        var objs = (text != "") ? ($.parseJSON(text)) : null;
        if (objs != null) {
            for (var i = 0; i < objs.length; i++) {
                amount = objs[i].Amount;
                totalAmount = totalAmount + amount;
            }
        }

        $(".total-targetgroup").text(kendo.toString(totalAmount, "n0"));
    }

    //C2XFunctions.prototype.onProjectTargetGroupBound = function (e) {
    //    c2x.updateCommandText(e, _productTargetGroupGridID); 

    //    var cancelBtn = $("#" + _productTargetGroupGridID + "  tr:eq(1)").find(".k-grid-cancel");
    //    if(cancelBtn.length > 0){
    //        cancelBtn.hide();
    //    }   
    //};

    function validateProjectTargetGroup(config) {        
        var targetDdl = $("#" + config.DdlTargetID).data("kendoComboBox");
        var targetSelectedIndex = targetDdl.select();
        var isValid = true;
        var validator;
        var controltovalidate;
        var temp = [];
        // check target group
        if (targetSelectedIndex >= 0) {
            var selectedItem = targetDdl.dataItem(targetSelectedIndex);
            var targetSelectedID = selectedItem.LovID;

            if (targetSelectedID == _targetGroupEtcValueID) {                
                var etcValue = $("#" + config.TxtEtcID).val();
                etcValue = $.trim(etcValue);
                if (etcValue == "") {
                    isValid = false;
                   
                    temp = (config.TxtEtcID).split("_");
                    controltovalidate = temp[temp.length - 1];
                    validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
                    $(validator).show();
                } else {
                    var isEtcDup = checkProjectTargetGroupEtcDup(config.UID, etcValue);
                    isValid = isEtcDup;

                    if (!isEtcDup) {
                        temp = (config.ValidatorTargetDupID).split("_");
                        controltovalidate = temp[temp.length - 1];
                        validator = $("span[id='" + config.ValidatorTargetDupID + "']").get(0);
                        $(validator).show();
                    }
                }
            }
        } else {
            isValid = false;
            temp = (config.DdlTargetID).split("_");
            controltovalidate = temp[temp.length - 1];
            validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
            $(validator).show();
        }

        // check target group amont
        var amount = $("#" + config.TxtAmountID).val();    

        if (amount.length == 0) {
            isValid = false;
            temp = (config.TxtAmountID).split("_");
            controltovalidate = temp[temp.length - 1];
            validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
            $(validator).show();
        }



        return isValid;
    }

    function checkProjectTargetGroupEtcDup(uid, targetEtc) {
        var isValid = true;
        var text = $('#' + _hiddenProjectTargetGroupID).val();
        if (text.length > 0) {
            var data = $.parseJSON(text);
            var item;
            for (var i = 0; i < data.length; i++) {
                item = data[i];
                if ((item.UID != uid) && (item.TargetOtherName == targetEtc)) {
                    isValid = false;
                    break;
                }
            }
        }


        return isValid;
    }

    function rebindDdlTargetGroup(){
        var targetDdl = $("#" + _textBoxProjectTargetID).data("kendoComboBox");
        var list = getActiveTargetGroupList();
        targetDdl.dataSource.data(list);
    }

    function clearValidatorCreateForm() {

        var temp, controltovalidate, validator;

        //clear required TextBoxProjectTarget
        temp = (_textBoxProjectTargetID).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear duplicate TextBoxProjectTarget        
        validator = $("span[id='ValidateProjectTargetGroupDupCreate']").get(0);
        $(validator).hide();

        //clear required TextBoxProjectTargetEtc
        temp = (_textBoxProjectTargetEtcCreate).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear required TextBoxProjectTargetAmout
        temp = (_textBoxProjectTargetAmountCreate).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();
    }

    function clearValidatorEditForm() {

        var temp, controltovalidate, validator;

        //clear required TextBoxProjectTarget       
        validator = $("span[id='ValidateProjectTargetGroupEdit']").get(0);
        $(validator).hide();

        //clear duplicate TextBoxProjectTarget        
        validator = $("span[id='ValidateProjectTargetGroupDupEdit']").get(0);
        $(validator).hide();

        //clear required TextBoxProjectTargetEtc
        temp = (_textBoxProjectTargetEtcEdit).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();

        //clear required TextBoxProjectTargetAmout
        temp = (_textBoxProjectTargetAmountEdit).split("_");
        controltovalidate = temp[temp.length - 1];
        validator = $("span[data-val-controltovalidate='" + controltovalidate + "']").get(0);
        $(validator).hide();
    }

    function clearCreateForm() {        
        var targetDdl = $("#" + _textBoxProjectTargetID).data("kendoComboBox");
        targetDdl.value("");
        $("#" + _textBoxProjectTargetEtcCreate).val("");        
        $("#" + _textBoxProjectTargetAmountCreate).val("");
        $('#' + _projectTargetEtcCreateBlock).hide();
    }
   
    function getActiveTargetGroupList(selectedID) {
        var list = [];
        var tgSelectedIDList = [];
        var item;
        
        var selectedId = (typeof (selectedID) != 'undefined') ? selectedID : 0;
        
        var projectTargetGroupText = $('#' + _hiddenProjectTargetGroupID).val();
        if (projectTargetGroupText.length > 0) {
            var data = $.parseJSON(projectTargetGroupText);
            var item;
            for (var i = 0; i < data.length; i++) {
                item = data[i];
                tgSelectedIDList.push(item.TargetID);
            }
        }

        var arrIndex = -1;
        for (var i = 0; i < _targetGroupList.length; i++) {
            item = _targetGroupList[i];            
            if (item.IsActive || (item.LovID == selectedId)) {
                
                if (item.LovID == selectedId) {
                    list.push(item);
                } else if (item.LovID == _targetGroupEtcValueID) {
                    list.push(item);
                } else if (tgSelectedIDList.length > 0) {
                    arrIndex = $.inArray(item.LovID, tgSelectedIDList);
                    if (arrIndex < 0) {
                        list.push(item);
                    }
                } else {
                    list.push(item);
                }
            }
        }

        return list;
    }

    function getProjectTargetModel(isCreate) {
        var ddlID = _textBoxProjectTargetID;
        var txtEtcID = _textBoxProjectTargetEtcCreate;       
        var txtAmountID = _textBoxProjectTargetAmountCreate;
        var uid = guid();
        if (!isCreate) {
            ddlID = _textBoxProjectTargetEditID;
            txtEtcID = _textBoxProjectTargetEtcEdit;           
            txtAmountID = _textBoxProjectTargetAmountEdit;
            uid = _currentEditItem.UID;
        }

        var targetDdl = $("#" + ddlID).data("kendoComboBox");
        var targetSelectedIndex = targetDdl.select();
        var targetID = 0;
        var targetName = "";
        var targetOtherName = "";
        var targetDesc = "";
        if (targetSelectedIndex >= 0) {
            var selectedItem = targetDdl.dataItem(targetSelectedIndex);
            targetID = selectedItem.LovID;
            targetName = selectedItem.LovName;
            targetDesc = targetName;


            if (targetID == _targetGroupEtcValueID) {
                targetOtherName = $("#" + txtEtcID).val();
                targetOtherName = $.trim(targetOtherName);
                targetDesc = targetOtherName;
            }


        }

        var amount = $("#" + txtAmountID).val();
        amount = amount.replace(/[^\d^\.]/g, '');
        amount = (amount != "") ? parseInt(amount, 0) : 0;        

        var newItem = {
            UID: uid,
            ProjectID: _projectID,
            ProjectTargetID: 0,
            TargetID: targetID,
            TargetName: targetName,
            TargetOtherName: targetOtherName,
            TargetDesc: targetDesc,
            Target:{LovID: targetID, LovName:targetName, LovCode:null, OrderNo:0, IsActive: true},
            Amount: amount           
        };

        return newItem;
    }

    function createFirstRow() {
        var grid = $("#" + _productTargetGroupGridID).data("kendoGrid");
        if (grid != null) {
            
            var newItem = {
                UID: null,
                ProjectTargetID: 0,
                TargetID: null,
                TargetName: "",
                TargetOtherName: "",
                TargetDesc: "",
                Target: null,
                Amount: null,              
            };
            var dataSource = grid.dataSource;
            dataSource.insert(0, newItem);
            grid.grid.editRow($("#" + _productTargetGroupGridID + " tr:eq(1)"));
        }
        
    }

    function disableCreateForm(flag) {
        clearCreateForm();
        clearValidatorCreateForm();
        var ddl = $("#" + _textBoxProjectTargetID).data("kendoComboBox");
        ddl.enable(!flag);

        if(flag){
            $("#" + _textBoxProjectTargetAmountCreate).attr("disabled", "disabled");
            $("#" + _btnAddProjectTarget).attr("disabled", "disabled");
        } else {
            $("#" + _textBoxProjectTargetAmountCreate).removeAttr("disabled");
            $("#" + _btnAddProjectTarget).removeAttr("disabled");
        }   
        
    }

    function guid() {
        function g() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }

        return (g() + g() + "-" + g() + "-" + g() + "-" + g() + "-" + g() + g() + g()).toUpperCase();
    }

   


    c2xProjectInfo = new C2XFunctions();
})();


