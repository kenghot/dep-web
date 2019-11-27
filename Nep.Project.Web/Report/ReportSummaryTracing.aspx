<%@ Page Language="C#" Title="รายงานสรุปการติดตามโครงการ" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
    CodeBehind="ReportSummaryTracing.aspx.cs" Inherits="Nep.Project.Web.Report.ReportSummaryTracing" 
    UICulture="th-TH" Culture="th-TH" %>
<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label"><%= Model.ProjectInfo_Province %></label>
                                <div class="col-sm-4">
                                    <input id="DdlProvince" runat="server" style="width:100%; " /> 
                                   
                                </div>
                            <label class="col-sm-2 control-label">เดือน/ปี</label>
                                <div class="col-sm-4">
                                    <nep:DatePicker ID="DatePickerBudgetYear" runat="server" Format="M/yyyy" EnabledTextBox="true" />                                                                  
                                </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ชื่อโครงการ</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxProjectName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">องค์กร</label>
                            <div class="col-sm-4">
                                <asp:TextBox ID="TextBoxOrganizationName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">สถานะ</label>
                            <div class="col-sm-4">
                                <asp:DropDownList runat="server" ID="DropDownListStatus" CssClass="form-control" 
                                   DataTextField="LovName" DataValueField="LovID">                                        
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-6"></div>
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm" 
                                OnClick="ButtonSearch_Click" Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <rsweb:ReportViewer runat="server" ID="ReportViewerSummaryTracing" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
        PageCountMode="Actual">
        <LocalReport DisplayName="รายงานสรุปการติดตามโครงการ">
        </LocalReport>
    </rsweb:ReportViewer>

    <script type="text/javascript">
        $(document).ready(function () {
            var aExport = $("a:contains('Word')");
            if (aExport != null) {
                aExport.each(function (index) {
                    $(this).hide();
                });
            }
        });
    </script>
</asp:Content>