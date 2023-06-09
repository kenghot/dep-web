﻿
var objIMP = null;
(function () {

    var ImportParticipants = function () {
    };

    ImportParticipants.prototype.config = function (config) {
        //console.log(config);
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
        //console.log(_targetGroupList);
    };
    ImportParticipants.prototype.importFile = function (e) {
        //Get the files from Upload control
       
        var files = jQuery.extend(true, {}, e.target.files);
        var i, f;
        //Loop through files
        e.target.form.reset();
     
        for (i = 0, f = files[i]; i != files.length; ++i) {
            var reader = new FileReader();
            var name = f.name;
            reader.onload = function (e) {
                var data = e.target.result;

                var result;
                var workbook = XLSX.read(data, { type: 'binary' });

                var sheet_name_list = workbook.SheetNames;
                sheet_name_list.forEach(function (y) { /* iterate through sheets */
                    //Convert the cell value to Json
                    var roa = XLSX.utils.sheet_to_json(workbook.Sheets[y]);
                    if (roa.length > 0) {
                        result = roa;
                    }
                });

               // console.log(result);
               
                for (i = 0; i < result.length; i++) {
                    var f = result[i];
                    $("#" + _participantFirstNameCreate).val(f.ชื่อ);
                    $("#" + _participantLastNameCreate).val(f.นามสกุล);
                    $("#" + _participantIdCardCreate).val(f.เลขประจำตัวประชาชน);
                    var ddl;
                    ddl = selectDDL(_participantGenderCreate, f.เพศ, true, "kendoDropDownList");
                    if (ddl.selectedIndex < 0) {
                        alert("ข้อมูลเพศ ไม่ถูกต้อง");
                        break;
                    }
                    ddl = selectDDL(_participantIsCrippleCreate, f.ประเภทผู้เข้าร่วม, true, "kendoDropDownList");
                    if (ddl.selectedIndex < 0) {
                        alert("ข้อมูลประเภทผู้เข้าร่วมไม่ถูกต้อง");
                        break;
                    }
                    ddl.trigger("change");
                    selectDDL(_textBoxParticipantTargetGroupID, f.กลุ่มเป้าหมายเข้าร่วม, true, "kendoComboBox");
                    var err = c2xProjectReport.createRowParticipantAuto();
                    if (err != "") {
                        alert(err);
                        break;
                    }
                }
              
            };
            reader.readAsArrayBuffer(f);
        }
        c2xProjectReport.cancelCreateRowParticipant();
    }

    function selectDDL(ddlName, text, isByText, kendoType) {
        var ddl = $("#" + ddlName).data(kendoType);
        if (!isByText) {
            ddl.select(0);
        } else {
            ddl.select(function (s) {
                return s.Text === text;
            });
        }
        return ddl;
        //console.log(idx);
        //if (idx >= 0) {
        //    ddl.select(idx);
        //}
    }
    //Check DES_PERSON
    ImportParticipants.prototype.CheckDESData = function (e) {
        var grid =  $('#' + _participantGridID).data("kendoGrid");
        var items = grid.dataSource.data();
        console.log(items);
        if (items.length == 0) {
            alert("ไม่มีข้อมูลให้ตรวจสอบ");
            return;
        }
        var ids = [];
   
        axios.post('/Serviceshandler/checkdesperson', { "items": items })
            .then(response => {
                 if (response.data != "") {
                   //  console.log(response.data);
                     var data = response.data;
                     if (!data.IsCompleted)
                     {
                         alert("เกิดข้อผิดพลาด (" + data.Message[0] + ")");
                         return;
                     }
                     var iCorrect, iError;
                     iCorrect = 0;
                     iError = 0;
                     var allData = data.Data;
                     for (i = 0; i < allData.length; i++) {
                         items[i].CheckDESResult = allData[i];
                         if (allData[i] != "") {
                             iError++;
                         } else {
                             iCorrect++;
                         }
                     }
                     console.log(items);
                     grid.dataSource.data(items);
                    // grid.saveChanges();
                     alert("ตรวจสอบสำเร็จทั้งหมด " + (iError + iCorrect).toString() + " รายการ\n ข้อมูลถูกต้อง " +
                         iCorrect + " รายการ\n ข้อมูลไม่ถูกต้อง " + iError + " รายการ\n" +
                         "*ชี้ที่เครื่องหมายกากบาทเพื่อดูรายละเอียด");
                }  
            }
            )
    }
    ImportParticipants.prototype.CheckIDFromDisability = function (id) {
  
        axios
            //.post('ajax_get_des_person.txt', "the_id=" + the_id)
            .post('/Serviceshandler/CheckIDFromDisability', { "id": txtCheck.val() })
            .then(response => {
                var mm
                try {
                    mm = eval(response.data)

                    txtFirstName.val('')
                    txtLastName.val('')
                    txtCardId.val('')
                    let id = txtCheck.val()
                    console.log(id)
                    console.log(`${id[0]}-${id[1]}${id[2]}${id[3]}${id[4]}-${id[5]}${id[6]}${id[7]}${id[8]}${id[9]}-${id[10]}${id[11]}-${id[12]}`)
                    if (mm) {
                        //main_body.maimad_id = the_id;
                        txtFirstName.val(someVar.FIRST_NAME_THAI)
                        txtLastName.val(someVar.LAST_NAME_THAI)
                        txtCardId.val(`${id[0]}-${id[1]}${id[2]}${id[3]}${id[4]}-${id[5]}${id[6]}${id[7]}${id[8]}${id[9]}-${id[10]}${id[11]}-${id[12]}`)
                        //main_body.maimad_sex = someVar.SEX_CODE;
                        //main_body.maimad_age = someVar.BIRTH_DATE;
                        //main_body.maimad_disabled = someVar.DEFORM_ID;
                        var ddl;
                        var gender = "ชาย"
                        if (someVar.SEX_CODE === "F") {
                            gender = "หญิง"
                        }
                        ddl = selectDDL(_participantGenderCreate, gender, true, "kendoDropDownList");
                        var targetSelect = 0
                        var targetCode = "9"
                        if (someVar.DEFORM_ID) {
                            var tgs = someVar.DEFORM_ID.split(',')
                            if (tgs && tgs.length > 0) {

                                var deform = parseInt(tgs[0]);
                                if (deform) {
                                    if (deform == 1 || deform == 6 || deform == 12) {
                                        targetCode = "1";
                                    }
                                    if (deform == 2 || deform == 7 || deform == 13) {
                                        targetCode = "2";
                                    }
                                    if (deform == 3 || deform == 8 || deform == 14) {
                                        targetCode = '3';
                                    }
                                    if (deform == 4 || deform == 9 || deform == 15) {
                                        targetCode = "4";
                                    }
                                    if (deform == 5 || deform == 10 || deform == 16) {
                                        targetCode = "5";
                                    }
                                    if (deform == 6 || deform == 11 || deform == 17) {
                                        targetCode = "6";
                                    }
                                    if (deform == 18) {
                                        targetCode = "7";
                                    }
                                    var ftarget = _targetGroupList.find(f => f.TargetCode == targetCode)
                                    if (ftarget) {
                                        targetSelect = ftarget.TargetID
                                    }
                                    
                                }
                            
                                
                            }
                        }
                        var ddlTarget = $('#' + _textBoxParticipantTargetGroupID).data("kendoComboBox")
                        if (ddlTarget) {
                            ddlTarget.select(s => s.TargetID == targetSelect)
                        }
                    } else {
                        alert("ไม่พบข้อมูล")
                    }
                } catch (err) {
                    console.log(err)
                    alert(err)
                }



            })
    }

    objIMP = new ImportParticipants();

})();
