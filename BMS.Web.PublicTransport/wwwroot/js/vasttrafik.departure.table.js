
Vue.component('departure-table', {
    props: {
        busStopEndpoint: String,
        departureEndpoint: String,
        busStop: String,
        showCount: String
    },
    data: function () {
        return {
            busStopName: this.busStop,
            busStopUrl: this.busStopEndpoint,
            departureUrl: this.departureEndpoint,
            limit: this.showCount,
            accessToken: 'decade0ffacade', 
            departureTimer: '',
            departureList: [],
            showResult: false,
            serverDate: '',
            serverTime: '',
        }
    },
    template: '#departureTableTemplate',
    methods: {
        updateDepartureTable: function () {
            var url = this.departureEndpoint
                    .replace('{accessToken}', this.accessToken)
                    .replace('{busstopname}', this.busStopName)
                    .replace('{limit}',
                        this.limit != null
                            ? this.limit
                            : 20);
            
            this.$http.get(url).then(function (response) {
                if (response != null && response.data != null && response.data.success && response.data.data != null) {
                    this.accessToken = response.data.data.accessToken;
                    this.departureList = response.data.data.departures;
                    this.serverDate = response.data.data.serverDate;
                    this.serverTime = response.data.data.serverTime;
                    this.showResult = true;
                } else {
                    console.dir(response);

                    this.departureList = [];
                    this.showResult = true;
                }
            }, function (error) {
                console.log(error.statusText);
            });
        }
    },
    created: function () {
        this.departureTimer = setInterval(this.updateDepartureTable, 30 * 1000);
        this.updateDepartureTable();
    },
    beforeDestroy() {
        clearInterval(this.departureTimer);
    }
});

var app = new Vue({
    el: '#departure-table-app'
});