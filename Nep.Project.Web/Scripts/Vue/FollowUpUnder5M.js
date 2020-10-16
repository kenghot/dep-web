var screenField = {
    lbTotal1:0, lbTotal2:1, lbGrandTotal:2,
    rd1_1_1: 3, mcb1_1_2: 4, tb1_1_3: 5, 
    rd1_2_1_1: 6, mcb1_2_1_2: 7, tb1_2_1_3: 8,
    rd1_2_2_1: 9, mcb1_2_2_2: 10, tb1_2_2_3: 11,
    rd1_2_3_1: 12, mcb1_2_3_2: 13, tb1_2_3_3: 14,
    rd1_3_1_1: 15, mcb1_3_1_2: 16, tb1_3_1_3: 17,
    rd1_3_2_1: 18, mcb1_3_2_2: 19, tb1_3_2_3: 20,
    rd1_4_1_1: 21, mcb1_4_1_2: 22, tb1_4_1_3: 23,
    rd2_1_1_1: 24,
    rd2_1_2_1: 25,
    rd2_2_1_1: 26,
    lbResultText: 27,
    tbComment: 28,
    tbSuggestion: 29,
    tbSignName1: 30, tbSignName2: 31, tbSignName3: 32,
    tbPosition1: 33, tbPosition2: 34, tbPosition3: 35,
    rd0_1: 36, tb0_1: 37, processMonth: 38, endDate: 39, evaluateDate: 40,
    tbProblem: 41, tbSolution: 42, tbSuccess: 43, tbImplimentor: 44, tbEvaluator:45,
   };

function CalulateFollowUnder5M() {
    //console.log("cal");
    var i = appVueQN.items;
    var GT = appVueQN.items[screenField.lbGrandTotal].v;
    i[screenField.lbTotal1].v = Number(i[screenField.rd1_1_1].v)
    + Number(i[screenField.rd1_2_1_1].v)
    + Number(i[screenField.rd1_2_2_1].v)
    + Number(i[screenField.rd1_2_3_1].v)
    + Number(i[screenField.rd1_3_1_1].v)
    + Number(i[screenField.rd1_3_2_1].v)
    + Number(i[screenField.rd1_4_1_1].v)
    ;
    i[screenField.lbTotal2].v = Number(i[screenField.rd2_1_1_1].v)
+ Number(i[screenField.rd2_1_2_1].v)
+ Number(i[screenField.rd2_2_1_1].v)
     ;
    
    var score = i[screenField.lbTotal1].v + i[screenField.lbTotal2].v;
    i[screenField.lbGrandTotal].v =  score ;
    var text = "";
 
    if (score < 20) {
        text = "<center>โครงการระดับ E (ปรับปรุง)</center><br>"
        text += "โครงการไม่ประสบความสำเร็จมีสิ่งที่ควรปรับปรุงแก้ไขโครงการไม่มีความสอดคล้องกับคนพิการไม่สามารถแก้ไขปัญหาและไม่ก่อให้เกิดประโยชน์ต่อคนพิการอย่างแท้จริงวัตถุประสงค์ไม่สอดคล้องกับการดำเนินกิจกรรรมหรือตัวชี้วัดทำให้โครงการไม่บรรลุผลตามเป้าหมายที่วางไว้";
    }

    if (score >= 20 && score < 40) {
        text = "<center>โครงการระดับ D (พอใช้)</center><br>โครงการประสบความสำเร็จประมาณ 20 - 39% โครงการมีความสอดคล้องแต่ยังไม่สามารถแก้ไขปัญหาของคนพิการที่เข้าร่วมโครงการได้รวมทั้งไม่สามารถทำให้คนพิการเพิ่มประสิทธิภาพและประสิทธิผลในตนเองได้อีกทั้งโครงการสามารถเข้าถึงคนพิการได้เพียงเล็กน้อยวัตถุประสงค์ไม่ตรงกับกลุ่มเป้าหมายที่ตั้งไว้";
    }
 

    if (score >= 40 && score < 60) {
        text = "<center>โครงการระดับ C (ปานกลาง)</center><br>โครงการประสบความสำเร็จประมาณ 40 - 59%  โครงการมีความสอดคล้องกับคนพิการสามารถแก้ไขปัญหาเบื้องต้นให้กับคนพิการได้ไม่ก่อให้เกิดประโยชน์ต่อคนพิการวัตถุประสงค์บางประการไม่สอดคล้องกับยุทธศาสตร์หรือนโยบายระดับประเทศ กรม และ กองทุน อีกทั้งระยะเวลาในการดำเนินโครงการยังไม่มีความเหมาะสม ";
    }

    if (score >= 60 && score < 80) {
        text = "<center>โครงการระดับ B (ดี)</center><br>โครงการประสบความสำเร็จประมาณ 60 - 79% สอดคล้องกับเกณฑ์ทั่วไป เกณฑ์เฉพาะประสิทธิภาพแล ประสิทธิผลของโครงการ แต่ยังต้องมีการพัฒนาและปรับปรุงในบางประเด็น เป็นโครงการที่ก่อให้เกิดการพัฒนาต่อกลุ่มคนพิการมีการจัดสรรทรัพยากรตามแผนงาน";
    }


    if (score >= 80) {
        text = "<center>โครงการระดับ A (ดีมาก)</center><br>โครงการประสบความสำเร็จสอดคล้องกับเกณฑ์ทั่วไปเกณฑ์เฉพาะประสิทธิภาพและประสิทธิผลของโครงการวัตถุประสงค์สอดคล้องกับยุทธศาสตร์หรือนโยบายระดับประเทศ กรม และกองทุน มีการจัดสรรทรัพยากร ที่ได้รับอย่างคุ้มค่าและเหมาะสมสามารถตอบสนองต่อปัญหาของคนพิการได้อย่างเป็นรูปธรรมและยั่งยืนคนพิการสามารถพึ่งตนเองได้ในระยะยาวอีกทั้งโครงการทำให้เกิดการเปลี่ยนแปลงในระดับท้องถิ่งและระดับประเทศ";
    }
 
    i[screenField.lbResultText].v = text;

}

function CalAllSatisfy() {
    Object.keys(screenField).forEach(function (a){
        if (a.substring(0,2) == "QN"){
            CalSastisfy({id:a})
        }
    }
    );
}