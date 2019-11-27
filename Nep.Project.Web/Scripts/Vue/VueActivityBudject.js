Vue.use(VueNumeric.default);
var appVueAB = new Vue({
    //el: '#divActivityBudget',
    data: {
        data: {
            Data: {
            "ProjectID": 3303,
            "OrganizationID": 1102,
            "TotalRequestAmount": 444,
            "TotalReviseAmount": 444,
            "IsBudgetGotSupport": null,
            "BudgetGotSupportName": null,
            "BudgetGotSupportAmount": null,
            "BudgetDetails": [
              {
                  "ProjectBudgetID": 17392,
                  "UID": null,
                  "No": 1,
                  "Detail": "ค่าอาหารจัดในโรงแรม อาหารเช้า ( 1 คน 1 มื้อ 1 บาท  )",
                  "Amount": 1,
                  "ReviseDetail": "ค่าอาหารจัดในโรงแรม อาหารเช้า ( 1 คน 1 มื้อ 1 บาท  )",
                  "ReviseAmount": 1,
                  "ReviseRemark": null,
                  "Revise1Amount": 1,
                  "Revise2Amount": 1,
                  "ActualExpense" : 1,
                  "ApprovalRemark": null,
                  "BudgetCode": "B1_1_1_2_M",
                  "ActivityID": 41
              }
            ],
            "ProjectApprovalStatusID": 27,
            "ProjectApprovalStatusCode": "6",
            "ProjectApprovalStatusName": "ขั้นตอนที่ 6 ทำสัญญาเรียบร้อยแล้ว",
            "ApprovalStatus": "1",
            "CreatorOrganizationID": 1102,
            "ProvinceID": 80,
            "RequiredSubmitData": null,
            "IsRequestCenter": false,
            "BudgetActivities": [
              {
                  "ActivityID": 41,
                  "ProjectID": 3303,
                  "RunNo": 1,
                  "ActivityName": "sdfasf",
                  "ActivityDESC": "asfsdaf",
                  "TotalAmount": 354,
                  "CreateDate": null
              }
            ],
            "ActivityID": null,
            "ReviseBudgetAmount": 0,
            "Interest": 0,
            },
        "Message": [],
        "IsCompleted": true
            },
        
        "Balance": 0,
        "TotalBalance": 0,
        "Summary": [{
                "Amount": 1,
                "ReviseAmount": 1,
                "Revise1Amount": 1,
                "Revise2Amount": 1,
                "ActualExpense": 1,
                "ActivityID": 1
        }],
        "GrandTotal": {
            "Amount": 1,
            "ReviseAmount": 1,
            "Revise1Amount": 1,
            "Revise2Amount": 1,
            "ActualExpense": 1,
            "ActivityID": 1
        },
        param: {projID:0, qnGroup:"", IsReported:""}
    },
    computed: { VueUrl: function () { return window.location.protocol + '//' + window.location.host + '/Questionarehandler/'; } },
    updated: function () {
        //console.log("sum");
        //this.sumData();
    },
    methods: {
        getData: function () {
            var j = { "ProjID": this.param.projID, "QNGroup": this.param.qnGroup};
        
            
            axios.post(this.VueUrl + 'getactivitybudget', j)
            .then(response => {
               // console.log(response.data);
                if (response.data != "") {
                    this.data = response.data;
                } else {
                    this.data = [];
                   
                }
                
               // console.log(this.data);
            }
            )
        
         
        },
        
        saveData: function () {
            var j = { "ProjID": this.param.projID, "QNGroup": this.param.qnGroup, "QNData": this.items, "IsReported": this.param.IsReported };
            var l = window.location;

            axios.post(this.VueUrl + 'savedata', j)
             .then(response => {
                 AfterQNSaved(response.data);
                
             }
            )
        },
        sumData: function () {
            var sum = [];
         
            var ba = this.data.Data.BudgetActivities;
            var bd = this.data.Data.BudgetDetails;
            var zero = {
                "Amount": 0,
                "ReviseAmount": 0,
                "Revise1Amount": 0,
                "Revise2Amount": 0,
                "ActualExpense": 0,
                "ActivityID": 0
            };
           // var amt  
            ba.forEach(function (a) {
                var amt = jQuery.extend(true, {}, zero)
                // console.log(a);
               
                bd.forEach(function (d) {
                   // console.log(d.ActivityID == a.ActivityID);
                    if (d.ActivityID === a.ActivityID) {
                       
                        
                        amt.Amount += d.Amount;
                        amt.reviseAmount += d.ReviseAmount;
                        amt.Revise1Amount += d.Revise1Amount;
                        amt.Revise2Amount += d.Revise2Amount;
                        amt.ActualExpense += d.ActualExpense;
                        //console.log(a.ActivityID + '|' + amt.Amount);
                    }
                   
                })
              
                sum.push(jQuery.extend(true, {}, amt));
           
            })
            var amt = jQuery.extend(true, {}, zero);
            sum.forEach(function (s) {
                amt.Amount += s.Amount;
                amt.reviseAmount += s.ReviseAmount;
                amt.Revise1Amount += s.Revise1Amount;
                amt.Revise2Amount += s.Revise2Amount;
                amt.ActualExpense += s.ActualExpense;
            });
            this.GrandTotal = jQuery.extend(true, {}, amt);
            this.Balance = this.data.Data.ReviseBudgetAmount - this.GrandTotal.ActualExpense;
            this.TotalBalance = this.Balance + this.data.Data.Interest;
            this.Summary = sum;
            //var bar = "1234567890" + this.TotalBalance.toString();
            //JsBarcode("#barcode", bar);
        },
        initialItems: function () {
            var i = this.items;
            this.field = screenField;
           // i = [];
            Object.keys(screenField).some(function (k, idx) {
                i.push({ n: k, v: null });
            });

        },
      

    },


  
})

function AfterQNSaved(o) {
    if (o.IsCompleted) {
        c2x.writeSummaryResult('บันทึกสำเร็จ');
    } else {
        c2x.writeSummaryResult(null,o.Message[0]);
    }
    
}