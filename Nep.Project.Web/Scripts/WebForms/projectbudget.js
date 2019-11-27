IsBudjectNotValid = true;
var totalCallFunction;

function BudgetMessageError(msg, span) {
    $(span).text(msg);
}

function limit_error(cal, limit, message, id) {
    if (cal > limit) {
        BudgetMessageError(message + cal, id);
    } else {
        BudgetMessageError("", id);
    }
}

function B1_1_1_2() {
    try {
        var q = QuestionareModel;
        var B1_1_1_2_M_1 = QuestionareModel.B1_1_1_2_M_1();
        var B1_1_1_2_M_2 = QuestionareModel.B1_1_1_2_M_2();
        var B1_1_1_2_M_3 = QuestionareModel.B1_1_1_2_M_3();

        var B1_1_1_2_L_1 = QuestionareModel.B1_1_1_2_L_1();
        var B1_1_1_2_L_2 = QuestionareModel.B1_1_1_2_L_2();
        var B1_1_1_2_L_3 = QuestionareModel.B1_1_1_2_L_3();


        var B1_1_1_2_D_1 = QuestionareModel.B1_1_1_2_D_1();
        var B1_1_1_2_D_2 = QuestionareModel.B1_1_1_2_D_2();
        var B1_1_1_2_D_3 = QuestionareModel.B1_1_1_2_D_3();

        B1_1_1_2_L_total( parseFloat(q.B1_1_1_2_M_1) * q.B1_1_1_2_M_2 * q.B1_1_1_2_M_3  )
        var B1_1_1_2_L_total = B1_1_1_2_L_1 * B1_1_1_2_L_2 * B1_1_1_2_L_3;
        var B1_1_1_2_D_total = B1_1_1_2_D_1 * B1_1_1_2_D_2 * B1_1_1_2_D_3;

        QuestionareModel.B1_1_1_2_M_total(B1_1_1_2_M_total);
        QuestionareModel.B1_1_1_2_L_total(B1_1_1_2_L_total);
        QuestionareModel.B1_1_1_2_D_total(B1_1_1_2_D_total);
       
        // alert(QuestionareModel.B1_1_1_2_M_total(B1_1_1_2_M_total));
        var total = B1_1_1_2_M_total + B1_1_1_2_L_total + B1_1_1_2_D_total;
        sum1to13();
        var Food3Meals = QuestionareModel.Food3Meals();
        //alert(Food3Meals);
        if (Food3Meals == "1") {
            // alert("Food3Meals1");
            if (total > 600) {

                //alert("โปรดกรอกข้อมูลไม่เกิน 600 บาท ตอนนี้ตัวเลขอยู่ที่ : " + total);
                BudgetMessageError("โปรดกรอกข้อมูลไม่เกิน 600 บาท ตอนนี้ตัวเลขอยู่ที่ : " + total, "#message_error_budget1");

                return false;
            } else {
                BudgetMessageError("", "#message_error_budget1");
                return true;

            }
        } else if (Food3Meals == "2") {
            // alert("Food3Meals2");
            if (total > 950) {
                //alert("โปรดกรอกข้อมูลไม่เกิน 950 บาท ตอนนี้ตัวเลขอยู่ที่ : " + total);
                //BudgetMessageError("โปรดกรอกข้อมูลไม่เกิน 950 บาท ตอนนี้ตัวเลขอยู่ที่ : " + total, spanError1);
                BudgetMessageError("โปรดกรอกข้อมูลไม่เกิน 950 บาท ตอนนี้ตัวเลขอยู่ที่ : " + total, "#message_error_budget1");
                return false;

            } else {
                BudgetMessageError("", "#message_error_budget1");
                return true;

            }
        } else {
            //alert("โปรดเลือกประเภทการจัดในสถานที่แบบไหน");
            BudgetMessageError("โปรดเลือกประเภทการจัดในสถานที่แบบไหน", "#message_error_budget1");
        }
        //alert("รายการเฉพาะ ค่าอาหาร : " + total);
        BudgetMessageError("", "#message_error_budget1");


    } catch (e) {

    }

}

