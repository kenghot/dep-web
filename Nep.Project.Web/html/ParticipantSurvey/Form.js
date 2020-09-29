Vue.component(VueQrcode.name, VueQrcode)

var VueParticipantSVForm = new Vue({
    el: '#appParticipantSVForm',
    vuetify: new Vuetify(),
    data() {
        return {
            projId: null,
            apiHost: '',
            surveyUrl: '',
            surveys: [],
            headers: [
                {
                    text: 'กิจกรรม',
                    value: 'Activity'
                },
                {
                    text: 'วัน เวลา',
                    value: 'CreateDatetime'
                },
                { text: 'รายละเอียด', value: 'actions', sortable: false },
            ]
        }
    },
    computed: {
        VueUrl: function () {
            return window.location.protocol + '//' + window.location.host;
        },

    },
    created() {
        let searchParams = new URLSearchParams(window.location.search)
        this.projId = searchParams.get('id')
        this.apiHost = window.location.protocol + '//' + window.location.host
        this.surveyUrl = this.apiHost + '/html/ParticipantSurvey/participantsurvey.aspx?projId=' + this.projId + '&mode=new'
        this.DisplayData()
    },
    methods: {
        viewSurvey(item) {
            return this.apiHost + '/html/ParticipantSurvey/participantsurvey.aspx?docId=' + item.DocId + '&mode=view'
        },
        DisplayData() {

            let data = this.$data
            this.serveys = []
            axios.get(this.VueUrl + '/api/projects/GetParticipantSurvey/' + this.projId)
                .then(response => {

                    if (response.data != "") {
                        if (response.data.IsCompleted) {
                            var result = response.data.Data
                            console.log(result)
                            if (result) {

                                let obj = result
                                data.surveys = obj.Surveys

                            }


                            window.setTimeout(function () {

                            }, 300)

                        }

                    }
                }
                )



        },
    }
})