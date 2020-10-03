Vue.component('base-material-stats-card', {
    template: '#base-material-stats-card',
    data() {
        return {
            title: 'this is a card'
        }
    },
    props: {
        // ...Card.props,
        color: {
            type: String,
        },
        icon: {
            type: String,
            required: true,
        },
        subIcon: {
            type: String,
            default: undefined,
        },
        subIconColor: {
            type: String,
            default: undefined,
        },
        subTextColor: {
            type: String,
            default: undefined,
        },
        subText: {
            type: String,
            default: undefined,
        },
        title: {
            type: String,
            default: undefined,
        },
        value: {
            type: String,
            default: undefined,
        },
        smallValue: {
            type: String,
            default: undefined,
        },
    }
}
)
Vue.component('base-material-card', {
    template: "#base-material-card",
    props: {
        avatar: {
            type: String,
            default: '',
        },
        color: {
            type: String,
            default: '',
        },
        icon: {
            type: String,
            default: undefined,
        },
        image: {
            type: Boolean,
            default: false,
        },
        text: {
            type: String,
            default: '',
        },
        title: {
            type: String,
            default: '',
        },
    },

    computed: {
        classes() {
            return {
                'v-card--material--has-heading': this.hasHeading,
            }
        },
        hasHeading() {
            return Boolean(this.$slots.heading || this.title || this.icon)
        },
        hasAltHeading() {
            return Boolean(this.$slots.heading || (this.title && this.icon))
        },
    },
}
)
Vue.component('line-chart', {
    extends: VueChartJs.Line,
    props: {
        chartdata: {
            type: Object,
            default: null
        },
        options: {
            type: Object,
            default: null
        }
    },
    mounted() {
        this.renderChart(this.chartdata, this.options)
    }
})
Vue.component('polar-chart', {
    extends: VueChartJs.PolarArea,
    props: {
        chartdata: {
            type: Object,
            default: null
        },
        options: {
            type: Object,
            default: null
        }
    },
    mounted() {
        this.renderChart(this.chartdata, this.options)
    }
})
Vue.component('bar-chart', {
    extends: VueChartJs.Bar,
    props: {
        chartdata: {
            type: Object,
            default: null
        },
        options: {
            type: Object,
            default: null
        }
    },
    mounted() {
        this.renderChart(this.chartdata, this.options)
    }
})
var VueDashBoard = new Vue({
    el: '#appDashBoard',
    vuetify: new Vuetify(),
    data() {
        return {
            message: "test1234",
            errorMessage: "",
            lineData1: {
                labels: ['หฟด', 'February', 'March', 'April', 'May', 'June', 'July'],
                datasets: [
                    {
                        label: 'Data One',
                        backgroundColor: [this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor()],
                        data: [40, 39, 10, 40, 39, 80, 90]
                    }
                ]
            },
            lineOption1:
            {
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: true,
                }
            }
        }
    },
    computed: {
        VueUrl: function () {
            return window.location.protocol + '//' + window.location.host;
        },

    },
    methods: {
        randomColor() {
            var letters = '0123456789ABCDEF'.split('');
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.floor(Math.random() * 16)];
            }
            return color;
        },
        DisplayData() {

            var j = { "ProjID": this.projId, "QNGroup": "EVALUATE" };

            var data = this
            axios.get(this.VueUrl + '/api/dashboard/Get/1234')
                .then(response => {
                    console.log(response.data)
                    if (response.data != "") {
                        if (response.data.IsCompleted) {
                            var result = response.data.Data

                            if (response.data.Data) {

                                data.lineData1 = result.projectTypeAmount;
             
                            } else {
                                data.errorMessage = "ไม่พบข้อมูล"
                            
                            }


                            window.setTimeout(function () {

                            }, 300)

                        } else {
                            data.errorMessage = response.data.Message[0]
                         
                        }

                    } else {
                        data.errorMessage = "ระบบขัดข้อง โปรดลองอีกครั้ง"
                         
                    }

                }
                )



        },
    }
})
