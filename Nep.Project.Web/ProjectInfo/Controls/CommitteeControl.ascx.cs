using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.ServiceModels;
using Nep.Project.Common;

namespace Nep.Project.Web.ProjectInfo.Controls
{

    public partial class CommitteeControl : Nep.Project.Web.Infra.BaseUserControl
    {
        public IServices.IProjectInfoService _service { get; set; }
        public Boolean? IsEnabled { get; set; }

        public String ValidateGroupName { 
            get {
                string name = "";
                if (ViewState["ValidateGroupName"] != null)
                {
                    name = ViewState["ValidateGroupName"].ToString();
                }
                return name;
            }
            set
            {
                ViewState["ValidateGroupName"] = value;
            }
        }

        public Decimal OrganizationTypeID
        {
            get
            {
                Decimal typeid = 0;
                if (ViewState["OrganizationTypeID"] != null)
                {
                    typeid = Convert.ToDecimal(ViewState["OrganizationTypeID"]);
                }
                return typeid;
            }
            set
            {
                ViewState["OrganizationTypeID"] = value;
            }
        }
               

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsEnabled.HasValue)
            {
                IsEnabled = true;
            }

            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if ((OrganizationTypeID == Common.OrganizationTypeID.สังกัดกรม) || 
                (OrganizationTypeID == Common.OrganizationTypeID.กระทรวง) || 
                (OrganizationTypeID == Common.OrganizationTypeID.องค์กรปกครองส่วนท้องถิ่น) || 
                ((IsEnabled.HasValue && (IsEnabled == false)) || (HiddenFieldOrganizationID.Value == "")))
            {
                DisableControl(true);
            }
            else
            {
                DisableControl(false);
            }
       