function B1_1_2_2() {
    try {

        var B1_1_2_2_L_1 = QuestionareModel.B1_1_2_2_L_1();
        var B1_1_2_2_L_2 = QuestionareModel.B1_1_2_2_L_2();
        var B1_1_2_2_L_3 = QuestionareModel.B1_1_2_2_L_3();

        var B1_1_2_2_D_1 = QuestionareModel.B1_1_2_2_D_1();
        var B1_1_2_2_D_2 = QuestionareModel.B1_1_2_2_D_2();
        var B1_1_2_2_D_3 = QuestionareModel.B1_1_2_2_D_3();



        var B1_1_2_2_L_total = B1_1_2_2_L_1 * B1_1_2_2_L_2 * B1_1_2_2_L_3;
        var B1_1_2_2_D_total = B1_1_2_2_D_1 * B1_1_2_2_D_2 * B1_1_2_2_D_3;

        QuestionareModel.B1_1_2_2_L_total(B1_1_2_2_L_total);
        QuestionareModel.B1_1_2_2_D_total(B1_1_2_2_D_total);

        var total = B1_1_2_2_L_total + B1_1_2_2_D_total;

        var Food2Meals = QuestionareModel.Food2Meals();

        if (Food2Meals == "1") {
            limit_error(total, 400, "โปรดกรอกข้อมูลไม่เกิน 400 บาท", "#message_error_budget2");
        } else if (Food2Meals == "2") {
            limit_error(total, 700, "โปรดกรอกข้อมูลไม่เกิน 700 บาท", "#message_error_budget2");
        } else {
            // alert("โปรดเลือกประเภทการจัดในสถานที่แบบไหน");
            BudgetMessageError("โปรดเลือกประเภทการจัดในสถานที่แบบไหน", "#message_error_budget2");
            return false;
        }
        BudgetMessageError("", "#message_error_budget2");

    } catch (e) {

    }



}

function B1_2() {
    try {

        var B1_2_1_1 = QuestionareModel.B1_2_1_1();
        var B1_2_1_2 = QuestionareModel.B1_2_1_2();
        var B1_2_1_3 = QuestionareModel.B1_2_1_3();

        var B1_2_2_1 = QuestionareModel.B1_2_2_1();
        var B1_2_2_2 = QuestionareModel.B1_2_2_2();
        var B1_2_2_3 = QuestionareModel.B1_2_2_3();



        var B1_2_1_total = B1_2_1_1 * B1_2_1_2 * B1_2_1_3;
        var B1_2_2_total = B1_2_2_1 * B1_2_2_2 * B1_2_2_3;

        QuestionareModel.B1_2_1_total(B1_2_1_total);
        QuestionareModel.B1_2_2_total(B1_2_2_total);


    } catch (e) {

    }



}

function B1_3_1() {
    try {

        var B1_3_1_1_1 = QuestionareModel.B1_3_1_1_1();
        var B1_3_1_1_2 = QuestionareModel.B1_3_1_1_2();
        var B1_3_1_1_3 = QuestionareModel.B1_3_1_1_3();

        var B1_3_1_2_1 = QuestionareModel.B1_3_1_2_1();
        var B1_3_1_2_2 = QuestionareModel.B1_3_1_2_2();
        var B1_3_1_2_3 = QuestionareModel.B1_3_1_2_3();

        var B1_3_1_3_1 = QuestionareModel.B1_3_1_3_1();
        var B1_3_1_3_2 = QuestionareModel.B1_3_1_3_2();
        var B1_3_1_3_3 = QuestionareModel.B1_3_1_3_3();


        var B1_3_1_1_total = B1_3_1_1_1 * B1_3_1_1_2 * B1_3_1_1_3;
        var B1_3_1_2_total = B1_3_1_2_1 * B1_3_1_2_2 * B1_3_1_2_3;
        var B1_3_1_3_total = B1_3_1_3_1 * B1_3_1_3_2 * B1_3_1_3_3;

        QuestionareModel.B1_3_1_1_total(B1_3_1_1_total);
        QuestionareModel.B1_3_1_2_total(B1_3_1_2_total);
        QuestionareModel.B1_3_1_3_total(B1_3_1_3_total);


    } catch (e) {

    }



}

function B1_3_2() {
    try {

        var B1_3_2_1_1 = QuestionareModel.B1_3_2_1_1();
        var B1_3_2_1_2 = QuestionareModel.B1_3_2_1_2();
        var B1_3_2_1_3 = QuestionareModel.B1_3_2_1_3();

        var B1_3_2_1_total = B1_3_2_1_1 * B1_3_2_1_2 * B1_3_2_1_3;

        QuestionareModel.B1_3_2_1_total(B1_3_2_1_total);


    } catch (e) {

    }

}

