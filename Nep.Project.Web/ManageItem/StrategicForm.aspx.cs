using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nep.Project.Web.User
{
    public partial class StrategicForm : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IUserProfileService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }
        public Decimal? ItemID
        {
            get
            {
                decimal? id = (decimal?)null;
                if(ViewState["ItemID"] != null){
                    id = Convert.ToDecimal(ViewState["ItemID"]);
                }else{
                    string strID = Request.QueryString["ItemID"];
                    decimal temID = 0;
                    Decimal.TryParse(strID, out temID);
                    id = (temID > 0) ? temID : (decimal?)null;
                    ViewState["ItemID"] = id;
                }
                
                return id;
            }
        }

        public int ProvinceSelectedIndex
        {
            get { 
                int i = -1;
                if (ViewState["ProvinceSelectedIndex"] != null)
                {
                    i = Convert.ToInt32(ViewState["ProvinceSelectedIndex"]);
                }
                return i;
            }
            set { ViewState["ProvinceSelectedIndex"] = value; }
        }

        public Boolean IsCreateMode
        {
            get
            {
                bool isTrue = (!ItemID.HasValue);
                return isTrue;
            }
        }

        public Boolean IsDeleteRole
        {
            get
            {
                bool isTrue = (UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_USER) && UserInfo.IsCenterOfficer);
                return isTrue;
            }
        }

        public Int32 AdmimistratorRoleID
        {
            get
            {
                return Convert.ToInt32(ViewState["AdmimistratorRoleID"]);
            }

            set
            {
                ViewState["AdmimistratorRoleID"] = value;
            }
        }

        public Int32 ProvinceRoleID
        {
            get
            {
                return Convert.ToInt32(ViewState["ProvinceRoleID"]);
            }

            set
            {
                ViewState["ProvinceRoleID"] = value;
            }
        }

        public List<ServiceModels.GenericDropDownListData> CenterProvince
        {
            get {
                List<ServiceModels.GenericDropDownListData> list = null;
                if(ViewState["CenterProvince"] != null){
                    list = (List<ServiceModels.GenericDropDownListData>)ViewState["CenterProvince"];
                }else{
                    var result = _provinceService.ListOrgProvince("ส่วนกลาง");
                    if(result.IsCompleted){
                        ViewState["CenterProvince"] = result.Data;
                        list = result.Data;
                    }else{
                        ShowErrorMessage(result.Message);
                    }
                }

                return list;
            }
            set { ViewState["CenterProvince"] = value; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_USER };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ItemID.HasValue && Request.QueryString["Success"] == "true")
                {
                    ShowResultMessage(Resources.Message.SaveSuccess);
                }                
                BindData();
            }

            if (ItemID.HasValue)
            {
                Page.Title = "แก้ไขข้อมูลยุทธศาสตร์";
            } else
            {
                Page.Title = "เพิ่มข้อมูลยุทธศาสตร์";
            }

          

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }


        private List<ServiceModels.GenericDropDownListData> GetCenterProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _provinceService.ListOrgProvince("ส่วนกลาง");
            if (result.IsCompleted)
            {
                list = result.Data;
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }

        private List<ServiceModels.GenericDropDownListData> GetProvince()
        {
            List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
            var result = _provinceService.ListProvince(null);
            if (result.IsCompleted)
            {
                list = result.Data;
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return list;
        }

        //private void BindDdlProvince()
        //{
        //    ComboBoxProvince.DataSource = GetProvince();
        //    ComboBoxProvince.DataBind();
        //    ComboBoxProvince.SelectedIndex = 0;
        //}

        private void BindData()
        {
            var adminRoleResult = _service.GetUserAdministratorRoleID();
            if (adminRoleResult.IsCompleted)
            {
                AdmimistratorRoleID = adminRoleResult.Data;
            }
            else
            {
                ShowErrorMessage(adminRoleResult.Message);
                AdmimistratorRoleID = -1;
            }

            var provinceRoleResult = _service.GetUserProvicnceRoleID();
            if(provinceRoleResult.IsCompleted){
                ProvinceRoleID = provinceRoleResult.Data;
            }else{
                ShowErrorMessage(adminRoleResult.Message);
                ProvinceRoleID = -1;
            }

            if (ItemID.HasValue)
            {
                var ItemResult = _service.GetItem((decimal)ItemID, "EVALUATIONSTRATEGIC");
                if (ItemResult.IsCompleted)
                {
                    ServiceModels.Item item = ItemResult.Data;

                    ItemName.Text = item.ITEMNAME;
                    IsActive.Checked = item.ISACTIVE;
                }
                else
                {
                    ShowErrorMessage(ItemResult.Message);
                }

            }
            else
            {                
                IsActive.Checked = true;
            }
            ButtonSave.Visible = IsDeleteRole;
        }

              
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            
            if (Page.IsValid)
            {
                
                ServiceModels.UserProfile user = new ServiceModels.UserProfile();
                ServiceModels.Item Item = new ServiceModels.Item();
                //user.FirstName = ItemName.Text.Trim();
                //user.IsActive = (IsActive.Checked) ? "1" : "0";

               
                Item.ITEMNAME = ItemName.Text.Trim();
                Item.ISACTIVE = (IsActive.Checked) ? true : false;
                Item.ITEMGROUP = "EVALUATIONSTRATEGIC";



                if (IsCreateMode){
                    var createResult = _service.CreateItem(Item);
                    if (createResult.IsCompleted)
                    {
                        Response.Redirect(Page.ResolveClientUrl("~/ManageItem/StrategicForm?Success=true&ItemID=" + createResult.Data.ITEMID));
                    }
                    else
                    {
                        ShowErrorMessage(createResult.Message);
                    }
                }
                else
                {
                    Item.ITEMID = (decimal)ItemID;

                    var updateResult = _service.UpdateItem(user, Item);
                    if (updateResult.IsCompleted)
                    {
                        Response.Redirect(Page.ResolveClientUrl("~/ManageItem/StrategicForm?Success=true&ItemID=" + Item.ITEMID));
                    }
                    else
                    {
                        ShowErrorMessage(updateResult.Message);
                    }
                }               
            }

        }

      
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (ItemID.HasValue)
            {
                var result = _service.DeleteItem((decimal)ItemID);
                if (result.IsCompleted)
                {
                    Response.Redirect(Page.ResolveClientUrl("~/ManageItem/StrategicForm?isDeleteSuccess=true"));
                }
                else
                {
                    ShowErrorMessage(result.Message);
                }

            }
        }

      
      
     
    }
}