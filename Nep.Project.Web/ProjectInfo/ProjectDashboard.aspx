<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ProjectDashboard.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.ProjectDashboard"
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
                                    text: "จำนวนโครงการตามสถานะ"
                                },
                                legend: {
                                    visible: false
                                },
                                seriesDefaults: {
                                    type: "column"
                                },
                                series: [{
                                    name: "ทั้งหมด",
                                    data: [val[0]],
                                    color: "LightBlue"
                                }, {
                                    name: "ใกล้หมดเวลา",
                                    data: [val[1]],
                                    color: "Yellow"
                                }, {
                                    name: "ยังไม่ส่งรายงานประเมิณผล",
                                    data: [val[2]],
                                    color: "Orange"
                                }, {
                                    name: "รอการติดตามประเมินผล",
                                    data: [val[3]],
                                    color: "Fuchsia"
                                }, {
                                    name: "ที่เสนอมาใหม่",
                                    data: [val[4]],
                                    color: "Lime"
                                }, {
                                    name: "อื่นๆ",
                                    data: [val[5]],
                                    color: "Silver"
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
                    text: "งบประมาณแยกตามสถานะ"
                },
                legend: {
                   visible: false
                },
                seriesDefaults: {
                    labels: {
                        //template: "#= category # - #= kendo.format('{0:P}', percentage)# #=value#",
                        template: "#= kendo.format('{0:n}',value)# บาท : #= kendo.format('{0:P}', percentage)# ",
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
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Yellow" Font-Size="Smaller">2. ใกล้หมดเวลา</asp:Label>
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Orange" Font-Size="Smaller">3. ยังไม่ส่งรายงานประเมิณผล</asp:Label>
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Fuchsia" Font-Size="Smaller">4. รอการติดตามประเมินผล</asp:Label>
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Lime" Font-Size="Smaller">5. ที่เสนอมาใหม่</asp:Label>
                <asp:Label runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" BackColor="Silver" Font-Size="Smaller">6. อื่นๆ</asp:Label>
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
   <nep:GridView runat="server" ID="GridProjectInfo" ItemType="Nep.Project.ServiceModels.ProjectInfo.ProjectInfoList" 
               DataKeyNames="ProjectInfoID" 
               AllowSorting="true"  AllowPaging="false" PageSize="9999999"
               AutoGenerateColumns="false" EnableSortingAndPagingCallbacks="true" CssClass="asp-grid project-info-grid"
               SelectMethod="GridProjectInfo_GetData" 
            
               OnRowDataBound="GridProjectInfo_RowDataBound" 
               PagerStyle-CssClass="asp-pagination"
               SortedAscendingHeaderStyle-CssClass="sort-asc" SortedDescendingHeaderStyle-CssClass="sort-desc"
               >
               <Columns>
                   <asp:TemplateField>
                       <HeaderTemplate>
                           <th style="width:80px">
                               <asp:LinkButton ID="LinkButtonProjectNo" runat="server" 
                                   Text="<%$ code:Model.Processing_ContractNo %>" CommandName="Sort" CommandArgument="ProjectNo"/>                               
                           </th>

                           <th  style="width:300px">
                               <asp:LinkButton ID="LinkButtonProjectName" runat="server" 
                                   Text="<%$ code:Model.Processing_ProjectName %>" CommandName="Sort" CommandArgument="ProjectName"/>
                           </th>
                           <%--kenghot--%>
                           <th   style="width:80px">
                               <asp:LinkButton ID="LinkButtonEndDate" runat="server" 
                                   Text="วันสิ้นสุด" CommandName="Sort" CommandArgument="ProjectEndDate"/>
                           </th>
                           <th   style="width:200px">
                               <asp:LinkButton ID="LinkButtonOrgName" runat="server" 
                                   Text="<%$ code:Model.Contract_OrgName %>" CommandName="Sort" CommandArgument="OrgName"/>  
                               
                           </th>

                           <th   style="width:100px">
                               <asp:LinkButton ID="LinkButtonProvince" runat="server" 
                                   Text="<%$ code:Model.ProjectInfo_Province %>" CommandName="Sort" CommandArgument="ProvinceName"/>
                           </th>

                           <th  style="width:80px">
                               <asp:LinkButton ID="LinkButtonBudgetYear" runat="server" 
                                   Text="<%$ code:UI.LabelBudgetYear %>" CommandName="Sort" CommandArgument="BudgetYear"/>
                           </th>

                           <th    style="width:80px">
                               <asp:LinkButton ID="LinkButtonBudgetAmount" runat="server" 
                                   Text="<%$ code:UI.LabelProjectBudget %>" CommandName="Sort" CommandArgument="BudgetAmount"/>
                           </th>
                           
                        
                       </HeaderTemplate>
                       
                       <ItemTemplate>
                           <td><%#Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("ProjectNo"), "", "-") %></td>

                           <td>
                                <asp:HiddenField ID="HiddenFieldIsCancelContract" runat="server" Value='<%#Eval("IsCancelContract") %>' />
                                <asp:HiddenField ID="HiddenFieldIsAlertFolloup" runat="server" Value='<%#Eval("IsFollowup") %>' />                    
                                <asp:HyperLink runat="server" ID="HyperLinkProjectName" NavigateUrl='<%#Page.ResolveClientUrl(String.Format("~/ProjectInfo/ProjectInfoForm?id={0}", Eval("ProjectInfoID"))) %>' 
                                    ToolTip="<%$ code:UI.ToolTipProjectNameClick %>" Text='<%#Eval("ProjectNameDesc") %>' />
                           </td>
                           <%--kenghot--%>
                           <td>
                               <%# String.Format("{0:dd/MM/yyyy}",Eval("ProjectEndDate")) %>
                           </td>
                           <td>
                               <%#Eval("OrganizationName") %>
                           </td>

                           <td>
                               <%#Eval("ProvinceName") %> 
                           </td>

                           <td style="text-align:center">
                               <%#Eval("BudgetYearThai") %>                              
                           </td>

                           <td style="text-align:right">
                               <%#Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("BudgetValue"), "N2", "") %>
                           </td>

              

           
                       </ItemTemplate>  
                       
                   </asp:TemplateField>  
               </Columns> 
                
            </nep:GridView>      



        </ContentTemplate>
    </asp:UpdatePanel> 
    
    
</asp:Content>