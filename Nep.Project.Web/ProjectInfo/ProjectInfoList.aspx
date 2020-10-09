<%@ Page Title="รายการโครงการ" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="ProjectInfoList.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.ProjectInfoList"
    UICulture="th-TH" Culture="th-TH" %>

<%@ Import Namespace="Nep.Project.Resources" %>



<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /*#divHelpHover {display:none;}
        #divHelpImg:hover #divHelpHover {display:block;}*/

        .approve-step-desc {
            cursor: default;
        }

            .approve-step-desc:hover {
                text-decoration: none !important;
            }

        .approval-step-width {
            width: 40px;
        }

        .project-info-grid tr th:first-child, .project-info-grid tr td:first-child {
            display: none;
            border: none;
        }


        .project-info-grid tr:first-child th:nth-child(2), .project-info-grid td:nth-child(2) {
            border-left: none;
        }

        .custom-command {
            width: 60px;
        }

        .project-info-grid tr.asp-pagination > td {
            display: table-cell !important;
            border-top: 1px solid #ccc;
        }

            .project-info-grid tr.asp-pagination > td table td:first-child {
                display: table-cell !important;
                border: 1px solid rgb(204, 204, 204);
            }

        .project-info-grid.view-data-org tr:first-child th:nth-child(4), .project-info-grid.view-data-org td:nth-child(4) {
            display: none;
            border: none;
        }

        /*.project-info-grid.view-data-org tr:first-child  th:nth-child(4), .project-info-grid.view-data-org td:nth-child(4),
        .project-info-grid.view-data-org tr:first-child  th:nth-child(5), .project-info-grid.view-data-org td:nth-child(5){
            display:none; 
            border:none;  
        }*/

        .project-info-grid.view-data-province tr:first-child th:nth-child(5), .project-info-grid.view-data-province td:nth-child(5) {
            /*  display:none; 
            border:none;  */
        }

        .empty-step {
            font-weight: normal;
            display: block;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- dashboard -->

    <div id="example">

        <div id="window">

            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="True">
                <ContentTemplate>

                    <asp:Label ID="lblDashBoard" runat="server" Text="Panel created."></asp:Label><br />
                    <asp:HiddenField ID="hdfDashBoard" runat="server" />
                    <!-- chart -->
                    <div id="chart"></div>
                    <script>
                        function createChart() {
                            var seriesChart = $.parseJSON($("input[id*='hdfDashBoard']").val());
                            $("#chart").kendoChart({
                                title: {
                                    text: "โครงการแยกตามประเภท"
                                },
                                legend: {
                                    position: "top"
                                },
                                seriesDefaults: {
                                    labels: {
                                        //template: "#= category # - #= kendo.format('{0:P}', percentage)# #=value#",
                                        template: "#=value# โครงการ - #= kendo.format('{0:P}', percentage)# ",
                                        position: "outsideEnd",
                                        visible: true,
                                        background: "transparent"
                                    }
                                },
                                //series: [{
                                //    type: "pie", data: dataChart }],
                                series: seriesChart,
                                tooltip: {
                                    visible: true,
                                    template: "#= category # - #= kendo.format('{0:P}', percentage) #"
                                }
                            });
                        }
                    </script>
                    <!-- end chart-->
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnRefreshDashBoard" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>


        <%--<span id="undo" style="display:none" class="k-button hide-on-narrow">Dashboard</span>--%>

        <div class="responsive-message"></div>

        <script>

            $(document).ready(function () {
                var myWindow = $("#window"),
                    undo = $("#undo");

                //undo.click(function() {
                //    myWindow.data("kendoWindow").open();
                //    undo.fadeOut();
                //});

                //function onClose() {
                //    undo.fadeIn();
                //}

                myWindow.kendoWindow({
                    width: "650px",
                    height: "500px",
                    title: "Dashboard",
                    visible: false,
                    actions: [
                        //"Pin",
                        //"Minimize",
                        "Maximize",
                        "Close"
                    ],
                    // close: onClose
                }).data("kendoWindow").center() //.open();
            });

        </script>

        <style>
            #example {
                min-height: 0px;
            }

            #undo {
                text-align: center;
                position: absolute;
                white-space: nowrap;
                padding: 1em;
                cursor: pointer;
            }

            .armchair {
                float: left;
                margin: 30px 30px 120px 30px;
                text-align: center;
            }

                .armchair img {
                    display: block;
                    margin-bottom: 10px;
                }

            .k-i-maximize {
                background-position: -16px -80px
            }

            .k-i-close {
                background-position: -32px -192px
            }

            .k-i-restore {
                background-position: -48px -80px
            }

            .k-window-titlebar {
                position: absolute;
                width: 100%;
                height: 1.1em;
                border-bottom-style: solid;
                border-bottom-width: 1px;
                margin-top: -2em;
                padding: .4em 0;
                font-size: 1.2em;
                white-space: nowrap;
                min-height: 16px;
            }

            .k-header {
                border-color: #dbdbdb;
            }

            .k-window {
                border-color: rgba(0,0,0,.3);
                -webkit-box-shadow: 1px 1px 7px 1px rgba(128,128,128,.3);
                box-shadow: 1px 1px 7px 1px rgba(128,128,128,.3);
                background-color: #fff;
                border-radius: 6px;
                display: inline-block;
                position: absolute;
                z-index: 10001;
                border-style: solid;
                border-width: 1px;
                padding-top: 2em;
            }
            /*@media screen and (max-width: 1023px) {
                    div.k-window {
                        display: none !important;
                    }
                }*/
        </style>
    </div>
    <!-- end Dashboard -->
    <asp:UpdatePanel ID="UpdatePanelSearch"
        UpdateMode="Conditional"
        runat="server">

        <ContentTemplate>

            <div class="count-alert-followup">
                <asp:Label ID="LabelTotalIsFollowup" runat="server" />
            </div>
            <div class="panel panel-default panel-search">
                <div class="panel-heading panel-heading-search"><%=UI.LabelSearch %></div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm noline">
                            <div runat="server" id="ColumnLeft" class="col-sm-12">
                                <div class="form-group form-group-sm noline">
                                    <div class="col-sm-11  text-right">
                                        <asp:LinkButton ID="LinkButtonExpandAdvanceSearch" runat="server" Text="<%$ code:UI.LabelAdvanceSearch %>" ToolTip="<%$ code:UI.LabelAdvanceSearch %>"
                                            OnClick="LinkButtonExpandAdvanceSearch_Click" CssClass="glyphicon glyphicon-menu-right">
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="LinkButtonCollapseAdvanceSearch" runat="server" Text="<%$ code:UI.LabelNormalSearch %>" ToolTip="<%$ code:UI.LabelNormalSearch %>"
                                            OnClick="LinkButtonCollapseAdvanceSearch_Click" Visible="false" CssClass="glyphicon glyphicon-menu-left" />
                                    </div>
                                </div>

                                <div class="form-group form-group-sm" id="FormGroupOrgName" runat="server">
                                    <label for="<%= TextBoxContractOrgName.ClientID %>" class="col-sm-2 control-label"><%= Model.Contract_OrgName %></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" ID="TextBoxContractOrgName" CssClass="form-control" />
                                    </div>

                                    <div id="FormGroupOrgType" runat="server">
                                        <label class="col-sm-2 control-label"><%= UI.LabelOrgType %></label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList runat="server" ID="DropDownListOrgType" CssClass="form-control">
                                                <asp:ListItem Text="<%$ code:UI.DropdownAll %>" Value=""></asp:ListItem>
                                                <asp:ListItem Text="<%$ code:Model.ProjectInfo_OrganizationGovType %>" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="<%$ code:Model.ProjectInfo_OrganizationPersonType %>" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group form-group-sm">
                                    <label for="<%= TextBoxProjectNo.ClientID %>" class="col-sm-2 control-label"><%= Model.Processing_ContractNo %></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" ID="TextBoxProjectNo" CssClass="form-control" />
                                    </div>

                                    <label for="<%=TextBoxProjectName.ClientID %>" class="col-sm-2 control-label"><%= Model.Processing_ProjectName %></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox runat="server" ID="TextBoxProjectName" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-group form-group-sm">
                                    <label for="<%= DropDownListProjectInfoType.ClientID %>" class="col-sm-2 control-label"><%= Model.ProjectInfo_ProjectInfoType %></label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList runat="server" ID="DropDownListProjectInfoType" CssClass="form-control"
                                            DataTextField="LovName" DataValueField="LovID">
                                        </asp:DropDownList>
                                    </div>

                                    <label for="<%= DatePickerBudgetYear.ClientID %>" class="col-sm-2 control-label"><%= UI.LabelBudgetYear %></label>
                                    <div class="col-sm-4">
                                        <nep:DatePicker ID="DatePickerBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" ClearTime="true" />
                                    </div>
                                </div>

                                <div class="form-group form-group-sm">
                                    <label for="<%= DatePickerSubmitedDateStart.ClientID %>" class="col-sm-2 control-label"><%= Model.ProjectInfo_SubmitedDate %></label>
                                    <div class="col-sm-4">
                                        <nep:DatePicker ID="DatePickerSubmitedDateStart" runat="server" Format="dd/MM/yyyy" EnabledTextBox="true" />
                                    </div>
                                    <label for="<%= DatePickerSubmitedDateEnd.ClientID %>" class="col-sm-2 control-label"><%= UI.LabelTo %></label>
                                    <div class="col-sm-4">
                                        <nep:DatePicker ID="DatePickerSubmitedDateEnd" runat="server" Format="dd/MM/yyyy" EnabledTextBox="true" />
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label for="<%=DatePickerEndDateStart.ClientID %>" class="col-sm-2 control-label">วันสิ้นสุดโครงการ</label>
                                    <div class="col-sm-4">
                                        <nep:DatePicker ID="DatePickerEndDateStart" runat="server" Format="dd/MM/yyyy" EnabledTextBox="true" />
                                    </div>
                                    <label for="<%= DatePickerEndDateEnd %>" class="col-sm-2 control-label"><%= UI.LabelTo %></label>
                                    <div class="col-sm-4">
                                        <nep:DatePicker ID="DatePickerEndDateEnd" runat="server" Format="dd/MM/yyyy" EnabledTextBox="true" />
                                    </div>
                                </div>
                                <div runat="server" class="form-group form-group-sm" id="FormGroupProvince">
                                    <label for="<%=DdlProvince.ClientID %>" class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %></label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="DdlProvince" runat="server"></asp:TextBox>
                                    </div>

                                </div>




                                <div class="form-group form-group-sm">
                                    <label for="<%=IsAlertFollowup.ClientID %>" class="col-sm-2 control-label"><%=UI.LabelFollowup %></label>
                                    <div class="col-sm-1 control-value">
                                        <asp:CheckBox ID="IsAlertFollowup" runat="server" />
                                    </div>

                                    <label for="<%=CheckBoxCancelContractStatus.ClientID %>" class="col-sm-2 control-label"><%=UI.LabelCancelContractStatus %></label>
                                    <div class="col-sm-1 control-value">
                                        <asp:CheckBox ID="CheckBoxCancelContractStatus" runat="server" />
                                    </div>
                                    <label for="<%=CheckBoxNotApprovalStatus.ClientID %>" class="col-sm-2 control-label"><%=UI.LabelNotApprovalStatus %></label>
                                    <div class="col-sm-1 control-value">
                                        <asp:CheckBox ID="CheckBoxNotApprovalStatus" runat="server" />
                                    </div>
                                    <label for="<%=CheckBoxCancelledProjectRequest.ClientID %>" class="col-sm-2 control-label"><%=UI.LabelCancelledProjectRequest %></label>
                                    <div class="col-sm-1 control-value">
                                        <asp:CheckBox ID="CheckBoxCancelledProjectRequest" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label for="<%=CheckConsiderCancel.ClientID %>" class="col-sm-5 control-label">ขอยกเลิกในช่วงระยะพิจารณาผล</label>
                                    <div class="col-sm-1 control-value">
                                        <asp:CheckBox ID="CheckConsiderCancel" runat="server" />
                                    </div>

                                    <label for="<%= CheckApproveCancel.ClientID %>" class="col-sm-5 control-label">ขอยกเลิกในกรณีอนุมัติโครงการแล้ว</label>
                                    <div class="col-sm-1 control-value">
                                        <asp:CheckBox ID="CheckApproveCancel" runat="server" />
                                    </div>

                                </div>






                            </div>
                            <!--column left-->

                            <div class="col-sm-4" runat="server" id="ColumnRight" visible="false">
                                <!--ประเภทความพิการ-->
                                <div class="form-group form-group-sm noline">
                                    <label for="<%=CheckBoxListTypeDisabilitys.ClientID %>" class="col-sm-12 form-group-title"><%=UI.LabelDisabilityType %></label>
                                </div>
                                <div class="form-group form-group-sm noline">
                                    <div class="col-sm-12">
                                        <asp:CheckBoxList ID="CheckBoxListTypeDisabilitys" runat="server" CssClass="form-control-checkbox"
                                            DataValueField="LovID" DataTextField="LovName">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <!--column right-->

                            <div class="form-group form-group-sm" runat="server" id="AdvanceSerchBlock2" visible="false">
                                <div class="col-sm-8">
                                    <!--ยุทธศาสตร์ที่สอดคล้องกับโครงการ-->
                                    <div class="form-group-title"><%=Model.ProjectInfo_StandardStrategic%></div>
                                    <div>
                                        <asp:CheckBoxList ID="CheckBoxListStandardStrategics" runat="server" CssClass="form-control-checkbox"></asp:CheckBoxList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <!--ขั้นตอนการอนุมัติ-->
                                    <div class="form-group-title"><%=UI.LabelApprovalProcess%></div>
                                    <asp:CheckBoxList ID="CheckBoxListApprovalProcess" runat="server" CssClass="form-control-checkbox"
                                        DataValueField="LovID" DataTextField="LovName">
                                    </asp:CheckBoxList>
                                </div>
                            </div>
                            <div class="form-group form-group-sm noline">
                                <div class="col-sm-12 button">

                                    <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                        OnClick="ButtonSearch_Click"
                                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" />

                                    <asp:Button runat="server" ID="ButtonAdd" ClientIDMode="Inherit" Visible="false" CssClass="btn btn-default btn-sm"
                                        OnClick="ButtonAdd_Click"
                                        Text="<%$ code:Nep.Project.Resources.UI.ButtonAdd %>" />

                                    <%--<asp:HyperLink ID="HyperLinkAddProject" runat="server" CssClass="btn btn-default btn-sm"  NavigateUrl="~/ProjectInfo/Create.aspx"                                  
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonAdd %>" Visible="false"/>--%>

                                    <asp:Button runat="server" ID="ButtonClear" CssClass="btn btn-green btn-sm"
                                        OnClick="ButtonClear_Click"
                                        Text="<%$ code:Nep.Project.Resources.UI.ButtonClear %>" />
                                    <%--         <asp:Button ID="btnRefreshDashBoard" runat="server" OnClientClick="$('#window').data('kendoWindow').open();" 
                                    CssClass="btn btn-default btn-sm"  OnClick="btnRefreshDashBoard_Click" Text="Dash Board" />--%>
                                    <asp:Button ID="btnRefreshDashBoard" runat="server" Visible="false"
                                        CssClass="btn btn-default btn-sm" OnClick="btnRefreshDashBoard_Click" Text="Dash Board" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--form-horizontal-->
                </div>
            </div>
            <!--panel-search-->

            <asp:Label ID="lblCannotAdd" runat="server" ForeColor="#FF3300" Visible="false" Font-Size="Large" Text="ไม่สามารถยืนเสนอโครงการใหม่ได้เนื่องจากไม่ได้ส่งแบบรายผลการปฏิบัติงาน ดังรายการต่อไปนี้"></asp:Label>
            <div id="divHelpImg">
                <div class="form-group form-group-sm">
                    <div class="col-sm-4">
                        <button type="button" class="btn btn-default btn-sm" onclick="CreateEPayment()">สร้างไฟล์สำหรับ e-Payment</button>
                    </div>
                    <div class="col-sm-6">
                        สถานะ 
                <asp:Image ID="imgHelp" alt="help" onmouseover="$('#divHelpHover')[0].style.display = 'block' "
                    onmouseleave="$('#divHelpHover')[0].style.display = 'none'" ImageUrl="~/Images/Approval/ApprovalStatus1_6.png" runat="server" Width="20px" Height="20px" />
                        <div id="divHelpHover" style="display: none">

                            <asp:Image runat="server" alt="arrpove" ImageUrl="~/Images/Approval/ApprovalStatus1_1.png" />อนุมัติ ตามวงเงินที่โครงการขอสนับสนุน<br />
                            <asp:Image runat="server" alt="arrpove" ImageUrl="~/Images/Approval/ApprovalStatus1_2.png" />อนุมัติ ปรับลดวงเงินสนับสนุน<br />
                            <asp:Image runat="server" alt="arrpove" ImageUrl="~/Images/Approval/ApprovalStatus1_3.png" />ไม่อนุมัติ<br />
                            <asp:Image runat="server" alt="arrpove" ImageUrl="~/Images/Approval/ApprovalStatus1_4.png" />ชะลอการพิจารณา<br />
                            <asp:Image runat="server" alt="arrpove" ImageUrl="~/Images/Approval/ApprovalStatus1_5.png" />ยกเลิก<br />
                            <asp:Image runat="server" alt="arrpove" ImageUrl="~/Images/Approval/ApprovalStatus1_6.png" />อื่น
                    <br />
                        </div>
                    </div>
                </div>
            </div>
            <nep:GridView runat="server" ID="GridProjectInfo" ItemType="Nep.Project.ServiceModels.ProjectInfo.ProjectInfoList"
                DataKeyNames="ProjectInfoID"
                SelectMethod="GridProjectInfo_GetData"
                AllowSorting="true" AllowPaging="true" PageSize="<%$ code:Nep.Project.Common.Constants.PAGE_SIZE %>"
                AutoGenerateColumns="false" EnableSortingAndPagingCallbacks="true" CssClass="asp-grid project-info-grid"
                OnRowCommand="GridProjectInfo_RowCommand"
                OnRowDataBound="GridProjectInfo_RowDataBound" PagerStyle-CssClass="asp-pagination"
                SortedAscendingHeaderStyle-CssClass="sort-asc" SortedDescendingHeaderStyle-CssClass="sort-desc">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <th id="th0" rowspan="2" style="width: 2px"></th>
                            <th id="th1" rowspan="2" style="width: 80px">
                                <asp:LinkButton ID="LinkButtonProjectNo" runat="server"
                                    Text="<%$ code:Model.Processing_ContractNo %>" CommandName="Sort" CommandArgument="ProjectNo" />
                            </th>

                            <th id="th2" rowspan="2" style="width: 300px">
                                <asp:LinkButton ID="LinkButtonProjectName" runat="server"
                                    Text="<%$ code:Model.Processing_ProjectName %>" CommandName="Sort" CommandArgument="ProjectName" />
                            </th>
                            <%--kenghot--%>
                            <th id="th3" rowspan="2" style="width: 80px">
                                <asp:LinkButton ID="LinkButtonEndDate" runat="server"
                                    Text="วันสิ้นสุด" CommandName="Sort" CommandArgument="ProjectEndDate" />
                            </th>
                            <th id="th4" rowspan="2" style="width: 200px">
                                <asp:LinkButton ID="LinkButtonOrgName" runat="server"
                                    Text="<%$ code:Model.Contract_OrgName %>" CommandName="Sort" CommandArgument="OrgName" />

                            </th>

                            <th id="th5" rowspan="2" style="width: 100px">
                                <asp:LinkButton ID="LinkButtonProvince" runat="server"
                                    Text="<%$ code:Model.ProjectInfo_Province %>" CommandName="Sort" CommandArgument="ProvinceName" />
                            </th>

                            <th id="th6" rowspan="2" style="width: 80px">
                                <asp:LinkButton ID="LinkButtonBudgetYear" runat="server"
                                    Text="<%$ code:UI.LabelBudgetYear %>" CommandName="Sort" CommandArgument="BudgetYear" />
                            </th>

                            <th id="th7" rowspan="2" style="width: 80px">
                                <asp:LinkButton ID="LinkButtonBudgetAmount" runat="server"
                                    Text="<%$ code:UI.LabelProjectBudget %>" CommandName="Sort" CommandArgument="BudgetValue" />
                            </th>

                            <th id="th8" colspan="6">
                                <%:UI.LabelApprovalProcess %>
                            </th>

                            <th id="th9" rowspan="2"></th>
                            <th rowspan="2" style="width: 35px">สถานะ 
                            </th>
                            <tr>
                                <th id="th10"></th>
                                <th class="approval-step-width">
                                    <asp:HyperLink ID="HyperLinkApprovalStep1" runat="server" ToolTip='<%$ code:ViewState["ApprovalNameStep1"].ToString() %>'
                                        CssClass="approve-step-desc" Text="1" NavigateUrl="javascript:void(0)" />
                                </th>
                                <th id="th11" class="approval-step-width">
                                    <asp:HyperLink ID="HyperLinkApprovalStep2" runat="server" ToolTip='<%$ code:ViewState["ApprovalNameStep2"].ToString() %>'
                                        CssClass="approve-step-desc" Text="2" NavigateUrl="javascript:void(0)" />
                                </th>
                                <th id="th12" class="approval-step-width">
                                    <asp:HyperLink ID="HyperLinkApprovalStep3" runat="server" ToolTip='<%$ code:ViewState["ApprovalNameStep3"].ToString() %>'
                                        CssClass="approve-step-desc" Text="3" NavigateUrl="javascript:void(0)" />
                                </th>
                                <th id="th13" class="approval-step-width">
                                    <asp:HyperLink ID="HyperLinkApprovalStep4" runat="server" ToolTip='<%$ code:ViewState["ApprovalNameStep4"].ToString() %>'
                                        CssClass="approve-step-desc" Text="4" NavigateUrl="javascript:void(0)" />
                                </th>
                                <th id="th14" class="approval-step-width">
                                    <asp:HyperLink ID="HyperLinkApprovalStep5" runat="server" ToolTip='<%$ code:ViewState["ApprovalNameStep5"].ToString() %>'
                                        CssClass="approve-step-desc" Text="5" NavigateUrl="javascript:void(0)" />
                                </th>
                                <th id="th15" class="approval-step-width">

                                    <asp:HyperLink ID="HyperLinkApprovalStep6" runat="server" ToolTip='<%$ code:ViewState["ApprovalNameStep6"].ToString() %>'
                                        CssClass="approve-step-desc" Text="6" NavigateUrl="javascript:void(0)" />
                                </th>

                            </tr>

                        </HeaderTemplate>

                        <ItemTemplate>
                            <td>
                                <input id="<%#Eval("ProjectInfoID") %>" type="checkbox" class="select-row" />
                            </td>
                            <td>

                                <%#Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("ProjectNo"), "", "-") %>
                                <br />
                                <asp:Button ID="ButtonAcknowledged" alt="รับเรื่อง" ToolTip="รับเรื่อง" runat="server" Text="รับเรื่อง" CssClass="btn btn-default btn-sm"
                                    CommandName="Acknowledged" CommandArgument='<%# Eval("ProjectInfoID") %>' Visible="false" OnClientClick="return ConfirmToDelete('ยืนยันการรับเรื่อง')" />
                            </td>

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

                            <td style="text-align: center">
                                <%#Eval("BudgetYearThai") %>                              
                            </td>

                            <td style="text-align: right">
                                <%#Nep.Project.Common.Web.WebUtility.DisplayInHtml(Eval("BudgetValue"), "N2", "") %>
                            </td>

                            <td>
                                <span id="Span1" runat="server" class="approved-checked" visible='<%# (Convert.ToBoolean(Eval("IsStep1Approved"))) %>'
                                    data-toggle="tooltip" data-placement="bottom" title='<%$ code:ViewState["ApprovalNameStep1"].ToString() %>'></span>

                            </td>

                            <td>
                                <span id="Span2" runat="server" class="approved-checked" visible='<%# (Convert.ToBoolean(Eval("IsStep2Approved"))) %>'
                                    data-toggle="tooltip" data-placement="bottom" title='<%$ code:ViewState["ApprovalNameStep2"].ToString() %>'></span>
                                <asp:Label ID="LabelDay2" runat="server" Style="font-size: 10px; border-style: solid; border-width: 1px" Visible='false' />

                            </td>

                            <td>
                                <span id="Span3" runat="server" class="approved-checked" visible='<%# ((Eval("ApprovalStatus") != null)  && (Eval("ApprovalStatus").ToString() == "1") && (Convert.ToBoolean(Eval("IsStep3Approved"))  == true))? true : false   %>'
                                    data-toggle="tooltip" data-placement="bottom" title='<%$ code:ViewState["ApprovalNameStep3"].ToString() %>'></span>
                                <span id="Span10" runat="server" class="empty-step" visible='<%# (Eval("ProvinceAbbr").ToString() == CenterProvinceAbbr)? true : false %>'>-</span>
                                <asp:Label ID="LabelDay3" runat="server" Style="font-size: 10px; border-style: solid; border-width: 1px" Visible='false' />

                            </td>

                            <td>
                                <span id="Span4" runat="server" class="approved-checked" visible='<%# ((Eval("ApprovalStatus") != null)  && (Eval("ApprovalStatus").ToString() == "1") &&  (Convert.ToBoolean(Eval("IsStep4Approved")) == true))? true : false %>'
                                    data-toggle="tooltip" data-placement="bottom" title='<%$ code:ViewState["ApprovalNameStep4"].ToString() %>'></span>
                                <span id="Span8" runat="server" class="empty-step" visible='<%# (Eval("ProvinceAbbr").ToString() != CenterProvinceAbbr)? true : false %>'>-</span>
                            </td>

                            <td>
                                <span id="Span5" runat="server" class="approved-checked" visible='<%# ((Eval("ApprovalStatus") != null)  && (Eval("ApprovalStatus").ToString() == "1") &&  (Convert.ToBoolean(Eval("IsStep5Approved")) == true))? true : false %>'
                                    data-toggle="tooltip" data-placement="bottom" title='<%$ code:ViewState["ApprovalNameStep5"].ToString() %>'></span>
                                <span id="Span9" runat="server" class="empty-step" visible='<%# (Eval("ProvinceAbbr").ToString() != CenterProvinceAbbr)? true : false %>'>-</span>

                            </td>

                            <td>
                                <span id="Span6" runat="server" class="approved-checked" visible='<%# ((Eval("ApprovalStatus") != null)  && (Eval("ApprovalStatus").ToString() == "1") && (Eval("IsStep6Approved") != null) && (Convert.ToBoolean(Eval("IsStep6Approved")) == true))? true : false %>'
                                    data-toggle="tooltip" data-placement="bottom" title='<%$ code:ViewState["ApprovalNameStep6"].ToString() %>'></span>
                                <asp:Label ID="LabelDay4" runat="server" Style="font-size: 10px; border-style: solid; border-width: 1px" Visible='false' />
                            </td>

                            <td class="custom-command">
                                <asp:HyperLink ID="ImageButton1" alt="report" runat="server" ImageUrl="~/Images/icon/excel_small.jpg"
                                    NavigateUrl='<%# "~/Report/ReportSummaryProjectInfo?id="+ Eval("ProjectInfoID").ToString() + "&provinceabbr=" + Eval("ProvinceAbbr").ToString() %>'
                                    Target="_blank" ToolTip="<%$ code:UI.ToolTipViewProjectInfoClick %>" Visible='<%# ( (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer)  && (Eval("ProjectApprovalStatusCode").ToString() != Nep.Project.Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร) )? true : false %>' />
                                <asp:HyperLink ID="ImageButton2" alt="report" runat="server" ImageUrl="~/Images/icon/excel_small.jpg"
                                    NavigateUrl='<%# "~/Report/ReportSummaryProjectInfo1?id="+ Eval("ProjectInfoID").ToString() + "&provinceabbr=" + Eval("ProvinceAbbr").ToString() %>'
                                    Target="_blank" ToolTip="<%$ code:UI.ToolTipViewProjectInfo1Click %>" Visible='<%# ( (UserInfo.IsCenterOfficer || UserInfo.IsProvinceOfficer) && (Eval("ProjectApprovalStatusCode").ToString() != Nep.Project.Common.LOVCode.Projectapprovalstatus.ร่างเอกสาร) )? true : false %>' />
                                <asp:ImageButton ID="ImageButton3" alt="delete" ToolTip="<%$ code:UI.ButtonDelete %>" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png"
                                    CommandName="del" CommandArgument='<%# Eval("ProjectInfoID") %>' Visible='<%#  IsDeleteRole%>' OnClientClick="return ConfirmToDelete()" />
                                <%-- <asp:ImageButton ID="BudgetDetailButtonDelete" alt="delete" ToolTip="<%$ code:UI.ButtonDelete %>" runat="server" ImageUrl="~/Images/icon/round_delete_icon_16.png" 
                                      CommandName="del" CommandArgument='<%# Eval("ProjectInfoID") %>' Visible='<%# (Convert.ToBoolean(Eval("IsDeletable")) && IsDeleteRole)%>' OnClientClick="return ConfirmToDelete()" /> --%>
                            </td>
                            <td style="text-align: center; vertical-align: central">
                                <asp:Image ID="imgApprovalStatus" alt="approve" runat="server" Visible="false" Width="35px" Height="35px" ImageAlign="AbsMiddle" />
                                <asp:Image ImageUrl="~/Images/icon/reported.png" ID="imgReported" alt="ส่งรายงานแล้ว" runat="server" Visible="false" Width="35px" Height="35px" ImageAlign="AbsMiddle" />
                                <%--<%#Eval("BudgetYearThai") %>--%>                              
                            </td>
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>

            </nep:GridView>
            <!--#GridProjectInfo-->

            <script type="text/javascript">
                function ConfirmToDelete(msg) {
                    var message = <%=Nep.Project.Common.Web.WebUtility.ToJSON(Nep.Project.Resources.Message.DeleteConfirmation)%>;
                    if (msg) {
                        message = msg
                    }

                    var isConfirm = window.confirm(message);
                    return isConfirm;
                }
                var col5 = $("tr.asp-pagination table td:contains('5')")
                if (col5.length != 0) {
                    console.log('ok');
                    col5[0].style.display = 'block';
                }
                function CreateEPayment() {
                    var sels = $.map($(".select-row:checked"), function (v) { return v.id })
                    if (sels.length == 0) {
                        alert("กรุณาเลือกโครงการที่จะดำเนินการ")
                        return
                    }
                    if (confirm("ยืนยันการสร้างไฟล์ e-Payment จำนวน " + sels.length + " โครงการ")) {
                        axios.post('http://localhost:8976/api/projects/CreateEPayment', sels).then(
                            response => {
                                console.log(response)
                                if (response && response.status == 200) {
                                    //const url = window.URL.createObjectURL(new Blob([response.data], { type: "application/zip" }))
                                    const url = window.URL.createObjectURL(b64toBlob(response.data))
                                    //const url = window.URL.createObjectURL(response.data)
                                    const link = document.createElement('a');
                                    link.href = url;
                                    const fileName = response.headers["content-disposition"].split("filename=")[1];
                                    link.setAttribute('download', fileName);
                                    document.body.appendChild(link);
                                    link.click();
                                    window.URL.revokeObjectURL(url);
                                    link.remove();// you need to remove that elelment which is created before.
                                } else {
                                    alert(response.data)
                                    return
                                }

                    }
                        ).catch(function (err) {
                            alert(err.response.data.Message)
                            return
                        } )
                }
                }
                function b64toBlob(data) {

                    var byteString = atob(data);
                    var ab = new ArrayBuffer(byteString.length);
                    var ia = new Uint8Array(ab);

                    for (var i = 0; i < byteString.length; i++) {
                        ia[i] = byteString.charCodeAt(i);
                    }
                    return new Blob([ab], { type: 'application/zip' });
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
