var c2xOprAddress = null;
(function () {
    var C2XFunctions = function () {
    };

    var _txtAddressID = "";
    var _txtMooID = "";
    var _txtBuildingID = "";
    var _txtSoiID = "";
    var _txtRoadID = "";
    var _ddlSubdistrictID = "";
    var _ddlDistrictID = "";
    var _ddlProvinceID = "";

    var _txtAttachmentID = "FileOperationAddress";
    var _attachmentControlID = "FileOperationAddress";
    var _attachmentAddedHiddenControlClientID = "HiddLocationAddedFiles";
    var _attachmentRemovedHiddenControlClientID = "HiddLocationRemoved";
    var _attachmentViewAttachmentPrefix = "";

    var _ddlPleaseSelect = "";
    var _urlGetProvince = "";
    var _urlGetDistrict = "";
    var _urlGetSubDistrict = "";
    var _currentEditing = null;
    var _projectID;

    var _reqMsg = "";
   
      
    C2XFunctions.prototype.config = function (config) {       
        _attachmentViewAttachmentPrefix = config.AttachmentViewAttachmentPrefix;        
        _txtAddressID = config.TxtAddressID;
        _txtMooID = config.TxtMooID;
        _txtBuildingID = config.TxtBuildingID;
        _txtSoiID = config.TxtSoiID;
        _txtRoadID = config.TxtRoadID;
        _ddlSubdistrictID = config.DdlSubdistrictID;
        _ddlDistrictID = config.DdlDistrictID;
        _ddlProvinceID = config.DdlProvinceID;

        _urlGetProvince = config.UrlGetProvince;
        _urlGetDistrict = config.UrlGetDistrict;
        _urlGetSubDistrict = config.UrlGetSubDistrict;

        _projectID = config.ProjectID;
        _currentEditing = window.parent.getCurrentEditItem();

        _ddlPleaseSelect = config.DdlPleaseSelect;

        _reqMsg = config.RequredFieldMsg;

        
        creatDropdownList(config.ProvinceData, config.DistrictData);
        bindDataItem(_currentEditing);
    };

    C2XFunctions.prototype.save = function () {

        var isValid = Page_ClientValidate("SaveAddress");

        if (isValid) {
            var model = getData();
            updateOperationAddress(model);
        }
       
        return false;
    };

    C2XFunctions.prototype.createValidator = function(){
        var container = $("#OperationAddressForm");
        $("#OperationAddressForm").kendoValidator({
            messages: {              
                // overrides the built-in message for the required rule
                required: function(input){c2xOprAddress.getRequiredMessage(input);}               
            }
        });
    };

    C2XFunctions.prototype.getRequiredMessage = function (input) {
        var id = $(input).attr("id");
        var label = $('data-label=["' + id + '"]').text();
        var message = _reqMsg.replace("{0}", label);
        return message;
    }

    function getData() {
        
        var uid;
        var gUid = null;
        var Runno;
        var operationAddressID;
        var currentAttachment;
        var currentRemovedAttachment;

        if ((typeof (_currentEditing) != "undefined") && (_currentEditing != null)) {
            Runno = _currentEditing.Runno;
            gUid = _currentEditing.uid;
            uid = _currentEditing.UID;
            operationAddressID = _currentEditing.OperationAddressID;
            var tmp = _currentEditing.LocationMapAttachment;
            if (tmp != null) {
                currentAttachment = { id: tmp.id, tempId: tmp.tempId, name: tmp.name, extension: tmp.extension, size: tmp.size };
            }

            var removedTmp = _currentEditing.RemovedLocationMapAttachment;
            if (removedTmp != null) {
                currentRemovedAttachment = { id: removedTmp.id, tempId: removedTmp.tempId, name: removedTmp.name, extension: removedTmp.extension, size: removedTmp.size };
            }

        } else {
            uid = guid();
            operationAddressID = null;
            currentAttachment = null;
            currentRemovedAttachment = null;
            Runno = null;
        }

        var txtAddress = $("#" + _txtAddressID).val();
        txtAddress = $.trim(txtAddress);
       
        var txtBuiding = $("#" + _txtBuildingID).val();
        txtBuiding = $.trim(txtBuiding);

        var txtMoo = $("#" + _txtMooID).val();
        txtMoo = $.trim(txtMoo);
      
        var txtSoi = $("#" + _txtSoiID).val();
        txtSoi = $.trim(txtSoi);

        var txtRoad = $("#" + _txtRoadID).val();
        txtRoad = $.trim(txtRoad);
              
        var ddl = $("#" + _ddlProvinceID).data("kendoComboBox");
        var provItem = ddl.dataItem();
       
        ddl = $("#" + _ddlDistrictID).data("kendoComboBox");
        var itemDist = ddl.dataItem();
               
        ddl = $("#" + _ddlSubdistrictID).data("kendoComboBox");
        var itemSubDist = ddl.dataItem();

        //attachment
        //var attach = $("#" + _attachmentControlID).data("kendoUpload");
        //var file = (attach.files != null) ? files[0] : null;
      

        var addedFile = $("#" + _attachmentAddedHiddenControlClientID).val();
        addedFile = (addedFile != "") ? ($.parseJSON(addedFile))[0] : null;       
        var attachAdded = (addedFile != null) ? {id:null, tempId: addedFile.tempId, name: addedFile.name, extension: addedFile.extension, size: addedFile.size} : null;
      
        var removedFile = $("#" + _attachmentRemovedHiddenControlClientID).val();
        removedFile = (removedFile != "") ? ($.parseJSON(removedFile))[0] : null;
        var attachRemoved = (removedFile != null) ? { id: removedFile.id, tempId: removedFile.tempId, name: removedFile.name, extension: removedFile.extension, size: removedFile.size } : currentRemovedAttachment;
       

        var model = {
            uid:gUid,
            UID: uid,
            OperationAddressID: operationAddressID,
            ProjectID: _projectID,
            Address: txtAddress,
            Building: txtBuiding,
            Moo: txtMoo,
            Soi: txtSoi,
            Road: txtRoad,
            SubDistrictID: (itemSubDist != null)?itemSubDist.Value : null,
            SubDistrict: (itemSubDist != null)?itemSubDist.Text : null,
            DistrictID: (itemDist != null)? itemDist.Value : null,
            District: (itemDist != null)? itemDist.Text : null,
            ProvinceID: provItem.Value,
            Province: provItem.Text,
            
            LocationMapID: null,
            LocationMapAttachment: (attachAdded != null) ? attachAdded : currentAttachment,
            AddedLocationMapAttachment: ((attachAdded == null) && (currentAttachment != null) && (currentAttachment.id == null))? currentAttachment: attachAdded,
            RemovedLocationMapAttachment: attachRemoved,
            Runno: Runno
        };
       
        return model;
    }


    function creatDropdownList(provinceList, districtList) {
        //Province
        var ddlProvince = $("#" + _ddlProvinceID).kendoComboBox({
            filter: "contains",
            placeholder: _ddlPleaseSelect,
            dataTextField: "Text",
            dataValueField: "Value",
            dataSource: {
                type: "json",
                serverFiltering: false,
                data:provinceList,
                schema: {
                    data: "Data",
                }               
            },

            change: function (e) { c2x.onProvinceComboboxChange(_ddlDistrictID, _ddlSubdistrictID, e); }
          
        }).data("kendoComboBox");
        ddlProvince.select(0);
        //ddlProvince.enable(false);

        //District
        var ddlDistrict = $("#" + _ddlDistrictID).kendoComboBox({
            autoBind: false,           
            filter: "contains",
            placeholder: _ddlPleaseSelect,
            dataTextField: "Text",
            dataValueField: "Value",
            dataSource: {
                type: "json",
                serverFiltering: false,
                transport: {
                    read: {
                        dataType: "json",
                        contentType: "application/x-www-form-urlencoded; charset=utf-8",
                        url: _urlGetDistrict,
                        type: "POST",
                        data: function () { return c2x.getProvinceComboboxParam(_ddlProvinceID); }
                    }
                },               
                schema: {
                    data: "Data"
                },
                requestEnd: c2x.onComboboxRequestEnd,
                error: c2x.onComboboxError
            },
            change: function (e) { c2x.onDistrictComboboxChange(_ddlSubdistrictID, e); }
        }).data("kendoComboBox");

        //Subdistrict
        var ddlSubDistrict = $("#" + _ddlSubdistrictID).kendoComboBox({
            autoBind: false,
            enable: false,
            filter: "contains",
            placeholder: _ddlPleaseSelect,
            dataTextField: "Text",
            dataValueField: "Value",
            dataSource: {
                type: "json",
                serverFiltering: false,
                transport: {
                    read: {
                        dataType: "json",
                        contentType: "application/x-www-form-urlencoded; charset=utf-8",
                        url: _urlGetSubDistrict,
                        type: "POST",
                        data: function () { return c2x.getProvinceComboboxParam(_ddlDistrictID); }
                    }
                },
                requestEnd: c2x.onComboboxRequestEnd,
                schema: {
                    data: "Data",
                },                
                error: c2x.onComboboxError
            }
        }).data("kendoComboBox");

       
    };

    C2XFunctions.prototype.onDdlProvinceSelecte = function (e) {
        var id = e.item.Value;
        var chilDdl = $("#" + _ddlDistrictID).data("kendoComboBox");
        chilDdl.dataSource.read();
    };

    C2XFunctions.prototype.onDdlDistrictSelecte = function (e) {
        var id = e.item.Value;
        var chilDdl = $("#" + _ddlSubdistrictID).data("kendoComboBox");
        chilDdl.dataSource.read();
    };

    C2XFunctions.prototype.onDdlDataBoundSelecte = function (e) {
        e.sender.enable = true;

    };

    C2XFunctions.prototype.onCascading = function (e) {
        if (e.userTriggered == true) {
            var id = e.sender.element.context.id;
            var ddlID = (id == _ddlProvinceID)? _ddlDistrictID : _ddlSubdistrictID;
            var ddl = $("#" + ddlID).data("kendoComboBox");
            ddl.dataSource.read();
        }
       
    };

   

    C2XFunctions.prototype.getDdlSelectedID = function(ddlID){
        var ddl = $("#" + ddlID).data("kendoComboBox");
        
        var item = ddl.dataItem();
       
        var id = (item != null) ? item.Value : null;
        return {parentid:id};
    }   

    function validate() {


        return false;
    }

    function guid() {
        function g() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }

        return (g() + g() + "-" + g() + "-" + g() + "-" + g() + "-" + g() + g() + g()).toUpperCase();
    }

    function bindDataItem(model) {

        var existingFiles = [];
        if (model != null) {
            $("#" + _txtAddressID).val(model.Address);
            $("#" + _txtBuildingID).val(model.Building);
            $("#" + _txtMooID).val(model.Moo);
            $("#" + _txtSoiID).val(model.Soi);
            $("#" + _txtRoadID).val(model.Road);

            var ddlProvince = $("#" + _ddlProvinceID).data("kendoComboBox");
            ddlProvince.value(model.ProvinceID);

            var ddlDistrict = $("#" + _ddlDistrictID).data("kendoComboBox");
            ddlDistrict.dataSource.read({ parentid: model.ProvinceID }).then(function () {
                ddlDistrict.enable(true);
                if (model.DistrictID != null) {
                    ddlDistrict.value(model.DistrictID);
                }
                
            });
            

            var ddlSubdistrict = $("#" + _ddlSubdistrictID).data("kendoComboBox");
            if (model.DistrictID != null) {
                ddlSubdistrict.dataSource.read({ parentid: model.DistrictID }).then(function () {
                    ddlSubdistrict.enable(true);
                    ddlSubdistrict.value(model.SubDistrictID);
                });
            }
                     

            if (model.LocationMapAttachment != null) {
                existingFiles.push(model.LocationMapAttachment);
            }
                  
        }       
       
        var attachConfig = {
            controlID: _attachmentControlID,
            existingFiles: existingFiles,
            fileUploadControlClientID: _txtAttachmentID,
            addedHiddenControlClientID: _attachmentAddedHiddenControlClientID,
            removedHiddenControlClientID: _attachmentRemovedHiddenControlClientID,
            enabled: true,
            multiple: false,
            viewAttachmentPrefix: _attachmentViewAttachmentPrefix
        };
        c2x.uploadFileSetup(attachConfig);

    }


    c2xOprAddress = new C2XFunctions();
})();