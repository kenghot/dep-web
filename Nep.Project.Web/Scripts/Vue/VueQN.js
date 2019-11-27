// binding example

// //declare filename with "screenField"
//<script>
//    var screenField = { f0: 0, f1: 1, f2: 2 };
//          
//</script>
// // bind input with items[field.f0].v
//<div id="divVueQN">
//    <input v-model.number="items[field.f0].v" type="number" />{{items[field.f0].v}}<br>
//    <input v-model="items[field.f1].v" type="text" />{{items[field.f1].v}}<br />
//    <textarea v-model="items[field.f2].v"></textarea>{{items[field.f2].v}}
//</div>
//<script src="../../Scripts/VueQN.js?v=<%= DateTime.Now.Ticks.ToString() %>"></script> 

//var VueUrl = window.location.protocol + '//' + window.location.host + '/Questionarehandler/';
Vue.use(VueNumeric.default);
var appVueQN = new Vue({
    //el: '#divVueQN',
    data: {
        items: [],
        field: {},
        param: { projID: 0, qnGroup: "", IsReported: "" },
        extend: {},
    },
    computed: { VueUrl: function () { return window.location.protocol + '//' + window.location.host + '/Questionarehandler/'; }},
    methods: {
        getData: function (f) {
            var j = { "ProjID": this.param.projID, "QNGroup": this.param.qnGroup};
            var t = this;
          
            axios.post(this.VueUrl + 'getdata', j)
            .then(response => {
                //console.log(j);
               // console.log(response.data);

                if (response.data != "") {
                    // this.items = response.data;
                    this.storeData(response.data);
                } else {
                    this.items = [];
                    this.initialItems();
                }
                t.$mount("#divVueQN")
                //this.afterGetDataFinish();
                
                if (f) { f();}
               // console.log(this.data);
            }
            )
        
         
        },
        storeData: function (data) {
            this.items = [];
            this.initialItems();
            this.items.forEach(function (a) {
                var find = data.find(x => x.n === a.n);
                if (find != null) {
                    a.v = find.v;
                }
            })
        },
        afterGetDataFinish: function() {

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
        initialItems: function () {
            var i = this.items;
            this.field = screenField;
          //  i = [];
           
            Object.keys(screenField).some(function (k, idx) {
                var v = null;
                // multiple select checkbox
                if (k.substring(0, 3) == "mcb") {
                    v = [];
                }
                
                i.push({ n: k, v: v });
                
            });

        }

    },
    beforeCreate: function () {
        //console.log('beforeCreate');
        //console.log(this.param);
    },
    created: function () {
        this.initialItems();
    },
    filters: {
        toStringNumber : function (value) {
            return value.toFixed(2);
        }

    }
  
})

function AfterQNSaved(o) {
    if (o.IsCompleted) {
        c2x.writeSummaryResult('บันทึกสำเร็จ');
    } else {
        c2x.writeSummaryResult(null,o.Message[0]);
    }
    
}

 
function printDiv(div) {

    if (document.getElementById != null) {
        var html = '';
        html += '<style>@page{size:landscape;}</style>';
        html += '<HTML>\n<HEAD>\n';
        if (document.getElementsByTagName != null) {
            var headTags = document.getElementsByTagName("head");
            if (headTags.length > 0) html += headTags[0].innerHTML;
        }
        
        html += '\n</HE' + 'AD>\n<BODY>\n';
        html += '<scr' + 'ipt type="text/javascript">' + 'window.onload = function() { window.print(); window.close(); };' + '</sc' + 'ript>';
        var printReadyElem = document.getElementById(div);

        if (printReadyElem != null) html += printReadyElem.innerHTML;
        else {
            alert("Error, no contents.");
            return;
        }

        html += '\n</BO' + 'DY>\n</HT' + 'ML>';
        var printWin = window.open("", "processPrint");
        printWin.document.open();
        printWin.document.write(html);
        printWin.document.close();



    }
}