function B1_4_1() {
    try {

        var B1_4_1_1 = QuestionareModel.B1_4_1_1();
        var B1_4_1_2 = QuestionareModel.B1_4_1_2();
        var B1_4_1_total = B1_4_1_1 * B1_4_1_2;
        QuestionareModel.B1_4_1_total(B1_4_1_total);

        var B1_4_2_1 = QuestionareModel.B1_4_2_1();
        var B1_4_2_2 = QuestionareModel.B1_4_2_2();
        var B1_4_2_total = B1_4_2_1 * B1_4_2_2;
        QuestionareModel.B1_4_2_total(B1_4_2_total);

        var B1_4_3_1 = QuestionareModel.B1_4_3_1();
        var B1_4_3_2 = QuestionareModel.B1_4_3_2();
        var B1_4_3_total = B1_4_3_1 * B1_4_3_2;
        QuestionareModel.B1_4_3_total(B1_4_3_total);

        var B1_4_4_1_1 = QuestionareModel.B1_4_4_1_1();
        var B1_4_4_1_2 = QuestionareModel.B1_4_4_1_2();
        var B1_4_4_1_total = B1_4_4_1_1 * B1_4_4_1_2;
        QuestionareModel.B1_4_4_1_total(B1_4_4_1_total);


        var B1_4_4_2_1 = QuestionareModel.B1_4_4_2_1();
        var B1_4_4_2_2 = QuestionareModel.B1_4_4_2_2();
        var B1_4_4_2_total = B1_4_4_2_1 * B1_4_4_2_2;
        QuestionareModel.B1_4_4_2_total(B1_4_4_2_total);


        var B1_4_5_1_1 = QuestionareModel.B1_4_5_1_1();
        var B1_4_5_1_2 = QuestionareModel.B1_4_5_1_2();
        var B1_4_5_1_total = B1_4_5_1_1 * B1_4_5_1_2;
        QuestionareModel.B1_4_5_1_total(B1_4_5_1_total);

        var B1_4_5_2_1 = QuestionareModel.B1_4_5_2_1();
        var B1_4_5_2_2 = QuestionareModel.B1_4_5_2_2();
        var B1_4_5_2_total = B1_4_5_2_1 * B1_4_5_2_2;
        QuestionareModel.B1_4_5_2_total(B1_4_5_2_total);



        var total_use_m = B1_4_5_1_total + B1_4_5_2_total;
        limit_error(total_use_m, 1800, "เบิกเกิน : " + total_use_m, "#message_error_budget15");



    } catch (e) {

    }

}

function B1_5() {
    try {

        var B1_5_1 = QuestionareModel.B1_5_1();
        var B1_5_2 = QuestionareModel.B1_5_2();
        var B1_5_3 = QuestionareModel.B1_5_3();
        var B1_5_total = B1_5_1 * B1_5_2 * B1_5_3;
        QuestionareModel.B1_5_total(B1_5_total);
        var per_day_B1_5 = B1_5_total / B1_5_2;

        limit_error(per_day_B1_5, 240, "เกินกว่ากำหนด : " + per_day_B1_5, "#message_error_budget17");


    } catch (e) {

    }

}

