<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Popup.Master" AutoEventWireup="true" CodeBehind="ActivityBudgetPopUp.aspx.cs" Inherits="Nep.Project.Web.ProjectInfo.ActivityBudgetPopUp" %>
<%@ Register src="Controls/ProjectBudgetDetail.ascx" tagname="ProjectBudgetDetail" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://unpkg.com/accounting-js"></script>
        <script src="https://cdn.jsdelivr.net/npm/vue@2.5.21/dist/vue.js"></script>
    <script src="https://unpkg.com/axios/dist/axios.min.js"></script>  
   <script src="https://unpkg.com/vue-numeric"></script>
    <br /><br />
    <asp:Label runat="server" ID="LabelProjectName" Font-Bold="True" Font-Size="Larger"></asp:Label><br />
    <asp:Label runat="server" ID="LabelActivity" Font-Bold="True" Font-Size="Larger"></asp:Label>
    <asp:Button runat="server" ID="ButtonClose" Visible="false" Text="ปิด" />
   <div id="divActivityBudget">
       <table class="asp-grid project-approval-grid" cellspacing="0" rules="all" border="1" id="ApprovalControl_GridViewActivity" style="border-collapse:collapse;">
							<tbody>
                                <tr>
								<th scope="col" style="width:60px;">กิจกรรมที่</th><th scope="col">รายละเอียด</th>
							    </tr>
                           <template v-for="act in data.Data.BudgetActivities">
                                <tr>
								<td>
                                                    <span >{{act.RunNo}}</span>
                                                </td>
                                    <td>
                                                    <span >{{act.ActivityName}}:{{act.ActivityDESC}}</span>
                                                    
                               
									<table class="asp-grid project-approval-grid" cellspacing="0" rules="all" border="1" :id="act.ActivityID" style="border-collapse:collapse;">
										<tbody>
                                            <tr>

                                                <th rowspan="2" style="width:30px"> 
                                                        ลำดับ      
                                                                                                                                
                                                    </th>
                                                    <th rowspan="2" style="width:300px"> 
                                                        ค่าใช้จ่ายที่เสนอขอ   
                                                                                                                                    
                                                    </th>
                                                    <th colspan="3"> 
                                                       จำนวนเงิน    
                                                                                                                                      
                                                    </th>
                                                    <th rowspan="2" style="width:100px">งบประมาณที่ใช้จ่ายจริง</th>                                                                                                      

                                                    </tr>
                                            <tr>
                                                      
                                                        <th>
                                                            เสนอขอ
                                                            
                                                        </th>
                                                        <th>ฝ่ายเลขานุการ</th>
                                                        
                                                            <th>คณะกรรมการจังหวัด</th>
                                                                                                               
                                                                                                        
                                                    </tr>
                              <template v-for="(bud,budidx) in  data.Data.BudgetDetails" v-if="bud.ActivityID == act.ActivityID">                 
										<tr>
										<td>
                                                        {{bud.No}}
                                                    </td>
                                                    <td>
                                                      {{bud.Detail}} 
                                                    </td>
                                                    <td>
                                                        <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="bud.Amount" ></vue-numeric>   
                                                    </td>
                                                    <td>
                                                       <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="bud.Revise2Amount" ></vue-numeric>   
                                                    </td>
                                                    
                                                        <td>
                                                         <vue-numeric  :read-only="true" separator="," v-bind:precision="2" :empty-value="0" v-model="bud.Revise1Amount" ></vue-numeric>    
                                                        </td>

                                                    <td>
                                                        <vue-numeric  separator="," v-bind:precision="2" :empty-value="0" v-model="data.Data.BudgetDetails[budidx].Revise2Amount" ></vue-numeric>
                                                    </td>
                                               
										</tr>
                                        </template> 

									</tbody></table>
								
                                                </td>
							</tr>
                            </template>    
						</tbody></table>
       <br />
       <table class="asp-grid project-approval-grid" cellspacing="0" rules="all" border="1" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity" style="border-collapse:collapse;">
							<tbody><tr>
								<th scope="col">&nbsp;</th><th scope="col" style="width:50px;">กิจกรรมที่</th><th scope="col">รายละเอียด</th>
							</tr><tr>
								<td></td><td>
                                                    <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_Label1_0">1</span>
                                                </td><td>
                                                    <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_LabelActivityName_0">sdfasf</span>
                                                    <input type="hidden" name="ctl00$MainContent$TabContainerProjectInfoForm$TabPanelProjectApproval$ApprovalControl$GridViewActivity$ctl02$HiddenActivitID" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_HiddenActivitID_0" value="41">
                                                    <div>
									<table class="asp-grid project-approval-grid" cellspacing="0" rules="all" border="1" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0" style="border-collapse:collapse;">
										<tbody><tr>
											<th scope="col"> 
                                                    </th><th rowspan="2" style="width:30px"> 
                                                        ลำดับ      
                                                                                                                                
                                                    </th>
                                                    <th rowspan="2" style="width:300px"> 
                                                        ค่าใช้จ่ายที่เสนอขอ   
                                                                                                                                    
                                                    </th>
                                                    <th colspan="3"> 
                                                       จำนวนเงิน    
                                                                                                                                      
                                                    </th>
                                                    <th rowspan="2" style="width:200px">หมายเหตุ</th>                                                                                                      
                                                    <th rowspan="2" style="width:60px"></th>
                                                    </tr><tr>
                                                        <th></th>
                                                        <th>
                                                            เสนอขอ
                                                            
                                                        </th>
                                                        <th>ฝ่ายเลขานุการ</th>
                                                        
                                                            <th>คณะกรรมการจังหวัด</th>
                                                                                                               
                                                                                                        
                                                    </tr>
                                                
										<tr>
											<td>                                                  
                                                    
                                                    </td><td>
                                                         1 
                                                    </td>
                                                    <td>
                                                        ค่าอาหารจัดในโรงแรม อาหารเช้า ( 1 คน 1 มื้อ 1 บาท  )
                                                    </td>
                                                    <td>
                                                        1.00
                                                    </td>
                                                    <td>
                                                        1.00
                                                    </td>
                                                    
                                                        <td>
                                                            1.00
                                                        </td>
                                                    
                                                    
                                                    
                                                    <td>
                                                        
                                                    </td>
                                                    <td>
                                                        <input type="image" name="ctl00$MainContent$TabContainerProjectInfoForm$TabPanelProjectApproval$ApprovalControl$GridViewActivity$ctl02$ApproveBudgetGridControl$GridViewApprovalBudgetDetail$ctl02$ApprovalBudgetDetailButtonEdit" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_ApprovalBudgetDetailButtonEdit_0" src="../Images/icon/doc_edit_icon_16.png">    
                                                    </td>
                                                
										</tr><tr>
											<td>                                                  
                                                    
                                                    </td><td>
                                                         2 
                                                    </td>
                                                    <td>
                                                        ค่าอาหารจัดในโรงแรม อาหารกลางวัน ( 1 คน 1 มื้อ 1 บาท  )
                                                    </td>
                                                    <td>
                                                        1.00
                                                    </td>
                                                    <td>
                                                        1.00
                                                    </td>
                                                    
                                                        <td>
                                                            1.00
                                                        </td>
                                                    
                                                    
                                                    
                                                    <td>
                                                        
                                                    </td>
                                                    <td>
                                                        <input type="image" name="ctl00$MainContent$TabContainerProjectInfoForm$TabPanelProjectApproval$ApprovalControl$GridViewActivity$ctl02$ApproveBudgetGridControl$GridViewApprovalBudgetDetail$ctl03$ApprovalBudgetDetailButtonEdit" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_ApprovalBudgetDetailButtonEdit_1" src="../Images/icon/doc_edit_icon_16.png">    
                                                    </td>
                                                
										</tr><tr>
											<td>                                                  
                                                    
                                                    </td><td>
                                                         3 
                                                    </td>
                                                    <td>
                                                        ค่าอาหารและเครื่องดื่ม จัดในสถานที่ราชการหรือเอกชนที่ไม่ใช่มืออาชีพ ( 4 คน 4 มื้อ 2 บาท  )
                                                    </td>
                                                    <td>
                                                        32.00
                                                    </td>
                                                    <td>
                                                        32.00
                                                    </td>
                                                    
                                                        <td>
                                                            30.00
                                                        </td>
                                                    
                                                    
                                                    
                                                    <td>
                                                        
                                                    </td>
                                                    <td>
                                                        <input type="image" name="ctl00$MainContent$TabContainerProjectInfoForm$TabPanelProjectApproval$ApprovalControl$GridViewActivity$ctl02$ApproveBudgetGridControl$GridViewApprovalBudgetDetail$ctl04$ApprovalBudgetDetailButtonEdit" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_ApprovalBudgetDetailButtonEdit_2" src="../Images/icon/doc_edit_icon_16.png">    
                                                    </td>
                                                
										</tr><tr>
											<td>                                                  
                                                    
                                                    </td><td>
                                                         4 
                                                    </td>
                                                    <td>
                                                        ค่าเช่าสถานที่ (จัดในสถานที่เอกชน มากกว่า 5 วัน) ( 300 บาท  )
                                                    </td>
                                                    <td>
                                                        300.00
                                                    </td>
                                                    <td>
                                                        300.00
                                                    </td>
                                                    
                                                        <td>
                                                            200.00
                                                        </td>
                                                    
                                                    
                                                    
                                                    <td>
                                                        
                                                    </td>
                                                    <td>
                                                        <input type="image" name="ctl00$MainContent$TabContainerProjectInfoForm$TabPanelProjectApproval$ApprovalControl$GridViewActivity$ctl02$ApproveBudgetGridControl$GridViewApprovalBudgetDetail$ctl05$ApprovalBudgetDetailButtonEdit" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_ApprovalBudgetDetailButtonEdit_3" src="../Images/icon/doc_edit_icon_16.png">    
                                                    </td>
                                                
										</tr><tr>
											<td>                                                  
                                                    
                                                    </td><td>
                                                         5 
                                                    </td>
                                                    <td>
                                                        ค่าเช่ารถตู้ปรับอากาศ ( 5 คัน 2 วัน 2 บาท  )
                                                    </td>
                                                    <td>
                                                        20.00
                                                    </td>
                                                    <td>
                                                        20.00
                                                    </td>
                                                    
                                                        <td>
                                                            20.00
                                                        </td>
                                                    
                                                    
                                                    
                                                    <td>
                                                        
                                                    </td>
                                                    <td>
                                                        <input type="image" name="ctl00$MainContent$TabContainerProjectInfoForm$TabPanelProjectApproval$ApprovalControl$GridViewActivity$ctl02$ApproveBudgetGridControl$GridViewApprovalBudgetDetail$ctl06$ApprovalBudgetDetailButtonEdit" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_ApprovalBudgetDetailButtonEdit_4" src="../Images/icon/doc_edit_icon_16.png">    
                                                    </td>
                                                
										</tr><tr>
											<td>
                                                    
                                                    </td><td colspan="2">
                                                        <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_LabelTotalAmountDesc">รวมเป็นเงินทั้งสิ้น</span>
                                                    </td>
                                                    <td>
                                                        <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_LabelRequestAmount">354.00</span>
                                                    </td>
                                                    <td>
                                                        <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_LabelReviseAmount">354.00</span>
                                                    </td>
                                                    
                                                            <td>
                                                                <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_0_GridViewApprovalBudgetDetail_0_LabelRevise1ProvinceAmount">252.00</span>
                                                            </td>
                                                        
                                                    
                                                    
                                                    <td></td>
                                                    <td></td>
                                                
										</tr>
									</tbody></table>
								</div>
                                                </td>
							</tr><tr>
								<td></td><td>
                                                    <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_Label1_1">2</span>
                                                </td><td>
                                                    <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_LabelActivityName_1">sdfasffff</span>
                                                    <input type="hidden" name="ctl00$MainContent$TabContainerProjectInfoForm$TabPanelProjectApproval$ApprovalControl$GridViewActivity$ctl03$HiddenActivitID" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_HiddenActivitID_1" value="42">
                                                    <div>
									<table class="asp-grid project-approval-grid" cellspacing="0" rules="all" border="1" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_1_GridViewApprovalBudgetDetail_1" style="border-collapse:collapse;">
										<tbody><tr>
											<th scope="col"> 
                                                    </th><th rowspan="2" style="width:30px"> 
                                                        ลำดับ      
                                                                                                                                
                                                    </th>
                                                    <th rowspan="2" style="width:300px"> 
                                                        ค่าใช้จ่ายที่เสนอขอ   
                                                                                                                                    
                                                    </th>
                                                    <th colspan="3"> 
                                                       จำนวนเงิน    
                                                                                                                                      
                                                    </th>
                                                    <th rowspan="2" style="width:200px">หมายเหตุ</th>                                                                                                      
                                                    <th rowspan="2" style="width:60px"></th>
                                                    </tr><tr>
                                                        <th></th>
                                                        <th>
                                                            เสนอขอ
                                                            
                                                        </th>
                                                        <th>ฝ่ายเลขานุการ</th>
                                                        
                                                            <th>คณะกรรมการจังหวัด</th>
                                                                                                               
                                                                                                        
                                                    </tr>
                                                
										<tr>
											<td>                                                  
                                                    
                                                    </td><td>
                                                         1 
                                                    </td>
                                                    <td>
                                                        สนับสนุนการจัดตั้งศูนย์บริการคนพิการทั่วไป  ค่าจัดสิ่งอำนวยความสะดวกสำหรับคนพิการ ( 90 บาท  )
                                                    </td>
                                                    <td>
                                                        90.00
                                                    </td>
                                                    <td>
                                                        90.00
                                                    </td>
                                                    
                                                        <td>
                                                            90.00
                                                        </td>
                                                    
                                                    
                                                    
                                                    <td>
                                                        
                                                    </td>
                                                    <td>
                                                        <input type="image" name="ctl00$MainContent$TabContainerProjectInfoForm$TabPanelProjectApproval$ApprovalControl$GridViewActivity$ctl03$ApproveBudgetGridControl$GridViewApprovalBudgetDetail$ctl02$ApprovalBudgetDetailButtonEdit" id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_1_GridViewApprovalBudgetDetail_1_ApprovalBudgetDetailButtonEdit_0" src="../Images/icon/doc_edit_icon_16.png">    
                                                    </td>
                                                
										</tr><tr>
											<td>
                                                    
                                                    </td><td colspan="2">
                                                        <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_1_GridViewApprovalBudgetDetail_1_LabelTotalAmountDesc">รวมเป็นเงินทั้งสิ้น</span>
                                                    </td>
                                                    <td>
                                                        <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_1_GridViewApprovalBudgetDetail_1_LabelRequestAmount">90.00</span>
                                                    </td>
                                                    <td>
                                                        <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_1_GridViewApprovalBudgetDetail_1_LabelReviseAmount">90.00</span>
                                                    </td>
                                                    
                                                            <td>
                                                                <span id="MainContent_TabContainerProjectInfoForm_TabPanelProjectApproval_ApprovalControl_GridViewActivity_ApproveBudgetGridControl_1_GridViewApprovalBudgetDetail_1_LabelRevise1ProvinceAmount">90.00</span>
                                                            </td>
                                                        
                                                    
                                                    
                                                    <td></td>
                                                    <td></td>
                                                
										</tr>
									</tbody></table>
								</div>
                                                </td>
							</tr>
						</tbody></table>
   </div>
   <%--<script type="text/javascript" src="../Scripts/projectbudget.js?v=<%= DateTime.Now.Ticks.ToString() %>">
   </script>--%>
    <script type="text/javascript" src="../Scripts/Vue/VueActivityBudject.js?v=<%= DateTime.Now.Ticks.ToString() %>"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterScript" runat="server">
</asp:Content>
