<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProcessingControl.ascx.cs" Inherits="Nep.Project.Web.ProjectInfo.Controls.ProcessingControl" %>

<%@ Import Namespace="Nep.Project.Resources" %>

<asp:UpdatePanel ID="UpdatePanelProcessing" 
                    UpdateMode="Conditional" 
                    runat="server" >
    <ContentTemplate>
        <asp:HiddenField runat="server" ID="ProjectID" />

        <div class="panel panel-default"><!--รายละเอียดโครงการ-->
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleProcessing %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">                    
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_ContractNo %></label>
                        <div class="col-sm-4 control-value">
                            8888/88
                        </div>    
                                                     
                    </div>
                    <div  class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_Oraganization %></label>
                        <div class="col-sm-4 control-value">
                           ศูนย์เทคโนโลยีการศึกษาเพื่อคนตาบอด ติวานนท์
                        </div>
                        
                        <label class="col-sm-2 control-label"><%= Model.Processing_ProjectName%></label>
                        <div class="col-sm-4 control-value">โครงการผลิตหนังสืออักษรเบรลล์</div>   
                    </div>
                    <div class="form-group form-group-sm">                        
                        <label class="col-sm-2 control-label"><%= Model.Processing_Date%></label>
                        <div class="col-sm-4 control-value">3 มีนาคม 2556</div> 
                        
                        <label class="col-sm-2 control-label"><%= Model.Processing_Province%></label>
                        <div class="col-sm-4 control-value">นนทบุรี</div>                                      
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_Organization%></label>
                        <div class="col-sm-4 control-value">ศูนย์เทคโนโลยีการศึกษาเพื่อคนตาบอด</div>
                        <label class="col-sm-2 control-label"><%=  Model.Processing_Responsible%></label>
                        <div class="col-sm-4 control-value">นายนวัฒน์ สมจิง</div>                   
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_Period%></label>
                        <div class="col-sm-4 control-value">30 วัน</div>                               
                    </div>                   
                </div>
            </div>
        </div><!--รายละเอียดโครงการ-->

        <div class="panel panel-default"><!--ที่ตั้งสำนักงาน-->
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.LabelProcessingLocation  %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_HouseNo%></label>
                        <div class="col-sm-10 control-value">78/2 อาคาร - หมู่ที่ 1 ซอย ติวานนท์-ปากเกร็ด1 ถนน ติวานนท์ ตำบล บางตลาด อำเภอ ปากเกร็ด จังหวัด นนทบุรี 11120</div>                                                      
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_Telephone%></label>
                        <div class="col-sm-4 control-value">02 4254332</div>
                        <label class="col-sm-2 control-label"><%= Model.Processing_Fax%></label>
                        <div class="col-sm-4 control-value">-</div>                                    
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_Email%></label>
                        <div class="col-sm-4 control-value">bookbar2009@gmail.com</div>                               
                    </div>                  
                </div>
            </div>
        </div><!--ที่ตั้งสำนักงาน-->

        <div class="panel panel-default"><!--กลุ่มเป้าหมาย-->
            <div class="panel-heading">
                <h3 class="panel-title"><%= UI.TitleTargetGroup %></h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group form-group-sm">
                        <label class="col-sm-12 form-group-title"><%= UI.LableReciever %></label>
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_Budget%></label>
                        <div class="col-sm-4 control-value">10000 บาท</div>                               
                    </div>
                    <div class="form-group form-group-sm">
                        <label class="col-sm-2 control-label"><%= Model.Processing_ActualExpense%></label>
                        <div class="col-sm-4 control-value">5000 บาท</div>                               
                    </div>
                    <div class="form-group form-group-sm">
                        <div class="col-sm-12 control-value"><%= Model.Processing_LableAttachment %></div>
                    </div>  
                </div>
            </div>
        </div><!--กลุ่มเป้าหมาย-->
        
        <div class="form-horizontal">        
            <div class="form-group form-group-sm">
                <div class="col-sm-12 text-center">
                    <%--<asp:Button runat="server" ID="ButtonSave" CssClass="btn btn-primary btn-sm"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonSave %>" />--%>

                    <%--<asp:HyperLink ID="HyperLink1" runat="server" CssClass="btn btn-red btn-sm" 
                        NavigateUrl="~/ProjectInfo/ProjectInfoList.aspx"
                        Text="<%$ code:Nep.Project.Resources.UI.ButtonCancel %>"></asp:HyperLink>--%>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


