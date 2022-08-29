var khProjBG = null;

(function () {
    var khProjectBudget = function () {
        this.KnockoutNumberFormat = function () {
            $('span.numberformat').text(function () {
                
                var str = $(this).html() + '';
              
                x = str.split('.');
                x1 = x[0]; x2 = x.length > 1 ? '.' + x[1] : '';
                var rgx = /(\d+)(\d{3})/;
                while (rgx.test(x1)) {
                    x1 = x1.replace(rgx, '$1' + ',' + '$2');
                }
                $(this).html(x1 + x2);
               
            });
        }
        this.BudgetMessageError = function (msg, span) {
            $(span).text(msg);
        };


        this.limit_error = function (cal, limit, message, id) {
            if (cal > limit) {
                this.BudgetMessageError(message, id);
                return true;
            } else {
                this.BudgetMessageError("", id);
                return false;
            }
        };

        this.x = function (d) {
            if (!d) return 0;
            else return parseFloat(d);
        };
        this.sum123 = function () {
            this.B1_1_1_2();
            this.B1_2();
            this.B1_3_1();
            this.B1_3_2();
        };
        this.B1_1_1_2 = function () {
            try {
                this.BudgetMessageError("", "#message_error_budget1");
                var q = QuestionareModel;
                if (q.BudgetType() != "1") {
                    return true;
                }
                if (q.SelectMealCount() == "2") {
                    q.B1_1_1_2_M_1(0);
                    q.B1_1_1_2_M_2(0);
                    q.B1_1_1_2_M_3(0);
                }

                var B1_1_1_2_M_total = this.x(q.B1_1_1_2_M_1()) * this.x(q.B1_1_1_2_M_2()) * this.x(q.B1_1_1_2_M_3());
                var B1_1_1_2_L_total = this.x(q.B1_1_1_2_L_1()) * this.x(q.B1_1_1_2_L_2()) * this.x(q.B1_1_1_2_L_3());
                var B1_1_1_2_D_total = this.x(q.B1_1_1_2_D_1()) * this.x(q.B1_1_1_2_D_2()) * this.x(q.B1_1_1_2_D_3());

                q.B1_1_1_2_M_total(B1_1_1_2_M_total);
                q.B1_1_1_2_L_total(B1_1_1_2_L_total);
                q.B1_1_1_2_D_total(B1_1_1_2_D_total);
                var gtotal = this.x(q.B1_1_1_2_M_total()) + this.x(q.B1_1_1_2_L_total()) + this.x(q.B1_1_1_2_D_total());
                var total = this.x(q.B1_1_1_2_M_3()) + this.x(q.B1_1_1_2_L_3()) + this.x(q.B1_1_1_2_D_3());
                if (total > 0) {
                    if (q.SelectMealCount() == "") {
                        this.BudgetMessageError("กรุณาเลือกจำนวนมื้อ", "#message_error_budget1");
                        return false;
                    }
                    var Food3Meals = q.Food3Meals();
                    if (Food3Meals == "") {
                        this.BudgetMessageError("กรุณาเลือกสถานที่จัด", "#message_error_budget1");
                        return false;
                    }
                    var limitmeal = 0;
                    if (q.SelectMealCount() == "1") {
                        if (Food3Meals == "1") {
                            limitmeal = 600;
                        } else {
                            limitmeal = 950;
                        }
                    } else {
                        if (Food3Meals == "1") {
                            limitmeal = 400;
                        } else {
                            limitmeal = 700;
                        }
                    }
                    if (total > limitmeal) {
                        this.BudgetMessageError("โปรดกรอกข้อมูลไม่เกิน " + limitmeal + " บาท", "#message_error_budget1");
                        return false;
                    }
                }
                this.BudgetMessageError("", "#message_error_budget1");
                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }

        };
        this.B1_2 = function () {
            try {
                var q = QuestionareModel;
                if (q.Food3Meals() != "1") {
                    q.B1_2_1_1(0); q.B1_2_1_2(0); q.B1_2_1_3(0);
                }
                if (q.Food3Meals() != "2") {
                    q.B1_2_2_1(0); q.B1_2_2_2(0); q.B1_2_2_3(0);
                }
                var B1_2_1_total = this.x(q.B1_2_1_1()) * this.x(q.B1_2_1_2()) * this.x(q.B1_2_1_3());
                var B1_2_2_total = this.x(q.B1_2_2_1()) * this.x(q.B1_2_2_2()) * this.x(q.B1_2_2_3());

                this.limit_error(this.x(q.B1_2_1_3()), 35, "โปรดกรอกข้อมูลไม่เกิน 35 บาท/คน/มื้อ", "#message_error_budget3");
                this.limit_error(this.x(q.B1_2_2_3()), 50, "โปรดกรอกข้อมูลไม่เกิน 50 บาท/คน/มื้อ", "#message_error_budget4");

                q.B1_2_1_total(B1_2_1_total);
                q.B1_2_2_total(B1_2_2_total);
                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }



        };

        this.B1_3_1 = function () {
            try {

                var q = QuestionareModel;
                if (q.Food3Meals() != "2") {
                    q.B1_3_1_1_1(0); q.B1_3_1_1_2(0); q.B1_3_1_1_3(0);
                    q.B1_3_1_2_1(0); q.B1_3_1_2_2(0); q.B1_3_1_2_3(0);
                    q.B1_3_1_3_1(0); q.B1_3_1_3_2(0); q.B1_3_1_3_3(0);
                }
                var B1_3_1_1_total = this.x(q.B1_3_1_1_1()) * this.x(q.B1_3_1_1_2()) * this.x(q.B1_3_1_1_3());
                var B1_3_1_2_total = this.x(q.B1_3_1_2_1()) * this.x(q.B1_3_1_2_2()) * this.x(q.B1_3_1_2_3());
                var B1_3_1_3_total = this.x(q.B1_3_1_3_1()) * this.x(q.B1_3_1_3_2()) * this.x(q.B1_3_1_3_3());

                q.B1_3_1_1_total(B1_3_1_1_total);
                q.B1_3_1_2_total(B1_3_1_2_total);
                q.B1_3_1_3_total(B1_3_1_3_total);

                this.limit_error(this.x(q.B1_3_1_1_3()), 1450, "โปรดกรอกข้อมูลไม่เกิน 1,450 บาท/คน/คืน", "#message_error_budget5");
                this.limit_error(this.x(q.B1_3_1_2_3()), 900, "โปรดกรอกข้อมูลไม่เกิน 900 บาท/คน/คืน", "#message_error_budget6");
                this.limit_error(this.x(q.B1_3_1_3_3()), 500, "โปรดกรอกข้อมูลไม่เกิน 500 บาท/คน/วัน", "#message_error_budget7");

                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }
        };

        this.B1_3_2 = function () {
            try {
                var q = QuestionareModel;

                if (q.Food3Meals() != "1") {
                    q.B1_3_2_1_1(0); q.B1_3_2_1_2(0); q.B1_3_2_1_3(0);
                }

                var B1_3_2_1_total = this.x(q.B1_3_2_1_1()) * this.x(q.B1_3_2_1_2()) * this.x(q.B1_3_2_1_3());

                q.B1_3_2_1_total(B1_3_2_1_total);

                this.limit_error(this.x(q.B1_3_2_1_3()), 300, "โปรดกรอกข้อมูลไม่เกิน 300 บาท/คน/วัน", "#message_error_budget8");
                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }

        };

        this.B1_4_1 = function () {
            try {
                var q = QuestionareModel;

                var B1_4_1_total = this.x(q.B1_4_1_1()) * this.x(q.B1_4_1_2()) * this.x(q.B1_4_1_3());
                q.B1_4_1_total(B1_4_1_total);
                this.limit_error(this.x(q.B1_4_1_2()), 1000, "โปรดกรอกข้อมูลไม่เกิน 1,000 บาท", "#message_error_budget9");

                var B1_4_2_total = this.x(q.B1_4_2_1()) * this.x(q.B1_4_2_2()) * this.x(q.B1_4_2_3());
                q.B1_4_2_total(B1_4_2_total);
                this.limit_error(this.x(q.B1_4_2_2()), 1200, "โปรดกรอกข้อมูลไม่เกิน 1,200 บาท", "#message_error_budget10");

                var B1_4_3_total = this.x(q.B1_4_3_1()) * this.x(q.B1_4_3_2()) * this.x(q.B1_4_3_3());
                q.B1_4_3_total(B1_4_3_total);
                this.limit_error(this.x(q.B1_4_3_2()), 1800, "โปรดกรอกข้อมูลไม่เกิน 1,800 บาท", "#message_error_budget11");

                var B1_4_4_1_total = this.x(q.B1_4_4_1_1()) * this.x(q.B1_4_4_1_2()) * this.x(q.B1_4_4_1_3());
                q.B1_4_4_1_total(B1_4_4_1_total);


                var B1_4_4_2_total = this.x(q.B1_4_4_2_1()) * this.x(q.B1_4_4_2_2()) * this.x(q.B1_4_4_2_3());
                q.B1_4_4_2_total(B1_4_4_2_total);
                this.limit_error(this.x(q.B1_4_4_2_1()), 3, "โปรดกรอกข้อมูลไม่เกิน 3 คน", "#message_error_budget13");


                var B1_4_5_1_total = this.x(q.B1_4_5_1_1()) * this.x(q.B1_4_5_1_2()) * this.x(q.B1_4_5_1_3());
                this.limit_error(this.x(q.B1_4_5_1_2()), 4, "โปรดกรอกข้อมูลไม่เกิน 4 บาท", "#message_error_budget15");
                q.B1_4_5_1_total(B1_4_5_1_total);

                var B1_4_5_2_total = this.x(q.B1_4_5_2_1()) * this.x(q.B1_4_5_2_2()) * this.x(q.B1_4_5_2_3());
                this.limit_error(this.x(q.B1_4_5_2_2()), 2, "โปรดกรอกข้อมูลไม่เกิน 2 บาท", "#message_error_budget16"); 
                q.B1_4_5_2_total(B1_4_5_2_total);


                var total_use_m = B1_4_5_1_total + B1_4_5_2_total;
               // this.limit_error(total_use_m, 1800, "โปรดกรอกข้อมูลไม่เกิน 1,800 บาท", "#message_error_budget14"); 
                this.sum1to13();


            } catch (e) {
                console.log("ERROR: " + e);

            }

        };

        this.B1_5 = function () {
            try {
                var q = QuestionareModel;
                var B1_5_total = this.x(q.B1_5_1()) * this.x(q.B1_5_2()) * this.x(q.B1_5_3());
                q.B1_5_total(B1_5_total);

                this.limit_error(this.x(q.B1_5_3()), 240, "โปรดกรอกข้อมูลไม่เกิน 240 บาท/วัน/คน", "#message_error_budget17");
                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }

        };

        this.B1_6 = function () {
            try {

                var q = QuestionareModel;
                /*6.1.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท)*/

                var B1_6_1_1_total = 0; // this.x(q.B1_6_1_1_1()) * this.x(q.B1_6_1_1_2()) * this.x(q.B1_6_1_1_3());
                var sum =  sumGridData(Vue6_1_1);
                
                B1_6_1_1_total = sum.total;
                q.B1_6_1_1_total(B1_6_1_1_total);
                q.vue_6_1_1(Vue6_1_1.items);
                //if (sum.message != '') {
                    this.limit_error(1, 0, sum.message, "#message_error_budget19");
                //}
                
              //  console.log(this.x(q.B1_6_1_1_2()) / this.x(q.B1_6_1_1_1()));
                //var err = this.limit_error(this.x(q.B1_6_1_1_2()) / this.x(q.B1_6_1_1_1()), 1, "โปรดกรอกข้อมูลไม่เกิน 1 คน/ชั่วโมง", "#message_error_budget18");
                // so confuse >>> จะต้องไม่เกิน 1 คน/ชั่วโมง
               //var err = this.limit_error(this.x(q.B1_6_1_1_1()), 1, "โปรดกรอกข้อมูลไม่เกิน 1 คน/ชั่วโมง", "#message_error_budget18");
              // console.log(err);
                /*6.1.2 วิทยากรภาคเอกชน (ไม่เกินชั่วโมงละ 1,200 บาท) */
                    var B1_6_1_2_total = 0;// this.x(q.B1_6_1_2_1()) * this.x(q.B1_6_1_2_2()) * this.x(q.B1_6_1_2_3());

                    sum = sumGridData(Vue6_1_2);
                    B1_6_1_2_total = sum.total;
                    q.B1_6_1_2_total(B1_6_1_2_total);
                    q.vue_6_1_2(Vue6_1_2.items);
                    this.limit_error(1, 0, sum.message, "#message_error_budget20");
                //q.B1_6_1_2_total(B1_6_1_2_total);
                //this.limit_error(this.x(q.B1_6_1_2_3()), 1200, "โปรดกรอกข้อมูลไม่เกิน 1200 บาท", "#message_error_budget20");
                //if (!err) {
                //    //err = this.limit_error(this.x(q.B1_6_1_2_2()) / this.x(q.B1_6_1_2_1()), 1, "โปรดกรอกข้อมูลไม่เกิน 1 คน/ชั่วโมง", "#message_error_budget18");
                //    err = this.limit_error(this.x(q.B1_6_1_2_1()), 1, "โปรดกรอกข้อมูลไม่เกิน 1 คน/ชั่วโมง", "#message_error_budget18");
                //}
                
                /*6.1.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ
                	  (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี) */
                    var B1_6_1_3_total = 0;// this.x(q.B1_6_1_3_1()) * this.x(q.B1_6_1_3_2()) * this.x(q.B1_6_1_3_3());
                sum = sumGridData(Vue6_1_3);
                B1_6_1_3_total = sum.total;
                q.B1_6_1_3_total(B1_6_1_3_total);
                q.vue_6_1_3(Vue6_1_3.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget20_1");

                //q.B1_6_1_3_total(B1_6_1_3_total);
                //if (!err) {
                //    //this.limit_error(this.x(q.B1_6_1_3_2()) / this.x(q.B1_6_1_3_1()), 1, "โปรดกรอกข้อมูลไม่เกิน 1 คน/ชั่วโมง", "#message_error_budget18");
                //    this.limit_error(this.x(q.B1_6_1_3_1()), 1, "โปรดกรอกข้อมูลไม่เกิน 1 คน/ชั่วโมง", "#message_error_budget18");
                //}
                


                /**6.1 ค่าตอบแทนวิทยากรบรรยาย (จะต้องไม่เกิน 1 คน/ชั่วโมง)  */
                // var lecturer = this.x(q.B1_6_1_1_1()) + this.x(q.B1_6_1_2_1()) + this.x(q.B1_6_1_2_3());
                // var hour = this.x(q.B1_6_1_1_2()) + this.x(q.B1_6_1_2_2()) + this.x(q.B1_6_1_3_2());
                // var total_per_lecturer_hour = hour / lecturer;
                // this.limit_error(total_per_lecturer_hour, 1, "โปรดกรอกข้อมูลไม่เกิน 1 คน/ชั่วโมง", "#message_error_budget18");

                /*
                -------------------------------------
                6.2.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท)*/
                var B1_6_2_1_total = 0; // this.x(q.B1_6_2_1_1()) * this.x(q.B1_6_2_1_2()) * this.x(q.B1_6_2_1_3());
                sum = sumGridData(Vue6_2_1);
                B1_6_2_1_total = sum.total;
                q.B1_6_2_1_total(B1_6_2_1_total);
                q.vue_6_2_1(Vue6_2_1.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget22");

                //q.B1_6_2_1_total(B1_6_2_1_total);
                //this.limit_error(this.x(q.B1_6_2_1_3()), 600, "โปรดกรอกข้อมูลไม่เกิน 600 บาท", "#message_error_budget22");

                /* 6.2.2 วิทยากรภาคเอกชน (ไม่เกินชั่วโมงละ 1,200 บาท)*/
                var B1_6_2_2_total = 0; // this.x(q.B1_6_2_2_1()) * this.x(q.B1_6_2_2_2()) * this.x(q.B1_6_2_2_3());
                sum = sumGridData(Vue6_2_2);
                B1_6_2_2_total = sum.total;
                q.B1_6_2_2_total(B1_6_2_2_total);
                q.vue_6_2_2(Vue6_2_2.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget23");

                //this.limit_error(this.x(q.B1_6_2_2_3()), 1200, "โปรดกรอกข้อมูลไม่เกิน 1200 บาท", "#message_error_budget23");
                //q.B1_6_2_2_total(B1_6_2_2_total);

                /* 6.2.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ
      (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี)*/
                var B1_6_2_3_total = 0;// this.x(q.B1_6_2_3_1()) * this.x(q.B1_6_2_3_2()) * this.x(q.B1_6_2_3_3());
                sum = sumGridData(Vue6_2_3);
                B1_6_2_3_total = sum.total;
                q.B1_6_2_3_total(B1_6_2_3_total);
                q.vue_6_2_3(Vue6_2_3.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget24");


            //    q.B1_6_2_3_total(B1_6_2_3_total);

                /**6.2 ค่าตอบแทนวิทยากรอภิปราย (จะต้องไม่เกิน 5 คน/ชั่วโมง)  */
                /*var lecturer1 = this.x(q.B1_6_2_1_1()) + this.x(q.B1_6_2_2_1()) + this.x(q.B1_6_2_3_1());
                var hour1 = this.x(q.B1_6_2_1_2()) + this.x(q.B1_6_2_2_2()) + this.x(q.B1_6_2_3_2());
                var total_per_lecturer_hour1 = lecturer1 / hour1;*/

               // this.limit_error(total_per_lecturer_hour1, 5, "โปรดกรอกข้อมูลไม่เกิน 5 คน/ชั่วโมง", "#message_error_budget21");
               /* if (this.x(q.B1_6_2_1_1()) > 5 || this.x(q.B1_6_2_2_1()) > 5  || this.x(q.B1_6_2_3_1()) > 5) {
                    this.limit_error(6, 5, "โปรดกรอกข้อมูลไม่เกิน 5 คน/ชั่วโมง", "#message_error_budget21");
                } else {
                    this.limit_error(4, 5, "โปรดกรอกข้อมูลไม่เกิน 5 คน/ชั่วโมง", "#message_error_budget21");
                }*/
                /*6.3.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท)*/
                var B1_6_3_1_total = 0; // this.x(q.B1_6_3_1_1()) * this.x(q.B1_6_3_1_2()) * this.x(q.B1_6_3_1_3()) * this.x(q.B1_6_3_1_4());
                sum = sumGridData(Vue6_3_1);
                B1_6_3_1_total = sum.total;
                q.B1_6_3_1_total(B1_6_3_1_total);
                q.vue_6_3_1(Vue6_3_1.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget26");

                //q.B1_6_3_1_total(B1_6_3_1_total);
                //this.limit_error(this.x(q.B1_6_3_1_3()), 600, "โปรดกรอกข้อมูลไม่เกิน 600 บาท", "#message_error_budget26");

                /*6.3.2 วิทยากรภาคเอกชน (ไม่เกินชั่วโมงละ 1,200 บาท)*/
                var B1_6_3_2_total = 0; // this.x(q.B1_6_3_2_1()) * this.x(q.B1_6_3_2_2()) * this.x(q.B1_6_3_2_3()) * this.x(q.B1_6_3_2_4());
                sum = sumGridData(Vue6_3_2);
                B1_6_3_2_total = sum.total;
                q.B1_6_3_2_total(B1_6_3_2_total);
                q.vue_6_3_2(Vue6_3_2.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget27");

                //q.B1_6_3_2_total(B1_6_3_2_total);
                //this.limit_error(this.x(q.B1_6_3_2_3()), 1200, "โปรดกรอกข้อมูลไม่เกิน 1,200 บาท", "#message_error_budget27");

                // 6.3.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ         (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี)
                var B1_6_3_3_total = 0; // this.x(q.B1_6_3_3_1()) * this.x(q.B1_6_3_3_2()) * this.x(q.B1_6_3_3_3()) * this.x(q.B1_6_3_3_4());
                sum = sumGridData(Vue6_3_3);
                B1_6_3_3_total = sum.total;
                q.B1_6_3_3_total(B1_6_3_3_total);
                q.vue_6_3_3(Vue6_3_3.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget28");

                //q.B1_6_3_3_total(B1_6_3_3_total);
                // 6.3 ค่าตอบแทนวิทยากรกลุ่ม (จะต้องไม่เกินกลุ่มละ 2 คน)

                //var err1 =  this.limit_error(this.x(q.B1_6_3_1_1()), 2, "โปรดกรอกข้อมูลไม่เกิน 2 คน", "#message_error_budget25");
                //if (!err1) { err1 = this.limit_error(this.x(q.B1_6_3_2_1()), 2, "โปรดกรอกข้อมูลไม่เกิน 2 คน", "#message_error_budget25"); }
                //if (!err1) { err1 = this.limit_error(this.x(q.B1_6_3_3_1()), 2, "โปรดกรอกข้อมูลไม่เกิน 2 คน", "#message_error_budget25"); }

                // 6.4.1 วิทยากรฝึกอาชีพทั่วไป (ไม่เกินชั่วโมงละ 400 บาท) 
                var B1_6_4_1_total = 0; // this.x(q.B1_6_4_1_1()) * this.x(q.B1_6_4_1_2()) * this.x(q.B1_6_4_1_3());
                sum = sumGridData(Vue6_4_1);
                B1_6_4_1_total = sum.total;
                q.B1_6_4_1_total(B1_6_4_1_total);
                q.vue_6_4_1(Vue6_4_1.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget29");

                //q.B1_6_4_1_total(B1_6_4_1_total);
                //this.limit_error(this.x(q.B1_6_4_1_3()), 400, "โปรดกรอกข้อมูลไม่เกิน 400 บาท", "#message_error_budget29");

                // 6.4.2 วิทยากรภาคฝึกอาชีพเชี่ยวชาญ
                var B1_6_4_2_total = 0; // this.x(q.B1_6_4_2_1()) * this.x(q.B1_6_4_2_2()) * this.x(q.B1_6_4_2_3());
                sum = sumGridData(Vue6_4_2);
                B1_6_4_2_total = sum.total;
                q.B1_6_4_2_total(B1_6_4_2_total);
                q.vue_6_4_2(Vue6_4_2.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget30");


                //q.B1_6_4_2_total(B1_6_4_2_total);
                //var err2 = this.limit_error(this.x(q.B1_6_4_2_3()), 1200, "โปรดกรอกข้อมูลไม่เกิน 1200 บาท", "#message_error_budget30");
                //if (!err2) { this.limit_error(this.x(q.B1_6_4_2_2()), 12, "โปรดกรอกข้อมูลไม่เกิน 12 ชั่วโมง/หลักสูตร", "#message_error_budget30"); }

                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }

        };

        this.B1_7 = function () {
            try {
                var q = QuestionareModel;
                var B1_7total = 0; // this.x(q.B1_6_4_2_1()) * this.x(q.B1_6_4_2_2()) * this.x(q.B1_6_4_2_3());
                sum = sumGridData(Vue1_7);
                B1_7total = sum.total;
                q.B1_7_total(B1_7total);
                q.vue1_7(Vue1_7.items);
                this.limit_error(1, 0, sum.message, "#message_error_budget31");
                this.sum1to13();
                //var q = QuestionareModel;
                ///*7. ค่าตอบแทนล่ามภาษามือ (ชั่วโมงละ 600 บาท และไม่เกิน 6 ชั่วโมง/วัน)*/
                //var B1_7_total = this.x(q.B1_7_1()) * this.x(q.B1_7_2()) * this.x(q.B1_7_3());
                ////var per_day_B1_7_3 = B1_7_total / B1_7_2;
                //q.B1_7_total(B1_7_total);
                //var err = this.limit_error(this.x(q.B1_7_3()), 600, "โปรดกรอกข้อมูลไม่เกิน 600 บาท", "#message_error_budget31");
                //if (!err) { this.limit_error(this.x(q.B1_7_2()), 6, "โปรดกรอกข้อมูลไม่เกิน 6 ชั่วโมง/วัน", "#message_error_budget31"); }
                //this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }
        };
        this.B1OtherExpense = function () {
            try {
                var q = QuestionareModel;
                var B1OtherExpense = 0; // this.x(q.B1_6_4_2_1()) * this.x(q.B1_6_4_2_2()) * this.x(q.B1_6_4_2_3());
                sum = sumGridData(Vue1_B1OtherExpense);
                B1OtherExpense = sum.total;
                q.B1OtherExpense_total(B1OtherExpense);
                q.vue1_B1OtherExpense(Vue1_B1OtherExpense.items);
                this.limit_error(1, 0, sum.message, "#message_error_B1OtherExpense");
                this.sum1to13();
                //var q = QuestionareModel;
                ///*7. ค่าตอบแทนล่ามภาษามือ (ชั่วโมงละ 600 บาท และไม่เกิน 6 ชั่วโมง/วัน)*/
                //var B1_7_total = this.x(q.B1_7_1()) * this.x(q.B1_7_2()) * this.x(q.B1_7_3());
                ////var per_day_B1_7_3 = B1_7_total / B1_7_2;
                //q.B1_7_total(B1_7_total);
                //var err = this.limit_error(this.x(q.B1_7_3()), 600, "โปรดกรอกข้อมูลไม่เกิน 600 บาท", "#message_error_budget31");
                //if (!err) { this.limit_error(this.x(q.B1_7_2()), 6, "โปรดกรอกข้อมูลไม่เกิน 6 ชั่วโมง/วัน", "#message_error_budget31"); }
                //this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }
        };

        this.B1_8 = function () {
            try {

                var q = QuestionareModel;
                /*8.1 กรณีจัดในโรงแรม (เบิกจ่ายตามจริง เหมาะสมและประหยัด)*/
                var B1_8_1_total = this.x(q.B1_8_1_1()) * this.x(q.B1_8_1_2());
                q.B1_8_1_total(B1_8_1_total);

                /*8.2 กรณีจัดในสถานที่ราชการ*/
                var B1_8_2_total = this.x(q.B1_8_2_1()) * this.x(q.B1_8_2_2());
                q.B1_8_2_total(B1_8_2_total);

                /*8.3.1 ระยะเวลาการดำเนินโครงการ ไม่เกิน 5 วัน (ไม่เกินวันละ 5,000 บาท)*/
                var B1_8_3_1_total = this.x(q.B1_8_3_1_1()) * this.x(q.B1_8_3_1_2());
                q.B1_8_3_1_total(B1_8_3_1_total);

                var err = this.limit_error(this.x(q.B1_8_3_1_1()), 5, "โปรดกรอกข้อมูลไม่เกิน 5 วัน", "#message_error_budget32");
                if (!err) { this.limit_error(this.x(q.B1_8_3_1_2()), 5000, "โปรดกรอกข้อมูลไม่เกิน 5000 บาท", "#message_error_budget32"); }
                //  >8.3.2 ระยะเวลาการดำเนินโครงการ มากกว่า 5 วัน (ให้เหมาจ่ายไม่เกิน 30,000 บาท/โครงการ)
                var B1_8_3_2_total = this.x(q.B1_8_3_2());
                q.B1_8_3_2_total(B1_8_3_2_total);

                this.limit_error(this.x(q.B1_8_3_2()), 30000, "โปรดกรอกข้อมูลไม่เกิน 30000 บาท/โครงการ", "#message_error_budget33");
                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }
        };

        this.B1_9 = function () {
            try {
                var q = QuestionareModel;

                /*9.1 ค่าเช่ารถตู้ปรับอากาศ (ไม่เกิน 1,800 บาท/คัน/วัน)*/
                var B1_9_1_total = this.x(q.B1_9_1_1()) * this.x(q.B1_9_1_2()) * this.x(q.B1_9_1_3());
                q.B1_9_1_total(B1_9_1_total);
                this.limit_error(this.x(q.B1_9_1_3()), 1800, "โปรดกรอกข้อมูลไม่เกิน 1,800 บาท/โครงการ", "#message_error_budget34");

                // 9.2.1 รถบัสแบบพัดลม (ไม่เกินวันละ 5,500 บาท/คัน/วัน)
                var B1_9_2_1_total = this.x(q.B1_9_2_1_1()) * this.x(q.B1_9_2_1_2()) * this.x(q.B1_9_2_1_3());
                q.B1_9_2_1_total(B1_9_2_1_total);
                this.limit_error(this.x(q.B1_9_2_1_3()), 5500, "โปรดกรอกข้อมูลไม่เกิน 5,500 บาท/คัน/วัน", "#message_error_budget35");

                // 9.2.2 รถบัสปรับอากาศ 30 – 32 ที่นั่ง (ไม่เกินวันละ 8,000 บาท/คัน/วัน)
                var B1_9_2_2_total = this.x(q.B1_9_2_2_1()) * this.x(q.B1_9_2_2_2()) * this.x(q.B1_9_2_2_3());
                q.B1_9_2_2_total(B1_9_2_2_total);
                this.limit_error(this.x(q.B1_9_2_2_3()), 8000, "โปรดกรอกข้อมูลไม่เกิน 8,000 บาท/คัน/วัน", "#message_error_budget36");
                // 9.2.3 รถบัสปรับอากาศ VIP 2 ชั้น 40 – 45 ที่นั่ง (ไม่เกินวันละ 12,000 บาท/คัน/วัน)
                var B1_9_2_3_total = this.x(q.B1_9_2_3_1()) * this.x(q.B1_9_2_3_2()) * this.x(q.B1_9_2_3_3());
                q.B1_9_2_3_total(B1_9_2_3_total);
                this.limit_error(this.x(q.B1_9_2_3_3()), 12000, "โปรดกรอกข้อมูลไม่เกิน 12,000 บาท/คัน/วัน", "#message_error_budget37");
                // 9.2.4 รถบัสปรับอากาศ VIP 2 ชั้น 40 – 50 ที่นั่ง (ไม่เกินวันละ 15,000 บาท/คัน/วัน)
                var B1_9_2_4_total = this.x(q.B1_9_2_4_1()) * this.x(q.B1_9_2_4_2()) * this.x(q.B1_9_2_4_3());
                q.B1_9_2_4_total(B1_9_2_4_total);
                this.limit_error(this.x(q.B1_9_2_4_3()), 15000, "โปรดกรอกข้อมูลไม่เกิน 15,000 บาท/คัน/วัน", "#message_error_budget38");

                this.sum1to13();
            } catch (e) {
                console.log("ERROR: " + e);

            }
        };

        this.B1_10 = function () {
            try {

                var q = QuestionareModel;
                /*10. ค่าน้ำมันเชื้อเพลิง (เบิกจ่ายตามจริง)*/
                var B1_10_total = this.x(q.B1_10_1()) * this.x(q.B1_10_2());
                q.B1_10_total(B1_10_total);
                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }
        };

        this.B1_11 = function () {
            try {

                var q = QuestionareModel;

                /*11.1 เอกสารทั่วไป (ไม่เกิน 100 บาท/คน/หลักสูตร)*/
                var B1_11_1_total = this.x(q.B1_11_1_1()) * this.x(q.B1_11_1_2());
                q.B1_11_1_total(B1_11_1_total);
                this.limit_error(this.x(q.B1_11_1_2()), 100, "โปรดกรอกข้อมูลไม่เกิน 100 บาท/คน/หลักสูตร", "#message_error_budget39");

                // 11.2 เอกสารอักษรเบรลล์ (ไม่เกิน 200 บาท/ชุด)
                var B1_11_2_total = this.x(q.B1_11_2_1()) * this.x(q.B1_11_2_2());
                q.B1_11_2_total(B1_11_2_total);
               // this.limit_error(this.x(q.B1_11_2_2()) / this.x(q.B1_11_2_1()), 200, "โปรดกรอกข้อมูลไม่เกิน 200 บาท/ชุด", "#message_error_budget40");
                this.limit_error(this.x(q.B1_11_2_2()) , 200, "โปรดกรอกข้อมูลไม่เกิน 200 บาท/ชุด", "#message_error_budget40");
                // 11.3 เอกสารเสียงหรือซีดี (ไม่เกินแผ่นละ 20 บาท)
                var B1_11_3_total = this.x(q.B1_11_3_1()) * this.x(q.B1_11_3_2());
                q.B1_11_3_total(B1_11_3_total);
                this.limit_error(this.x(q.B1_11_3_2()), 20, "โปรดกรอกข้อมูลไม่เกิน 20 บาท", "#message_error_budget41");
                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }
        };

        //this.B1_12 = function () {
        //    try {

        //        var q = QuestionareModel;
        //        // 12. ค่ากระเป๋าผ้า (ไม่เกิน 80 บาท/ใบ/คน)
        //        var B1_12_total = this.x(q.B1_12_1()) * this.x(q.B1_12_2());
        //        q.B1_12_total(B1_12_total);
        //        this.limit_error(this.x(q.B1_12_2()), 80, "โปรดกรอกข้อมูลไม่เกิน 80 บาท", "#message_error_budget42");
        //        this.sum1to13();

        //    } catch (e) {
        //        console.log("ERROR: " + e);

        //    }
        //};

        this.B1_13 = function () {
            try {
                var q = QuestionareModel;

                // <b>13. ค่าวัสดุฝึกอบรมหรือฝึกอาชีพ (ตามความจำเป็นของแต่ละโครงการ)</b>
                var B1_13_1 = this.x(q.B1_13_1());
                var B1_13_2 = this.x(q.B1_13_2());
                var B1_13_total = B1_13_1 * B1_13_2;
                q.B1_13_total(B1_13_total);
                this.sum1to13();

            } catch (e) {
                console.log("ERROR: " + e);

            }
        };

        this.B1_14 = function () {
            try {

                var q = QuestionareModel;
                // 14.1 ค่าใช้จ่ายในการติดตามหรือประเมินผลโครงการหรือถอดบทเรียน

                var B1_14_1_total = this.x(q.B1_14_1_1()) * this.x(q.B1_14_1_2());
                var B1_14_2_total = this.x(q.B1_14_2_1()) * this.x(q.B1_14_2_2()) * this.x(q.B1_14_2_3());
                this.limit_error(this.x(q.B1_14_2_3()), 300, "โปรดกรอกข้อมูลไม่เกิน 300 บาท", "#message_error_budget46");
                var B1_14_3_total = this.x(q.B1_14_3_1()) * this.x(q.B1_14_3_2()) * this.x(q.B1_14_3_3());
                this.limit_error(this.x(q.B1_14_3_3()), 300, "โปรดกรอกข้อมูลไม่เกิน 300 บาท", "#message_error_budget47");
                var B1_14_4_total = this.x(q.B1_14_4_1()) * this.x(q.B1_14_4_2()) * this.x(q.B1_14_4_3());
                this.limit_error(this.x(q.B1_14_4_3()), 700, "โปรดกรอกข้อมูลไม่เกิน 700 บาท", "#message_error_budget48");
                var B1_14_5_total = this.x(q.B1_14_5_1()) * this.x(q.B1_14_5_2());
                var B1_14_6_total = this.x(q.B1_14_6_1()) * this.x(q.B1_14_6_2());
                var B1_14_7_total = this.x(q.B1_14_7_1()) * this.x(q.B1_14_7_2());
                var B1_14_8_total = this.x(q.B1_14_8_1()) * this.x(q.B1_14_8_2());
               var err = this.limit_error(this.x(q.B1_14_8_1()), 2, "โปรดกรอกข้อมูลไม่เกิน 2 คน/โครงการ", "#message_error_budget49");
               if (!err) { this.limit_error(this.x(q.B1_14_8_2()), 1000, "โปรดกรอกข้อมูลไม่เกิน 1,000 บาท/คน", "#message_error_budget49"); }

                q.B1_14_1_total(B1_14_1_total);
                q.B1_14_2_total(B1_14_2_total);
                q.B1_14_3_total(B1_14_3_total);
                q.B1_14_4_total(B1_14_4_total);
                q.B1_14_5_total(B1_14_5_total);
                q.B1_14_6_total(B1_14_6_total);
                q.B1_14_7_total(B1_14_7_total);
                q.B1_14_8_total(B1_14_8_total);
                if (q.BudgetType() == "1") {
                    this.sum1to13New();
                }
            } catch (e) {
                console.log("ERROR: " + e);

            }
        };

        this.sum1to13 = function () {

            var q = QuestionareModel;
            if (q.BudgetType() == "1") {
                //console.log("function sum1to13");
                var total;

                total = this.x(q.B1_1_1_2_M_total()) + this.x(q.B1_1_1_2_L_total()) + this.x(q.B1_1_1_2_D_total()) + this.x(q.B1_2_1_total()) + this.x(q.B1_2_2_total()) + this.x(q.B1_3_1_1_total()) + this.x(q.B1_3_1_2_total()) + this.x(q.B1_3_1_3_total()) + this.x(q.B1_3_2_1_total()) + this.x(q.B1_4_1_total()) + this.x(q.B1_4_2_total()) + this.x(q.B1_4_3_total()) + this.x(q.B1_4_4_1_total()) + this.x(q.B1_4_5_1_total()) + this.x(q.B1_4_4_2_total()) + this.x(q.B1_4_5_2_total()) + this.x(q.B1_5_total()) + this.x(q.B1_6_1_1_total()) + this.x(q.B1_6_1_2_total()) + this.x(q.B1_6_1_3_total()) + this.x(q.B1_6_2_1_total()) + this.x(q.B1_6_2_2_total()) + this.x(q.B1_6_3_1_total()) + this.x(q.B1_6_3_2_total()) + this.x(q.B1_6_3_3_total()) + this.x(q.B1_6_4_1_total()) + this.x(q.B1_6_4_2_total()) + this.x(q.B1_7_total()) + this.x(q.B1_8_1_total()) + this.x(q.B1_8_2_total()) + this.x(q.B1_8_3_1_total()) + this.x(q.B1_9_1_total()) + this.x(q.B1_9_2_1_total()) + this.x(q.B1_9_2_2_total()) + this.x(q.B1_9_2_3_total()) + this.x(q.B1_9_2_4_total()) + this.x(q.B1_10_total()) + this.x(q.B1_11_1_total()) + this.x(q.B1_11_2_total()) + this.x(q.B1_11_3_total()) + this.x(q.B1_13_total()) + this.x(q.B1_13_other()) + this.x(q.B1OtherExpense_total());

             //   var totalMore10Per = this.x(q.B1_14_1_total()) + this.x(q.B1_14_2_total()) + this.x(q.B1_14_3_total()) + this.x(q.B1_14_4_total()) + this.x(q.B1_14_5_total()) + this.x(q.B1_14_6_total()) + this.x(q.B1_14_7_total()) + this.x(q.B1_14_8_total()) + this.x(q.B1_14_9_other());

                // var textbox = $('input[name*=TextBoxTotalAmount]');
                // textbox.val(total);
                //sum1to13
                var cal = total; // + totalMore10Per;
                var finish_total = this.chkNum(cal);
               // console.log("cal : " + cal + "finish : " + finish_total);
                var textbox = $('input[name*=TextBoxTotalAmount]');
                var span = $('span[id*=LabelTotalBudgetAmount]');
                textbox.val(finish_total);
                span.text(finish_total);

              //  var sum1to13 = $('span[id*=sum1to13]');
               // var cal10per = $('span[id*=cal10per]');
               // var total14all = $('span[id*=total14all]');
               // sum1to13.text(this.chkNum(total));
               // cal10per.text(this.chkNum((total * 10) / 100));
                //total14all.text(this.chkNum(totalMore10Per));
               // this.limit_error(totalMore10Per, (total * 10) / 100, "(ค่าบริหารโครงการเกิน 10% ของค่าใช้จ่ายทั้งหมดของโครงการ)", "#message_error_budget45");
               // console.log("Total : " + total + " limit  10% : " + (total * 10) / 100 + " total choose 14 have : " + totalMore10Per);
            }
        };
        this.sum1to13New = function () {

            var q = QuestionareModel;
            if (q.BudgetType() == "1" && $('#tabManageBudget')[0].style.display != 'none') {
                //console.log("function sum1to13");
                var total;

                total = this.x(q.B1_1_1_2_M_total()) + this.x(q.B1_1_1_2_L_total()) + this.x(q.B1_1_1_2_D_total()) + this.x(q.B1_2_1_total()) + this.x(q.B1_2_2_total()) + this.x(q.B1_3_1_1_total()) + this.x(q.B1_3_1_2_total()) + this.x(q.B1_3_1_3_total()) + this.x(q.B1_3_2_1_total()) + this.x(q.B1_4_1_total()) + this.x(q.B1_4_2_total()) + this.x(q.B1_4_3_total()) + this.x(q.B1_4_4_1_total()) + this.x(q.B1_4_5_1_total()) + this.x(q.B1_4_4_2_total()) + this.x(q.B1_4_5_2_total()) + this.x(q.B1_5_total()) + this.x(q.B1_6_1_1_total()) + this.x(q.B1_6_1_2_total()) + this.x(q.B1_6_1_3_total()) + this.x(q.B1_6_2_1_total()) + this.x(q.B1_6_2_2_total()) + this.x(q.B1_6_3_1_total()) + this.x(q.B1_6_3_2_total()) + this.x(q.B1_6_3_3_total()) + this.x(q.B1_6_4_1_total()) + this.x(q.B1_6_4_2_total()) + this.x(q.B1_7_total()) + this.x(q.B1_8_1_total()) + this.x(q.B1_8_2_total()) + this.x(q.B1_8_3_1_total()) + this.x(q.B1_9_1_total()) + this.x(q.B1_9_2_1_total()) + this.x(q.B1_9_2_2_total()) + this.x(q.B1_9_2_3_total()) + this.x(q.B1_9_2_4_total()) + this.x(q.B1_10_total()) + this.x(q.B1_11_1_total()) + this.x(q.B1_11_2_total()) + this.x(q.B1_11_3_total())  + this.x(q.B1_13_total()) + this.x(q.B1_13_other());

                var totalMore10Per = this.x(q.B1_14_1_total()) + this.x(q.B1_14_2_total()) + this.x(q.B1_14_3_total()) + this.x(q.B1_14_4_total()) + this.x(q.B1_14_5_total()) + this.x(q.B1_14_6_total()) + this.x(q.B1_14_7_total()) + this.x(q.B1_14_8_total());
                var other = 0;
                var isError = false;
                var t = this;
                if (!appVueOther.items) {
                    appVueOther.items = [];
                }
                    q.other_expenses(appVueOther.items)
                    appVueOther.items.forEach(function (i) {
                        other += i.amount;
                        if (i.amount <= 0 || i.detail =="") {
                           isError = t.limit_error(1,0, "ระบุค่าใช้จ่ายอื่นๆ ไม่ครบ", "#message_error_budget45");
                        }
                        
                    })
              
                totalMore10Per = totalMore10Per + other;
                // var textbox = $('input[name*=TextBoxTotalAmount]');
                // textbox.val(total);
                //sum1to13
                var cal = total + totalMore10Per;
                var finish_total = this.chkNum(cal);
                // console.log("cal : " + cal + "finish : " + finish_total);
                var textbox = $('input[name*=TextBoxTotalAmount]');
                var span = $('span[id*=LabelTotalBudgetAmount]');
                textbox.val(finish_total);
                span.text(finish_total);
                q.total14all(totalMore10Per.toFixed(2).toString());

                //var sum1to13 = $('span[id*=sum1to13]');
                //var cal10per = $('span[id*=cal10per]');
                var total14all = $('span[id*=total14all]');
                //sum1to13.text(this.chkNum(total));
                //cal10per.text(this.chkNum((total * 10) / 100));
                total14all.text(this.chkNum(totalMore10Per));
                if (!isError) {
                   // console.log(totalMore10Per); console.log(q.cal10per());
                    var chk = q.cal10per().replace(/[^\d\.\-]/g, "");
                   //console.log(chk);
                    this.limit_error(totalMore10Per, this.x(chk), "(ค่าบริหารโครงการเกิน 10% ของค่าใช้จ่ายทั้งหมดของโครงการ)", "#message_error_budget45");
                }
                
                //console.log("Total : " + total + " limit  10% : " + (total * 10) / 100 + " total choose 14 have : " + totalMore10Per);
            }
        };
        this.chkNum = function (ele) {
            var num = parseFloat(ele);
            ele = num.toFixed(2);
            return this.numberWithCommas(ele);
        };
        this.numberWithCommas = function (x) {
            return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        };

        this.Food3Meals1 = function () {
            //var q = QuestionareModel;
            //var e = q.Food3Meals();
            //var m3 = $('input[id=B1_1_1_2_M_3]');
            //if (e == 1) {
            //    m3.attr("max", "200");
            //}
            //console.log("TEST RADIO : " + e + " : B1_1_1_2_M_3 : " + m3.val());
            this.B1_1_1_2();
        };

        // var Food3Meals = $("input[name='Food3Meals']");
        // Food3Meals.change(function(){

        //     console.log("TEST RADIO :"+ Food3Meals.val());
        // });


        this.RadioBudgetClick = function () {
            var q = QuestionareModel;
            if (q.BudgetType() == "1") {
                this.totalCallFunction();
                this.sum1to13();
            } else {
                if (q.SupportBudgetType() == "1") {
                    this.B21();
                } else {
                    this.B22();
                }
            }
        };
        this.totalCallFunction = function () {
            this.B1_1_1_2();
            //this.B1_1_2_2();
            this.B1_2();
            this.B1_3_1();
            this.B1_3_2();
            this.B1_4_1();
            this.B1_5();
            this.B1_6();
            this.B1_7();
            this.B1_8();
            this.B1_9();
            this.B1_10();
            this.B1_11();
            //this.B1_12();
            this.B1_13();
            this.B1OtherExpense();
            this.B1_14();
            

        };
        this.CheckProjectBudgetValidate = function () {
            var isValid = true;
            $("#spanValidate").text("");
            var q = QuestionareModel;
            if (q.BudgetType() == "1") {
                this.totalCallFunction();
                this.sum1to13();
                this.sum1to13New();
                $("span[id^='message_error_budget']").each(function () {
                    if (this.innerHTML != "") {
                        isValid = false;
                        // return false;
                    }
                });
            } else {
                if (q.SupportBudgetType() == "1") {
                    this.B21();
                    $("span[id^='b21_message_error']").each(function () {
                        if (this.innerHTML != "") {
                            isValid = false;
                            // return false;
                        }
                    });
                } else {
                    this.B22();
                    $("span[id^='b22_message_error']").each(function () {
                        if (this.innerHTML != "") {
                            isValid = false;
                            // return false;
                        }
                    });
                }
            }


            if (!isValid) {
                //$("#spanValidate").text("ข้อมูลที่ระบุยังไม่ถูกต้อง กรุณาตรวจสอบ");
                alert("ข้อมูลที่ระบุยังไม่ถูกต้อง กรุณาตรวจสอบอีกครั้ง");
                // location.hash = '#' + anchorValidate;
            }
            return isValid;
        };

        this.B21 = function () {

            try {
                var q = QuestionareModel;
                this.limit_error(this.x(q.B21_1_1()), 300000, "โปรดกรอกข้อมูลไม่เกิน 300000 บาท", "#b21_message_error1");
                this.limit_error(this.x(q.B21_1_2()), 5000, "โปรดกรอกข้อมูลไม่เกิน 5000 บาท", "#b21_message_error2");
                //จ่ายตามจริงแต่ไม่เกิน 2,000 บาท/เดือน 
                // var chk_B21_2_1 = this.x(q.B21_2_1_1()) * this.x(q.B21_2_1_2());
                // var B21_2_1_total = chk_B21_2_1 >= 1200 ? this.limit_error(chk_B21_2_1, 2000, "โปรดกรอกข้อมูลไม่เกิน 400 บาท", "#b21_message_error1") : chk_B21_2_1;

                this.limit_error(this.x(q.B21_2_1_1()), 2000, "โปรดกรอกข้อมูลไม่เกิน 2000 บาท", "#b21_message_error3");
                var B21_2_1_total = this.x(q.B21_2_1_1()) * this.x(q.B21_2_1_2());
                //อัตราค่าจ้างขั้นต่ำของแต่ละพื้นที่ตามประกาศ 
                this.limit_error(this.x(q.B21_2_2_1()), 9000, "โปรดกรอกข้อมูลไม่เกิน 9000 บาท", "#b21_message_error4");
                var B21_2_2_total = this.x(q.B21_2_2_1()) * this.x(q.B21_2_2_2()) * this.x(q.B21_2_2_3());
                //คณะกรรมการค่าจ้างค่าอาหารกลางวัน ห้ามเกิน 120
                this.limit_error(this.x(q.B21_2_3_1_1()), 120, "โปรดกรอกข้อมูลไม่เกิน 120 บาท", "#b21_message_error5");
                var B21_2_3_1_total = this.x(q.B21_2_3_1_1()) * this.x(q.B21_2_3_1_2()) * this.x(q.B21_2_3_1_3());
                //ค่าอาหารว่างและเครื่องดื่ม
                this.limit_error(this.x(q.B21_2_3_2_1()), 35, "โปรดกรอกข้อมูลไม่เกิน 35 บาท", "#b21_message_error6");
                var B21_2_3_2_total = this.x(q.B21_2_3_2_1()) * this.x(q.B21_2_3_2_2()) * this.x(q.B21_2_3_2_3());
                //ค่าพาหนะเดินทาง (เบิกจ่ายตามจริง)
                this.limit_error(this.x(q.B21_2_3_3_1()), 800, "โปรดกรอกข้อมูลไม่เกิน 800 บาท", "#b21_message_error7");
                var B21_2_3_3_total = this.x(q.B21_2_3_3_1()) * this.x(q.B21_2_3_3_2()) * this.x(q.B21_2_3_3_3());
                //ค่าวัสดุอุปกรณ์สำนักงาน
                //10,000 บาท/ปี (ขอสนับสนุนครบ 12 เดือน)
                var B21_2_4_total = this.x(q.B21_2_4_1()) * this.x(q.B21_2_4_2());
                var year = Math.ceil(q.B21_2_4_2() / 12);
                var base = 10000 * year;
                var err2_4 = this.limit_error(this.x(q.B21_2_4_2()), 12, "โปรดกรอกข้อมูลไม่เกิน 12 เดือน", "#b21_message_error8");
                if (!err2_4) { this.limit_error(this.x(B21_2_4_total), 10000, "โปรดกรอกข้อมูลไม่เกิน 10,000 บาท", "#b21_message_error8"); }
               
                //หมวดที่ 3 การจัดบริการตามภารกิจของศูนย์บริการคนพิการทั่วไป
                //1.การประเมินศักยภาพคนพิการ และทำแผนพัฒนาศักยภาพ คนพิการรายบุคคล ก่อนการจัดบริการ
                var B21_3_1_total = this.x(q.B21_3_1_1()) * this.x(q.B21_3_1_2()) * this.x(q.B21_3_1_3());
                this.limit_error(this.x(q.B21_3_1_1()), 2000, "โปรดกรอกข้อมูลไม่เกิน 2000 บาท", "#b21_message_error9");
                //2.การฝึกทักษะด้านการทำ ความคุ้นเคยกับสภาพแวดล้อม และการเคลื่อนไหว (Orientation & Mobility : O&M) สำหรับ คนพิการทางการเห็น
                var B21_3_2_total = this.x(q.B21_3_2_1()) * this.x(q.B21_3_2_2());
                this.limit_error(this.x(q.B21_3_2_1()), 9000, "โปรดกรอกข้อมูลไม่เกิน 9000 บาท", "#b21_message_error10");
                //3.การบริการผู้ช่วยคนพิการ
                var B21_3_3_total = this.x(q.B21_3_3_1()) * this.x(q.B21_3_3_2()) * this.x(q.B21_3_3_3());
                this.limit_error(this.x(q.B21_3_3_1()), 300, "โปรดกรอกข้อมูลไม่เกิน 300 บาท", "#b21_message_error11");
                //การบริการล่ามภาษามือ
                var B21_3_4_total = this.x(q.B21_3_4_1()) * this.x(q.B21_3_4_2()) * this.x(q.B21_3_4_3()) * this.x(q.B21_3_4_4());
                this.limit_error(this.x(q.B21_3_4_1()), 600, "โปรดกรอกข้อมูลไม่เกิน 600 บาท", "#b21_message_error12");
                //การฟื้นฟูสมรรถภาพ ทางด้านร่างกาย
                var B21_3_6_total = this.x(q.B21_3_6_1()) * this.x(q.B21_3_6_2()) * this.x(q.B21_3_6_3());
                this.limit_error(this.x(q.B21_3_6_1()), 150, "โปรดกรอกข้อมูลไม่เกิน 150 บาท", "#b21_message_error13");
                //การพัฒนาทักษะการช่วยเหลือตนเอง
                //รายบุคคล
                var B21_3_7_1_total = this.x(q.B21_3_7_1_1()) * this.x(q.B21_3_7_1_2()) * this.x(q.B21_3_7_1_2());
                this.limit_error(this.x(q.B21_3_7_1_1()), 150, "โปรดกรอกข้อมูลไม่เกิน 150 บาท", "#b21_message_error14");
                //รายกลุ่ม
                var B21_3_7_2_total = this.x(q.B21_3_7_2_1()) * this.x(q.B21_3_7_2_2()) * this.x(q.B21_3_7_2_3());
                this.limit_error(this.x(q.B21_3_7_2_1()), 75, "โปรดกรอกข้อมูลไม่เกิน 75 บาท", "#b21_message_error15");
                //การพัฒนาทักษะทางการพูด
                //รายบุคคล
                var B21_3_8_1_total = this.x(q.B21_3_8_1_1()) * this.x(q.B21_3_8_1_2()) * this.x(q.B21_3_8_1_3());
                this.limit_error(this.x(q.B21_3_8_1_1()), 150, "โปรดกรอกข้อมูลไม่เกิน 150 บาท", "#b21_message_error16");
                //รายกลุ่ม
                var B21_3_8_2_total = this.x(q.B21_3_8_2_1()) * this.x(q.B21_3_8_2_2()) * this.x(q.B21_3_8_2_3());
                this.limit_error(this.x(q.B21_3_8_2_1()), 75, "โปรดกรอกข้อมูลไม่เกิน 75 บาท", "#b21_message_error17");
                //การพัฒนาสู่สุขภาวะ
                //รายบุคคล
                var B21_3_9_1_total = this.x(q.B21_3_9_1_1()) * this.x(q.B21_3_9_1_2()) * this.x(q.B21_3_9_1_3());
                this.limit_error(this.x(q.B21_3_9_1_1()), 150, "โปรดกรอกข้อมูลไม่เกิน 150 บาท", "#b21_message_error18");
                // รายกลุ่ม
                var B21_3_9_2_total = this.x(q.B21_3_9_2_1()) * this.x(q.B21_3_9_2_2()) * this.x(q.B21_3_9_2_3());
                this.limit_error(this.x(q.B21_3_9_2_1()), 75, "โปรดกรอกข้อมูลไม่เกิน 75 บาท", "#b21_message_error19");
                //การปรับพฤติกรรม
                //รายบุคคล
                var B21_3_10_1_total = this.x(q.B21_3_10_1_1()) * this.x(q.B21_3_10_1_2()) * this.x(q.B21_3_10_1_3());
                this.limit_error(this.x(q.B21_3_10_1_1()), 300, "โปรดกรอกข้อมูลไม่เกิน 300 บาท", "#b21_message_error20");
                //รายกลุ่ม
                var B21_3_10_2_total = this.x(q.B21_3_10_2_1()) * this.x(q.B21_3_10_2_2()) * this.x(q.B21_3_10_2_3());
                this.limit_error(this.x(q.B21_3_10_2_1()), 150, "โปรดกรอกข้อมูลไม่เกิน 150 บาท", "#b21_message_error21");
                //การพัฒนาทักษะทางการได้ยิน
                //รายบุคคล
                var B21_3_11_total = this.x(q.B21_3_11_1()) * this.x(q.B21_3_11_2()) * this.x(q.B21_3_11_3());
                this.limit_error(this.x(q.B21_3_11_1()), 150, "โปรดกรอกข้อมูลไม่เกิน 150 บาท", "#b21_message_error22");
                //การพัฒนาทักษะทางการเห็น
                // รายบุคคล//
                var B21_3_12_1_total = this.x(q.B21_3_12_1_1()) * this.x(q.B21_3_12_1_2()) * this.x(q.B21_3_12_1_3());
                this.limit_error(this.x(q.B21_3_12_1_1()), 150, "โปรดกรอกข้อมูลไม่เกิน 150 บาท", "#b21_message_error23");
                // รายกลุ่ม
                var B21_3_12_2_total = this.x(q.B21_3_12_2_1()) * this.x(q.B21_3_12_2_2()) * this.x(q.B21_3_12_2_3());
                this.limit_error(this.x(q.B21_3_12_2_1()), 75, "โปรดกรอกข้อมูลไม่เกิน 75 บาท", "#b21_message_error24");
                // การเสริมสร้างพัฒนาการ
                // รายบุคคล
                var B21_3_13_1_total = this.x(q.B21_3_13_1_1()) * this.x(q.B21_3_13_1_2()) * this.x(q.B21_3_13_1_3());
                this.limit_error(this.x(q.B21_3_13_1_1()), 150, "โปรดกรอกข้อมูลไม่เกิน 150 บาท", "#b21_message_error25");
                // รายกลุ่ม
                var B21_3_13_2_total = this.x(q.B21_3_13_2_1()) * this.x(q.B21_3_13_2_2()) * this.x(q.B21_3_13_2_3());
                this.limit_error(this.x(q.B21_3_13_2_1()), 75, "โปรดกรอกข้อมูลไม่เกิน 75 บาท", "#b21_message_error26");
                // บริการกายอุปกรณ์<br />- รถโยกคนพิการ<br />- ไม้เท้าขาว</
                // /ตามระเบียบที่รัฐมนตรีว่าการกระทรวง พม. ประกาศกำหนด
                var B21_3_14_total = this.x(q.B21_3_14_1()) * this.x(q.B21_3_14_2());
                
                var B21_3_14_1_total = this.x(q.B21_3_14_1_1()) * this.x(q.B21_3_14_2_1());

                this.limit_error(this.x(q.B21_3_14_1_1()), 200, "โปรดกรอกข้อมูลไม่เกิน 200 บาท", "#b21_message_error27");
                // B21_3_15_1
                var B21_3_15_total = this.x(q.B21_3_15_1()) * this.x(q.B21_3_15_2());
                this.limit_error(this.x(q.B21_3_15_1()), 300, "โปรดกรอกข้อมูลไม่เกิน 300 บาท", "#b21_message_error28");
                //B21_3_16_1_1
                // รถจักรยานยนต์ 
                var B21_3_16_1_total = this.x(q.B21_3_16_1_1()) * this.x(q.B21_3_16_1_2());
                this.limit_error(this.x(q.B21_3_16_1_1()), 2, "โปรดกรอกข้อมูลไม่เกิน 2 บาท", "#b21_message_error29");
                // รถยนต์
                var B21_3_16_2_total = this.x(q.B21_3_16_2_1()) * this.x(q.B21_3_16_2_2());
                this.limit_error(this.x(q.B21_3_16_2_1()), 4, "โปรดกรอกข้อมูลไม่เกิน 4 บาท", "#b21_message_error30");



                var total = this.x(q.B21_1_1()) + this.x(q.B21_1_2()) + this.x(B21_2_1_total) + this.x(B21_2_2_total) + this.x(B21_2_3_1_total) +
                    this.x(B21_2_3_2_total) + this.x(B21_2_3_3_total) + this.x(B21_2_4_total) + this.x(B21_3_1_total) + this.x(B21_3_2_total) +
                    this.x(B21_3_3_total) + this.x(B21_3_4_total) + this.x(q.B21_3_5()) + this.x(B21_3_6_total) + this.x(B21_3_7_1_total) +
                    this.x(B21_3_7_2_total) + this.x(B21_3_8_1_total) + this.x(B21_3_8_2_total) + this.x(B21_3_9_1_total) + this.x(B21_3_9_2_total) +
                    this.x(B21_3_10_1_total) + this.x(B21_3_10_2_total) + this.x(B21_3_11_total) + this.x(B21_3_12_1_total) + this.x(B21_3_12_2_total) +
                    this.x(B21_3_13_1_total) + this.x(B21_3_13_2_total) + this.x(B21_3_14_total) + this.x(B21_3_14_1_total) + this.x(B21_3_15_total) + this.x(B21_3_16_1_total) + this.x(B21_3_16_2_total);


                var textbox = $('input[name*=TextBoxTotalAmount]');
                var span = $('span[id*=LabelTotalBudgetAmount]');
                textbox.val(this.chkNum(total));
                span.text(this.chkNum(total));

            } catch (e) {
                console.log("ERROR: " + e);
            }
        };

        this.B22 = function () {

            try {
                var q = QuestionareModel;
                this.limit_error(this.x(q.B22_1_1()), 300000, "โปรดกรอกข้อมูลไม่เกิน 300,000 บาท", "#b22_message_error1");
                this.limit_error(this.x(q.B22_1_2()), 5000, "โปรดกรอกข้อมูลไม่เกิน 5,000 บาท", "#b22_message_error2");
                //ค่าอาหารกลางวัน ห้ามเกิน 120
                this.limit_error(this.x(q.B22_2_3_1_1()), 120, "โปรดกรอกข้อมูลไม่เกิน 120 บาท", "#b22_message_error3");
                var B22_2_3_1_total = this.x(q.B22_2_3_1_1()) * this.x(q.B22_2_3_1_2()) * this.x(q.B22_2_3_1_3());
                //ค่าอาหารว่างและเครื่องดื่ม
                this.limit_error(q.B22_2_3_2_1(), 35, "โปรดกรอกข้อมูลไม่เกิน 35 บาท", "#b22_message_error4");
                var B22_2_3_2_total = this.x(q.B22_2_3_2_1()) * this.x(q.B22_2_3_2_2()) * this.x(q.B22_2_3_2_3());
                //ค่าพาหนะเดินทาง (เบิกจ่ายตามจริง)
                this.limit_error(q.B22_2_3_3_1(), 800, "โปรดกรอกข้อมูลไม่เกิน 800 บาท", "#b22_message_error5");
                var B22_2_3_3_total = this.x(q.B22_2_3_3_1()) * this.x(q.B22_2_3_3_2()) * this.x(q.B22_2_3_3_3());
                //ค่าวัสดุอุปกรณ์สำนักงาน
                //10,000 บาท/ปี (ขอสนับสนุนครบ 12 เดือน)
                var B22_2_4_total = this.x(q.B22_2_4_1()) * this.x(q.B22_2_4_2());
                var year = Math.ceil(q.B22_2_4_2() / 12);
                var base = 10000 * year;
                //this.limit_error(this.x(B22_2_4_total), this.x(base), "โปรดกรอกข้อมูลไม่เกิน " + base + " บาท", "#b22_message_error6");
                var err2_2_4 = this.limit_error(this.x(q.B22_2_4_2()), 12, "โปรดกรอกข้อมูลไม่เกิน 12 เดือน", "#b22_message_error6");
                if (!err2_2_4) { this.limit_error(this.x(B22_2_4_total), 10000, "โปรดกรอกข้อมูลยอดรวมไม่เกิน 10,000 บาท", "#b22_message_error6"); }
                //3.การบริการผู้ช่วยคนพิการ
                var B22_3_3_total = this.x(q.B22_3_3_1()) * this.x(q.B22_3_3_2()) * this.x(q.B22_3_3_3());
                this.limit_error(this.x(q.B22_3_3_1()), 300, "โปรดกรอกข้อมูลไม่เกิน 300 บาท", "#b22_message_error7");
                //การบริการล่ามภาษามือ
                var B22_3_4_total = this.x(q.B22_3_4_1()) * this.x(q.B22_3_4_2()) * this.x(q.B22_3_4_3()) * this.x(q.B22_3_4_4());
                this.limit_error(this.x(q.B22_3_4_1()), 600, "โปรดกรอกข้อมูลไม่เกิน 600 บาท", "#b22_message_error8");
                //B22_3_14_1
                var B22_3_14_total = this.x(q.B22_3_14_1()) * this.x(q.B22_3_14_2());
                var B22_3_14_1_total = this.x(q.B22_3_14_1_1()) * this.x(q.B22_3_14_2_1());
                this.limit_error(this.x(q.B22_3_14_1_1()), 200, "โปรดกรอกข้อมูลไม่เกิน 200 บาท", "#b22_message_error9");
                //B22_3_16_1_1
                //รถจักรยานยนต์
                var B22_3_16_1_total = this.x(q.B22_3_16_1_1()) * this.x(q.B22_3_16_1_2());
                this.limit_error(this.x(q.B22_3_16_1_1()), 2, "โปรดกรอกข้อมูลไม่เกิน 2 บาท", "#b22_message_error10");
                //รถยนต์
                var B22_3_16_2_total = this.x(q.B22_3_16_2_1()) * this.x(q.B22_3_16_2_2());
                this.limit_error(this.x(q.B22_3_16_2_1()), 4, "โปรดกรอกข้อมูลไม่เกิน 4 บาท", "#b22_message_error11");

                var total = this.x(q.B22_1_1()) + this.x(q.B22_1_2()) + this.x(B22_2_3_1_total) + this.x(B22_2_3_2_total) + this.x(B22_2_3_3_total) +
                    this.x(B22_2_4_total) + this.x(B22_3_3_total) + this.x(B22_3_4_total) + this.x(q.B22_3_5()) + this.x(B22_3_14_total) + this.x(B22_3_14_1_total) +
                    this.x(B22_3_16_1_total) + this.x(B22_3_16_2_total);

                var textbox = $('input[name*=TextBoxTotalAmount]');
                var span = $('span[id*=LabelTotalBudgetAmount]');
                textbox.val(this.chkNum(total));
                span.text(this.chkNum(total));

            } catch (e) {
                console.log("ERROR: " + e);
            }
        };




        /* tab C */
        this.C_1_1 = function () {
            try {
                console.log("C_1_1");
                var q = QuestionareModel;
                var t1 = 0,
                    t2 = 0;
                var C_1_1_2_1 = q.C_1_1_2_1();
                var C_1_1_2_2 = q.C_1_1_2_2();

                t1 = (C_1_1_2_1 == true) ? 1 : 0;
                t2 = (C_1_1_2_2 == true) ? 1 : 0;

                //T_1_1_3 = t1 + t2;
                //q.T_1_1_3(T_1_1_3);
                //this.sect1();
                //console.log(t1 + " + " + t2 + " = " + T_1_1_3);
            } catch (e) {
                console.log("ERROR: " + e);
            }
        };
        this.C_1_2 = function () {
            try {
                console.log("C_1_2");
                var q = QuestionareModel;

                var  t1 = 0,
                    t2 = 0;
                var C_1_2_2_1 = q.C_1_2_2_1();
                var C_1_2_2_2 = q.C_1_2_2_2();

                t1 = (C_1_2_2_1 == true) ? 1 : 0;
                t2 = (C_1_2_2_2 == true) ? 1 : 0;
              /*  T_1_2_3 = t1 + t2;*/

                //q.T_1_2_3(T_1_2_3);
                //this.sect1();
                //console.log(t1 + " + " + t2 + " = " + T_1_2_3 + " : C_1_2_2_1 : " + C_1_2_2_1 + " : C_1_2_2_2 : " + C_1_2_2_2);
            } catch (e) {
                console.log("ERROR: " + e);
            }
        };

        this.C_1_3 = function () {
            try {
                console.log("C_1_3");
                var q = QuestionareModel;
                var total, T_1_3_2_1, T_1_3_2_2;
                T_1_3_2_1 = q.T_1_3_2_1();
                T_1_3_2_2 = q.T_1_3_2_2();
               /* T_1_3_3 = this.x(T_1_3_2_2) / this.x(T_1_3_2_1);*/

                //if (T_1_3_3 == 1) {
                //    total = 2;
                //} else if (T_1_3_3 >= 0.5 && T_1_3_3 <= 0.999) {
                //    total = 1;
                //} else {
                //    total = 0;
                //}
                // total = (T_1_3_3 == 1) ? 2 : (T_1_3_3 >= 0.5 && T_1_3_3 <= 0.9) ? 1 : 0;

               /* q.T_1_3_3(total);*/
                //this.sect1();
                //console.log("T_1_3_3 : " + T_1_3_3 + ": t-1-3-2-1 : " + T_1_3_2_1 + "; t-1-3-2-2 : " + T_1_3_2_2);
            } catch (e) {
                console.log("ERROR: " + e);
            }
        };
        this.C_1_4 = function () {
            try {
                console.log("C_1_4");
                var q = QuestionareModel;

                /*var T_1_4_3;*/
                var C_1_4_2 = q.C_1_4_2();

                //T_1_4_3 = (C_1_4_2 == true) ? 1 : 0;


                //q.T_1_4_3(T_1_4_3);
                //this.sect1();
                //console.log("C_1_4_2 : " + T_1_4_3);
            } catch (e) {
                console.log("ERROR: " + e);
            }
        };
        this.C_1_5 = function () {
            try {
                console.log("C_1_5");
                var q = QuestionareModel;

                var c1, c2, c3;
                var C_1_5_2_1 = q.C_1_5_2_1();
                var C_1_5_2_2 = q.C_1_5_2_2();
                var C_1_5_2_3 = q.C_1_5_2_3();

                c1 = (C_1_5_2_1 == true) ? 1 : 0;
                c2 = (C_1_5_2_2 == true) ? 1 : 0;
                c3 = (C_1_5_2_3 == true) ? 1 : 0;


                //q.T_1_5_3(c1 + c2 + c3);
                //this.sect1();
                //console.log("T_1_5_3 : " + c1 + c2 + c3);

            } catch (e) {
                console.log("ERROR: " + e);
            }
        };
        // C_2
        this.C_2_1 = function () {
            var q = QuestionareModel;
            var S_2_1_4 = q.S_2_1_4(),
                C_2_1_2 = q.C_2_1_2();

            var v = (C_2_1_2 == true) ? 1 : 0;
            var h = (v == "1") ? S_2_1_4 : "0";
            //q.T_2_1_4_1(h);
            //this.sect2();
        };
        this.C_2_2 = function () {
            var q = QuestionareModel;
            var C_2_2_2 = q.C_2_2_2();
            var c1 = C_2_2_2 == true ? 1 : 0;
            //q.S_2_2_4(c1);
            //this.sect2();
        };
        this.C_2_3 = function () {
            var q = QuestionareModel;
            var C_2_3_2 = q.C_2_3_2();
            var t = C_2_3_2 == true ? 1 : 0;
            //q.S_2_3_4(t);
            //this.sect2();
        };
        this.C_2_4 = function () {
            var q = QuestionareModel;
            var C_2_4_2 = q.C_2_4_2();
            var t = C_2_4_2 == true ? 1 : 0;
            //q.S_2_4_4(t);
            //this.sect2();
        };
        this.C_2_5 = function () {
            var q = QuestionareModel;
            var C_2_5_2_2 = q.C_2_5_2_2();
            var t = C_2_5_2_2 == true ? 1 : 0;
            //q.S_2_5_4(t);
            //this.sect2();
        };
        this.C_2_6 = function () {
            var q = QuestionareModel;
            var C_2_6_2_1 = q.C_2_6_2_1();
            var t = C_2_6_2_1 == true ? 1 : 0;
            //q.S_2_6_4(t);
            //this.sect2();
        };
        //section 3


        this.sect1 = function () {
            //var q = QuestionareModel;
            //var SECT_1_total = this.x(q.T_1_1_3()) + this.x(q.T_1_2_3()) + this.x(q.T_1_3_3()) + this.x(q.T_1_4_3()) + this.x(q.T_1_5_3());
            //q.SECT_1_total(SECT_1_total);
            //this.total_score();
            //console.log(q.SECT_1_total);
        };
        this.SECT_1_total = function () {
            var q = QuestionareModel,
                T_1_1_3 = q.T_1_1_3() ? q.T_1_1_3() : "0",
                T_1_2_3 = q.T_1_2_3() ? q.T_1_2_3() : "0",
                T_1_3_3 = q.T_1_3_3() ? q.T_1_3_3() : "0",
                T_1_4_3 = q.T_1_4_3() ? q.T_1_4_3() : "0",
                T_1_5_3 = q.T_1_5_3() ? q.T_1_5_3() : "0";
            var total = this.x(q.T_1_1_3()) + this.x(q.T_1_2_3()) + this.x(q.T_1_3_3()) + this.x(q.T_1_4_3()) + this.x(q.T_1_5_3()) ;

            q.SECT_1_total(Math.floor(total));
            this.total_score();

        };
        this.sect2 = function () {
            //var q = QuestionareModel;
            //Beer02092021
            //var SECT_2_total = this.x(q.T_2_1_4_1()) + this.x(q.S_2_2_4()) + this.x(q.S_2_3_4()) + this.x(q.S_2_4_4()) + this.x(q.S_2_5_4()) + this.x(q.S_2_6_4());
            //var SECT_2_total = Math.floor(this.x(q.T_2_1_4_1())) ;
            // q.SECT_2_total(SECT_2_total);
            //this.total_score();
            console.log(q.SECT_2_total);
        };
        this.SECT_2_total = function () {
            var q = QuestionareModel,
                S_2_1_4 = q.S_2_1_4() ? q.S_2_1_4() : "0";
            var total = this.x(q.S_2_1_4());

            q.SECT_2_total(Math.floor(total));
            this.total_score();

        };
        this.SECT_2_1 = function () {
            var q = QuestionareModel,
                chkText = 0,
                subtotal = 0;

            if (q.T_2_PJ_1_2() != "") {
                var a1 = (q.R_2_PJ_1_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + this.x(a1);
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_1_3("");
                q.R_2_PJ_1_6("");
            }
            if (q.T_2_PJ_2_2() != "") {
                var a2 = (q.R_2_PJ_2_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + this.x(a2);
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_2_3("");
                q.R_2_PJ_2_6("");
            }
            if (q.T_2_PJ_3_2() != "") {
                var a3 = (q.R_2_PJ_3_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + this.x(a3);
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_3_3("");
                q.R_2_PJ_3_6("");
            }
            if (q.T_2_PJ_4_2() != "") {
                var a4 = (q.R_2_PJ_4_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + this.x(a4);
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_4_3("");
                q.R_2_PJ_4_6("");
            }
            if (q.T_2_PJ_5_2() != "") {
                var a5 = (q.R_2_PJ_5_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + this.x(a5);
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_5_3("");
                q.R_2_PJ_5_6("");
            }
            if (q.T_2_PJ_6_2() != "") {
                var a6 = (q.R_2_PJ_6_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + this.x(a6);
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_6_3("");
                q.R_2_PJ_6_6("");
            }
            if (q.T_2_PJ_7_2() != "") {
                var a7 = (q.R_2_PJ_7_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + this.x(a7);
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_7_3("");
                q.R_2_PJ_7_6("");
            }
            if (q.T_2_PJ_8_2() != "") {
                var a8 = (q.R_2_PJ_8_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + a8;
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_8_3("");
                q.R_2_PJ_8_6("");
            }
            if (q.T_2_PJ_9_2() != "") {
                var a9 = (q.R_2_PJ_9_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + a9;
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_9_3("");
                q.R_2_PJ_9_6("");
            }
            if (q.T_2_PJ_10_2() != "") {
                var a10 = (q.R_2_PJ_10_6() == "1") ? "1" : "0";
                subtotal = this.x(subtotal) + a10;
                chkText = this.x(chkText) + 1;
            } else {
                q.R_2_PJ_10_3("");
                q.R_2_PJ_10_6("");
            }
            // var subtotal = chkText / 100;
            // var subtotal = this.x(a1) + this.x(a2) + this.x(a3) + this.x(a4) + this.x(a5) + this.x(a6) + this.x(a7) + this.x(a8) + this.x(a9) + this.x(a10);

            var average = subtotal / chkText;
            var point = (average == 1) ? 5 : (average >= 0.75 && average <= 0.99) ? 4 : (average >= 0.5 && average <= 0.74) ? 3 : (average >= 0.33 && average <= 0.49) ? 2 : (average <= 0.32 && average >= 0.01) ? 1 : 0;

            q.SECT_2_1_total(point);
            this.SECT_3_total();
            this.total_score();
            console.log("Subtotal = " + subtotal + ": chkText : " + chkText + " : average = " + average + " : point= " + point);
        };
        this.SECT_3_total = function () {
            var q = QuestionareModel,
                SECT_3_total;
            //SECT_3_total = this.x(q.S_2_2()) + this.x(q.R_2_CC()) + this.x(q.SECT_2_1_total());
            SECT_3_total = Math.floor(this.x(q.S_2_2()));
            q.SECT_3_total(SECT_3_total);
            this.total_score();
            console.log("SECT_3_total = " + SECT_3_total);
        };
        this.SECT_4_total = function () {
            var q = QuestionareModel,
                //C_3_1_2 = q.C_3_1_2() == true ? "2" : "0",
                //C_3_2_2_1 = q.C_3_2_2_1() == true ? "1" : "0",
                //C_3_2_2_2 = q.C_3_2_2_2() == true ? "1" : "0",
                //C_3_2_2_3 = q.C_3_2_2_3() == true ? "1" : "0",
                //C_3_2_2_4 = q.C_3_2_2_4() == true ? "1" : "0",
                //C_3_2_2_5 = q.C_3_2_2_5() == true ? "1" : "0",
                //C_3_3_2_1 = q.C_3_3_2_1() == true ? "1" : "0",
                //S_3_3_4 = C_3_3_2_1 == 1 ? q.S_3_3_4() : "0";

                //C_3_1_2 = q.C_3_1_2() == true ? "2" : "0",
                //C_3_2_2_1 = q.C_3_2_2_1() == true ? "1" : "0",
                //C_3_2_2_2 = q.C_3_2_2_2() == true ? "1" : "0",
                //C_3_2_2_3 = q.C_3_2_2_3() == true ? "1" : "0",
                //C_3_2_2_4 = q.C_3_2_2_4() == true ? "1" : "0",
                //C_3_2_2_5 = q.C_3_2_2_5() == true ? "1" : "0",
                //C_3_3_2_1 = q.C_3_3_2_1() == true ? "1" : "0",
                T_3_1_4 = q.T_3_1_4() ? q.T_3_1_4() : "0",
                T_3_2_4 = q.T_3_2_4()? q.T_3_2_4() : "0",
                S_3_3_4 = q.S_3_3_4() ? q.S_3_3_4() : "0";
            //var total = this.x(C_3_1_2) + this.x(C_3_2_2_1) + this.x(C_3_2_2_2) + this.x(C_3_2_2_3) + this.x(C_3_2_2_4) + this.x(C_3_2_2_5) + this.x(S_3_3_4);
            var total = this.x(T_3_1_4) + this.x(T_3_2_4) + this.x(S_3_3_4);

            q.SECT_4_total(total);
            this.total_score();

        };

        this.total_score = function () {
            var q = QuestionareModel;

            var subtotal = this.x(q.SECT_1_total()) + this.x(q.SECT_2_total()) + this.x(q.SECT_3_total()) + this.x(q.SECT_4_total());
            var percent = (subtotal / 100) * 100;
            q.Total_Score(subtotal);
            q.Percent_Score(Math.floor(percent));

        };

    };

    khProjBG = new khProjectBudget();
})();