function B1_6() {
    try {

        /*6.1.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท)*/
        var B1_6_1_1_1 = QuestionareModel.B1_6_1_1_1();
        var B1_6_1_1_2 = QuestionareModel.B1_6_1_1_2();
        var B1_6_1_1_3 = QuestionareModel.B1_6_1_1_3();
        var B1_6_1_1_total = B1_6_1_1_1 * B1_6_1_1_2 * B1_6_1_1_3;
        QuestionareModel.B1_6_1_1_total(B1_6_1_1_total);
        /*6.1.2 วิทยากรภาคเอกชน (ไม่เกินชั่วโมงละ 1,200 บาท) */
        var B1_6_1_2_1 = QuestionareModel.B1_6_1_2_1();
        var B1_6_1_2_2 = QuestionareModel.B1_6_1_2_2();
        var B1_6_1_2_3 = QuestionareModel.B1_6_1_2_3();
        var B1_6_1_2_total = B1_6_1_2_1 * B1_6_1_2_2 * B1_6_1_2_3;
        QuestionareModel.B1_6_1_2_total(B1_6_1_2_total);
        /*6.1.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ
        	  (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี) */
        var B1_6_1_3_1 = QuestionareModel.B1_6_1_3_1();
        var B1_6_1_3_2 = QuestionareModel.B1_6_1_3_2();
        var B1_6_1_3_3 = QuestionareModel.B1_6_1_3_3();
        var B1_6_1_3_total = B1_6_1_3_1 * B1_6_1_3_2 * B1_6_1_3_3;
        QuestionareModel.B1_6_1_3_total(B1_6_1_3_total);
        /**6.1 ค่าตอบแทนวิทยากรบรรยาย (จะต้องไม่เกิน 1 คน/ชั่วโมง)  */
        var lecturer = B1_6_1_1_1 + B1_6_1_2_1 + B1_6_1_3_1;
        var hour = B1_6_1_1_2 + B1_6_1_2_2 + B1_6_1_3_2;
        var total_per_lecturer_hour = hour / lecturer;
        limit_error(total_per_lecturer_hour, 1, "เกินกว่ากำหนด :", "#message_error_budget18");


        /*6.2.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท)*/
        var B1_6_2_1_1 = QuestionareModel.B1_6_2_1_1();
        var B1_6_2_1_2 = QuestionareModel.B1_6_2_1_2();
        var B1_6_2_1_3 = QuestionareModel.B1_6_2_1_3();
        var B1_6_2_1_total = B1_6_2_1_1 * B1_6_2_1_2 * B1_6_2_1_3;

        QuestionareModel.B1_6_2_1_total(B1_6_2_1_total);

        /* 6.2.2 วิทยากรภาคเอกชน (ไม่เกินชั่วโมงละ 1,200 บาท)*/
        var B1_6_2_2_1 = QuestionareModel.B1_6_2_2_1();
        var B1_6_2_2_2 = QuestionareModel.B1_6_2_2_2();
        var B1_6_2_2_3 = QuestionareModel.B1_6_2_2_3();
        var B1_6_2_2_total = B1_6_2_2_1 * B1_6_2_2_2 * B1_6_2_2_3;

        QuestionareModel.B1_6_2_2_total(B1_6_2_2_total);

        /* >6.2.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ
	  (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี)*/
        var B1_6_2_3_1 = QuestionareModel.B1_6_2_3_1();
        var B1_6_2_3_2 = QuestionareModel.B1_6_2_3_2();
        var B1_6_2_3_3 = QuestionareModel.B1_6_2_3_3();
        var B1_6_2_3_total = B1_6_2_2_1 * B1_6_2_2_2 * B1_6_2_3_3;

        QuestionareModel.B1_6_2_3_total(B1_6_2_3_total);

        /*>6.2 ค่าตอบแทนวิทยากรอภิปราย (จะต้องไม่เกิน 5 คน/ชั่วโมง) */
        var lecturer1 = B1_6_2_1_1 + B1_6_2_2_1 + B1_6_2_3_1;
        var hour1 = B1_6_2_1_2 + B1_6_2_2_2 + B1_6_2_3_2;
        var total_per_lecturer_hour2 = hour1 / lecturer1;
        limit_error(total_per_lecturer_hour2, 5, "เกินกว่ากำหนด :", "#message_error_budget21");

        /*6.3.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท)*/
        var B1_6_3_1_1 = QuestionareModel.B1_6_3_1_1();
        var B1_6_3_1_2 = QuestionareModel.B1_6_3_1_2();
        var B1_6_3_1_3 = QuestionareModel.B1_6_3_1_3();
        var B1_6_3_1_total = B1_6_3_1_1 * B1_6_3_1_2 * B1_6_3_1_3;

        var limit_hour = B1_6_3_1_total / B1_6_3_1_2;

        QuestionareModel.B1_6_3_1_total(B1_6_3_1_total);
        limit_error(limit_hour, 600, "เกินกว่ากำหนด :", "#message_error_budget25");

        /*6.3.1 วิทยากรภาครัฐ (ไม่เกินชั่วโมงละ 600 บาท)*/
        var B1_6_3_2_1 = QuestionareModel.B1_6_3_2_1();
        var B1_6_3_2_2 = QuestionareModel.B1_6_3_2_2();
        var B1_6_3_2_3 = QuestionareModel.B1_6_3_2_3();
        var B1_6_3_2_total = B1_6_3_2_1 * B1_6_3_2_2 * B1_6_3_2_3;

        var limit_hour1 = B1_6_3_2_total / B1_6_3_2_2;

        QuestionareModel.B1_6_3_2_total(B1_6_3_2_total);
        limit_error(limit_hour1, 1200, "เกินกว่ากำหนด :", "#message_error_budget26");
        // 6.3.3 วิทยากรผู้ทรงคุณวุฒิ โดยจะต้องมีเอกสารรับรองความเชี่ยวชาญในด้านนั้นๆ         (ให้เสนอคณะอนุกรรมการบริหารกองทุนฯ เป็นรายกรณี)
        var B1_6_3_3_1 = QuestionareModel.B1_6_3_3_1();
        var B1_6_3_3_2 = QuestionareModel.B1_6_3_3_2();
        var B1_6_3_3_3 = QuestionareModel.B1_6_3_3_3();
        var B1_6_3_3_total = B1_6_3_3_1 * B1_6_3_3_2 * B1_6_3_3_3;
        QuestionareModel.B1_6_3_3_total(B1_6_3_3_total);
        // 6.3 ค่าตอบแทนวิทยากรกลุ่ม (จะต้องไม่เกินกลุ่มละ 2 คน)
        var lecturer2 = B1_6_3_1_1 + B1_6_3_2_1 + B1_6_3_3_1;

        var limit_lecturer2 = lecturer2 / 3;
        limit_error(limit_lecturer2, 2, "เกินกว่ากำหนด :", "#message_error_budget24");

        // 6.4.1 วิทยากรฝึกอาชีพทั่วไป (ไม่เกินชั่วโมงละ 400 บาท)
        var B1_6_4_1_1 = QuestionareModel.B1_6_4_1_1();
        var B1_6_4_1_2 = QuestionareModel.B1_6_4_1_2();
        var B1_6_4_1_3 = QuestionareModel.B1_6_4_1_3();
        var B1_6_4_1_total = B1_6_4_1_1 * B1_6_4_1_2 * B1_6_4_1_3;

        var limit_hourb1641 = B1_6_4_1_total / B1_6_4_1_2;

        QuestionareModel.B1_6_4_1_total(B1_6_4_1_total);
        limit_error(limit_hourb1641, 400, "เกินกว่ากำหนด :", "#message_error_budget27");

        // 6.4.2 วิทยากรภาคฝึกอาชีพเชี่ยวชาญ
        var B1_6_4_2_1 = QuestionareModel.B1_6_4_2_1();
        var B1_6_4_2_2 = QuestionareModel.B1_6_4_2_2();
        var B1_6_4_2_3 = QuestionareModel.B1_6_4_2_3();
        var B1_6_4_2_total = B1_6_4_2_1 * B1_6_4_2_2 * B1_6_4_2_3;

        var limit_hourb1642 = B1_6_4_2_total / B1_6_4_2_2;

        QuestionareModel.B1_6_4_2_total(B1_6_4_2_total);
        limit_error(limit_hourb1642, 1200, "เกินชั่วโมงละ 1,200 บาท :", "#message_error_budget28");


    } catch (e) {

    }

}

