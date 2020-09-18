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
            default: 'success',
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
var VueDashBoard = new Vue({
    el: '#appDashBoard',
    vuetify: new Vuetify(),
    data() {
        return {
            message: "test1234"
        }
    }
})
