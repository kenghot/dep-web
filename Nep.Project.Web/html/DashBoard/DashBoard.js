Vue.component('base-material-stats-card', {
    template: '#base-material-stats-card',
    data() {
        return {
            title: 'this is a card',
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
        objId: {
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
    methods: {
        refreshChart() {
            this.renderChart(this.chartdata, this.options)
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
    methods: {
        refreshChart() {
            this.$refs.bar.renderChart(this.chartdata, this.options)
            this.$refs.polar.renderChart(this.chartdata, this.options)
        }
    },

    mounted() {
        this.renderChart(this.chartdata, this.options)
    }
})
Vue.component('group-chart', {
    template: '#group-chart',
    data() {
        return {
            headers: [
                { text: 'สี', value: 'color' },
                { text: 'รายละเอียด', value: 'description' },
                { text: 'โครงการ', value: 'projects' },
                { text: 'จำนวนเงิน', value: 'amount' }
            ],
        }
    },
    props: {
        title: "",
        chartdata: {
            type: Object,
            default: null
        },
        options: {
            type: Object,
            default: null
        }
    },
    methods: {
        refreshChart() {
            this.$refs.bar.renderChart(this.chartdata, this.options)
            this.$refs.polar.renderChart(this.chartdata, this.options)
        }
    },
})
var VueDashBoard = new Vue({
    el: '#appDashBoard',
    vuetify: new Vuetify(),
    data() {
        return {
            dialog: false,
            projectDialog: {
                title: "",
                items: [],
                headers: [],
                headers1: [
                    { text: "ชื่อโครงการ", value: "ProjectName" },
                    { text: "ชื่อองค์กร", value: "Organization" },
                    { text: "วันที่ยื่นคำร้อง", value: "submitdate", dataType:"Date" },
                    { text: "วันสิ้นสุด", value: "projectendeate", dataType:"Date" },
                    { text: "จังหวัด", value: "ProvinceName" },
                    { text: "สถานะ", value: "ApproveStatus" },
                    {text:"ติดตาม", value: "FollowStatus"},
                ],
                headers2: [
                    { text: "ชื่อองค์กร", value: "Organization" },
                    { text: "จังหวัด", value: "ProvinceName" },
                    { text: "วันที่ยื่นคำร้อง", value: "submitdate", dataType: "Date" },
                    { text: "ชื่อผู้ร้องขอ", value: "FollowStatus" },
                    { text: "สถานะ", value: "ApproveStatus" },
                ],

            },
            budgetYear: 0,
            budgetYears: [],
            errorMessage: "",

            chartData: {},
            dataDefault: {
                projectData: {
                    labels: [],
                    datasets: [
                        {
                            label: 'Data One',
                            backgroundColor: [this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor()],
                            data: [4, 3, 1, 4, 9, 8, 7]
                        }
                    ]
                },
                amountData: {
                    labels: [],
                    datasets: [
                        {
                            label: 'Data One',
                            backgroundColor: [this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor(), this.randomColor()],
                            data: [40, 39, 10, 40, 39, 80, 90]
                        }
                    ]
                },
                legendDatas: [
                    { color: this.randomColor(), description: 'd1', projects: 1, amount: 100 },
                    { color: this.randomColor(), description: 'd2', projects: 2, amount: 200 },
                    { color: this.randomColor(), description: 'd3', projects: 3, amount: 300 },
                    { color: this.randomColor(), description: 'd4', projects: 4, amount: 400 },
                ]
            },

            lineOption1:
            {
                title: {
                    displa: true,
                },
                responsive: true,
                maintainAspectRatio: false,
                legend: {
                    display: false,
                }
            }
        }
    },
    created() {
        var y = new Date().getFullYear()
        this.budgetYear = { BC: y , BE: y   + 543 }
        this.budgetYears = []
        for (i = 0; i < 5; i++) {
            this.budgetYears.push({ BC: y - i ,BE: y - i + 543})
        }
        this.DisplayData()
    },
    computed: {
        VueUrl: function () {
            return window.location.protocol + '//' + window.location.host;
        },

    },
    methods: {
        ShowProjects(o) {
            console.log(o)
            this.projectDialog.headers = []
            if (o.objId === "2") {
                this.projectDialog.headers = [...this.projectDialog.headers2]
            } else {
                this.projectDialog.headers = [...this.projectDialog.headers1]
            }

           
            this.projectDialog.title = o.subText
            var data = this
            axios.get(this.VueUrl + '/api/dashboard/GetProjects/' + this.budgetYear.BC + '/' + o.objId)
                .then(response => {
                    console.log(response.data)
                    if (response.data != "") {
                        if (response.data.IsCompleted) {
                            var result = response.data.Data

                            if (response.data.Data) {
                                //data.summary = result.summary;
                                data.projectDialog.items = result
                                this.dialog = true

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
            axios.get(this.VueUrl + '/api/dashboard/Get/' + this.budgetYear.BC  )
                .then(response => {
                    console.log(response.data)
                    if (response.data != "") {
                        if (response.data.IsCompleted) {
                            var result = response.data.Data

                            if (response.data.Data) {
                                //data.summary = result.summary;
                                data.chartData = result;


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
