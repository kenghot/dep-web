var VueContract = new Vue({
    el: '#appContract',
    vuetify: new Vuetify(),
    data() {
        return {
            dialogEditContractNo: false,
            contractNo: "",
            password: "",
        }
    },
    methods: {
        EditContractNo(id) {
            this.dialogEditContractNo = true
        },
    }

})