function B1_7() {
    try {

        /*7. ค่าตอบแทนล่ามภาษามือ (ชั่วโมงละ 600 บาท และไม่เกิน 6 ชั่วโมง/วัน)*/
        var B1_7_1 = QuestionareModel.B1_7_1();
        var B1_7_2 = QuestionareModel.B1_7_2();
        var B1_7_3 = QuestionareModel.B1_7_3();
        var B1_7_total = B1_7_1 * B1_7_2 * B1_7_3;
        var per_day_B1_7_3 = B1_7_total / B1_7_2;
        QuestionareModel.B1_7_total(B1_7_total);
        limit_error(per_day_B1_7_3, 600, "เกิน ชั่วโมงละ :", "#message_error_budget29");


    } catch (e) {

    }
}

function B1_8() {
    try {

        /*8.1 กรณีจัดในโรงแรม (เบิกจ่ายตามจริง เหมาะสมและประหยัด)*/
        var B1_8_1_1 = QuestionareModel.B1_8_1_1();
        var B1_8_1_2 = QuestionareModel.B1_8_1_2();
        var B1_8_1_total = B1_8_1_1 * B1_8_1_2;
        QuestionareModel.B1_8_1_total(B1_8_1_total);

        /*8.2 กรณีจัดในสถานที่ราชการ*/
        var B1_8_2_1 = QuestionareModel.B1_8_2_1();
        var B1_8_2_2 = QuestionareModel.B1_8_2_2();
        var B1_8_2_total = B1_8_2_1 * B1_8_2_2;
        QuestionareModel.B1_8_2_total(B1_8_2_total);

        /*8.3.1 ระยะเวลาการดำเนินโครงการ ไม่เกิน 5 วัน (ไม่เกินวันละ 5,000 บาท)*/
        var B1_8_3_1_1 = QuestionareModel.B1_8_3_1_1();
        var B1_8_3_1_2 = QuestionareModel.B1_8_3_1_2();
        var B1_8_3_1_total = B1_8_3_1_1 * B1_8_3_1_2;
        QuestionareModel.B1_8_3_1_total(B1_8_3_1_total);
        var cal_total1831 = B1_8_3_1_total / B1_8_3_1_1;
        limit_error(cal_total1831, 5000, "ไม่เกินวันละ 5,000 บาท :" + cal_total1831, "#message_error_budget30");
        //  >8.3.2 ระยะเวลาการดำเนินโครงการ มากกว่า 5 วัน (ให้เหมาจ่ายไม่เกิน 30,000 บาท/โครงการ)
        var B1_8_3_2_total = QuestionareModel.B1_8_3_2();

        QuestionareModel.B1_8_3_2_total(B1_8_3_2_total);

        limit_error(B1_8_3_2_total, 30000, "เกิน 30,000 บาท" + B1_8_3_2_total, "#message_error_budget31");


    } catch (e) {

    }
}

