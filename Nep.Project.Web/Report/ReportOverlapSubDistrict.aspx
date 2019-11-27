<%@ Page Language="C#" Title="รายงานสรุปความซ้ำซ้อนผู้รับบริการระดับตำบล" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" 
   CodeBehind="ReportOverlapSubDistrict.aspx.cs"  Inherits="Nep.Project.Web.Report.ReportOverlapSubDistrict" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default panel-search">
        <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group form-group-sm noline">
                    <div runat="server" class="col-sm-12">
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ปีงบประมาณ</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DropDownListYear" AutoPostBack="false" runat="server" ClientIDMode="Inherit" CssClass="form-control" ></asp:DropDownList>                          
                            </div>
                            <div class="col-sm-2 control-label">ภาค</div>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DropDownListRegion" AutoPostBack="false" runat="server" ClientIDMode="Inherit" CssClass="form-control" ></asp:DropDownList>                          
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">จังหวัด</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DropDownListProvince" AutoPostBack="false" runat="server" ClientIDMode="Inherit" CssClass="form-control" ></asp:DropDownList>                          
                            </div>
                            <label class="col-sm-2 control-label">อำเภอ</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DropDownListDistrict" AutoPostBack="false" runat="server" ClientIDMode="Inherit" CssClass="form-control" ></asp:DropDownList>                          
                            </div>
                        </div>
                        <div class="form-group form-group-sm">
                            <label class="col-sm-2 control-label">ตำบล</label>
                            <div class="col-sm-4">
                                <asp:DropDownList ID="DropDownListSubDistrict" AutoPostBack="false" runat="server" ClientIDMode="Inherit" CssClass="form-control" ></asp:DropDownList>                          
                            </div>
                        </div>
                        <div class="form-group form-group-sm noline">
                            <div class="col-sm-12 button">
                                <asp:Button runat="server" ID="ButtonSearch" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                    Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" OnClick="ButtonSearch_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <rsweb:ReportViewer runat="server" ID="ReportViewerOverlapSubDistrict" CssClass="report-viewer" Width="1000px" Height="500px" ProcessingMode="Local" SizeToReportContent="False"
        PageCountMode="Actual">
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