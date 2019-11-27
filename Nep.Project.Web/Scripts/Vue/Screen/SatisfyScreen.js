var sc = new Vue({
    el: '#divSatisfy',
    data: {
        items: [
            { title: "<b>1. ความพึงพอใจเกี่ยวกับการให้บริการของเจ้าหน้าที่<b>",id: "1_0", hasInput:true }
        ],
        html:"test \n test"
    },
    methods: {
        GenScrren: function () {
            var js = "var screenField = {\n fAll: 0, \n";
            var i = this.items;
            var n = 1;
            i.foreach(function (e) {
                var j = 0
                for (j = 0; j < 5 ; j++) {
                    js += "f" + e.id + "_" + j.toString() + ":" + n.toString() + ",";
                    n++;
                    js += "f" + e.id + "_" + j.toString() + "p:" + n.toString() + ",";
                    n++;
                }

            })
            js += "\n}";
            this.html = js + "\n";
        }
    }
    
})