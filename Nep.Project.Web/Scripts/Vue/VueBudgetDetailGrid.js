Vue.use(VueNumeric.default);
var appVueOther
function InitialOtherExpense() {
    //console.log('click');
    if (!appVueOther) {
        appVueOther = new Vue({
            data: { items: [] },
            methods: { removeRow: function (i, e) { e.preventDefault(); this.items.splice(i,1); khProjBG.B1_14(); } }

        });
        appVueOther.$mount('#tableGrid');
    }
    
}

function addOtherClick() {
    if (!appVueOther.items) {
        appVueOther.items = [{ detail: "", amount: 0.00 }];
    } else {
        appVueOther.items.push({ detail: "", amount: 0 });
    }
    
}

Vue.component('budget-grid',
    {
        props: ["data"],
        methods: {
            removeRow: function (idx) {
                this.data.items.splice(idx, 1);
                //khProjBG.B1_6();
                this.$parent.onDataChange();
            },
            addRow: function () {
                if (this.data.maxRow > 0 && this.data.items.length >= this.data.maxRow) {
                    alert('ไม่สามารถเพิ่มรายการได้มากกว่า ' + this.data.maxRow.toString() + ' รายการ');
                    return false;
                }
                for (i = 0; i < this.data.items.length; i++) {
                    if (this.data.items[i][this.data.cols.length] == 0) {
                        alert('ไม่สามารถเพิ่มรายการได้หากยังระบุข้อมูลยังไม่ครบ');
                        return false;
                    }
                }
                var item = new Array(this.data.cols.length + 1);
                this.data.items.push(item);
                return true;
            },

        },
    template: '<table  style="width:90%;border-style: solid;border-color: #ccc;border-width: 1px;"   class="k-grid">' +
              '<thead class="k-grid-header" role="rowgroup">' +
               '<tr role="row">' +
                  '<th scope="col" role="columnheader" class="k-header" style="width:80px"><button @click.prevent="addRow()">เพิ่มรายการ</button></th>' +
                  '<template v-for="c in data.cols"><th scope="col" role="columnheader" class="k-header">{{c.title}}</th></template>' +
                  '<th scope="col" role="columnheader" class="k-header">ยอดรวม</th>' +
               '</tr>' +
               '<template v-for="(i,idx) in data.items">' +
                  '<tr>' +
                   '<td><button @click.prevent="removeRow(idx)">ลบ</button></td>' +
                          '<template v-for="(cl,cix) in data.cols">' +
                            '<template v-if="cl.istext">' +
                            '<td><textarea v-model="data.items[idx][cix]" row="3" cols="60"></textarea></td>' +
                            '</template><template v-else>' +
                            '<td><vue-numeric style="width:100px" v-on:blur="$emit(' + "'on-input-blur'" + ');"  separator="," v-bind:precision="2" :empty-value="0" v-model="data.items[idx][cix]" ></vue-numeric></td>' +
                            '</template>' + 
                          '</template>' +
                           '<td><vue-numeric   :read-only="true"  separator="," v-bind:precision="2" :empty-value="0" v-model="data.items[idx][data.cols.length]" ></vue-numeric></td>' +
                                     //  ' <td><vue-numeric onblur="khProjBG.B1_14();"  separator="," v-bind:precision="2" :empty-value="0" v-model="items[idx].amount" ></vue-numeric></td>'
                   '</tr>' +
                '</template>' +
                '</thead>' +
               '</table>'
 
       
        
}
);

