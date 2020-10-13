Vue.component('k-radio', {
    template: '#k-radio',
    props: ['topic', 'obj', 'fld', 'viewmode'],
    data() {
        return {
            myVal: this.val,
        }
    },

})
var VueFollowProcessing = new Vue({
    el: '#appFollowProcessing',
    vuetify: new Vuetify(),
    data() {
        return {
            menuDate: false,
            step: "100",
            panel: 0,
            formValid: true,
            formDisabled: false,
            errorMessage: "",
            projId: null,
            docId: null,
            mode: "",
            extend: {
                projectName: "",
                organization: "",
                contractEndDate: "",
                startDate: "",

            },
            defaultResult: {
                rd1_1: "",
                txt1_1_1: "",
                rd2_1: "",
                txt2_1_1: "",
                txt2_1_2: "",
                date2_1: new Date().toISOString().substr(0, 10),
                txt2_1_3: "",
                rd2_2: "",
                txt2_2_1: "",
                txt2_2_2: "",
                rd2_3: "",
                txt2_3_1: "",
                txt2_3_2: "",
                txt2_3_3: "",
                txt2_3_4: "",
                rd2_4: "",
                txt2_4_1: "",
                txt2_4_2: "",
                rd3_1: "",
                txt3_1_1: "",
                txt3_1_2: "",
                rd4_1: "",
                txt4_1_1: "",
                txt4_1_2: "",
                rd5_1: "",
                txt5_1_1: "",
                txt5_1_2: "",
                rd6_1: "",
                txt6_1_1: "",
                txt6_1_2: "",
                rd6_2: "",
                txt6_2_1: "",
                txt6_2_2: "",
                rd7_1: "",
                txt7_1_1: "",
                txt8: "",
                name1: "",
                name2: "",
                name3: "",
            },

            result: {},
        }
    },
    methods: {
        DisplayData() {


            let data = this.$data
           
                axios.get(this.VueUrl + '/api/systems/GetDocumentByKey/' + this.projId + '/FLUPROCESS')
                    .then(response => {
                        console.log(response)
                        if (response.data != "") {
                            if (response.data.IsCompleted) {
                                var result = response.data.Data

                                if (response.data.Data) {

                                    data.result = JSON.parse(result.DATA)
                                    data.projId = result.PROJECTID

                                } else {
                                    data.result = JSON.parse(this.defaultResult)

                                }


                                window.setTimeout(function () {

                                }, 300)

                            } else {
                                data.errorMessage = response.data.Message[0]
                                //this.formDisabled = true
                            }

                        } else {
                            data.errorMessage = "ระบบขัดข้อง โปรดลองอีกครั้ง"
                            //this.formDisabled = true
                        }

                    }
                    )
           


        },
        GetProject() {

            var j = { "ProjID": this.projId, "QNGroup": "EVALUATE" };

            let id = this.projId
            let data = this.$data

            axios.get(this.VueUrl + '/api/projects/GetProjectInformation/' + id)
                .then(response => {


                    if (response.data != "") {
                        if (response.data.IsCompleted) {
                            var pinfo = response.data.Data
                            if (response.data.Data && response.data.Data && pinfo.ProjectInfoNameTH) {
                                data.result.projectName = pinfo.ProjectInfoNameTH
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
                alert("กรุณาระบุข้อมูลให้ถูกต้อง")
                var err = $(".v-messages__message")
                if (err && err.length > 0) {
                    err[0].scrollIntoView()
                }
                return
            }
            if (!this.projId) {
                alert("ไม่พบข้อมูลโครงการ (no project id)")
                return
            }
            var j = { "KeyId": this.projId, "DocGroup": "FLUPROCESS", IsReported: "0", "Data": JSON.stringify(this.result) }
            //var t = this
            //this.validData()
            //if (this.errors.length > 0) {
            //    this.showSnack("ข้อมูลยังไม่สมบูรณ์ กรุณาตรวจสอบการแจ้งเตือน", "error")

            //    return

            //}

            axios.post(this.VueUrl + '/api/systems/SaveDocument', j)
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
            return window.location.protocol + '//' + window.location.host;
        },
        score1: function () {
            return parseInt(this.result.rd1_1) || 0
        },
        score2: function () {
            return (parseInt(this.result.rd2_1) || 0) + (parseInt(this.result.rd2_2) || 0) + (parseInt(this.result.rd2_3) || 0) + (parseInt(this.result.rd2_4) || 0)
        },
        score3: function () {
            return parseInt(this.result.rd3_1) || 0
        },
        score4: function () {
            return parseInt(this.result.rd4_1) || 0
        },
        score5: function () {
            return parseInt(this.result.rd5_1) || 0
        },
        score6: function () {
            return (parseInt(this.result.rd6_1) || 0) + (parseInt(this.result.rd6_2) || 0)
        },
        score7: function () {
            let tot = this.score1 + this.score2 + this.score3 + this.score4 + this.score5 + this.score6
            this.rd7_1 = "0"
            if (tot >= 75) {
                this.result.rd7_1 = "1"
            }
            if (tot >= 50 && tot < 75) {
                this.result.rd7_1 = "2"
            }
            if (tot < 50) {
                this.result.rd7_1 = "3"
            }
            return tot
        },
    },
    mounted() {
        let searchParams = new URLSearchParams(window.location.search)
        this.result = JSON.parse(JSON.stringify(this.defaultResult))
        //console.log(this.result)
        //this.projId = searchParams.get('projId')
        //this.docId = searchParams.get('docId')
        //this.mode = searchParams.get('mode')
        //this.formDisabled = false
        //if (this.mode === "new") {
        //    this.formDisabled = false

        //    if (!this.projId) {
        //        this.errorMessage = "ไม่ได้ระบุ projId"

        //        this.formDisabled = true
        //        return
        //    }

        //} else if (this.mode === "view") {
        //    if (!this.docId) {
        //        this.errorMessage = "ไม่ได้ระบุ docId"

        //        this.formDisabled = true
        //        return
        //    }
        //    this.formDisabled = true
        //}
        //else {
        //    this.errorMessage = "mode ไม่ถูกต้อง"
        //    this.formDisabled = true
        //}
        // this.DisplayData()
    }
})