function B1_9() {
    try {

        /*9.1 ค่าเช่ารถตู้ปรับอากาศ (ไม่เกิน 1,800 บาท/คัน/วัน)*/
        var B1_9_1_1 = QuestionareModel.B1_9_1_1();
        var B1_9_1_2 = QuestionareModel.B1_9_1_2();
        var B1_9_1_3 = QuestionareModel.B1_9_1_3();
        var B1_9_1_total = B1_9_1_1 * B1_9_1_2 * B1_9_1_3;
        QuestionareModel.B1_9_1_total(B1_9_1_total);

        // 9.2.1 รถบัสแบบพัดลม (ไม่เกินวันละ 5,500 บาท/คัน/วัน)
        var B1_9_2_1_1 = QuestionareModel.B1_9_2_1_1();
        var B1_9_2_1_2 = QuestionareModel.B1_9_2_1_2();
        var B1_9_2_1_3 = QuestionareModel.B1_9_2_1_3();
        var B1_9_2_1_total = B1_9_2_1_1 * B1_9_2_1_2 * B1_9_2_1_3;
        QuestionareModel.B1_9_2_1_total(B1_9_2_1_total);

        // 9.2.2 รถบัสปรับอากาศ 30 – 32 ที่นั่ง (ไม่เกินวันละ 8,000 บาท/คัน/วัน)
        var B1_9_2_2_1 = QuestionareModel.B1_9_2_2_1();
        var B1_9_2_2_2 = QuestionareModel.B1_9_2_2_2();
        var B1_9_2_2_3 = QuestionareModel.B1_9_2_2_3();
        var B1_9_2_2_total = B1_9_2_2_1 * B1_9_2_2_2 * B1_9_2_2_3;
        QuestionareModel.B1_9_2_2_total(B1_9_2_2_total);
        // 9.2.3 รถบัสปรับอากาศ VIP 2 ชั้น 40 – 45 ที่นั่ง (ไม่เกินวันละ 12,000 บาท/คัน/วัน)
        var B1_9_2_3_1 = QuestionareModel.B1_9_2_3_1();
        var B1_9_2_3_2 = QuestionareModel.B1_9_2_3_2();
        var B1_9_2_3_3 = QuestionareModel.B1_9_2_3_3();
        var B1_9_2_3_total = B1_9_2_3_1 * B1_9_2_3_2 * B1_9_2_3_3;
        QuestionareModel.B1_9_2_3_total(B1_9_2_3_total);
        // 9.2.4 รถบัสปรับอากาศ VIP 2 ชั้น 40 – 50 ที่นั่ง (ไม่เกินวันละ 15,000 บาท/คัน/วัน)        
        var B1_9_2_4_1 = QuestionareModel.B1_9_2_4_1();
        var B1_9_2_4_2 = QuestionareModel.B1_9_2_4_2();
        var B1_9_2_4_3 = QuestionareModel.B1_9_2_4_3();
        var B1_9_2_4_total = B1_9_2_4_1 * B1_9_2_4_2 * B1_9_2_4_3;
        QuestionareModel.B1_9_2_4_total(B1_9_2_4_total);

    } catch (e) {

    }
}

function B1_10() {
    try {

        /*10. ค่าน้ำมันเชื้อเพลิง (เบิกจ่ายตามจริง)*/
        var B1_10_1 = QuestionareModel.B1_10_1();
        var B1_10_2 = QuestionareModel.B1_10_2();
        var B1_10_total = B1_10_1 * B1_10_2;
        QuestionareModel.B1_10_total(B1_10_total);

    } catch (e) {

    }
}

