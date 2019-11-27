<%@ Page Title="ลงทะเบียนผู้ใช้งาน" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="RegisterComplete.aspx.cs" Inherits="Nep.Project.Web.Register.RegisterComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <div class="form-horizontal">
        <div class="form-group form-group-sm">
            <div class="col-sm-12">
                ระบบได้รับข้อมูลการสมัครสมาชิกของท่านแล้ว กรุณารอรับอีเมล์ที่ส่งยืนยันการสมัคร และยืนยันการลงทะเบียนภายใน 3 วัน
            </div>
        </div>
        <div class="form-group form-group-sm">
            <div class="col-sm-12 text-center">                
                <asp:HyperLink runat="server" ID="ButtonBack" ClientIDMode="Inherit" CssClass="btn btn-default btn-sm"
                    Text="<%$ code:Nep.Project.Resources.UI.ButtonBack %>" NavigateUrl="~/Account/Login"/>
            </div>
        </div>
    </div>
</asp:Content>
