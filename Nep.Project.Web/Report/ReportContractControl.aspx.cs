using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.ServiceModels.Report;
using Nep.Project.ServiceModels;
using Nep.Project.Business;
using Nep.Project.Common;
using Nep.Project.Resources;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Configuration;

namespace Nep.Project.Web.Report
{
    public partial class ReportContractControl : System.Web.UI.Page
    {

        public IServices.IProjectInfoService _service { get; set; }
        public IServices.IOrganizationParameterService _paramService { get; set; }
        string err = "";
        Decimal ProjectID;
        string urlQRCode = ConfigurationManager.AppSettings["WEBSITE_URL"] +"/ProjectInfo/ProjectInfoForm?id=";
        string addressNo = "";
        string street = "-";
        string moo = "-";
        string subDistrict = "";
        string district = "";
        string province = "";
        string refNoAndrefyear = "";
        string refNo1 = "";
        string refyear = "";
        string contractGiverDate = "";
        string contractReceiveFullName = "";
        string contractReceiveDate = "";
        string approvalNoAndYear = "";
        string approvalDate = "";
        string witnessFullName1 = "";
        string witnessFullName2 = "";
        string ContractReceivePositionSign = "";
        string textAttachPage1 = "";
        string textAttachPage2 = "";
        string textAttachPage3 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["projectId"] != null)
                {
                    ProjectID= Decimal.Parse(Request.QueryString["projectId"]);// get projectid
                    GenReportProjectContract(); // go to gen report
                }
                else
                {
                    err = "ไม่สามารถสร้างรายงานได้";
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                Common.Logging.LogError(Logging.ErrorType.WebError, "Show Report", ex);
            }
            if (err != "")
            {
                Label1.Text = string.Format("พบข้อผิดพลาด : {0}", err);
            }
            
        }
        private void GenReportProjectContract()
        {
            var resultReportFormatContract = _service.GetReportFormatContract(ProjectID);//get data report
            if (resultReportFormatContract.IsCompleted)
            {
                ServiceModels.Report.ReportFormatContract ReportContract = resultReportFormatContract.Data; //get data report
                var resultTabContract = _service.GetProjectContractByProjectID(ProjectID); //get data TabContract
                ServiceModels.ProjectInfo.TabContract TabContract = resultTabContract.Data;// get data TabContract
                var projectApprovalResult = _service.GetProjectApprovalResult(ProjectID); // get data ProjectApproval
                ServiceModels.ProjectInfo.ProjectApprovalResult objProjectApproval = projectApprovalResult.Data;
                if (objProjectApproval.IsCenterReviseProject == true && objProjectApproval.ProjectTypeCode != "7" && objProjectApproval.ProjectTypeCode != "13")
                {   //ส่วนกลาง true =ส่วนกลาง ,ไม่เป็นงานวิจัยและสื่อโฆษณา
                    SaveReportProjectContractCenter(ReportContract, TabContract);
                }
                else if (objProjectApproval.IsCenterReviseProject == false && objProjectApproval.ProjectTypeCode != "7" && objProjectApproval.ProjectTypeCode != "13")
                { //จังหวัด false =จังหวัด ,ไม่เป็นงานวิจัยและสื่อโฆษณา
                    SaveReportProjectContractProvince(ReportContract, TabContract);
                }
                else if (objProjectApproval.ProjectTypeCode == "7" || objProjectApproval.ProjectTypeCode == "13")
                {   //วิจัย  7=วิจัยและนวัตกรรม ,13 =ผลิตสื่อโฆษณา
                    SaveReportProjectContractResearch(ReportContract, TabContract);
                }
                else
                {
                    err = "ไม่เจอข้อมูล ProvinceeCode และ ProjectTypeCode ";
                }
            }
            else
            {
                err = "ไม่สามารถสร้างรายงานได้";
            }
        }
        public dynamic GenQRCode(string textQRCode)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
            };
            var QRcodeurl = new BarcodeWriter();
            QRcodeurl.Format = BarcodeFormat.QR_CODE;
            QRcodeurl.Options = options;
            var resultqrcodeQty = QRcodeurl.Write(textQRCode);
            var gifQRCode = iTextSharp.text.Image.GetInstance(resultqrcodeQty, BaseColor.WHITE);
            gifQRCode.ScaleAbsolute(55, 55); //set size image
            gifQRCode.Border = 0;
            gifQRCode.BorderWidth = 0f;
            gifQRCode.SetAbsolutePosition(530, 780); //set position at page
            return gifQRCode;
        }
        private void GetDataReportFormatContract(ServiceModels.Report.ReportFormatContract ReportContract, ServiceModels.ProjectInfo.TabContract TabContract)
        {
            try
            {
                if (TabContract.ExtendData.AddressAt != null)
                {
                    var data = TabContract.ExtendData.AddressAt;
                    addressNo = data.AddressNo;
                    street = data.Street;
                    moo = data.Moo;
                    subDistrict = data.SubDistrictName;
                    district = data.DistrictName;
                    province = data.ProvinceName;
                }
            }
            catch
            {

            }
            witnessFullName1 = TabContract.ViewerName1 + " " + TabContract.ViewerSurname1;
            witnessFullName2 = TabContract.ViewerName2 + " " + TabContract.ViewerSurname2;
            refNo1 = TabContract.AttorneyNo;
            refyear = Nep.Project.Common.Web.WebUtility.ToBuddhaYear(TabContract.AttorneyYear);
            contractGiverDate = Common.Web.WebUtility.ToBuddhaDateFormat(TabContract.ContractGiverDate, "d MMMM yyyy"); ;
            textAttachPage1 = Nep.Project.Common.Web.WebUtility.numberToThaiText(ReportContract.AttachPage1);
            textAttachPage2 = Nep.Project.Common.Web.WebUtility.numberToThaiText(ReportContract.AttachPage2);
            textAttachPage3 = Nep.Project.Common.Web.WebUtility.numberToThaiText(ReportContract.AttachPage3);
            if (refNo1 != null && refyear != null)
            {
                refNoAndrefyear = refNo1 + "/" + refyear;
            }
            var objProjectApprovalResult = _service.GetProjectApprovalResult(ProjectID);
            if (objProjectApprovalResult.IsCompleted)
            {
                string convertApprovalYear = Nep.Project.Common.Web.WebUtility.ToBuddhaYear(objProjectApprovalResult.Data.ApprovalYear);
                if (objProjectApprovalResult.Data.ApprovalNo != null && convertApprovalYear != null)
                {
                    approvalNoAndYear = objProjectApprovalResult.Data.ApprovalNo + "/" + convertApprovalYear;
                }
                approvalDate = Nep.Project.Common.Web.WebUtility.ToBuddhaDateFormat(objProjectApprovalResult.Data.ApprovalDate, "d MMMM yyyy"); ;
            }
            //มอบอำนาจหรือไม่
            if (TabContract.AuthorizeFlag)
            {   //มอบอำนาจ
                contractReceiveFullName = TabContract.ReceiverName + " " + TabContract.ReceiverSurname;
                ContractReceivePositionSign = TabContract.ReceiverPosition;
                contractReceiveDate = Common.Web.WebUtility.ToBuddhaDateFormat(TabContract.AuthorizeDate, "d MMMM yyyy");
            }
            else
            {   //ไม่มอบอำนาจ 
                contractReceiveFullName = TabContract.ContractReceiveName + " " + TabContract.ContractReceiveSurname;
                ContractReceivePositionSign = TabContract.ContractReceivePosition;
            }
        }
        private void SaveReportProjectContractCenter(ServiceModels.Report.ReportFormatContract ReportContract, ServiceModels.ProjectInfo.TabContract TabContract)
        {
            GetDataReportFormatContract(ReportContract, TabContract);
            string oldFile = Server.MapPath("~/Content/Files/nep-contract-center.pdf");
            MemoryStream ms = new MemoryStream();
            Stream pdfStream = new FileStream(oldFile, FileMode.Open);
            var pdfReader = new PdfReader(pdfStream);
            var pdfStamper = new PdfStamper(pdfReader, ms);
            //page1
            PdfContentByte pdfContentPage1 = pdfStamper.GetOverContent(1);
            //  Font text size and color
            BaseFont baseFont = BaseFont.CreateFont(Server.MapPath("~/Fonts/THSarabun.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            pdfContentPage1.SetColorFill(BaseColor.BLACK);
            pdfContentPage1.SetFontAndSize(baseFont, 16);
            pdfContentPage1.BeginText();
            // postion and write page1
            pdfContentPage1.AddImage(GenQRCode(urlQRCode + ProjectID.ToString()));
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ContractNo, 470, 705, 0);//สัญญาที่ 
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.SignAt, 200, 680, 0);//สัญญานี้ทำขึ้น ณ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, addressNo, 140, 660, 0); //ตั้งอยู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, moo, 200, 660, 0);//หมู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, street, 260, 660, 0);//ถนน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, subDistrict, 145, 642, 0);//ตำบล
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, district, 280, 642, 0);//อำเภอ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, province, 420, 642, 0);//จังหวัด
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ContractDate, 170, 625, 0); //วันที่ ระหว่าง
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ContractBy, 300, 608, 0);//โดย
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Position, 300, 588, 0);//ตำแหน่ง
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, refNoAndrefyear, 400, 570, 0);//ผู้รับมอบ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, contractGiverDate, 160, 552, 0);//ลงวันที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverName, 150, 535, 0);//ฝ่ายหนึ่งกลับ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverAddressNo, 170, 515, 0);//สำนักงานเลขที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (ReportContract.ReceiverMoo!=null)? ReportContract.ReceiverMoo:"", 235, 515, 0);//หมู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (ReportContract.ReceiverStreet != null) ? ReportContract.ReceiverStreet : "", 300, 515, 0);//ถนน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverSubdistrict, 470, 515, 0);//ตำบล
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverDistrict, 170, 500, 0);//อำเภอ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverProvince, 380, 500, 0);//จังหวัด
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, contractReceiveFullName, 170, 482, 0);//โดย
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, contractReceiveDate, 280, 465, 0);//25.ผู้มีอำนาจ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Amount, 180, 370, 0);//จำนวนเงิน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.AmountString, 435, 370, 0); //จำนวนเงิน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ProjectName, 300, 335, 0);//ส่งคำร้อง
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, approvalNoAndYear, 300, 265, 0); //ครั้งที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, approvalDate, 430, 265, 0);//ลงวันที่
            pdfContentPage1.EndText();
            //Page2
            PdfContentByte pdfContentPage2 = pdfStamper.GetOverContent(2);
            pdfContentPage2.SetColorFill(BaseColor.BLACK);
            pdfContentPage2.SetFontAndSize(baseFont, 16);
            pdfContentPage2.BeginText();
            pdfContentPage2.AddImage(GenQRCode(urlQRCode + ProjectID.ToString()));
            pdfContentPage2.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.AttachPage1, 142, 183, 0);
            pdfContentPage2.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage1, 175, 183, 0);
            pdfContentPage2.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.AttachPage2, 445, 120, 0);
            pdfContentPage2.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage2, 475, 120, 0);
            pdfContentPage2.EndText();
            //Page3
            PdfContentByte pdfContentPage3 = pdfStamper.GetOverContent(3);
            pdfContentPage3.SetColorFill(BaseColor.BLACK);
            pdfContentPage3.SetFontAndSize(baseFont, 16);
            pdfContentPage3.BeginText();
            pdfContentPage3.AddImage(GenQRCode(urlQRCode + ProjectID.ToString()));
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_LEFT, approvalNoAndYear, 400, 740, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, approvalDate, 155, 720, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.AttachPage3, 230, 720, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage3, 260, 720, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ContractBy, 330, 465, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Position, 330, 440, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, contractReceiveFullName, 330, 375, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ContractReceivePositionSign, 330, 355, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, witnessFullName1, 330, 290, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, witnessFullName2, 330, 230, 0);
            pdfContentPage3.EndText();
            pdfStamper.Close();

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline;filename=" + "nep-contract-" + ProjectID.ToString() + ".pdf");
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.Flush();
            Response.Clear();
            Response.End();
        }
        private void SaveReportProjectContractProvince(ServiceModels.Report.ReportFormatContract ReportContract, ServiceModels.ProjectInfo.TabContract TabContract)
        {
            GetDataReportFormatContract(ReportContract, TabContract);
            string oldFile = Server.MapPath("~/Content/Files/nep-contract-province.pdf");
            MemoryStream ms = new MemoryStream();
            Stream pdfStream = new FileStream(oldFile, FileMode.Open);
            var pdfReader = new PdfReader(pdfStream);
            var pdfStamper = new PdfStamper(pdfReader, ms);

            //page1
            PdfContentByte pdfContentPage1 = pdfStamper.GetOverContent(1);
            //  Font text size and color
            BaseFont baseFont = BaseFont.CreateFont(Server.MapPath("~/Fonts/THSarabun.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            pdfContentPage1.SetColorFill(BaseColor.BLACK);
            pdfContentPage1.SetFontAndSize(baseFont, 16);
            pdfContentPage1.BeginText();
            pdfContentPage1.AddImage(GenQRCode(urlQRCode+ProjectID.ToString()));
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ContractNo.Replace("/"," "), 473, 740, 0);//สัญญาที่ 
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.SignAt, 200, 715, 0);//สัญญานี้ทำขึ้น ณ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, addressNo, 140, 695, 0); //ตั้งอยู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, moo, 200, 695, 0);//หมู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, street, 260, 695, 0);//ถนน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, subDistrict, 145, 677, 0);//ตำบล
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, district, 280, 677, 0);//อำเภอ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, province, 420, 677, 0);//จังหวัด
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ContractDate, 170, 660, 0); //วันที่ ระหว่าง
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ContractBy, 300, 643, 0);//โดย
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Position, 300, 625, 0);//ตำแหน่ง
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.DirectiveNo, 450, 605, 0);//17.ผู้รับมอบ ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.DirectiveDate, 170, 588, 0);//18ลงวันที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.DirectProvinceNo, 450, 588, 0);//19.คำสั่งจงหวัด
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.DirectProvinceDate.ToString(), 170, 570, 0);//20ลงวันที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverName, 150, 552, 0);//ฝ่ายหนึ่งกลับ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverAddressNo, 170, 533, 0);//สำนักงานเลขที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, (ReportContract.ReceiverMoo != null) ? ReportContract.ReceiverMoo : "", 235, 535, 0);//หมู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (ReportContract.ReceiverStreet != null) ? ReportContract.ReceiverStreet : "", 300, 533, 0);//ถนน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverSubdistrict, 470, 533, 0);//ตำบล
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverDistrict, 170, 517, 0);//อำเภอ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverProvince, 380, 517, 0);//จังหวัด
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, contractReceiveFullName, 170, 500, 0);//โดย
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, contractReceiveDate, 280, 480, 0);//25.ผู้มีอำนาจ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Amount, 180, 390, 0);//จำนวนเงิน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.AmountString, 435, 390, 0); //จำนวนเงิน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ProjectName, 300, 353, 0);//โครงการ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, approvalNoAndYear, 300, 280, 0); //ครั้งที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, approvalDate, 430, 280, 0);//ลงวันที่
            pdfContentPage1.EndText();
            //Page2
            PdfContentByte pdfContentPage2 = pdfStamper.GetOverContent(2);
            pdfContentPage2.SetColorFill(BaseColor.BLACK);
            pdfContentPage2.SetFontAndSize(baseFont, 16);
            pdfContentPage2.BeginText();
            pdfContentPage2.AddImage(GenQRCode(urlQRCode + ProjectID.ToString()));
            pdfContentPage2.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.AttachPage1, 142, 193, 0);
            pdfContentPage2.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage1, 175, 193, 0);
            pdfContentPage2.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.AttachPage2, 428, 130, 0);
            pdfContentPage2.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage2, 455, 130, 0);
            pdfContentPage2.EndText();
            //Page3
            PdfContentByte pdfContentPage3 = pdfStamper.GetOverContent(3);
            pdfContentPage3.SetColorFill(BaseColor.BLACK);
            pdfContentPage3.SetFontAndSize(baseFont, 16);
            pdfContentPage3.BeginText();
            pdfContentPage3.AddImage(GenQRCode(urlQRCode + ProjectID.ToString()));
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ReceiverProvince, 260, 750, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, approvalNoAndYear, 460, 750, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, approvalDate, 165, 728, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.AttachPage3, 270, 728, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage3, 295, 728, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ContractBy, 330, 475, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Position, 330, 450, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, contractReceiveFullName, 330, 385, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ContractReceivePositionSign, 330, 365, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, witnessFullName1, 330, 300, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, witnessFullName2, 330, 238, 0);
            pdfContentPage3.EndText();
            pdfStamper.Close();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline;filename=" + "nep-contract-" + ProjectID.ToString() + ".pdf");
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.Flush();
            Response.Clear();
            Response.End();

        }
        private void SaveReportProjectContractResearch(ServiceModels.Report.ReportFormatContract ReportContract, ServiceModels.ProjectInfo.TabContract TabContract)
        {
            GetDataReportFormatContract(ReportContract, TabContract);
            // file formate nep ->insert text
            string oldFile = Server.MapPath("~/Content/Files/nep-contract-research.pdf");
            MemoryStream ms = new MemoryStream();
            Stream pdfStream = new FileStream(oldFile, FileMode.Open);
            var pdfReader = new PdfReader(pdfStream);
            var pdfStamper = new PdfStamper(pdfReader, ms);

            //page1
            PdfContentByte pdfContentPage1 = pdfStamper.GetOverContent(1);
            //  Font text size and color
            BaseFont baseFont = BaseFont.CreateFont(Server.MapPath("~/Fonts/THSarabun.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            pdfContentPage1.SetColorFill(BaseColor.BLACK);
            pdfContentPage1.SetFontAndSize(baseFont, 16);
            pdfContentPage1.BeginText();
            // postion and write page1
            pdfContentPage1.AddImage(GenQRCode(urlQRCode + ProjectID.ToString()));
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ContractNo, 470, 705, 0);//สัญญาที่ 
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.SignAt, 200, 680, 0);//สัญญานี้ทำขึ้น ณ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, addressNo, 140, 660, 0); //ตั้งอยู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, moo, 200, 660, 0);//หมู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, street, 260, 660, 0);//ถนน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, subDistrict, 145, 642, 0);//ตำบล
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, district, 280, 642, 0);//อำเภอ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, province, 420, 642, 0);//จังหวัด
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ContractDate, 170, 625, 0); //วันที่ ระหว่าง
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ContractBy, 300, 608, 0);//โดย
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Position, 300, 588, 0);//ตำแหน่ง
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, refNoAndrefyear, 400, 570, 0);//ผู้รับมอบ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, contractGiverDate, 170, 552, 0);//ลงวันที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverName, 150, 535, 0);//ฝ่ายหนึ่งกลับ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverAddressNo, 170, 515, 0);//สำนักงานเลขที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (ReportContract.ReceiverMoo != null) ? ReportContract.ReceiverMoo : "", 235, 515, 0);//หมู่ที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, (ReportContract.ReceiverStreet != null) ? ReportContract.ReceiverStreet : "", 300, 515, 0);//ถนน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverSubdistrict, 470, 515, 0);//ตำบล
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverDistrict, 170, 500, 0);//อำเภอ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ReportContract.ReceiverProvince, 380, 500, 0);//จังหวัด
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, contractReceiveFullName, 170, 482, 0);//โดย
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, contractReceiveDate, 280, 465, 0);//25.ผู้มีอำนาจ
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Amount, 180, 370, 0);//จำนวนเงิน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.AmountString, 435, 370, 0); //จำนวนเงิน
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ProjectName, 300, 335, 0);//ส่งคำร้อง
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, approvalNoAndYear, 300, 265, 0); //ครั้งที่
            pdfContentPage1.ShowTextAligned(PdfContentByte.ALIGN_LEFT, approvalDate, 430, 265, 0);//ลงวันที่
            pdfContentPage1.EndText();
            //Page2
            PdfContentByte pdfContentPage2 = pdfStamper.GetOverContent(2);
            pdfContentPage2.AddImage(GenQRCode(urlQRCode + ProjectID.ToString()));
            //Page3
            PdfContentByte pdfContentPage3 = pdfStamper.GetOverContent(3);
            pdfContentPage3.SetColorFill(BaseColor.BLACK); ;
            pdfContentPage3.SetFontAndSize(baseFont, 16);
            pdfContentPage3.BeginText();
            pdfContentPage3.AddImage(GenQRCode(urlQRCode + ProjectID.ToString()));
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.AttachPage1, 175, 678, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage1, 220, 678, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.AttachPage2, 460, 615, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage2, 498, 615, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_LEFT, approvalNoAndYear, 310, 570, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, approvalDate, 460, 570, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.AttachPage3, 140, 553, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, textAttachPage3, 180, 553, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.ContractBy, 330, 338, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ReportContract.Position, 330, 315, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, contractReceiveFullName, 330, 250, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, ContractReceivePositionSign, 330, 228, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, witnessFullName1, 330, 165, 0);
            pdfContentPage3.ShowTextAligned(PdfContentByte.ALIGN_CENTER, witnessFullName2, 330, 105, 0);
            pdfContentPage3.EndText();
            pdfStamper.Close();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline;filename=" + "nep-contract-" + ProjectID.ToString() + ".pdf");
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.Flush();
            Response.Clear();
            Response.End();
        }


    }
}