function B1_11() {
    try {

        /*11.1 เอกสารทั่วไป (ไม่เกิน 100 บาท/คน/หลักสูตร)*/
        var B1_11_1_1 = QuestionareModel.B1_11_1_1();
        var B1_11_1_2 = QuestionareModel.B1_11_1_2();
        var B1_11_1_total = B1_11_1_1 * B1_11_1_2;
        QuestionareModel.B1_11_1_total(B1_11_1_total);

        // 11.2 เอกสารอักษรเบรลล์ (ไม่เกิน 200 บาท/ชุด)
        var B1_11_2_1 = QuestionareModel.B1_11_2_1();
        var B1_11_2_2 = QuestionareModel.B1_11_2_2();
        var B1_11_2_total = B1_11_2_1 * B1_11_2_2;
        QuestionareModel.B1_11_2_total(B1_11_2_total);

        // 11.3 เอกสารเสียงหรือซีดี (ไม่เกินแผ่นละ 20 บาท)
        var B1_11_3_1 = QuestionareModel.B1_11_3_1();
        var B1_11_3_2 = QuestionareModel.B1_11_3_2();
        var B1_11_3_total = B1_11_3_1 * B1_11_3_2;
        QuestionareModel.B1_11_3_total(B1_11_3_total);

    } catch (e) {

    }
}

function B1_12() {
    try {

        // 12. ค่ากระเป๋าผ้า (ไม่เกิน 80 บาท/ใบ/คน)
        var B1_12_1 = QuestionareModel.B1_12_1();
        var B1_12_2 = QuestionareModel.B1_12_2();
        var B1_12_total = B1_12_1 * B1_12_2;
        QuestionareModel.B1_12_total(B1_12_total);

    } catch (e) {

    }
}

function B1_13() {
    try {

        // <b>13. ค่าวัสดุฝึกอบรมหรือฝึกอาชีพ (ตามความจำเป็นของแต่ละโครงการ)</b>
        var B1_13_1 = QuestionareModel.B1_13_1();
        var B1_13_2 = QuestionareModel.B1_13_2();
        var B1_13_total = B1_13_1 * B1_13_2;
        QuestionareModel.B1_13_total(B1_13_total);

    } catch (e) {

    }
}

function B1_14() {
    try {

        // 14.1 ค่าใช้จ่ายในการติดตามหรือประเมินผลโครงการหรือถอดบทเรียน
        var B1_14_1_1 = QuestionareModel.B1_14_1_1();
        var B1_14_1_2 = QuestionareModel.B1_14_1_2();
        var B1_14_1_total = B1_14_1_1 * B1_14_1_2;
        QuestionareModel.B1_14_1_total(B1_14_1_total);

    } catch (e) {

    }
}

