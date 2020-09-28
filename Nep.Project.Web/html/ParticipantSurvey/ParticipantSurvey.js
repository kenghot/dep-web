Vue.component('k-radio', {
    template: '#k-radio',
    props: ['topic', 'obj', 'fld','viewmode'],
    data() {
        return {
            myVal : this.val,
        }
    },
 
})
var VueParticipantSurvey = new Vue({
    el: '#appParticipantSurvey',
    vuetify: new Vuetify(),
    data() {
        return {
            panel: 0,
            formValid: true,
            formDisabled: false,
            errorMessage: "",
            projId: null,
            defaultResult: {
                projectName: "",
                activity:"",
                gender: "",
                age: "",
                position: "",
                area:"",
                radio_g_1_1: "",
                radio_g_1_2: "",
                radio_g_1_3: "",
                radio_g_2_1: "",
                radio_g_2_2: "",
                radio_g_2_3: "",
                radio_g_3_1: "",
                radio_g_3_2: "",
                radio_g_3_3: "",
                radio_g_4_1: "",
                radio_g_4_2: "",
                radio_g_4_3: "",
                radio_g_5_1: "",
                radio_g_5_2: "",
                radio_g_5_3: "",
                radio_s_1_1: "",
                radio_s_1_2: "",
                radio_s_1_3: "",
                radio_s_2_1: "",
                radio_s_2_2: "",
                radio_s_2_3: "",
                radio_s_3_1: "",
                radio_s_3_2: "",
                radio_s_3_3: "",
                radio_s_4_1: "",
                radio_s_4_2: "",
                radio_s_4_3: "",
                radio_s_5_1: "",
                radio_s_5_2: "",
                radio_s_5_3: "",
            },
            result: {},
        }
    },
    methods: {
        DisplayData() {

            var j = { "ProjID": this.projId, "QNGroup": "EVALUATE" };

            //axios.post(this.VueUrl + 'getqndata', j)
            //    .then(response => {
            //        //console.log(j);
            //        console.log(response.data);

            //        if (response.data != "") {
            //            if (response.data.IsCompleted) {
            //                if (response.data.Data && response.data.Data != "null") {
            //                    this.result = JSON.parse(response.data.Data)
            //                } else {
            //                    this.result = JSON.parse(JSON.stringify(this.defaultResult))
            //                }


            //                window.setTimeout(function () {

            //                }, 300)

            //            } else {
            //                this.result = JSON.parse(JSON.stringify(this.defaultResult))
            //            }

            //        } else {
            //            VueMaster.showSnack("ระบบขัดข้อง โปรดลองอีกครั้ง", "error")
            //        }

            //    }
            //)
            let id = this.projId
            let data = this.$data
            axios.get(this.VueUrl + '/api/projects/GetProjectInformation/' + id)
                .then(response => {
                    console.log(j);
                    console.log(response.data);

                    if (response.data != "") {
                        if (response.data.IsCompleted) {
                            var pinfo = response.data.Data
                            if (response.data.Data && response.data.Data && pinfo.ProjectInfoNameTH) {


                                this.result.projectName = pinfo.ProjectInfoNameTH
                            } else {
                                data.errorMessage = "ไม่พบข้อมูลโครงการ"
                                this.formDisabled = true
                            }


                            window.setTimeout(function () {

                            }, 300)

                        } else {
                            data.errorMessage = response.data.Message[0]
                            this.formDisabled = true
                        }

                    } else {
                        data.errorMessage = "ระบบขัดข้อง โปรดลองอีกครั้ง"
                        this.formDisabled = true
                    }

                }
                )

        },
        SaveData() {
            if (!this.$refs.form.validate()) {
                return
            }
            if (!this.projId) {
                alert("ไม่พบข้อมูลโครงการ (no project id)")
                return
            }
            var j = { "KeyId": this.projId, "DocGroup": "PARTICIPANTSV", IsReported: "0", "Data": JSON.stringify(this.result) }
            //var t = this
            //this.validData()
            //if (this.errors.length > 0) {
            //    this.showSnack("ข้อมูลยังไม่สมบูรณ์ กรุณาตรวจสอบการแจ้งเตือน", "error")

            //    return

            //}

            axios.post(this.VueUrl + '/api/systems/InsertDocument', j)
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
            return window.location.protocol + '//' + window.location.host  ;
        },

    },
    mounted() {
        let searchParams = new URLSearchParams(window.location.search)
        this.projId = searchParams.get('projId')

        if (!this.projId) {
            this.errorMessage = "ไม่ได้ระบุ projId"

            this.formDisabled = true
            return
        }
        let mode = searchParams.get('mode')
        this.formDisabled = true
        if (mode === "edit") {
            this.formDisabled = false

        } else {
            this.formDisabled = true
        }
        this.DisplayData()
    }
})