using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.UserControls
{
    public class GridView : System.Web.UI.WebControls.GridView
    {
        private Int32 _totalRows = 0;
        public GridView():base()
        {
            this.PageSize = Common.Constants.PAGE_SIZE;
            this.CssClass = "asp-grid";
            this.PagerStyle.CssClass = "asp-pagination";
            this.SortedAscendingHeaderStyle.CssClass = "sort-asc";
            this.SortedDescendingHeaderStyle.CssClass = "sort-desc";
            this.RowDataBound += new GridViewRowEventHandler(ListGridView_RowDataBound);
            this.RowCreated += new GridViewRowEventHandler(ListGridView_RowCreated);
        }

        void ListGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager && e.Row != null && e.Row.Cells.Count >= 1)
            {
                Label labelTotal = new Label();
                labelTotal.ID = "LabelTotal";
                labelTotal.CssClass = "label-total-row";
                e.Row.Cells[0].Controls.AddAt(0, labelTotal);
            }
        }

        void ListGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager && e.Row != null && e.Row.Cells.Count >= 1)
            {
                Label labelTotal = e.Row.Cells[0].FindControl("LabelTotal") as Label;
                if (labelTotal != null)
                {
                    var total = this._totalRows;
                    //if (_isInsertFirstLine)
                    //{
                    //    if (total == 1)
                    //    {
                    //        total--;
                    //    }
                    //    else
                    //    {
                    //        total -= ((this._totalRows - 1) / (base.PageSize)) + 1;
                    //    }
                    //}

                    labelTotal.Text = String.Format(Nep.Project.Resources.UI.LabelTotalRow, total);
                }                
            }
        }

        //public override object DataSource
        //{
        //    get
        //    {
        //        return base.DataSource;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            Type valueType = value.GetType();
        //            if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(ServiceModels.ReturnQueryData<>))
        //            {
        //                PropertyInfo isCompletedProperty = valueType.GetProperty("IsCompleted");

        //                if ((Boolean)isCompletedProperty.GetValue(value, null))
        //                {
        //                    PropertyInfo objProperty = valueType.GetProperty("Data");
        //                    object objDatasource = objProperty.GetValue(value, null);
        //                    base.DataSource = objDatasource;

        //                    PropertyInfo totalProperty = valueType.GetProperty("TotalRow");
        //                    this._totalRows = (int)totalProperty.GetValue(value, null);

        //                    Type dataObjType = objDatasource.GetType();

        //                    //PropertyInfo countProperty = dataObjType.GetProperty("Count");
        //                    //int count = (int)countProperty.GetValue(objDatasource, null);
        //                    //if (_isInsertFirstLine)
        //                    //{
        //                    //    if (this._totalRows == 0)
        //                    //    {
        //                    //        this._totalRows++;
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        this._totalRows += ((this._totalRows - 1) / (base.PageSize - 1)) + 1;
        //                    //    }

        //                    //    // First row is Add-Inline
        //                    //    count -= 1;
        //                    //}

        //                    // Fired event PageIndexChanging for re-binding data when delete last item in last page
        //                    if (this.AllowPaging && this._totalRows == 0 && this.PageIndex > 0)
        //                    {
        //                        this.SetPageIndex(this.PageIndex - 1);
        //                    }
        //                }
        //                else
        //                {
        //                    base.DataSource = null;
        //                    this._totalRows = 0;
        //                }
        //            }
        //            else
        //            {
        //                base.DataSource = value;
        //            }
        //        }
        //        else
        //        {
        //            base.DataSource = value;
        //        }
        //    }
        //}


        public Int32 TotalRows
        {
            get
            {
                return _totalRows;
            }
            set
            {
                _totalRows = value;
            }
        }
        
        List<ServiceModels.IFilterDescriptor> _filterDescriptors;
        public List<ServiceModels.IFilterDescriptor> FilterDescriptors
        {            
            set
            {
                this.PageIndex = 0;
                this.TotalRows = 0;
                _filterDescriptors = value;
            }
        }
             

        ServiceModels.QueryParameter _queryParameter;
        public ServiceModels.QueryParameter QueryParameter
        {
            get{                
                if (_queryParameter == null)
                {                    
                    _queryParameter = Nep.Project.Common.Web.NepHelper.ToQueryParameter(_filterDescriptors, this.PageIndex, this.PageSize, this.SortExpression, this.SortDirection);
                }
               
                ServiceModels.QueryParameter obj = _queryParameter;  
                
                return obj;
            }
        }
               
        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();

            return new Object[] { obj, _totalRows, _filterDescriptors };
        }

        protected override void LoadControlState(object state)
        {
            if (state != null)
            {
                Object[] p = state as Object[];
                if (p[0] != null)
                {
                    base.LoadControlState(p[0]);
                }
                if (p[1] != null)
                {
                    _totalRows = (Int32)p[1];
                }
                if (p[2] != null)
                {
                    _filterDescriptors = (List<ServiceModels.IFilterDescriptor>)p[2];
                }
            }
        }

        
    }
}