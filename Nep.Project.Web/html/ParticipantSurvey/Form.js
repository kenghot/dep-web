Vue.component(VueQrcode.name, VueQrcode)

var VueParticipantSVForm = new Vue({
    el: '#appParticipantSVForm',
    vuetify: new Vuetify(),
    data() {
        return {
           
        }
    },
    computed: {
        surveyUrl: function () {
            let searchParams = new URLSearchParams(window.location.search)
            var id = searchParams.get('id')
            return window.location.protocol + '//' + window.location.host + '/html/ParticipantSurvey/participantsurvey.aspx?projId=' + id + '&mode=edit'
        }
    }
})