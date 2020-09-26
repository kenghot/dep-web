var VueContract = new Vue({
    el: '#appContract',
    vuetify: new Vuetify(),
    data() {
        return {
            dialogEditContractNo: false,
            contractNo: "",
            password: "",
            projectId: 0,
        }
    },
    computed: {
        apiUrl: function () {
            return window.location.protocol + '//' + window.location.host + '/api/Systems/';
        },
 
    },
    methods: {
        EditContractNo(id) {
     

            axios.get(this.apiUrl + 'EditContractNo/' + id)
                .then(response => {
                     
                    console.log(response.data);

                    if (response.data !== "") {
                        if (response.data.IsCompleted) {

                            this.contractNo = response.data.Data
                            const that = this
                            window.setTimeout(function () {
                               // that.validData();
                            }, 300)
                            this.projectId = id
                            this.dialogEditContractNo = true;

                        } else {
                            alert(response.data.Message[0])
                        }

                    } else {
                        alert("ระบบขัดข้อง โปรดลองอีกครั้ง")
                    }

                }
                )
        },
        SaveContractNo() {
            var parm = {ContractNo : this.contractNo , Password : this.password}

            axios.post(`${this.apiUrl}SaveContractNo/${this.projectId}`,parm)
                .then(response => {
                    //console.log(j);
                    console.log(response.data);

                    if (response.data !== "") {
                        if (response.data.IsCompleted) {
                            const that = this
                            window.setTimeout(function () {
                                //that.validData();
                            }, 300)
                            this.dialogEditContractNo = false;
                            document.getElementById(this.refreshButton).click();
                        } else {
                            alert(response.data.Message[0])
                        }

                    } else {
                        alert("ระบบขัดข้อง โปรดลองอีกครั้ง")
                    }

                }
                )
        },
    }

})