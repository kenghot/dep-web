using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.DBModels;
using System.IO;
namespace TestApp
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var db = new Nep.Project.DBModels.Model.NepProjectDBEntities();
            var x = from xs in db.ProjectGeneralInfoes  select xs;
            var sAll = "";
            var iRec = 0;
            decimal CheckSum = 0;
            //try
            //{
                foreach (Nep.Project.DBModels.Model.ProjectGeneralInfo p in x)
                {
                try
                {
                    var org = (from orgs in db.MT_Organization
                               where orgs.OrganizationID == p.OrganizationID
                               select orgs).FirstOrDefault();
                    if (org == null)
                    {
                        continue;
                    }

                    
                    var sRow = "";
                    var pinf = (from pinfs in db.ProjectInformations where pinfs.ProjectID == p.ProjectID select pinfs).FirstOrDefault();
                    if (pinf == null)
                    {
                        pinf = new Nep.Project.DBModels.Model.ProjectInformation();
                    }
                    var Oprovince = (from pv in db.MT_Province where pv.ProvinceID == pv.ProvinceID select pv).FirstOrDefault();
                    var Osect = Oprovince.Section.LOVName;
                    decimal PSectID = 0;
                    var Psect = "";
                    var Pprov = "";
                    var pAdd = (from padds in db.ProjectOperationAddresses where padds.ProjectID == p.ProjectID select padds).FirstOrDefault();

                    if (pAdd == null)
                    {
                        pAdd = new Nep.Project.DBModels.Model.ProjectOperationAddress();
                    }
                    else
                    {
                        var Pprovince = (from pv in db.MT_Province where pv.ProvinceID == pAdd.ProvinceID select pv).FirstOrDefault();
                        Pprov = Pprovince.ProvinceName;
                        Psect = Pprovince.Section.LOVName;
                        PSectID = Pprovince.Section.LOVID;
                    }


                    sRow = //iRec.ToString().Trim() + "|" +
                    DateField(pinf.ProjectDate) + "|" +
                    DecField(pinf.BudgetYear) + "|" +
                    Osect.Trim() + "|" +
                    Oprovince.ProvinceName.Trim() + "|" +
                    Psect.Trim() + "|" +
                    StringField(Pprov, 50) + "|" +
                    StringField(pAdd.District, 50) + "|" +
                    StringField(pAdd.SubDistrict, 50) + "|" +
                    DecField(PSectID) + "|" +
                    DecField(pAdd.ProvinceID) + "|" +
                    DecField(pAdd.DistrictID) + "|" +
                    DecField(pAdd.SubDistrictID) + "|" +
                    DecField(p.ProjectID) + "|" +
                      StringField(pinf.ProjectNameTH, 50) + "|";


                    if (pinf.ProjectTypeID.HasValue)
                    {
                        var pType = (from pt in db.MT_ListOfValue
                                     where pt.LOVID == pinf.ProjectTypeID
                                     select pt).FirstOrDefault();
                        sRow += DecField(pType.LOVID) + "|" + StringField(pType.LOVName, 50) + "|";

                    }
                    else
                    {
                        sRow += "||";
                    }
                    if (pinf.DisabilityTypeID.HasValue)
                    {
                        var pType = (from pt in db.MT_ListOfValue
                                     where pt.LOVID == pinf.DisabilityTypeID
                                     select pt).FirstOrDefault();
                        sRow += DecField(pType.LOVID) + "|" + StringField(pType.LOVName, 50) + "|";

                    }
                    else
                    {
                        sRow += "||";
                    }
                    

                    sRow += StringField(org.OrganizationType.ToBeUnder, 50) + "|" +
                            ((org.OrganizationType.ToBeUnder == "1") ? "รัฐบาล" : "เอกชน") + "|";
                    sRow += StringField(org.OrganizationType.OrganizationTypeCode, 50) + "|" +
                            StringField(org.OrganizationType.OrganizationType, 50) + "|" +
                            DateField(p.SubmitedDate) + "|";
                    var Poper = (from oper in db.ProjectOperations
                                 where oper.ProjectID == p.ProjectID
                                 select oper).FirstOrDefault();
                    if (Poper == null) Poper = new Nep.Project.DBModels.Model.ProjectOperation();

                    sRow += DateField(Poper.StartDate) + "|" + DateField(Poper.EndDate) + "|" +
                            StringField(p.ProjectApprovalStatus.LOVName, 50) + "|";
                    // end common data
                    sRow = GetBudget(sRow, p.ProjectID, ref CheckSum,ref iRec);

                    sAll += sRow  ;
 
                  }catch (Exception ex)
                     {
                         Label1.Text = "check1 :" + p.ProjectID.ToString() + " " + ex.Message;
                        return;
                  }
                }
                var fn = string.Format("PJMAS_NEW_{0:yyMM_yyMMdd_hhmmss}.csv", DateTime.Now);
                var h = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|\r\n", fn, "Proj_Master", iRec, 44, 29, CheckSum);
            //2018-9-13
            //sAll = h + sAll;


            var full = Server.MapPath(".") + "\\temp\\" + fn;
                System.IO.File.AppendAllText(full, sAll, System.Text.Encoding.UTF8);
                Label1.Text = string.Format("{0} records completed",iRec);
            //}
            //catch(Exception ex)
            //{
            //    Label1.Text = "main error<br>"+ ex.Message ;
            //}

            
     
        }
        private string GetBudget(string common,decimal pid, ref decimal checksum, ref int irec)
        {
            try
            {
                string sRet = "";
                var db = new Nep.Project.DBModels.Model.NepProjectDBEntities();
                var bud = from buds in db.ProjectBudgets
                          where buds.ProjectID == pid
                          select buds;
                var rep = (from reps in db.ProjectReports where reps.ProjectID == pid select reps.ActualExpense).FirstOrDefault();
                decimal expense, budget, approve;
                expense = 0;
                budget = 0;
                approve = 0;
                if (rep.HasValue) { expense = rep.Value; }
                foreach (Nep.Project.DBModels.Model.ProjectBudget b in bud)
                {
                    budget += b.BudgetValue;
                    approve += b.BudgetReviseValue2;
                }
                checksum += expense;
                var p = (from ps in db.ProjectParticipants
                         where ps.ProjectID == pid
                         select ps).Count();
                //common += string.Format("{0:##,#0.###0}|{1:##,#0.###0}|{2:##,#0.###0}|{3:##,#0.###0}|{4:##,#0}|"
                //, budget, approve, expense, approve - expense, p); 

                //2018-9-13
                common += string.Format("{0:###0.###0}|{1:###0.###0}|{2:###0.###0}|{3:###0.###0}|{4:###0}|"
                    , budget, approve, expense, approve - expense, p);

                var fol = (from f in db.PROJECTFOLLOWUP2 where f.PROJECTID == pid select f).FirstOrDefault();
                decimal? score1 = 0;
                decimal? score2 = 0;
                decimal? per1 = 0;
                decimal? per2 = 0;
                decimal? TotScore = 0;
                string result1 = "";
                string result2 = "";
                string totResult = "";
                if (fol != null)
                {
                    score1 = fol.TOTALSCORE1;
                    score2 = fol.TOTALSCORE2;
                    per1 = fol.TOTALPERCENT1;
                    per2 = fol.TOTALPERCENT2;
                    TotScore = fol.TOTALTALSCORE;
                    result1 = (score1 >= 25) ? "Pass" : "Failed";
                    result2 = (score2 >= 25) ? "Pass" : "Failed";
                    totResult = (TotScore >= 50) ? "Pass" : "Failed";
                }

                // part 1
                irec++;
                sRet = irec.ToString().Trim() + "|" + common +
                    string.Format("{5:#0}|1|ส่วนที่ 1|50|50|25|50|{0:#0}|{1:#0}|{2:#0}|{3}|{4}|\r\n",
                    score1, per1, score1, result1, totResult,TotScore);

                // part 2
                irec++;
                sRet += irec.ToString().Trim() + "|" + common +
                                string.Format("{5:#0}|2|ส่วนที่ 2|50|50|25|50|{0:#0}|{1:#0}|{2:#0}|{3}|{4}|\r\n",
                                score2, per2, score2, result2, totResult,TotScore);
                                
                return sRet;
            } catch(Exception ex)
            {
                throw new Exception("GetBudget error: " + ex.Message);
            }
          
        }
        private string StringField(string s, int lenght)
        {   
            try
            {

                if (string.IsNullOrEmpty(s))
                {
                    return "";
                }
                s = s.Trim().PadRight(lenght, ' ');
                s = s.Substring(0, lenght).Trim();
                return s;
            }catch (Exception ex)
            {
                throw new Exception("StringField : " + ex.Message);
            }
           
        }
        private string DateField(DateTime?  d)
        {
            try
            {
                if (!d.HasValue)
                {
                    return "";
                }
                var s = "";
                s = string.Format("{0:dd/MM/yyyy}", d);
                return s;
            }
            catch (Exception ex)
            {
                throw new Exception("DateField : " + ex.Message);
            }
            
        }
        private string DecField(decimal? d)
        {
            try
            {
                if (!d.HasValue)
                {
                    return "";
                }
                var s = "";
                s = d.ToString();
                return s;
            }
            catch (Exception ex)
            {
                throw new Exception("DecField : " + ex.Message);
            }
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var db = new Nep.Project.DBModels.Model.NepProjectDBEntities();
            var x = from xs in db.ProjectGeneralInfoes select xs;
            var sAll = "";
            var iRec = 0;
            decimal CheckSum = 0;
            //try
            //{
            foreach (Nep.Project.DBModels.Model.ProjectGeneralInfo p in x)
            {
                try
                {
                    var org = (from orgs in db.MT_Organization
                               where orgs.OrganizationID == p.OrganizationID
                               select orgs).FirstOrDefault();
                    if (org == null)
                    {
                        continue;
                    }

                    //iRec++;
                    var sRow = "";
                    var pinf = (from pinfs in db.ProjectInformations where pinfs.ProjectID == p.ProjectID select pinfs).FirstOrDefault();
                    if (pinf == null)
                    {
                        pinf = new Nep.Project.DBModels.Model.ProjectInformation();
                    }
                    var Oprovince = (from pv in db.MT_Province where pv.ProvinceID == pv.ProvinceID select pv).FirstOrDefault();
                    var Osect = Oprovince.Section.LOVName;
                    decimal PSectID = 0;
                    var Psect = "";
                    var Pprov = "";
                    var pAdd = (from padds in db.ProjectOperationAddresses where padds.ProjectID == p.ProjectID select padds).FirstOrDefault();

                    if (pAdd == null)
                    {
                        pAdd = new Nep.Project.DBModels.Model.ProjectOperationAddress();
                    }
                    else
                    {
                        var Pprovince = (from pv in db.MT_Province where pv.ProvinceID == pAdd.ProvinceID select pv).FirstOrDefault();
                        Pprov = Pprovince.ProvinceName;
                        Psect = Pprovince.Section.LOVName;
                        PSectID = Pprovince.Section.LOVID;
                    }


                    sRow = //iRec.ToString().Trim() + "|" +
                    DateField(pinf.ProjectDate) + "|" +
                    DecField(pinf.BudgetYear) + "|" +
                    Osect.Trim() + "|" +
                    Oprovince.ProvinceName.Trim() + "|" +
                    Psect.Trim() + "|" +
                    StringField(Pprov, 50) + "|" +
                    StringField(pAdd.District, 50) + "|" +
                    StringField(pAdd.SubDistrict, 50) + "|" +
                    DecField(PSectID) + "|" +
                    DecField(pAdd.ProvinceID) + "|" +
                    DecField(pAdd.DistrictID) + "|" +
                    DecField(pAdd.SubDistrictID) + "|" +
                    DecField(p.ProjectID) + "|" +
                      StringField(pinf.ProjectNameTH, 50) + "|";


                    if (pinf.ProjectTypeID.HasValue)
                    {
                        var pType = (from pt in db.MT_ListOfValue
                                     where pt.LOVID == pinf.ProjectTypeID
                                     select pt).FirstOrDefault();
                        sRow += DecField(pType.LOVID) + "|" + StringField(pType.LOVName, 50) + "|";

                    }
                    else
                    {
                        sRow += "||";
                    }
                    if (pinf.DisabilityTypeID.HasValue)
                    {
                        var pType = (from pt in db.MT_ListOfValue
                                     where pt.LOVID == pinf.DisabilityTypeID
                                     select pt).FirstOrDefault();
                        sRow += DecField(pType.LOVID) + "|" + StringField(pType.LOVName, 50) + "|";

                    }
                    else
                    {
                        sRow += "||";
                    }


                    sRow += StringField(org.OrganizationType.ToBeUnder, 50) + "|" +
                            ((org.OrganizationType.ToBeUnder == "1") ? "รัฐบาล" : "เอกชน") + "|";
                    sRow += StringField(org.OrganizationType.OrganizationTypeCode, 50) + "|" +
                            StringField(org.OrganizationType.OrganizationType, 50) + "|" +
                            DateField(p.SubmitedDate) + "|";
                    var Poper = (from oper in db.ProjectOperations
                                 where oper.ProjectID == p.ProjectID
                                 select oper).FirstOrDefault();
                    if (Poper == null) Poper = new Nep.Project.DBModels.Model.ProjectOperation();

                    sRow += DateField(Poper.StartDate) + "|" + DateField(Poper.EndDate) + "|" +
                            StringField(p.ProjectApprovalStatus.LOVName, 50) + "|";
                    // end common data
                    //sRow = GetBudget(sRow, p.ProjectID, ref CheckSum);
                    var sDet = "";
                    
                     List<Nep.Project.ServiceModels.ProjectInfo.ProjectParticipant> 
                      participants = (from participant in db.ProjectParticipants
                                        where participant.ProjectID == p.ProjectID
                                        select new Nep.Project.ServiceModels.ProjectInfo.ProjectParticipant
                                        {
                                                                                           ProjectParticipantID = participant.ProjectParticipantsID,
                                                                                           FirstName = participant.FirstName,
                                                                                           LastName = participant.LastName,
                                                                                           IDCardNo = participant.IDCardNo,
                                                                                           Gender = participant.Gender,
                                                                                           IsCripple = participant.IsCripple,
                                                                                           ProjectTargetGroupID = participant.ProjectTargetGroupID,
                                                                                           TargetGroupID = participant.TargetGroupID,
                                                                                           TargetGroupName = participant.TargetGroup.LOVName,
                                                                                           TargetGroupCode = participant.TargetGroup.LOVCode,
                                                                                           TargetGroupEtc = participant.TargetGroupETC


                                                                                       }).ToList();
                    var allP = participants.Count();
                    foreach (Nep.Project.ServiceModels.ProjectInfo.ProjectParticipant tmp in participants)
                    {
                        if (int.Parse(tmp.IsCripple) > 1)
                        {
                            tmp.TargetGroupID = 0;
                            tmp.TargetGroupName = "";
                        }
                        iRec++;
                        sDet += iRec.ToString().Trim() + "|" + sRow + tmp.IsCrippleDesc + "|" +
                            tmp.TargetGroupName + "|" +
                            tmp.GenderName + "|0|" +
                            ((tmp.IsCripple == "1") ? "yes" : "no") + "|" +
                            allP.ToString() + "|0|\r\n";
                        CheckSum += allP;
                    }
                   


                    sAll +=  sDet ;

                }
                catch (Exception ex)
                {
                    Label1.Text = "check1 :" + p.ProjectID.ToString() + " " + ex.Message;
                    return;
                }
            }
            var fn = string.Format("PJPAR_NEW_{0:yyMM_yyMMdd_hhmmss}.csv", DateTime.Now);
            var h = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|\r\n", fn, "Proj_Participate", iRec, 34, 33, CheckSum);
            sAll = h + sAll;
            var full = Server.MapPath(".") + "\\temp\\" + fn;
            System.IO.File.AppendAllText(full, sAll, System.Text.Encoding.UTF8);
            Label1.Text = string.Format("{0} records completed", iRec);
            //}
            //catch(Exception ex)
            //{
            //    Label1.Text = "main error<br>"+ ex.Message ;
            //}


        }
    }
}