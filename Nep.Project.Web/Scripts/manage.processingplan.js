var c2xPlan = null;
(function () {
    var C2XFunctions = function () {
    };

   
    var _gridOperationAddressID = "";
    var _hiddOperationAddressID = "";
    var _columnTitle;
    var _addressList = null;
    var _isViewMode = false;
    var _projectID = 0;
    var _currentEditItem = null;
    var _popupUrl = "";
    var _addressFormTitle = "";
    var _addressLabel = "";
    var _mooLabel = "";
    var _buildingLabel = "";
    var _soiLabel = "";
    var _roadLabel = "";
    var _mapLabel = "";
    var _viewAttachmentUrl = "";

    C2XFunctions.prototype.config = function (config) {
        _viewAttachmentUrl = config.ViewAttachmentUrl;
        _hiddOperationAddressID = config.HiddOperationAddressID;
        _gridOperationAddressID = config.GridOperationAddressID;
        _columnTitle = config.ColumnTitle;
        _popupUrl = config.PopupUrl;
        _addressFormTitle = config.AddressFormTitle;

        _addressLabel = config.AddressLabel;
        _mooLabel = config.MooLabel;
        _buildingLabel = config.BuildingLabel;
        _soiLabel = config.SoiLabel;
        _roadLabel = config.RoadLabel;
       
        _mapLabel = config.MapLabel;


        _isViewMode = config.IsView,
        _projectID = config.ProjectID;

       
    };

    C2XFunctions.prototype.getAddressFormat = function (data) {
        var text = "";
        text = _addressLabel + " " + data.Address;
        if (data.Moo != null && (data.Moo != "")) {
            text += " " + _mooLabel + " " + data.Moo;
        }
        if (data.Building != null && (data.Building != "")) {
            text += " " + _buildingLabel + " " + data.Building;
        }
        if (data.Soi != null && (data.Soi != "")) {
            text += " " + _soiLabel + " " + data.Soi;
        }
        if (data.Road != null && (data.Road != "")) {
            text += " " + _roadLabel + " " + data.Road;
        }
        var addlink = false;
        if ((data.LocationMapAttachment != null)){ //&& (data.AddedLocationMapAttachment == null && data.RemovedLocationMapAttachment == null)) {
            addlink = true;

        }
   
        if ((data.RemovedLocationMapAttachment != null)) {
            addlink = false;

        }
        if (addlink) {
            var attahc = data.LocationMapAttachment;
            text += "<br />" + _mapLabel + " <a href='' target='_blank' class='file-link' onclick='return c2xPlan.openAttachment(\"" + attahc.tempId + "\", \"" + attahc.id + "\", \"" + attahc.name + "\");'>" + attahc.name + "</a>";

        }
 
        return text;
    };

    C2XFunctions.prototype.openAttachment = function (tempId, id, name) {
        var url = "";
       
        if ((tempId != 'null') && (tempId != '')) {
            url = "AttachmentHandler/View/Temp/" + tempId + '/' + encodeURIComponent(c2x.htmlDecode(name));
        } else {
            
           url = "AttachmentHandler/View/" + _viewAttachmentUrl + "/" + id + '/' + encodeURIComponent(c2x.htmlDecode(name));
            
        }
        if (url != '') {
            window.open(url, '_blank');
        }
        return false;
    }

    C2XFunctions.prototype.getAttachmentFormat = function (data) {
        return "-";
    };

    C2XFunctions.prototype.getEditItem = function () {
        return _currentEditItem;
    };

   C2XFunctions.prototype.editRowAddress = function (e) {
       //c2x.updateCommandText(e);     

       _currentEditItem = c2x.clone(e.model);
       
   };

   C2XFunctions.prototype.cancelEditRowAddress = function (e) {
       //c2x.updateCommandText(e);

       _currentEditItem = null;
   };

   C2XFunctions.prototype.destroyAddress = function (model) {
       
        var uid = model.UID;
        var id = model.OperationAddressID;

        var text = $('#' + _hiddOperationAddressID).val();
        if (text.length > 0) {

            var data = $.parseJSON(text);

            data = $.grep(data, function (item) {
                return (((item.UID != null) && (item.UID != uid)) || ((item.OperationAddressID != null) && (item.OperationAddressID != id)));
            });



            if (data.length == 0) {
                $('#' + _hiddOperationAddressID).val("");
                $("#" + _gridOperationAddressID).hide();

            } else {
                var newValue = kendo.stringify(data);
                $('#' + _hiddOperationAddressID).val(newValue);
            }
        } else {
            $("#" + _gridOperationAddressID).hide();
        }

    };

    C2XFunctions.prototype.updateAddress = function (model) {
       
        var uid = model.UID;
        var id = model.OperationAddressID;
         
        var text = $('#' + _hiddOperationAddressID).val();
        var item;
        var isFound = false;
        var data = [];
        if (text != "") {
            data = $.parseJSON(text);
            for (var i = 0; i < data.length; i++) {
                item = data[i];
                if (((uid != null) && (item.UID == uid)) || (id != null && (item.OperationAddressID == id))) {
                    data[i] = model;
                    isFound = true;
                    break;
                }
            }         
        }


        var grid = $("#" + _gridOperationAddressID).data("kendoGrid");
        if (!isFound) {
            model.Runno = data.length + 1 ;
            data.push(model);
            grid.dataSource.add(model);
            grid.saveChanges();
        } else {           

            var editModel = grid.dataSource.getByUid(model.uid);
            editModel.set("Runno", model.Runno);
            editModel.set("Address", model.Address);
            editModel.set("Building", model.Building);
            editModel.set("Moo", model.Moo);
            editModel.set("Soi", model.Soi);
            editModel.set("Road", model.Road);
            editModel.set("SubDistrictID", model.SubDistrictID);
            editModel.set("SubDistrict", model.SubDistrict);
            editModel.set("DistrictID", model.DistrictID);
            editModel.set("District", model.District);
            editModel.set("ProvinceID", model.ProvinceID);
            editModel.set("Province", model.Province);
            editModel.set("LocationMapID", model.LocationMapID);
            editModel.set("LocationMapAttachment", model.LocationMapAttachment);
            editModel.set("AddedLocationMapAttachment", model.AddedLocationMapAttachment);
            editModel.set("RemovedLocationMapAttachment", model.RemovedLocationMapAttachment);
            if (model.RemovedLocationMapAttachment) {
                model.AddedLocationMapAttachment = null;
                //model.LocationMapAttachment = null;
            }
             grid.saveChanges();
           
        }
         
        //for (var i = 0; i < data.length; i++) {
        //    item = data[i];
        //    item.Runno = i+1;
             
        //}
       // grid.saveChanges();
        var newValue = kendo.stringify(data);
        $('#' + _hiddOperationAddressID).val(newValue);

        $("#" + _gridOperationAddressID).show();
        $(".operation-address-validator").css("visibility", "hidden");
    };    

    C2XFunctions.prototype.openAddressForm = function (uid, id) {
        c2x.clearResultMsg();
        var pageUrl = _popupUrl + "?projectid=" + _projectID;
        if (typeof (uid) == 'undefined') {
            _currentEditItem = null;
        }        
       
        var w = c2x.openFormDialog(pageUrl, _addressFormTitle, { width: 800, height: 450 }, null);    
        return false;
    }

    C2XFunctions.prototype.onCommandEditClick = function(e){
        var tr = $(e.target).closest("tr");
        var grid = $("#" + _gridOperationAddressID).data("kendoGrid");
        var editItem = grid.dataItem(tr);
        _currentEditItem = editItem;
        c2xPlan.openAddressForm(editItem.UID, editItem.OperationAddressID);
    }

    C2XFunctions.prototype.onCommandDeleteClick = function (e) {
        var tr = $(e.target).closest("tr");
        var grid = $("#" + _gridOperationAddressID).data("kendoGrid");
        var editItem = grid.dataItem(tr);
        grid.dataSource.remove(editItem);
        for (i = 0; i < grid.dataSource._data.length; i++)
        {
            grid.dataSource._data[i].Runno = i + 1;
        }
        grid.saveChanges();

        c2xPlan.destroyAddress(editItem);
    }


    C2XFunctions.prototype.createOperationAddressGrid = function () {
        var columns = [
                { field: 'Runno', title: "กิจกรรมที่", width: '50px', template: "#=data.Runno#" },
                {
                    field: 'Address', title: _columnTitle.Address, width: '400px', template: "#=c2xPlan.getAddressFormat(data)#"
                },
                {
                    field: 'SubDistrictID', title: _columnTitle.SubDistrictID, width: '150px', template: "#=(data.SubDistrict != null )?data.SubDistrict: '-'#"
                },
                {
                    field: 'DistrictID', title: _columnTitle.DistrictID, width: '150px', template: "#=(data.DistrictID != null)?data.District : '-'#"
                },
                {
                    field: 'ProvinceID', title: _columnTitle.ProvinceID, width: '150px', template: "#=data.Province#"
                },

        ];
        if (!_isViewMode) {
            columns.push({
                command: [
                    {
                        name: "custom-edit", text: "",
                        click: function (e) {                            
                            c2xPlan.onCommandEditClick(e);
                        }
                    },
                    {
                        name: 'custom-delete', text: '',
                        click: function (e) {
                            c2xPlan.onCommandDeleteClick(e);
                        }
                    }
                ], title: '&nbsp;', width: '65px'
            });
        }

        $('#' + _gridOperationAddressID).kendoGrid({
            autoBind: false,
            dataSource: {               
                schema: {
                    model: {
                        id: "UID",
                        fields: {
                            UID: { nullable: false },
                            Runno: { type:"number", nullable: true},
                            OperationAddressID: { type: "number", nullable: true },
                            ProjectID: { type: "number", },
                            Address: { type: "string",},
                            Building: { type: "string"},
                            Moo: { type: "string" },
                            Soi: { type: "string" },
                            Road: { type: "string" },
                            SubDistrictID: { type: "number", nullable: false, },
                            SubDistrict: { type: "string", },
                            DistrictID: { type: "number", nullable: false,  },
                            District: { type: "string", },
                            ProvinceID: { type: "number", nullable: false, },
                            Province: { type: "string", },
                            LocationMapID: { type: "number", nullable: false },
                            LocationMapAttachment: { type: "object" },
                            AddedLocationMapAttachment: { type: "object" },
                            RemovedLocationMapAttachment: { type: "object" }
                        }
                    }
                },
                pageSize: Number.MAX_VALUE
            },
            scrollable: false,
            pageable: false,
            columns: columns,
            dataBound: function (e) { c2x.updateCommandText(e, _gridOperationAddressID); },

        });

        bindOperationAddressData();
    };

    function bindOperationAddressData() {
        var text = $('#' + _hiddOperationAddressID).val();
        
        if (text != "") {

            var json = $.parseJSON(text);

            if ((json != null) && (json.length > 0)) {

                for (var i = 0; i < json.length; i++) {
                    json[i].UID = guid();
                }

                $("#" + _gridOperationAddressID).show();

                var grid = $("#" + _gridOperationAddressID).data("kendoGrid");
                grid.dataSource.data(json);
                $("#" + _gridOperationAddressID).show();
                
            } else {
                $("#" + _gridOperationAddressID).hide();
               
            }
        } else {
            $("#" + _gridOperationAddressID).hide();
        }
    }

    function guid() {
        function g() {
            return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1)
        }

        return (g() + g() + "-" + g() + "-" + g() + "-" + g() + "-" + g() + g() + g()).toUpperCase();
    }

    c2xPlan = new C2XFunctions();
})();
