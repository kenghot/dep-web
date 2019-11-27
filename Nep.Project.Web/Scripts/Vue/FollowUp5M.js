var screenField = {
    numTotalDays: 0,
    num1_1_1: 1, num1_1_2: 2,
    num2_1_1: 3, num2_1_2: 4,
    num2_2_1: 5, num2_2_2: 6,
    num2_3_1: 7, num2_3_2: 8,
    num3_1_1: 9, num3_1_2: 10,
    num4_1_1: 11, num4_1_2: 12,
    num5_1_1: 13, num5_1_2: 14,
    num6_1_1: 15, num6_1_2: 16,
    sumGrid1: 17, sumGrid2: 18,
    sumAll: 19,
    gradeText: 20,
    tbSignName1: 21,
    tbPosition1: 22,
   };

function CalulateFollowUp5M() {
    //console.log("cal");
    var i = appVueQN.items;
    i[screenField.sumGrid1].v = 0;
    i[screenField.sumGrid2].v = 0;
   
    CalulateSumFollowUp5M(i[screenField.num1_1_1], i[screenField.num1_1_2], 5);
    CalulateSumFollowUp5M(i[screenField.num2_1_1], i[screenField.num2_1_2], 3);
    CalulateSumFollowUp5M(i[screenField.num2_2_1], i[screenField.num2_2_2], 3);
    CalulateSumFollowUp5M(i[screenField.num2_3_1], i[screenField.num2_3_2], 3);
    CalulateSumFollowUp5M(i[screenField.num3_1_1], i[screenField.num3_1_2], 1);
    CalulateSumFollowUp5M(i[screenField.num4_1_1], i[screenField.num4_1_2], 10);
    CalulateSumFollowUp5M(i[screenField.num5_1_1], i[screenField.num5_1_2], 10);
    CalulateSumFollowUp5M(i[screenField.num6_1_1], i[screenField.num6_1_2], 2);
    var s1 = i[screenField.sumGrid2].v;
    var s2 = Number((i[screenField.sumGrid1].v / 100).toFixed(2));
    //console.log(i[screenField.sumGrid2].v);
    //console.log((i[screenField.sumGrid1].v / 100).toFixed(2));
    i[screenField.sumGrid2].v = s2
    
    var all = s1 + s2;
    i[screenField.sumAll].v = all;
    var text = "";
    if (all < 70) {
        text = "ระดับ D (โครงการยังไม่ประสบผลสำเร็จเท่าที่ควรไม่ควรได้รับการสนับสนุนและดำเนินการใด ๆ ต่อ)";
    }
    if (all >= 70 && all < 75) {
        text = "ระดับ C (โครงการประสบผลสำเร็จ แต่ไม่อยู่ในระดับที่สมควรขยายผล/ดำเนินต่อหากไม่มีการปรับเปลี่ยน)";
    }
    if (all >= 75 && all < 80) {
        text = "ระดับ C+ (โครงการประสบผลสำเร็จ แต่ยังสามารถปรับปรุงประสิทธิภาพและประสิทธิผลในการดำเนินโครงการ)";
    }
    if (all >= 80 && all < 85) {
        text = "ระดับ B (โครงการประสบผลสำเร็จครบถ้วน)";
    }
    if (all >= 85 && all < 90) {
        text = "ระดับ B+ (โครงการประสบผลสำเร็จ สมควรได้รับการขยายผลดำเนินการต่อ/ดำเนินการต่อ)";
    }
    if (all >= 90 && all < 95) {
        text = "ระดับ A (โครงการประสบผลสำเร็จ สมควรได้รับการขยายผลดำเนินการต่อ/ถ่ายทอดบทเรียน)";
    }
    if (all >= 95) {
        text = "ระดับ A+ (โครงการประสบผลสำเร็จ สมควรได้รับการขยายผลดำเนินการต่อ/ถ่ายทอดบทเรียนอย่างมาก)";
    }
    i[screenField.gradeText].v = text;
}

function CalulateSumFollowUp5M(n1,n2, d) {
    n2.v = Number((n1.v / d).toFixed(2));
     
    appVueQN.items[screenField.sumGrid1].v += n1.v;
    appVueQN.items[screenField.sumGrid2].v += n2.v;
 
}
