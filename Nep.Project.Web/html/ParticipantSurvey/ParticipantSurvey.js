Vue.component('k-radio', {
    template: '#k-radio',
    props: ['topic', 'obj', 'fld'],
    data() {
        return {
            myVal : this.val,
        }
    }
})
var VueParticipantSurvey = new Vue({
    el: '#appParticipantSurvey',
    vuetify: new Vuetify(),
    data() {
        return {
            projId: null,
            defaultResult: {
                radio1_1: "",
                radio1_2: "",
                radio1_3: "",
            },
            result: {},
        }
    },
    methods: {
        DisplayData(projId) {

            var j = { "ProjID": projId, "QNGroup": "EVALUATE" };

            axios.post(this.VueUrl + 'getqndata', j)
                .then(response => {
                    //console.log(j);
                    console.log(response.data);

                    if (response.data != "") {
                        if (response.data.IsCompleted) {
                            if (response.data.Data && response.data.Data != "null") {
                                this.result = JSON.parse(response.data.Data)
                            } else {
                                this.result = JSON.parse(JSON.stringify(this.defaultResult))
                            }


                            window.setTimeout(function () {

                            }, 300)

                        } else {
                            this.result = JSON.parse(JSON.stringify(this.defaultResult))
                        }

                    } else {
                        VueMaster.showSnack("ระบบขัดข้อง โปรดลองอีกครั้ง", "error")
                    }

                }
                )

        },
        SaveData() {
            console.log('save')
            if (!this.projId) {
                alert("ไม่พบข้อมูลโครงการ (no project id)")
                return
            }
            var j = { "ProjID": this.projId, "QNGroup": "EVALUATE", IsReported: "0", "QNData": JSON.stringify(this.result) }
            //var t = this
            //this.validData()
            //if (this.errors.length > 0) {
            //    this.showSnack("ข้อมูลยังไม่สมบูรณ์ กรุณาตรวจสอบการแจ้งเตือน", "error")

            //    return

            //}

            axios.post(this.VueUrl + 'savedata', j)
                .then(response => {
                    //console.log(j);
                    console.log(response.data);

                    if (response.data) {
                        if (response.data.IsCompleted) {
                            //console.log(this.refreshButton)
                            alert("บันทึกสำเร็จ", "success")

                            return true

                        } else {
                            alert(`ไม่สามารถบันทึกข้อมูลได้ (${response.data.Message[0]})`, "error")
                            return false
                        }


                    } else {
                        alert("ระบบขัดข้อง โปรดลองอีกครั้ง", "error")
                        return false
                    }

                })
                .catch((err) => {
                    alert("ระบบขัดข้อง โปรดลองอีกครั้ง", "error")
                    return false
                }
                )

            return false
        },
    },
    created() {
        this.result = JSON.parse(JSON.stringify(this.defaultResult))
    },
    computed: {
        VueUrl: function () {
            return window.location.protocol + '//' + window.location.host + '/Questionarehandler/';
        },

    },
    mounted() {
        let searchParams = new URLSearchParams(window.location.search)
        this.projId = searchParams.get('projId')
    }
})