            RegisterClientScrip();

        }

        //protected void Page_PreRender(object sender, EventArgs e)
        //{
        //    if (IsEnabled.HasValue && (IsEnabled == false))
        //    {
        //        DisableControl(true);
        //    }
        //    else
        //    {
        //        DisableControl(false);
        //    }

        //    RegisterClientScrip();
        //}

        protected void RepeaterCommittee_DataBinding(object sender, EventArgs e)
        {
            List<ServiceModels.ProjectInfo.Committee> listCommittee = new List<ServiceModels.ProjectInfo.Committee>();
            Repeater CommitteeRepeater = (Repeater)sender;

            ServiceModels.ProjectInfo.Committee itemMain = new ServiceModels.ProjectInfo.Committee();
            itemMain.No = 1;
            itemMain.CommitteePosition = "ประธาน/นายก";
            listCommittee.Add(itemMain);

            // กรรมการ
            for (int i = 2; i < 13; i++)
            {
                ServiceModels.ProjectInfo.Committee item = new ServiceModels.ProjectInfo.Committee();
                item.No = i;
                item.CommitteePosition = "กรรมการ";               
                listCommittee.Add(item);
            }

            // เจ้าหน้าที่
            for (int j = 1; j < 4; j++)
            {
                ServiceModels.ProjectInfo.Committee itemOfficer = new ServiceModels.ProjectInfo.Committee();
                if (j == 1)
                    itemOfficer.No = 13;
                else
                    itemOfficer.No = null;

                itemOfficer.CommitteePosition = "เจ้าหน้าที่ " + j;               
                listCommittee.Add(itemOfficer);
            }

            CommitteeRepeater.DataSource = listCommittee;
        }

        //public void BindRepeaterCommittee(List<ServiceModels.ProjectInfo.Committee> list)
        //{
        //    List<ServiceModels.ProjectInfo.Committee> listCommittee = new List<ServiceModels.ProjectInfo.Committee>();
        //    listCommittee = list;

        //    for (int i = 0; i < RepeaterCommittee.Items.Count; i++)
        //    {               
                
        //        TextBox txtName = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberName");
        //        TextBox txtSurname = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberSurname");
        //        TextBox txtPosition = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberPosition");
        //        txtName.Text = "";
        //        txtSurname.Text = "";
        //        txtPosition.Text = "";

        //        decimal orderno = i + 1;
        //        var item = listCommittee.Where(x => x.No == orderno).FirstOrDefault();
        //        if (item != null)
        //        {
                    
        //            txtName.Text = item.MemberName;
        //            txtSurname.Text = item.MemberSurname;
        //            txtPosition.Text = item.MemberPosition;
                                        
        //        }
        //    }
        //}

        //private void DisableControl(bool isDisabled)
        //{
        //    bool isEnabled = (!isDisabled);
        //    for (int i = 0; i < RepeaterCommittee.Items.Count; i++)
        //    {
               
        //        TextBox txtName = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberName");
        //        TextBox txtSurname = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberSurname");
        //        TextBox txtPosition = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberPosition");

        //        txtName.Enabled = isEnabled;
        //        txtSurname.Enabled = isEnabled;
        //        txtPosition.Enabled = isEnabled;       
                        
        //    }
        //}

        //public List<ServiceModels.ProjectInfo.Committee> GetDataEditingCommittee()
        //{
        //    List<ServiceModels.ProjectInfo.Committee> list = new List<ServiceModels.ProjectInfo.Committee>();
        //    try
        //    {
        //        string name;
        //        string lastName;
        //        string position;
        //        string committeePosition;
        //        decimal no;
        //        for (int i = 0; i < RepeaterCommittee.Items.Count; i++)
        //        {
        //            committeePosition = string.Empty;           
        //            no = i + 1;
        //            TextBox txtName = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberName");
        //            TextBox txtSurname = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberSurname");
        //            TextBox txtPosition = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberPosition");

        //            name = txtName.Text.Trim();
        //            lastName = txtSurname.Text.Trim();
        //            position = txtPosition.Text.Trim();                    

        //            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(position))
        //            {
        //                if (no == 1)
        //                    committeePosition = "1";
        //                else if (no > 1 && no < 13)
        //                    committeePosition = "2";
        //                else if (no >= 13)
        //                    committeePosition = "3";
                                                
        //                ServiceModels.ProjectInfo.Committee dataItem = new ServiceModels.ProjectInfo.Committee();
        //                dataItem.No = no;
        //                dataItem.CommitteePosition = committeePosition;
        //                dataItem.MemberName = name;
        //                dataItem.MemberSurname = lastName;
        //                dataItem.MemberPosition = position;
        //                list.Add(dataItem);
                                                
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Logging.LogError(Logging.ErrorType.WebError, "Project Info", ex);
        //    }

        //    return list;
        //}

        //public String ValidateOrganizationCommittee()
        //{
        //    String errorMessage = "";
        //    List<ServiceModels.ProjectInfo.Committee> listCommittee = new List<ServiceModels.ProjectInfo.Committee>();
        //    bool hasPosition1 = false;
        //    bool hasPosition2 = false;
        //    bool hasPosition3 = false;
        //    bool isSkipRow = false;
        //    bool isEntryInvalid = false;
            
        //    int fillRow = 0;
        //    int emptyRow = 0;           
        //    int no;
           
        //    TextBox txtName;
        //    TextBox txtSurname;
        //    TextBox txtPosition;

        //    string name;
        //    string lastName;
        //    string position;
        //    for (int i = 0; i < RepeaterCommittee.Items.Count; i++)
        //    {
        //        txtName = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberName");
        //        txtSurname = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberSurname");
        //        txtPosition = (TextBox)RepeaterCommittee.Items[i].FindControl("TextBoxMemberPosition");

        //        name = txtName.Text.Trim();
        //        lastName = txtSurname.Text.Trim();
        //        position = txtPosition.Text.Trim();     
        //        no = i + 1;

                

        //        if ((!String.IsNullOrEmpty(name)) && (!String.IsNullOrEmpty(lastName)) && (!String.IsNullOrEmpty(position)))
        //        {
        //            if (no == 1)
        //                hasPosition1 = true;
        //            else if (no > 1 && no < 13)
        //                hasPosition2 = true;
        //            else if (no >= 13)
        //                hasPosition3 = true;

        //            fillRow = no;
        //            emptyRow = (no == 1) ? 2 : emptyRow;
        //            emptyRow = (no == 2) ? 13 : emptyRow;  
        //            emptyRow = (no == 13) ? 16 : emptyRow;                   
        //        }
        //        else if (String.IsNullOrEmpty(name) && String.IsNullOrEmpty(name) && String.IsNullOrEmpty(position))
        //        {
        //            emptyRow = no;
        //        }
        //        else
        //        {
        //            isEntryInvalid = true;
        //            break;
        //        }

        //        if ((no != 1) && (no != 2) && (no != 13) && (fillRow > emptyRow))
        //        {
        //            isSkipRow = true;
        //            break;
        //        }

        //    }// end for            

        //    if (isEntryInvalid)
        //    {
        //        errorMessage = Nep.Project.Resources.Error.CommitteeValidateEntryName;
        //    }
        //    else if (isSkipRow)
        //    {
        //        errorMessage = Nep.Project.Resources.Error.CommitteeValidateSkip;
        //    }
        //    else if ((!hasPosition1) || (!hasPosition2) || (!hasPosition3))
        //    {
        //        errorMessage = Nep.Project.Resources.Error.ValidateCommitee;
        //    }
        //    return errorMessage;
        //}

        private void DisableControl(bool isDisabled)
        {
            bool isEnabled = (!isDisabled);
            TextBoxHeadCommitteeFirstname.Enabled = isEnabled;
            TextBoxHeadCommitteeSurname.Enabled = isEnabled;
            TextBoxHeadCommitteePosition.Enabled = isEnabled;

            TextBoxCommitteeFirstname.Enabled = isEnabled;
            TextBoxCommitteeLastname.Enabled = isEnabled;
            TextBoxCommitteePosition.Enabled = isEnabled;
            //kenghot
            ComboBoxPosition.Enabled = isEnabled;
            //end kenghot
            ImageButtonSaveCommittee.Visible = isEnabled;
            ImageButtonCancelCommittee.Visible = isEnabled;

            TextBoxOfficerFirstname.Enabled = isEnabled;
            TextBoxOfficerLastname.Enabled = isEnabled;
            TextBoxOfficerPosition.Enabled = isEnabled;
            ImageButtonSaveOfficer.Visible = isEnabled;
            ImageButtonCancelOfficer.Visible = isEnabled;
            ImageHelpCommittee.Visible = isEnabled;
            ImageHelpOfficer.Visible = isEnabled;

        }
        //kenghot
 
        public void RefreshPosition()
        {


            var result = _service.ListPosition();
            ComboBoxPosition.DataSource = result.Data;
            ComboBoxPosition.DataBind();
        }

        public void BindRepeaterCommittee(List<ServiceModels.ProjectInfo.Committee> list, Decimal organizationID, Decimal organizationTypeID)
        {
            int no = 0;
            List<ServiceModels.ProjectInfo.Committee> committeeList = new List<ServiceModels.ProjectInfo.Committee>();
            List<ServiceModels.ProjectInfo.Committee> officerList = new List<ServiceModels.ProjectInfo.Committee>();
            OrganizationTypeID = organizationTypeID;

            if ((organizationTypeID == Common.OrganizationTypeID.กระทรวง) || 
                (organizationTypeID == Common.OrganizationTypeID.สังกัดกรม) || 
                (organizationTypeID == Common.OrganizationTypeID.องค์กรปกครองส่วนท้องถิ่น))
            {
                IsEnabled = false;
                //TextBoxHeadCommitteeFirstname.Enabled = false;
                //TextBoxHeadCommitteeSurname.Enabled = false;
                //TextBoxHeadCommitteePosition.Enabled = false;
            }
            else
            {
                if ((list != null) && (list.Count > 0))
                {
                    //ServiceModels.ProjectInfo.Committee head = list.Where(x => x.CommitteePosition == "1").FirstOrDefault();
                    //TextBoxHeadCommitteeFirstname.Text = head.MemberName;
                    //TextBoxHeadCommitteeSurname.Text = head.MemberSurname;
                    //TextBoxHeadCommitteePosition.Text = head.MemberPosition;

                    committeeList = list.Where(x => x.CommitteePosition == "2").ToList();
                    for (int i = 0; i < committeeList.Count; i++)
                    {
                        no++;
                        committeeList[i].No = no;
                        committeeList[i].UID = Guid.NewGuid().ToString();
                    }

                    no = 0;
                    officerList = list.Where(x => x.CommitteePosition == "3").ToList();
                    for (int i = 0; i < officerList.Count; i++)
                    {
                        no++;
                        officerList[i].No = no;
                        officerList[i].UID = Guid.NewGuid().ToString();
                    }

                    if (IsEnabled.HasValue && (IsEnabled == false))
                    {
                        CommitteeForm.Visible = false;
                        CommitteeLabelBlock.Visible = true;

                        OfficerForm.Visible = false;
                        OfficerLabel.Visible = true;
                    }
                    else
                    {
                        CommitteeForm.Visible = true;
                        CommitteeLabelBlock.Visible = false;

                        OfficerForm.Visible = true;
                        OfficerLabel.Visible = false;
                    }
                }
            }

            

            HiddenFieldOrganizationID.Value = organizationID.ToString();
            HiddenFieldCommitteeData.Value = (committeeList.Count > 0) ? Newtonsoft.Json.JsonConvert.SerializeObject(committeeList) : "";
            HiddenFieldOfficerData.Value = (officerList.Count > 0) ? Newtonsoft.Json.JsonConvert.SerializeObject(officerList) : "";
           
        }

        public String ValidateOrganizationCommittee()
        {
            String errorMessage = "";
            //string headFirstName = TextBoxHeadCommitteeFirstname.Text;
            //headFirstName = headFirstName.Trim();

            //string headLastName = TextBoxHeadCommitteeSurname.Text;
            //headLastName = headLastName.Trim();

            //string headPosition = TextBoxHeadCommitteePosition.Text;
            //headPosition = headPosition.Trim();

            string committeeText = HiddenFieldCommitteeData.Value;
            List<ServiceModels.ProjectInfo.Committee> committeeList = (committeeText != "")? Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceModels.ProjectInfo.Committee>>(committeeText): null;

            string officerText = HiddenFieldOfficerData.Value;
            List<ServiceModels.ProjectInfo.Committee> officerList = (officerText != "") ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceModels.ProjectInfo.Committee>>(officerText) : null;

            //if ((headFirstName == "") || (headLastName == "") || (headPosition == ""))
            //{
            //    if (headFirstName == "")
            //    {
            //        errorMessage = String.Format(Nep.Project.Resources.Error.RequiredField, (Nep.Project.Resources.Model.Committee_Firstname + "ประธาน/นายก"));
            //    }
            //    else if (headLastName == "")
            //    {
            //        errorMessage = String.Format(Nep.Project.Resources.Error.RequiredField, (Nep.Project.Resources.Model.Committee_Surname + "ประธาน/นายก"));
            //    }
            //    else if (headPosition == "")
            //    {
            //        errorMessage = String.Format(Nep.Project.Resources.Error.RequiredField, (Nep.Project.Resources.Model.Committee_Position+ "ประธาน/นายก"));
            //    }
            //}else 
            if (committeeList == null)
            {
                errorMessage = String.Format(Nep.Project.Resources.Error.RequiredField, "กรรมการ");
            }
            else if (officerList == null)
            {
                errorMessage = String.Format(Nep.Project.Resources.Error.RequiredField, "เจ้าหน้าที่");
            }
            return errorMessage;   
        }

        public List<ServiceModels.ProjectInfo.Committee> GetDataEditingCommittee()
        {
            Decimal orgID = Convert.ToDecimal(HiddenFieldOrganizationID.Value);
            List<ServiceModels.ProjectInfo.Committee> list = new List<ServiceModels.ProjectInfo.Committee>();
            ServiceModels.ProjectInfo.Committee item;


            if (IsEnabled.HasValue && (IsEnabled == true) && 
                ((OrganizationTypeID != Common.OrganizationTypeID.กระทรวง) && 
                 (OrganizationTypeID != Common.OrganizationTypeID.สังกัดกรม) &&
                 (OrganizationTypeID != Common.OrganizationTypeID.องค์กรปกครองส่วนท้องถิ่น)))
            {
                //string headFirstName = TextBoxHeadCommitteeFirstname.Text;
                //headFirstName = headFirstName.Trim();

                //string headLastName = TextBoxHeadCommitteeSurname.Text;
                //headLastName = headLastName.Trim();

                //string headPosition = TextBoxHeadCommitteePosition.Text;
                //headPosition = headPosition.Trim();

            
                //no++;
                //item = new ServiceModels.ProjectInfo.Committee
                //{
                //    OrganizationID = orgID,
                //    No = no,
                //    CommitteePosition = "1",
                //    MemberName = headFirstName,
                //    MemberSurname = headLastName,
                //    MemberPosition = headPosition
                //};
                //list.Add(item);
                int no = 0;
                string committeeText = HiddenFieldCommitteeData.Value;
                List<ServiceModels.ProjectInfo.Committee> committeeList = (committeeText != "") ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceModels.ProjectInfo.Committee>>(committeeText) : new List<ServiceModels.ProjectInfo.Committee>();
                for (int i = 0; i < committeeList.Count; i++)
                {
                    no++;
                    item = committeeList[i];
                    item.No = no;
                    list.Add(item);
                }


                string officerText = HiddenFieldOfficerData.Value;
                List<ServiceModels.ProjectInfo.Committee> officerList = (officerText != "") ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<ServiceModels.ProjectInfo.Committee>>(officerText) : new List<ServiceModels.ProjectInfo.Committee>();
                for (int i = 0; i < officerList.Count; i++)
                {
                    no++;
                    item = officerList[i];
                    item.No = no;
                    list.Add(item);
                }
            }

            

            return list;
        }

        private void RegisterClientScrip()
        {
            String scriptUrl = ResolveUrl("~/Scripts/manage.committee.js");
            var refScript = "<script type='text/javascript' src='" + scriptUrl + "'></script>";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelCommittee,
                       this.GetType(),
                       "RefUpdatePanelCommitteeScript",
                       refScript,
                       false);

            String script = @" 
                $(function () {   
                    c2xCommittee.committeeConfig({
                        CheckDupMsg : '" + String.Format(Nep.Project.Resources.Error.DuplicateValue, Nep.Project.Resources.Model.Committee_Firstname) + @"',
                        RequiredFirstnameMsg : '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Firstname) + @"',
                        RequiredLastnameMsg : '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Surname) + @"',
                        RequiredPositionMsg : '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_Position) + @"',
                        RequiredPositionCodeMsg : '" + String.Format(Nep.Project.Resources.Error.RequiredField, Nep.Project.Resources.Model.Committee_PositionCode) + @"',                       

                        ColumnTitle : {No: '" + Nep.Project.Resources.Model.Committee_OrderNo + @"',
                                       FirstName: '" + Nep.Project.Resources.Model.Committee_Firstname + @"', 
                                       LastName: '" + Nep.Project.Resources.Model.Committee_Surname  + @"',
                                       PositionCode: '" + Nep.Project.Resources.Model.Committee_PositionCode + @"',
                                       Position: '" +  Nep.Project.Resources.Model.Committee_Position +@"' },

                        IsView : " + Newtonsoft.Json.JsonConvert.SerializeObject(!IsEnabled) + @",
                        CommitteeGridID : 'CommitteeGrid',
                        OfficerGridID : 'OfficerGrid',

                        HiddenOganizationID: '" + HiddenFieldOrganizationID.ClientID + @"',
                        HiddenCommitteeDataID : '" + HiddenFieldCommitteeData.ClientID + @"',
                        HiddenOfficerDataID: '"+ HiddenFieldOfficerData.ClientID + @"',

                       
                        TxtMainCommitteeFirstNameID: '" + TextBoxHeadCommitteeFirstname.ClientID + @"',
                        TxtMainCommitteeLastNameID: '" + TextBoxHeadCommitteeSurname.ClientID + @"',
                        CBBMainCommitteePosition: '" + ComboBoxPosition.ClientID + @"',

                        BtnSaveCommitteeID: '" + ImageButtonSaveCommittee.ClientID +@"',
                        BtnCancelCommitteeID: '" + ImageButtonCancelCommittee.ClientID + @"',
                        TxtCommitteeFirstNameID: '" + TextBoxCommitteeFirstname.ClientID + @"',
                        TxtCommitteeLastNameID: '" + TextBoxCommitteeLastname.ClientID + @"',
                        TxtCommitteePositionID: '"+ TextBoxCommitteePosition.ClientID +@"',        

                        BtnSaveOfficerID: '" + ImageButtonSaveOfficer.ClientID + @"',
                        BtnCancelOfficerID: '" + ImageButtonCancelOfficer.ClientID + @"',
                        TxtOfficerFirstNameID: '" + TextBoxOfficerFirstname.ClientID + @"',
                        TxtOfficerLastNameID: '" + TextBoxOfficerLastname.ClientID + @"',
                        TxtOfficerPositionID: '" + TextBoxOfficerPosition.ClientID + @"',

                    });

                    c2xCommittee.createGridCommittee('CommitteeGrid');
                    c2xCommittee.createGridCommittee('OfficerGrid');
                }); 

                function validateHeadCommitteeName(oSrc, args) {
                 
                 var isValid = c2xCommittee.validateDupHeadCommittee();
                   
                 args.IsValid = isValid;
               }
                ";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "UpdatePanelCommitteeScript", script, true);
           
        }
    }
}