var c2xProc = null;
(function () {
    var C2XProcFunctions = function () {
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

    C2XProcFunctions.prototype.config = function (config) {
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
    C2XProcFunctions.prototype.getProcessDateFormat = function (data) {
        var s = new Date(data.ProcessStart);
        var e = new Date(data.ProcessEnd);

        var ret = s.getDate() + '/' + (s.getMonth()+1) + '/' + (s.getFullYear() + 543) + ' ถึง ' + e.getDate() + '/' + (e.getMonth()+1) + '/' + (e.getFullYear() + 543);
        return ret;
    };
    C2XProcFunctions.prototype.getAddressFormat = function (data) {
        //console.log(data);
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
        if (data.SubDistrict != null && (data.SubDistrict != "")) {
            text += " แขวง " + data.SubDistrict;
        }
        if (data.District != null && (data.District != "")) {
            text += " เขต " + data.District;
        }
        if (data.Province != null && (data.Province != "")) {
            text += " จังหวัด " + data.Province;
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
            text += "<br />ไฟล์แนบ <a href='' target='_blank' class='file-link' onclick='return c2xProc.openAttachment(\"" + attahc.tempId + "\", \"" + attahc.id + "\", \"" + attahc.name + "\");'>" + attahc.name + "</a>";

        }
        if (data.Latitude && data.Longitude) {
            text += "<br /> <a href='http://maps.google.com/maps?q=loc:" + data.Latitude.toString() + ',' + data.Longitude.toString() + "' target='_blank' class='file-link' >ที่ตั้งกิจกรรม</a>"
        }
        if (data.ImageAttachments) {
            text += "<br>รูปกิจกรรม"
            for (i = 0; i <= data.ImageAttachments.length - 1; i++) {
                var img = data.ImageAttachments[i];
                addlink = false;
                if ((img != null)) { //&& (data.AddedLocationMapAttachment == null && data.RemovedLocationMapAttachment == null)) {
                    addlink = true;

                }

                if ((data.RemovedImageAttachments && data.RemovedImageAttachments[i] != null)) {
                    addlink = false;

                }
                if (addlink) {

                   // text += "<br />รูปที่ " + (i + 1) + " <a href='' target='_blank' class='file-link' onclick='return c2xProc.openAttachment(\"" + img.tempId + "\", \"" + img.id + "\", \"" + img.name + "\");'>" + img.name + "</a>";
                    text += "<br /><a href='" + img.ImageFullPath + "' target='_blank' class='file-link' >" + "รูปที่ " + (i + 1) + "</a>";
                }
                
            }
        }
        return text;
    };

    C2XProcFunctions.prototype.openAttachment = function (tempId, id, name) {
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

    C2XProcFunctions.prototype.getAttachmentFormat = function (data) {
        return "-";
    };

    C2XProcFunctions.prototype.getEditItem = function () {
        return _currentEditItem;
    };

   C2XProcFunctions.prototype.editRowAddress = function (e) {
       //c2x.updateCommandText(e);     

       _currentEditItem = c2x.clone(e.model);
       
   };

   C2XProcFunctions.prototype.cancelEditRowAddress = function (e) {
       //c2x.updateCommandText(e);

       _currentEditItem = null;
   };

   C2XProcFunctions.prototype.destroyAddress = function (model) {
       
        var uid = model.UID;
        var id = model.ProcessID;

        var text = $('#' + _hiddOperationAddressID).val();
        if (text.length > 0) {

            var data = $.parseJSON(text);

            data = $.grep(data, function (item) {
                return (((item.UID != null) && (item.UID != uid)) || ((item.ProcessID != null) && (item.ProcessID != id)));
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

    C2XProcFunctions.prototype.updateAddress = function (model) {
       
        var uid = model.UID;
        var id = model.ProcessID;
         
        var text = $('#' + _hiddOperationAddressID).val();
        var item;
        var isFound = false;
        var data = [];
        if (text != "") {
            data = $.parseJSON(text);
            for (var i = 0; i < data.length; i++) {
                item = data[i];
                if (((uid != null) && (item.UID == uid)) || (id != null && (item.ProcessID == id))) {
                    data[i] = model;
                    isFound = true;
                    break;
                }
            }         
        }


        var grid = $("#" + _gridOperationAddressID).data("kendoGrid");
        if (!isFound) {
            model.Runno = data.length + 1;
            model.ProcessID = -1 * (Math.random() * 100000);
            data.push(model);
            grid.dataSource.add(model);
            grid.saveChanges();
        } else {           

            var editModel = grid.dataSource.getByUid(model.uid);
            console.log(editModel);
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
            editModel.set("ProcessID", model.ProcessID);
            editModel.set("Description", model.Description);
            editModel.set("ProcessStart", model.ProcessStart.toString());
            editModel.set("ProcessEnd", model.ProcessEnd.toString());
            editModel.set("AddedLocationMapAttachment", model.AddedLocationMapAttachment);
            editModel.set("RemovedLocationMapAttachment", model.RemovedLocationMapAttachment);
            if (model.RemovedLocationMapAttachment) {
                model.AddedLocationMapAttachment = null;
                //model.LocationMapAttachment = null;
            }
            editModel.set("ImageAttachments", model.ImageAttachments);
             grid.saveChanges();
           
        }
         
        //for (var i = 0; i < data.length; i++) {
        //    item = data[i];
        //    item.Runno = i+1;
             
        //}
        // grid.saveChanges();
        console.log(data);
        var newValue = kendo.stringify(data);
        console.log(newValue);
        $('#' + _hiddOperationAddressID).val(newValue);

        $("#" + _gridOperationAddressID).show();
        $(".operation-address-validator").css("visibility", "hidden");
    };    

    C2XProcFunctions.prototype.openAddressForm = function (uid, id) {
        c2x.clearResultMsg();
        var pageUrl = _popupUrl + "?projectid=" + _projectID;
        if (typeof (uid) == 'undefined') {
            _currentEditItem = null;
        }        
       
        var w = c2x.openFormDialog(pageUrl, _addressFormTitle, { width: 800, height: 550 }, null);    
        return false;
    }

    C2XProcFunctions.prototype.onCommandEditClick = function(e){
        var tr = $(e.target).closest("tr");
        var grid = $("#" + _gridOperationAddressID).data("kendoGrid");
        var editItem = grid.dataItem(tr);
        _currentEditItem = editItem;
        //console.log(editItem);
        c2xProc.openAddressForm(editItem.UID, editItem.OperationAddressID);
    }

    C2XProcFunctions.prototype.onCommandDeleteClick = function (e) {
        var tr = $(e.target).closest("tr");
        var grid = $("#" + _gridOperationAddressID).data("kendoGrid");
        var editItem = grid.dataItem(tr);
        grid.dataSource.remove(editItem);
        for (i = 0; i < grid.dataSource._data.length; i++)
        {
            grid.dataSource._data[i].Runno = i + 1;
        }
        grid.saveChanges();

        c2xProc.destroyAddress(editItem);
    }


    C2XProcFunctions.prototype.createOperationAddressGrid = function () {
        var columns = [
                { field: 'Runno', title: "กิจกรรมที่", width: '50px', template: "#=data.Runno#" },
                {
                    field: 'Address', title: _columnTitle.Address, width: '400px', template: "#=c2xProc.getAddressFormat(data)#"
                },
                {
                    field: 'Description', title: 'รายละเอียดกิจกรรม', width: '150px', template: "#=(data.Description != null )?data.Description: '-'#"
                },
                {
                    field: 'ProcessDate', title: 'ระยะเวลาดำเนินการกิจกรรม', width: '150px', template: "#=c2xProc.getProcessDateFormat(data)#"
                },
             

        ];
        if (!_isViewMode) {
            columns.push({
                command: [
                    {
                        name: "custom-edit", text: "",
                        click: function (e) {
                            c2xProc.onCommandEditClick(e);
                        }
                    },
                    {
                        name: 'custom-delete', text: '',
                        click: function (e) {
                            c2xProc.onCommandDeleteClick(e);
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
                            Description: { type: "string", },
                            ProcessStart: { type: "date", },
                            ProcessEnd: { type: "date", },
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
               // console.log(json);
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

    c2xProc = new C2XProcFunctions();
})();
