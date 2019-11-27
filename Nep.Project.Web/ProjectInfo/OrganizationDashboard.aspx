<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="OrganizationDashboard.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.OrganizationDashboard"
     UICulture="th-TH" Culture="th-TH" %>
<%@ Import Namespace="Nep.Project.Resources" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #divHelpHover {display:none;}
        #divHelpImg:hover #divHelpHover {display:block;}

        .approve-step-desc {
            cursor:default; 
         
        }

        .approve-step-desc:hover {
            text-decoration:none  !important;
        }

        .approval-step-width {
            width:40px;
        }

         .project-info-grid tr th:first-child , .project-info-grid tr td:first-child {             
            display:none;
            border:none;
        }
              

        .project-info-grid tr:first-child  th:nth-child(2), .project-info-grid td:nth-child(2)  {
            border-left:none;       
        }

        .custom-command{
            width:60px;
        }

         .project-info-grid tr.asp-pagination > td {
            display:table-cell !important;           
            border-top: 1px solid #ccc;            
        }

        .project-info-grid tr.asp-pagination > td table td:first-child {
            display:table-cell !important; 
            border:1px solid rgb(204, 204, 204);
        }

        .project-info-grid.view-data-org tr:first-child  th:nth-child(4), .project-info-grid.view-data-org td:nth-child(4){
            display:none; 
            border:none;  
        }

        /*.project-info-grid.view-data-org tr:first-child  th:nth-child(4), .project-info-grid.view-data-org td:nth-child(4),
        .project-info-grid.view-data-org tr:first-child  th:nth-child(5), .project-info-grid.view-data-org td:nth-child(5){
            display:none; 
            border:none;  
        }*/
        
        .project-info-grid.view-data-province tr:first-child  th:nth-child(5), .project-info-grid.view-data-province td:nth-child(5){
            display:none; 
            border:none;  
        }

        .empty-step {
           font-weight:normal;
           display:block;
           text-align:center;
           
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <!-- dashboard -->

               <!-- chart -->
                <div id="chart"></div>
                    <script>
                        function ShowRow(status) {

                            var trAll = $('#MainContent_GridProjectInfo tr');
                            trAll.hide();
                            var trh = $('#MainContent_GridProjectInfo tr.rowHeader');
                            trh.show();
                            var trs = $('#MainContent_GridProjectInfo tr.' + status);
                            trs.show();
                        }
                        function onGraphClick(e) {
                            //console.log(e);
                            if (e.series.index >= 0)
                              ShowRow(e.series.index + 1);
                        }
                        function onPieClick(e) {
                            //console.log(e);
                             
                                ShowRow(e.dataItem.remark);
                        }
                        function createGraph()
                        {
                            var val = $.parseJSON($("input[id*='hdfDashBoardGrahp']").val());
                            $("#divGraph").kendoChart({
                                title: {
                                    text: "จำนวนองค์กรแยกตามสถานะ"
                                },
                                legend: {
                                    visible: false
                                },
                                seriesDefaults: {
                                    type: "column"
                                },
                                series: [
                                    //{
                                    //name: "ทั้งหมด",
                                    //data: [val[0]],
                                    //color: "LightBlue"
                                    //},
                                {
                                    name: "อนุมัติแล้ว",
                                    data: [val[0]],
                                    color: "Lime"
                                }, {
                                    name: "ยังไม่อนุมัติ",
                                    data: [val[1]],
                                    color: "Yellow"
                                }],
                                valueAxis: {
                                    labels: {
                                        format: "{0}"
                                    },
                                    line: {
                                        visible: false
                                    },
                                    axisCrossingValue: 0
                                },
                                categoryAxis: {
                                    //categories: [1, 2, 3, 4, 5],
                                    line: {
                                        visible: false
                                    },
                                    labels: {
                                        padding: { top: 0 }
                                    }
                                },
                                tooltip: {
                                    visible: true,
                                    format: "{0}%",
                                    template: "#= series.name #: #= value #"
                                },
                                seriesClick: onGraphClick
                            });
                        }
                        function createChart() {
                            var seriesChart =  $.parseJSON($("input[id*='hdfDashBoardPie']").val()) ;
            $("#divPie").kendoChart({
                title: {
                    text: "จำนวนองค์กรแยกตามสถานะ"
                },
                legend: {
                   visible: false
                },
                seriesDefaults: {
                    labels: {
                        //template: "#= category # - #= kendo.format('{0:P}', percentage)# #=value#",
                        template: "#= kendo.format('{0:n}',value)# องค์กร : #= kendo.format('{0:P}', percentage)# ",
                        position: "outsideEnd",
                        visible: true,
                        background: "transparent"
                    }
                },
                //series: [{
                //    type: "pie", data: dataChart }],
                series: seriesChart ,
                tooltip: {
                    visible: true,
                    template: "#= category # - #= kendo.format('{0:P}', percentage) #"
                },
                seriesClick: onPieClick
            });
        }
                        </script>
          
    <!-- end Dashboard -->
    <asp:UpdatePanel ID="UpdatePanelSearch" 
                    UpdateMode="Conditional"
                    runat="server">
        
        <ContentTemplate>
              <asp:HiddenField ID="hdfDashBoardPie" runat="server" />
              <asp:HiddenField ID="hdfDashBoardGrahp" runat="server" />
              <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm noline">

                  <div runat="server" id="Div1" class="col-sm-12">
                      <asp:Label runat="server" ID="lblMessage" ForeColor="Red"></asp:Label>
                       <div class="form-group form-group-sm">
                            <%--<div runat="server"  class="form-group form-group-sm" id="FormGroupProvince">--%>
                                <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %></label>
                                <div class="col-sm-4">
                                    <asp:TextBox id="DdlProvince" runat="server" ></asp:TextBox>
                                </div>
                               
                            <%--</div>--%>
                                <label class="col-sm-2 control-label"><%= UI.LabelBudgetYear %></label>
                                <div class="col-sm-2">
                                    <nep:DatePicker ID="DatePickerBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" ClearTime="true"/>                                                                                                   
                                </div>
                               
                                <div class="col-sm-2">
                                <asp:Button runat="server" ID="Button1" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" 
                                    OnClick="ButtonSearch_Click"
                                    Text="แสดงผล"/>
                                </div>
                </div>
                   </div>
                           </div>
                       </div>
                     </div>
            <div style="text-align:center">
             <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="LightBlue" Font-Size="Smaller"> 1. ทั้งหมด </asp:Label>
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Yellow" Font-Size="Smaller">2. องค์กรที่อนุมัติแล้ว</asp:Label>
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Orange" Font-Size="Smaller">3. องค์กรที่ยังไม่อนุมัติ</asp:Label>
<%--                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Fuchsia" Font-Size="Smaller">4. รอการติดตามประเมินผล</asp:Label>
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Lime" Font-Size="Smaller">5. ที่เสนอมาใหม่</asp:Label>
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Silver" Font-Size="Smaller">6. อื่นๆ</asp:Label>--%>
            </div>
            <br />
            <div >
                <div id="divGraph" class="col-sm-6" style="height:400px"></div>
                <div id="divPie" class="col-sm-6" style="height:500px"></div>
            </div>
<%--     <nep:GridView runat="server" ID="GridProjectInfo" ItemType="Nep.Project.ServiceModels.ProjectInfo.ProjectInfoList" 
               DataKeyNames="ProjectInfoID" 
               SelectMethod="GridProjectInfo_GetData" 
               AllowSorting="true"  AllowPaging="true" PageSize="<%$ code:Nep.Project.Common.Constants.PAGE_SIZE %>"
               AutoGenerateColumns="false" EnableSortingAndPagingCallbacks="true" CssClass="asp-grid project-info-grid"
               OnRowDataBound="GridProjectInfo_RowDataBound" PagerStyle-CssClass="asp-pagination"
               SortedAscendingHeaderStyle-CssClass="sort-asc" SortedDescendingHeaderStyle-CssClass="sort-desc"
               >--%>
   
            <nep:GridView runat="server" ID="OrganizationGrid" ItemType="Nep.Project.ServiceModels.RegisteredOrganizationList" DataKeyNames="OrganizationEntryID"
                SelectMethod="OrganizationGrid_GetData" AllowSorting="true" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="<%#Nep.Project.Common.Constants.PAGE_SIZE %>"
                CssClass="asp-grid" PagerStyle-CssClass="asp-pagination"
                OnRowDataBound="OrganizationGrid_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.Organization_OrganizationName %>" ItemStyle-Width="200" SortExpression="OrganizationName">
                        <ItemTemplate>
                            <asp:HyperLink ID='lnkOrganization' runat='server' Text='<%# Eval("OrganizationName") %>' NavigateUrl='<%# "~/Organization/OrganizationRequestForm?ID=" + Eval("OrganizationEntryID") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.Organization_RequestDate %>" ItemStyle-Width="150" SortExpression="RegisterDate" >
                        <ItemTemplate>
                           <%# Nep.Project.Common.Web.WebUtility.ToBuddhaDateFormat(Convert.ToDateTime(Eval("RegisterDate")), Nep.Project.Common.Constants.UI_FORMAT_DATE_TIME, "-")  %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="วันที่อนุมัติ" ItemStyle-Width="150" SortExpression="ApproveDate" >
                        <ItemTemplate>
                           <%# (Eval("ApproveDate") == null ? "-" : Nep.Project.Common.Web.WebUtility.ToBuddhaDateFormat(Convert.ToDateTime(Eval("ApproveDate")), Nep.Project.Common.Constants.UI_FORMAT_DATE_TIME, "-"))  %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:DynamicField HeaderText="<%$ code: Nep.Project.Resources.Model.Organization_RequestName %>" ItemStyle-Width="120" DataField="RegisterName" />
                    <asp:TemplateField HeaderText="<%$ code: Nep.Project.Resources.Model.Organization_Address %>">
                        <ItemTemplate>
                           <asp:Literal ID="LiteralAddress" runat="server" Text='<%# FormatAddress(Item) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:DynamicField HeaderText="<%$ code: Nep.Project.Resources.Model.ProjectInfo_OrgUnderSupport %>" ItemStyle-Width="200" DataField="OrgUnderSupport" NullDisplayText="-" ConvertEmptyStringToNull="true" />
                    <asp:DynamicField HeaderText="สถานะ" ItemStyle-Width="80" DataField="Status" />
<%--                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="custom-command" ItemStyle-Width="35" Visible="<%$ code:IsDeleteRole %>">
                        <ItemTemplate>                            
                            <asp:ImageButton ID="BudgetDetailButtonDelete" ToolTip="<%$ code:Nep.Project.Resources.UI.ButtonDelete %>" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png" 
                                      CommandName="del" CommandArgument='<%# Eval("OrganizationEntryID") %>' Visible='<%# (Convert.ToBoolean(Eval("IsDeletable")))%>' OnClientClick="return ConfirmToDelete()" />
                        </ItemTemplate>                        
                    </asp:TemplateField>   --%>  
                
                </Columns>
            </nep:GridView>      



        </ContentTemplate>
    </asp:UpdatePanel> 
    
    
</asp:Content>