function sum1to13() {
   // console.log("TEST");
    var B1_1_1_2_M_total = QuestionareModel.B1_1_1_2_M_total();
    var B1_1_1_2_L_total = QuestionareModel.B1_1_1_2_L_total();
    var B1_1_1_2_D_total = QuestionareModel.B1_1_1_2_D_total();
    var B1_1_2_2_L_total = QuestionareModel.B1_1_2_2_L_total();
    var B1_1_2_2_D_total = QuestionareModel.B1_1_2_2_D_total();
    var B1_2_1_total = QuestionareModel.B1_2_1_total();
    var B1_2_2_total = QuestionareModel.B1_2_2_total();
    var B1_3_1_1_total = QuestionareModel.B1_3_1_1_total();
    var B1_3_1_2_total = QuestionareModel.B1_3_1_2_total();
    var B1_3_1_3_total = QuestionareModel.B1_3_1_3_total();
    var B1_3_2_1_total = QuestionareModel.B1_3_2_1_total();
    var B1_4_1_total = QuestionareModel.B1_4_1_total();
    var B1_4_2_total = QuestionareModel.B1_4_2_total();
    var B1_4_3_total = QuestionareModel.B1_4_3_total();
    var B1_4_4_1_total = QuestionareModel.B1_4_4_1_total();
    var B1_4_5_1_total = QuestionareModel.B1_4_5_1_total();
    var B1_4_4_2_total = QuestionareModel.B1_4_4_2_total();
    var B1_5_total = QuestionareModel.B1_5_total();
    var B1_6_1_1_total = QuestionareModel.B1_4_5_2_total();
    var B1_6_1_2_total = QuestionareModel.B1_6_1_2_total();
    var B1_6_1_3_total = QuestionareModel.B1_6_1_3_total();
    var B1_6_2_1_total = QuestionareModel.B1_6_2_1_total();
    var B1_6_2_2_total = QuestionareModel.B1_6_2_2_total();
    var B1_6_3_1_total = QuestionareModel.B1_6_3_1_total();
    var B1_6_3_2_total = QuestionareModel.B1_6_3_2_total();
    var B1_6_3_3_total = QuestionareModel.B1_6_3_3_total();
    var B1_6_4_1_total = QuestionareModel.B1_6_4_1_total();
    var B1_6_4_2_total = QuestionareModel.B1_6_4_2_total();
    var B1_7_total = QuestionareModel.B1_7_total();
    var B1_8_1_total = QuestionareModel.B1_8_1_total();
    var B1_8_2_total = QuestionareModel.B1_8_2_total();
    var B1_8_3_1_total = QuestionareModel.B1_8_3_1_total();
    var B1_9_1_total = QuestionareModel.B1_9_1_total();
    var B1_9_2_1_total = QuestionareModel.B1_9_2_1_total();
    var B1_9_2_2_total = QuestionareModel.B1_9_2_2_total();
    var B1_9_2_3_total = QuestionareModel.B1_9_2_3_total();
    var B1_9_2_4_total = QuestionareModel.B1_9_2_4_total();
    var B1_10_total = QuestionareModel.B1_10_total();
    var B1_11_1_total = QuestionareModel.B1_11_1_total();
    var B1_11_2_total = QuestionareModel.B1_11_2_total();
    var B1_11_3_total = QuestionareModel.B1_11_3_total();
    var B1_12_total = QuestionareModel.B1_12_total();
    var B1_13_total = QuestionareModel.B1_13_total();
    var B1_13_other = QuestionareModel.B1_13_other();


    // var totalAll = B1_1_1_2_M_total + B1_1_1_2_L_total + B1_1_1_2_D_total + B1_1_2_2_L_total + B1_1_2_2_D_total + B1_2_1_total + B1_2_2_total + B1_3_1_1_total + B1_3_1_2_total + B1_3_1_3_total + B1_3_2_1_total + B1_4_1_total + B1_4_2_total + B1_4_3_total + B1_4_4_1_total + B1_4_5_1_total + B1_4_4_2_total + B1_5_total + B1_6_1_1_total + B1_6_1_2_total + B1_6_1_3_total + B1_6_2_1_total + B1_6_2_2_total + B1_6_3_1_total + B1_6_3_2_total + B1_6_3_3_total + B1_6_4_1_total + B1_6_4_2_total + B1_7_total + B1_8_1_total + B1_8_2_total + B1_9_1_total + B1_9_2_2_total + B1_8_3_1_total + B1_9_2_1_total + B1_9_2_3_total + B1_9_2_4_total + B1_10_total + B1_11_1_total + B1_11_2_total + B1_11_3_total + B1_12_total + B1_13_total + B1_13_other;
    var total;

    total = QuestionareModel.B1_1_1_2_M_total() + QuestionareModel.B1_1_1_2_L_total() + QuestionareModel.B1_1_1_2_D_total() + QuestionareModel.B1_1_2_2_L_total() + QuestionareModel.B1_1_2_2_D_total() + QuestionareModel.B1_3_1_1_total() + QuestionareModel.B1_3_1_2_total() + QuestionareModel.B1_3_1_3_total() + QuestionareModel.B1_3_2_1_total() + QuestionareModel.B1_4_1_total() + QuestionareModel.B1_4_2_total() + QuestionareModel.B1_4_3_total() + QuestionareModel.B1_4_4_1_total() + QuestionareModel.B1_4_4_2_total() + QuestionareModel.B1_4_5_1_total() + QuestionareModel.B1_4_5_2_total() + QuestionareModel.B1_5_total() + QuestionareModel.B1_6_1_1_total() +
    QuestionareModel.B1_6_1_2_total();

    var textbox = $('input[name*=TextBoxTotalAmount]');
    var span = $('span[id*=LabelTotalBudgetAmount]');
    textbox.val(total);
    span.text(total);
 
}

function totalCallFunction() {
    B1_1_1_2();
    B1_1_2_2();
    B1_2();
    B1_3_1();
    B1_3_2();
    B1_4_1();
    B1_5();
    B1_6();
    B1_7();
    B1_8();
    B1_9();
    B1_10();
    B1_11();
    B1_12();
    B1_13();
    // B1_14();

}

function CheckProjectBudgetValidate() {
    totalCallFunction();
    sum1to13();
    var isValid = true;
    $("#spanValidate").text("");
    $("span[id^='message_error_budget']").each(function () {
        if (this.innerHTML != "") {
            isValid = false;
            return false;
        }
    });
    if (!isValid) {
        $("#spanValidate").text("ข้อมูลที่ระบุยังไม่ถูกต้อง กรุณาตรวจสอบ");
        location.hash = '#' + anchorValidate;
    }
    return isValid;
}

$("input[id^='B1_']").each(function () {
    alert("TEST ID");
});