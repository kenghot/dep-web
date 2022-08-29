using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.ModelBinding;
using Nep.Project.Resources;

namespace Nep.Project.Web.ManageItem
{
    public partial class StrategicList : Nep.Project.Web.Infra.BasePage
    {
        public IServices.IUserProfileService _service { get; set; }
        public IServices.IProviceService _provinceService { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            String[] functions = new String[] { Common.FunctionCode.MANAGE_USER };
            Functions = functions;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                var isDel = Request.QueryString["isDeleteSuccess"];
                if (!String.IsNullOrEmpty(isDel) && (isDel == "true"))
                {
                    ShowResultMessage(Nep.Project.Resources.Message.DeleteSuccess);
                }
                //BindDdlProvince();
                //BindUserRole();

                if (IsDeleteRole)
                {
                    //LinkButtonAdd.Visible = true;
                }

            }

        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }




        public Boolean IsDeleteRole
        {
            get
            {
                bool isTrue = (UserInfo.Roles.Contains(Common.FunctionCode.MANAGE_USER) && UserInfo.IsCenterOfficer);
                return isTrue;
            }
        }

        //protected void BindUserRole()
        //{
        //    ddlRole.Items.Clear();
        //    var roleResult = _service.ListRole();
        //    if (roleResult.IsCompleted)
        //    {
        //        List<ServiceModels.GenericDropDownListData> ddlList = roleResult.Data;
        //        ddlList.Insert(0, new ServiceModels.GenericDropDownListData { Value = "", Text = Nep.Project.Resources.UI.DropdownAll });
        //        ddlRole.DataSource = ddlList;
        //        ddlRole.DataBind();
        //    }
        //    else
        //    {
        //        ShowErrorMessage(roleResult.Message);
        //    }
        //}

        public void ButtonSearch_Click(object sender, EventArgs e)
        {
            List<ServiceModels.IFilterDescriptor> userNameFields = new List<ServiceModels.IFilterDescriptor>();
            if (!String.IsNullOrEmpty(TextBoxStrategicName.Text))
            {
                userNameFields.Add(new ServiceModels.FilterDescriptor() { Field = "ITEMNAME", Operator = ServiceModels.FilterOperator.Contains, Value = TextBoxStrategicName.Text.Trim() });
            }

            List<ServiceModels.IFilterDescriptor> fields = new List<ServiceModels.IFilterDescriptor>();

            List<ServiceModels.IFilterDescriptor> filterComposite = new List<ServiceModels.IFilterDescriptor>();
            if (userNameFields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.Or,
                    FilterDescriptors = userNameFields

                });
            }

            if (fields.Count > 0)
            {
                filterComposite.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = fields
                });
            }

            List<ServiceModels.IFilterDescriptor> filters = new List<ServiceModels.IFilterDescriptor>();
            if (filterComposite.Count > 0)
            {
                filters.Add(new ServiceModels.CompositeFilterDescriptor()
                {
                    LogicalOperator = ServiceModels.FilterCompositionLogicalOperator.And,
                    FilterDescriptors = filterComposite
                });
            }


            this.StrategicGrid.FilterDescriptors = filters;
            this.StrategicGrid.DataBind();

        }

        public List<ServiceModels.ItemList> StrategicGrid_GetData(int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            var result = _service.ListWithItem(StrategicGrid.QueryParameter);
            List<ServiceModels.ItemList> data = new List<ServiceModels.ItemList>();
            totalRowCount = 0;

            if (result.IsCompleted)
            {
                data = result.Data;
                totalRowCount = result.TotalRow;
                StrategicGrid.TotalRows = result.TotalRow;

                if (totalRowCount == 0)
                {
                    ShowResultMessage(Nep.Project.Resources.Message.NoRecord);
                }
            }
            else
            {
                ShowErrorMessage(result.Message);
            }

            return data;

        }

        protected void StrategicGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    ServiceModels.ItemList item = (ServiceModels.ItemList)e.Row.DataItem;
                    HyperLink link = (HyperLink)e.Row.Cells[1].FindControl("lnkStrategicName");

                    String url = "~/ManageItem/StrategicForm";
                    url = ResolveUrl(url + "?ItemID=" + item.ITEMID);
                    link.NavigateUrl = url;
                }
            }
        }

        protected void StrategicGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String key = e.CommandArgument.ToString();
            decimal id = 0;
            Decimal.TryParse(key, out id);

            //if ((e.CommandName == "del") && (id > 0))
            //{
            //    var result = _service.DeleteUser(id);
            //    if (result.IsCompleted)
            //    {
            //        StrategicGrid.DataBind();
            //        ShowResultMessage(result.Message);
            //    }
            //    else
            //    {
            //        ShowErrorMessage(result.Message);
            //    }
            //}

        }


        //private void BindDdlProvince()
        //{
        //    var result = _provinceService.ListOrgProvince(null);
        //    List<ServiceModels.GenericDropDownListData> list = new List<ServiceModels.GenericDropDownListData>();
        //    if (result.IsCompleted)
        //    {
        //        list = result.Data;
        //        list.Insert(0, new ServiceModels.GenericDropDownListData { Value = "", Text = Nep.Project.Resources.UI.DropdownAll });
        //        list.Insert(1, new ServiceModels.GenericDropDownListData { Value = "0", Text = Nep.Project.Resources.UI.LabelNotProvinceName });
        //    }
        //    else
        //    {
        //        ShowErrorMessage(result.Message);
        //    }

        //    //ComboBoxProvince.DataSource = list;
        //    //ComboBoxProvince.DataBind();
        //    //ComboBoxProvince.SelectedIndex = 0;
        //}

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            TextBoxStrategicName.Text = String.Empty;
        }

        

       
    }
}