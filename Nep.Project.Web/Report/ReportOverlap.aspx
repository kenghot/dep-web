<%@ Page Language="C#" Title="รายงานสรุปความซ้ำซ้อนผู้รับบริการทั้งประเทศ" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportOverlap.aspx.cs" Inherits="Nep.Project.Web.Report.ReportOverlap"
    UICulture="th-TH" Culture="th-TH" %>

<%@ Import Namespace="Nep.Project.Resources" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
     <div class="hiddenfile">
<input  type="file" id="UploadedFile" name="UploadedFile" />
     </div>
    <asp:UpdatePanel ID="UpdatePanelSearch"
        UpdateMode="Conditional"
        runat="server">

        <ContentTemplate>
            <div class="panel panel-default panel-search">
                <div class="panel-heading panel-heading-search">ค้นหาข้อมูล</div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group form-group-sm noline">
                            <div runat="server" class="col-sm-12">
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-2 control-label"><%= UI.LabelBudgetYear %><span class="required"></span></label>
                                    <div class="col-sm-4">
                                        <nep:DatePicker ID="DatePickerStartBudgetYear" runat="server" Format="yyyy" EnabledTextBox="true" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorYear" ControlToValidate="DatePickerStartBudgetYear"
                                            runat="server" CssClass="error-text" SetFocusOnError="true"
                                            Text='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelBudgetYear) %>'
                                            ErrorMessage='<%$ code: String.Format(Nep.Project.Resources.Error.RequiredField, UI.LabelBudgetYear) %>'
                                            ValidationGroup="Search" />
                                    </div>
                                    <div class="col-sm-2 control-label">จังหวัด</div>
                                    <div class="col-sm-4">
                                        <input id="DdlProvince" runat="server" style="width: 100%;" />

                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-2 control-label">ชื่อ - นามสกุล</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBoxName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2 control-label">เลขที่บัตรประชาชน</div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="TextBoxIdCardNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group form-group-sm">
                                    <label class="col-sm-6">หากต้องการค้นหาหลายบุคคล สามารถทำได้โดยการใส่ ',' คั่นแต่ละรายการ เช่น 'สมชาย ใจดี,สุขใจ กล้าหาญ' หรือ '3100602782221,2310059332132'
                                        <br />หรือ คลิ๊ก =></label>
                                     <div class="col-sm-6 button">
                                    <asp:Button runat="server" ID="Button2" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                        Text="อัพโหลดแบบฟอร์มตรวจสอบความซ้ำซ้อนผู้เข้าร่วม" OnClientClick="$('#UploadedFile').click();return false;" CausesValidation="False" /><br />
                                         <a href="../Content/Files/ตรวจสอบความซ้ำซ้อนผู้เข้าร่วม.xlsx">ดาวน์โหลดแบบฟอร์มตรวจสอบความซ้ำซ้อนผู้เข้าร่วม</a>
                                    <%--     <button ID="Button3"  class="btn btn-primary btn-sm"
                                         onclick="$('#UploadedFile').click()"  >ดาวน์โหลดแบบฟอร์มตรวจสอบความซ้ำซ้อนผู้เข้าร่วม</button>--%>
                                         </div>
                                </div>
                                <div class="form-group form-group-sm noline">
                                    <div class="col-sm-12 button">
                                        <asp:Button runat="server" ID="Button1" ClientIDMode="Inherit" CssClass="btn btn-primary btn-sm"
                                            Text="<%$ code:Nep.Project.Resources.UI.ButtonSearch %>" OnClick="ButtonSearch_Click" ValidationGroup="Search" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <style>
                .hiddenfile {
 width: 0px;
 height: 0px;
 overflow: hidden;
}
            </style>

            <rsweb:ReportViewer runat="server" ID="ReportViewerOverlap" CssClass="report-viewer" Width="100%" Height="100%" ProcessingMode="Local" SizeToReportContent="True"
                PageCountMode="Actual">
                <LocalReport DisplayName="รายงานสรุปความซ้ำซ้อนผู้รับบริการทั้งประเทศ">
                </LocalReport>
            </rsweb:ReportViewer>
            
<script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/jszip.js"></script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.8.0/xlsx.js"></script>
<script>

    document.getElementById('UploadedFile').addEventListener('change', handleFileSelect, false);
    var excelObj
    var ExcelToJSON = function () {

        this.parseExcel = function (file) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var data = e.target.result;
                var workbook = XLSX.read(data, {
                    type: 'binary'
                });
                workbook.SheetNames.forEach(function (sheetName) {
                    // Here is your object
                    var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName])
                    var json_object = JSON.stringify(XL_row_object)
                    excelObj = JSON.parse(json_object)
                    console.log(excelObj)
                    var names = []
                    var cards = []
                    var txtName = $('#<%= TextBoxName.ClientID %>')
                    var txtCardID = $('#<%= TextBoxIdCardNo.ClientID %>')
                    for (n = 0; n < excelObj.length; n++) {
                        var i =excelObj[n]
                        var name = ''
                        if (i['ชื่อ']) {
                            name = i['ชื่อ'].trim() + ' '
                        }
                        if (i['ชื่อ']) {
                            name += i['นามสกุล'].trim()
                        }
                        if (name && name.trim() != '') {
                            names.push(name)
                        }
                        if (i.เลขบัตรประชาชน && i.เลขบัตรประชาชน.trim() != '') {
                            cards.push(i.เลขบัตรประชาชน.trim())
                        }
                    }
                    txtName.val('')
                    txtCardID.val('')
                    if (names.length > 0) {
                        txtName.val(names.join(","))
                    }
                    if (cards.length > 0) {
                        txtCardID.val(cards.join(","))
                    }
 
                })
            };

            reader.onerror = function (ex) {
                console.log(ex);
            };

            reader.readAsBinaryString(file)
        };
    };

    function handleFileSelect(evt) {

        var files = evt.target.files; // FileList object
        var xl2json = new ExcelToJSON();
        xl2json.parseExcel(files[0]);
    }
    function handleFileSelect(evt) {

        var files = evt.target.files; // FileList object
        var xl2json = new ExcelToJSON();
        xl2json.parseExcel(files[0]);
    }



</script>

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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
