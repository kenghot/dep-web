using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;

namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class TabAssessmentControl : Nep.Project.Web.Infra.BaseUserControl
    {

        public IServices.IProviceService _provinceService { get; set; }
        public IServices.IProjectInfoService _productService { get; set; }

        public Decimal ProjectID
        {
            get
            {
                if (ViewState["ProjectID"] == null)
                {
                    string stID = Request.QueryString["id"];
                    decimal id = 0;
                    Decimal.TryParse(stID, out id);
                    ViewState["ProjectID"] = id;
                }


                return (decimal)ViewState["ProjectID"];
            }

            set
            {
                ViewState["ProjectID"] = value;
            }
        }

        public bool IsEditable
        {
            get
            {
                bool isEdit = false;
                if(ViewState["IsEditable"] != null){
                    isEdit = Convert.ToBoolean(ViewState["IsEditable"]);
                }
                return isEdit;
            }
            set {
                ViewState["IsEditable"] = value;
            }
        }
              

        protected void ButtonSave_Click(object sender, EventArgs e)
        {              
            if (Page.IsValid)
            {
                var result = _productService.SaveProjectEvaluation(GetData());
                if (result.IsCompleted)
                {
                    Nep.Project.Web.ProjectInfo.ProjectInfoForm page = (Nep.Project.Web.ProjectInfo.ProjectInfoForm)this.Page;
                    page.RebindData("TabPanelAssessment");
                    ShowResultMessage(result.Message);
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }  
        }

       

        public void BindData()
        {
            BindDropdownList();
            //BindComboBoxOrganizationProvince();
            BindChcekBoxStandardStrategics();

            if (ProjectID > 0)
            {
                var result = _productService.GetAssessmentProject(ProjectID);
                if (result.IsCompleted)
                {
                    ServiceModels.ProjectInfo.AssessmentProject obj = result.Data;
                    List<Common.ProjectFunction> functions = _productService.GetProjectFunction(obj.ProjectID).Data;
                    bool isEditable = functions.Contains(Common.ProjectFunction.SaveEvaluation);
                    ButtonSave.Visible = isEditable;
                    IsEditable = isEditable;
                    
                    LabelOfficerProvince.Text = obj.AssessmentProvinceName;
                    HiddenFieldOfficerProvince.Value = obj.AssessmentProvinceID.ToString();
                                     

                    LabelProjectNo.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(obj.ProjectNo ,null, "-");

                    OrganizationName.Text = obj.OrganizationName;

                    LabelProjectName.Text = obj.ProjectName;

                    LabelBudget.Text = Nep.Project.Common.Web.WebUtility.DisplayInHtml(obj.BudgetRequest, "N2", "-");

                    #region เกณฑ์ชี้วัดข้อ 4
                    if (obj.IsPassAss4.HasValue)
                    {
                        RadioButtonListIsPassAss4.SelectedValue = (obj.IsPassAss4 == true) ? "1" : "0";
                    }
                    else
                    {
                        RadioButtonListIsPassAss4.SelectedValue = "1";
                    }
                    #endregion

                    #region เกณฑ์ชี้วัดข้อ 5
                    if (obj.IsPassAss5.HasValue)
                    {
                        RadioButtonListIsPassAss5.SelectedValue = (obj.IsPassAss5 == true) ? "1" : "0";
                    }
                    else
                    {
                        RadioButtonListIsPassAss5.SelectedValue = "1";
                    }
                    #endregion

                    #region เกณฑ์ชี้วัดข้อ 6
                    if((obj.IsPassAss4.HasValue && (obj.IsPassAss4 == true)) && (obj.IsPassAss5.HasValue && (obj.IsPassAss5 == true)) ){
                        // ข้อ ก
                        if (obj.Assessment61.HasValue)
                        {
                            DropDownListAssessmentProjectName.SelectedValue = obj.Assessment61.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentProjectName.SelectedIndex = 0;
                        }

                        // ข้อ ข
                        if (obj.Assessment62.HasValue)
                        {
                            DropDownListAssessmentReason.SelectedValue = obj.Assessment62.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentReason.SelectedIndex = 0;
                        }

                        // ข้อ ค
                        if (obj.Assessment63.HasValue)
                        {
                            DropDownListAssessmentObjective.SelectedValue = obj.Assessment63.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentObjective.SelectedIndex = 0;
                        }

                        // ข้อ ง
                        if (obj.Assessment64.HasValue)
                        {
                            DropDownListAssessmentTargetGroup.SelectedValue = obj.Assessment64.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentTargetGroup.SelectedIndex = 0;
                        }

                        // ข้อ จ
                        if (obj.Assessment65.HasValue)
                        {
                            DropDownListAssessmentLocation.SelectedValue = obj.Assessment65.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentLocation.SelectedIndex = 0;
                        }

                        // ข้อ ฉ
                        if (obj.Assessment66.HasValue)
                        {
                            DropDownListAssessmentTiming.SelectedValue = obj.Assessment66.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentTiming.SelectedIndex = 0;
                        }

                        // ข้อ ช
                        if (obj.Assessment67.HasValue)
                        {
                            DropDownListAssessmentOperationMethod.SelectedValue = obj.Assessment67.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentOperationMethod.SelectedIndex = 0;
                        }

                        // ข้อ ซ
                        if (obj.Assessment68.HasValue)
                        {
                            DropDownListAssessmentBudget.SelectedValue = obj.Assessment68.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentBudget.SelectedIndex = 0;
                        }

                        // ข้อ ณ
                        if (obj.Assessment69.HasValue)
                        {
                            DropDownListAssessmentExpection.SelectedValue = obj.Assessment69.ToString();
                        }
                        else
                        {
                            DropDownListAssessmentExpection.SelectedIndex = 0;
                        }

                        TotalScore.Text = (obj.TotalScore.HasValue) ? obj.TotalScore.ToString() : "-";
                        TotalScoreDesc.Text = Common.Web.WebUtility.DisplayInHtml(obj.EvaluationScoreDesc, null, "");
                    }
                    
                    #endregion                     

                    //ความคิดเห็นประกอบการพิจารณา
                    TextBoxAssessmentDesc.Text = obj.AssessmentDesc;

                    #region ยุทธศาสตร์     
                    if((obj.IsPassMission1.HasValue) && (obj.IsPassMission1 == true)){
                        RadioButtonListStandardStrategics.Items[0].Selected = true;
                    }

                    if ((obj.IsPassMission2.HasValue) && (obj.IsPassMission2 == true))
                    {
                        RadioButtonListStandardStrategics.Items[1].Selected = true;
                    }

                    if ((obj.IsPassMission3.HasValue) && (obj.IsPassMission3 == true))
                    {
                        RadioButtonListStandardStrategics.Items[2].Selected = true;
                    }

                    if ((obj.IsPassMission4.HasValue) && (obj.IsPassMission4 == true))
                    {
                        RadioButtonListStandardStrategics.Items[3].Selected = true;
                    }

                    if ((obj.IsPassMission5.HasValue) && (obj.IsPassMission5 == true))
                    {
                        RadioButtonListStandardStrategics.Items[4].Selected = true;
                    }
                    if ((obj.IsPassMission6.HasValue) && (obj.IsPassMission6 == true))
                    {
                        RadioButtonListStandardStrategics.Items[5].Selected = true;
                    }
                    #endregion

                    //ยุทธศาสตร์จังหวัด
                    TextBoxProvinceMissionDesc.Text = obj.ProvinceMissionDesc;
                                       
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }
            }
        }

        //private void BindComboBoxOrganizationProvince()
        //{
        //    var provinceResult = _provinceService.ListOrgProvince("");

        //    if (provinceResult.IsCompleted)
        //    {
        //        List<ServiceModels.GenericDropDownListData> provinces = provinceResult.Data;
        //        provinces.Insert(0, new ServiceModels.GenericDropDownListData {Text = Nep.Project.Resources.UI.DropdownPleaseSelect, Value = "" });
        //        ComboBoxOrganizationProvince.DataSource = provinces;
        //        ComboBoxOrganizationProvince.DataBind();
        //    }
        //    else
        //    {
        //        ShowErrorMessage(provinceResult.Message);
        //    }
        //}

        private void BindChcekBoxStandardStrategics()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            ServiceModels.GenericDropDownListData data1 = new ServiceModels.GenericDropDownListData();
            data1.Text = Nep.Project.Resources.UI.LabelStandardStrategic1;
            data1.Value = "1";
            list.Add(data1);

            ServiceModels.GenericDropDownListData data2 = new ServiceModels.GenericDropDownListData();
            data2.Text = Nep.Project.Resources.UI.LabelStandardStrategic2;
            data2.Value = "2";
            list.Add(data2);

            ServiceModels.GenericDropDownListData data3 = new ServiceModels.GenericDropDownListData();
            data3.Text = Nep.Project.Resources.UI.LabelStandardStrategic3;
            data3.Value = "3";
            list.Add(data3);

            ServiceModels.GenericDropDownListData data4 = new ServiceModels.GenericDropDownListData();
            data4.Text = Nep.Project.Resources.UI.LabelStandardStrategic4;
            data4.Value = "4";
            list.Add(data4);

            ServiceModels.GenericDropDownListData data5 = new ServiceModels.GenericDropDownListData();
            data5.Text = Nep.Project.Resources.UI.LabelStandardStrategic5;
            data5.Value = "5";
            list.Add(data5);
            ServiceModels.GenericDropDownListData data6 = new ServiceModels.GenericDropDownListData();
            data6.Text = Nep.Project.Resources.UI.LabelStandardStrategic6;
            data6.Value = "6";
            list.Add(data6);

            RadioButtonListStandardStrategics.DataSource = list;
            RadioButtonListStandardStrategics.DataTextField = "Text";
            RadioButtonListStandardStrategics.DataValueField = "Value";
            RadioButtonListStandardStrategics.DataBind();
        }

        private void BindDropdownList()
        {
            List<ServiceModels.GenericDropDownListData> list;
            int score;

            // 1.ชื่อโครงการ
            list = new List<ServiceModels.GenericDropDownListData>();        
            score = Convert.ToInt32(Common.AssessmentProjectNameScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProjectNameScore.Score2);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentProjectName.DataSource = list;
            DropDownListAssessmentProjectName.DataBind();

            // 2.หลักการและเหตุผล
            list = new List<ServiceModels.GenericDropDownListData>();          
            score = Convert.ToInt32(Common.AssessmentPrinciplesScore.Score15);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentPrinciplesScore.Score2);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentReason.DataSource = list;
            DropDownListAssessmentReason.DataBind();

            // 3.วัตถุประสงค์ของโครงการ
            list = new List<ServiceModels.GenericDropDownListData>();           
            score = Convert.ToInt32(Common.AssessmentObjectiveScore.Score15);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentObjectiveScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentObjectiveScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentObjective.DataSource = list;
            DropDownListAssessmentObjective.DataBind();

            // 4.กลุ่มเป้าหมาย
            list = new List<ServiceModels.GenericDropDownListData>();           
            score = Convert.ToInt32(Common.AssessmentProjectTargetScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProjectTargetScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProjectTargetScore.Score3);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProjectTargetScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentTargetGroup.DataSource = list;
            DropDownListAssessmentTargetGroup.DataBind();

            // 5.สถานที่
            list = new List<ServiceModels.GenericDropDownListData>();            
            score = Convert.ToInt32(Common.AssessmentSupportPlaceScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentSupportPlaceScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentSupportPlaceScore.Score3);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentSupportPlaceScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentLocation.DataSource = list;
            DropDownListAssessmentLocation.DataBind();

            // 6.ระยะเวลา
            list = new List<ServiceModels.GenericDropDownListData>();            
            score = Convert.ToInt32(Common.AssessmentPeriodScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentPeriodScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentPeriodScore.Score3);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentPeriodScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentTiming.DataSource = list;
            DropDownListAssessmentTiming.DataBind();

            // 7.วิธีการดำเนินงาน
            list = new List<ServiceModels.GenericDropDownListData>();
            score = Convert.ToInt32(Common.AssessmentProcessingScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProcessingScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentProcessingScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentOperationMethod.DataSource = list;
            DropDownListAssessmentOperationMethod.DataBind();

            //8.ข้อบ่งชี้ด้านงบประมาณ
            list = new List<ServiceModels.GenericDropDownListData>();
            score = Convert.ToInt32(Common.AssessmentBudgetScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentBudgetScore.Score5);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentBudgetScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentBudget.DataSource = list;
            DropDownListAssessmentBudget.DataBind();

            //9.ผลที่คาดว่าจะได้รับ
            list = new List<ServiceModels.GenericDropDownListData>();
            score = Convert.ToInt32(Common.AssessmentAnticipationScore.Score10);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentAnticipationScore.Score3);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            score = Convert.ToInt32(Common.AssessmentAnticipationScore.Score0);
            list.Add(new ServiceModels.GenericDropDownListData() { Text = score.ToString(), Value = score.ToString() });

            DropDownListAssessmentExpection.DataSource = list;
            DropDownListAssessmentExpection.DataBind();
        }
              

        private ServiceModels.ProjectInfo.AssessmentProject GetData()
        {
            Decimal num = 0;
            Decimal totalScore = 0;

            ServiceModels.ProjectInfo.AssessmentProject obj = new ServiceModels.ProjectInfo.AssessmentProject();
            obj.ProjectID = ProjectID;

            decimal? assProID = (!String.IsNullOrEmpty(HiddenFieldOfficerProvince.Value))? Convert.ToDecimal(HiddenFieldOfficerProvince.Value) : (decimal?)null;
            obj.AssessmentProvinceID = assProID;

            obj.IsPassAss4 = (RadioButtonListIsPassAss4.SelectedValue == "1") ? true : false;
            obj.IsPassAss5 = (RadioButtonListIsPassAss5.SelectedValue == "1") ? true : false;

            #region เกณฑ์ชี้วัดข้อ 6
            Decimal.TryParse(DropDownListAssessmentProjectName.SelectedValue, out num);
            totalScore += num;
            obj.Assessment61 = num;

            Decimal.TryParse(DropDownListAssessmentReason.SelectedValue, out num);
            totalScore += num;
            obj.Assessment62 = num;

            Decimal.TryParse(DropDownListAssessmentObjective.SelectedValue, out num);
            totalScore += num;
            obj.Assessment63 = num;

            Decimal.TryParse(DropDownListAssessmentTargetGroup.SelectedValue, out num);
            totalScore += num;
            obj.Assessment64 = num;

            Decimal.TryParse(DropDownListAssessmentLocation.SelectedValue, out num);
            totalScore += num;
            obj.Assessment65 = num;

            Decimal.TryParse(DropDownListAssessmentTiming.SelectedValue, out num);
            totalScore += num;
            obj.Assessment66 = num;

            Decimal.TryParse(DropDownListAssessmentOperationMethod.SelectedValue, out num);
            totalScore += num;
            obj.Assessment67 = num;

            Decimal.TryParse(DropDownListAssessmentBudget.SelectedValue, out num);
            totalScore += num;
            obj.Assessment68 = num;

            Decimal.TryParse(DropDownListAssessmentExpection.SelectedValue, out num);
            totalScore += num;
            obj.Assessment69 = num;

            obj.TotalScore = totalScore;
            #endregion 

            #region ยุทธศาสตร์
            if (RadioButtonListStandardStrategics.Items[0].Selected)
            {
                obj.IsPassMission1 = true;
            }
            if (RadioButtonListStandardStrategics.Items[1].Selected)
            {
                obj.IsPassMission2 = true;
            }
            if (RadioButtonListStandardStrategics.Items[2].Selected)
            {
                obj.IsPassMission3 = true;
            }
            if (RadioButtonListStandardStrategics.Items[3].Selected)
            {
                obj.IsPassMission4 = true;
            }
            if (RadioButtonListStandardStrategics.Items[4].Selected)
            {
                obj.IsPassMission5 = true;
            }
            if (RadioButtonListStandardStrategics.Items[5].Selected)
            {
                obj.IsPassMission6 = true;
            }
            #endregion

            obj.AssessmentDesc = TextBoxAssessmentDesc.Text.TrimEnd();

            //ยุทธศาสตร์
            var checkboxList = RadioButtonListStandardStrategics.Items;

            //ยุทธศาสตร์จังหวัด
            obj.ProvinceMissionDesc = TextBoxProvinceMissionDesc.Text.TrimEnd();
            obj.IPAddress = Request.UserHostAddress;
            return obj;
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            UpdatePanelAssessment.RenderControl(writer);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HandleStandardStrategicCheckbox" + this.ClientID, @"
                $(function () {
                        if (typeof (handleStandardStrategicCheckbox) != 'undefined') {
                            handleStandardStrategicCheckbox();                            
                        }                  
                    });", true);

            RegisterClientScript();
        }

        private void RegisterClientScript()
        {
            string searchTd = "\"td\"";
            String script = @"
                function handleStandardStrategicCheckbox() {              

                    var controls = $('.standard-strategic-checkbox').find(" + searchTd + @").get();
                    $.each(controls, function () {
                        var td = $(this).get(0);                   
                        $(td).attr('onclick', 'unselectedStrategicCheckbox(event)');
                    });      

                    if("+ Common.Web.WebUtility.ToJSON(IsEditable) +@"){
                        handleCriterion();
                        handleAssessmentDdl(); 
                    }
                             
                }

                function unselectedStrategicCheckbox(e) {
                    var tagName = e.target.tagName.toLowerCase();
                    var checkbox;
                    if (tagName == 'label') {                  
                        checkbox = $(e.target).prev().get(0);
                    } else {
                        checkbox = $(e.target).get(0);
                    }

                    var isChecked = checkbox.checked;
                    var valueChecked = $(checkbox).val();

                    if (isChecked) {
                        var checkboxList = $('.standard-strategic-checkbox').find(" + searchTd + @").get();
                        $.each(checkboxList, function () {
                            var ck = $(this).find('input').get(0);
                            var value = $(ck).val();
                            if (value != valueChecked) {
                                ck.checked = false;
                            }
                        });
                    }
                }

            function handleCriterion() {
                var input = $('.criterion-no-4').find('input');
                var el;
                if ((input != null) && (input.length > 0)) {
                    for (var i = 0; i < input.length; i++) {
                        el = input[i];
                        $(el).click(function () { checkedCriterion(el) });
                    }
                }

                var input = $('.criterion-no-5').find('input');
                if ((input != null) && (input.length > 0)) {
                    for (var i = 0; i < input.length; i++) {
                        el = input[i];
                        $(el).click(function () { checkedCriterion(el) });
                    }
                }
            }

            function handleAssessmentDdl() {
                var ddl = $('.assessment-dropdownlist');
                if ((ddl != null) && (ddl.length > 0)) {
                    for (var i = 0; i < ddl.length; i++) {
                        $(ddl[i]).change(function () { calAssessmentScore() });
                    }
                }
            }

            function checkedCriterion(el) {
                var input = $('.criterion-no-4').find('input');
                var el;
                var isPass = true;
                if ((input != null) && (input.length > 0)) {
                    for (var i = 0; i < input.length; i++) {
                        el = input[i];
                        if (el.checked) {
                            isPass = (el.value == 1);
                        }
                    }
                }

                if (isPass) {
                    input = $('.criterion-no-5').find('input');
                    if ((input != null) && (input.length > 0)) {
                        for (var i = 0; i < input.length; i++) {
                            el = input[i];
                            if (el.checked) {
                                isPass = (el.value == 1);
                            }
                        }
                    }
                }

                if (isPass) {
                    calAssessmentScore();
                } else {
                    var scoreLabel = $('.total-score').get(0);
                    var scoreDescLabel = $('.total-score-desc').get(0);
                    $(scoreLabel).text('-');
                    $(scoreDescLabel).text('');
                }
            }

            function calAssessmentScore() {
                var total = 0, score = 0;
                var ddl = $('.assessment-dropdownlist');
                var scoreDescLabel = $('.total-score-desc').get(0);
                var scoreDesc = '';

                if ((ddl != null) && (ddl.length > 0)) {
                    for (var i = 0; i < ddl.length; i++) {
                        score = parseInt($(ddl[i]).val(), 10);
                        total += score
                    }
                    var scoreLabel = $('.total-score').get(0);
                    $(scoreLabel).text(total);

                    scoreDesc = (total >= 70) ? 'ผ่าน' : 'ไม่ผ่าน';
                    $(scoreDescLabel).text(scoreDesc);
                }
            }
            ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelAssessment,
                       this.GetType(),
                       "ManageAssessmentScript",
                       script,
                       true);

        }
    }
}