var Vue6_1_1, Vue6_1_2, Vue6_1_3;
var Vue6_2_1, Vue6_2_2, Vue6_2_3;
var Vue6_3_1, Vue6_3_2, Vue6_3_3;
var Vue6_4_1, Vue6_4_2;
var Vue1_7;
var Vue1_B1OtherExpense;
function CreateVueGrids() {

    Vue6_1_1 = new Vue({
        el: '#div6_1_1',
        data: { items: [], cols: [{ title: 'คน', limit: 1 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 600 }], maxRow:0 },
        methods: {
            onDataChange: function () {
            khProjBG.B1_6();
        }}
    });
    if (QuestionareModel.vue_6_1_1()) {
        Vue6_1_1.items = QuestionareModel.vue_6_1_1();
    }
    //////////////////

    Vue6_1_2 = new Vue({
        el: '#div6_1_2',
        data: { items: [], cols: [{ title: 'คน', limit: 1 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 1200 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_1_2()) {
        Vue6_1_2.items = QuestionareModel.vue_6_1_2();
    }
    //////////////////

    Vue6_1_3 = new Vue({
        el: '#div6_1_3',
        data: { items: [], cols: [{ title: 'คน', limit: 1 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 0 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_1_3()) {
        Vue6_1_3.items = QuestionareModel.vue_6_1_3();
    }
    ////////////////////////////////////////////////////////
    Vue6_2_1 = new Vue({
        el: '#div6_2_1',
        data: { items: [], cols: [{ title: 'คน', limit: 5 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 600 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_2_1()) {
        Vue6_2_1.items = QuestionareModel.vue_6_2_1();
    }
    //////////////////
    Vue6_2_2 = new Vue({
        el: '#div6_2_2',
        data: { items: [], cols: [{ title: 'คน', limit: 5 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 1200 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_2_2()) {
        Vue6_2_2.items = QuestionareModel.vue_6_2_2();
    }
    //////////////////
    Vue6_2_3 = new Vue({
        el: '#div6_2_3',
        data: { items: [], cols: [{ title: 'คน', limit: 5 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 0 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_2_3()) {
        Vue6_2_3.items = QuestionareModel.vue_6_2_3();
    }
    ////////////////////////////////////////////////////////
    Vue6_3_1 = new Vue({
        el: '#div6_3_1',
        data: { items: [], cols: [{ title: 'กลุ่ม', limit: 0 }, { title: 'คน', limit: 2 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 600 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_3_1()) {
        Vue6_3_1.items = QuestionareModel.vue_6_3_1();
    }
    /////////////////
    Vue6_3_2 = new Vue({
        el: '#div6_3_2',
        data: { items: [], cols: [{ title: 'กลุ่ม', limit: 0 }, { title: 'คน', limit: 2 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 1200 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_3_2()) {
        Vue6_3_2.items = QuestionareModel.vue_6_3_2();
    }
    /////////////////
    Vue6_3_3 = new Vue({
        el: '#div6_3_3',
        data: { items: [], cols: [{ title: 'กลุ่ม', limit: 0 }, { title: 'คน', limit: 2 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 0 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_3_3()) {
        Vue6_3_3.items = QuestionareModel.vue_6_3_3();
    }
    /////////////////------------------------
    Vue6_4_1 = new Vue({
        el: '#div6_4_1',
        data: { items: [], cols: [{ title: 'คน', limit: 0 }, { title: 'ชั่วโมง', limit: 0 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 400 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_4_1()) {
        Vue6_4_1.items = QuestionareModel.vue_6_4_1();
    }
    //////////////////
    Vue6_4_2 = new Vue({
        el: '#div6_4_2',
        data: { items: [], cols: [{ title: 'คน', limit: 0 }, { title: 'ชั่วโมง', limit: 12}, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 1200 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_6();
            }
        }
    });
    if (QuestionareModel.vue_6_4_2()) {
        Vue6_4_2.items = QuestionareModel.vue_6_4_2();
    }

    //if (QuestionareModel.vue_6_4_2()) {
    //    Vue6_4_2.items = QuestionareModel.vue_6_4_2();
    //}
    //////////////////
    Vue1_7 = new Vue({
        el: '#div1_7',
        data: { items: [], cols: [{ title: 'คน', limit: 0 }, { title: 'ชั่วโมง', limit: 6 }, { title: 'วัน', limit: 0 }, { title: 'บาท', limit: 600 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1_7();
            }
        }
    });

    if (QuestionareModel.vue1_7()) {
        Vue1_7.items = QuestionareModel.vue1_7();
    }
    //////////////////
    Vue1_B1OtherExpense = new Vue({
        el: '#div1_B1OtherExpense',
        data: { items: [], cols: [ { title: 'รายละเอียด', limit: 0, istext: true }, { title: 'บาท', limit: 0 }], maxRow: 0 },
        methods: {
            onDataChange: function () {
                khProjBG.B1OtherExpense();
            }
        }
    });
    if (QuestionareModel.vue1_B1OtherExpense()) {
        Vue1_B1OtherExpense.items = QuestionareModel.vue1_B1OtherExpense();
    }
}

function sumGridData(v) {
    var i = v.items;
    var len = v.cols.length;
    var total = 0;
    for (l = 0; l < i.length; l++) {
        var o = i[l];
        var sum = 1;
        for (j = 0; j < len; j++) {
            if  (!v.cols[j].istext) {
                if (v.cols[j].limit > 0) {
                    if (o[j] > v.cols[j].limit) {
                        v.$set(o, len, 0);
                        return { total: 0, message: 'ไม่สามารถระบุได้มากกว่า ' + v.cols[j].limit.toString() + ' ' + v.cols[j].title };
                    }

                }
                sum = sum * o[j];
            }

        }
        v.$set(o, len, sum);
        total += sum;
    } 
    return { total: total, message: '' };
}