using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nep.Project.ServiceModels;

namespace Nep.Project.Business
{
    public class MailService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private static Regex TitleRegex = new Regex(@"\<title\>(.*?)\</title\>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        
        public MailService(DBModels.Model.NepProjectDBEntities db)
        {
            _db = db;
        }

        private String ReadMailTemplate(String templateName, Dictionary<String, String> replaceValues)
        {
            var stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Nep.Project.Business.mail." + templateName + ".html");
            var textStream = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8, true);
            var template = new StringBuilder(textStream.ReadToEnd());

            foreach (var item in replaceValues)
            {
                template.Replace('{' + item.Key + '}', System.Web.HttpUtility.HtmlEncode(item.Value));
            }
           
            return template.ToString();
        }

        private void SendMailWithTemplate(String templateName, Dictionary<String, String> parameters, params String[] addresses)
        {
            var content = ReadMailTemplate(templateName, parameters);
            SendMail(content, addresses);
        }

        private void SendMail(String htmlMessage, params String[] addresses)
        {
            
            //UTF8Encoding utf8 = new UTF8Encoding();
            //string unicodeString = "กองกองทุนและส่งเสริมความเสมอภาคคนพิการ";
            //byte[] encodedBytes = utf8.GetBytes(unicodeString);
            //string displayName = utf8.GetString(encodedBytes);

            string displayName = "กองกองทุนและส่งเสริมความเสมอภาคคนพิการ";
            var mail = new System.Net.Mail.MailMessage();
            mail.HeadersEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;            
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.From = new MailAddress("no-reply@nep.go.th", displayName, System.Text.Encoding.UTF8);

            var match = TitleRegex.Match(htmlMessage);
            if (match.Success)
            {
                var subject = match.Groups[1].Value;
                mail.Subject = subject;
            }
            else
            {
                mail.Subject = Resources.UI.SystemName;
            }

            mail.Body = htmlMessage;
            foreach (var address in addresses)
            {
                if (String.IsNullOrWhiteSpace(address))
                    continue;
                mail.To.Add(new MailAddress(address));
            }

            SendMail(mail);
        }
        public void SendMailManual(System.Net.Mail.MailMessage mail)
        {
            SendMail(mail);
        }
        private void SendMail(System.Net.Mail.MailMessage mail)
        {
            var smtp = GetSmtp();
            System.Threading.ThreadPool.QueueUserWorkItem(delegate (object state)
            {
                using (smtp)
            {
                try
                    {
                        var cred = smtp.Credentials as System.Net.NetworkCredential;

                        if (cred != null && !String.IsNullOrWhiteSpace(cred.UserName) && cred.UserName.EndsWith("@gmail.com"))
                        {
                            var from = mail.From;
                            mail.From = new MailAddress(cred.UserName, from.DisplayName, System.Text.Encoding.UTF8);
                        }

                        smtp.Send(mail);
                    }
                    catch (Exception ex)
                    {
                        var s = ex.Message;
                    }
            }
        });
        }

        private SmtpClient GetSmtp()
        {
            var config = _db.MT_OrganizationParameter;

            var smtp = new SmtpClient(
                config.First(x => x.ParameterCode == Common.OrganizationParameterCode.MAIL_SERVER).ParameterValue,
                Int16.Parse(config.First(x => x.ParameterCode == Common.OrganizationParameterCode.MAIL_PORT).ParameterValue));
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.UseDefaultCredentials = !Boolean.Parse(config.First(x => x.ParameterCode == Common.OrganizationParameterCode.MAIL_AUTH).ParameterValue);

            if (!smtp.UseDefaultCredentials)
            {
                smtp.Credentials = new System.Net.NetworkCredential(
                    config.First(x => x.ParameterCode == Common.OrganizationParameterCode.MAIL_USER).ParameterValue,
                    config.First(x => x.ParameterCode == Common.OrganizationParameterCode.MAIL_PASS).ParameterValue);
            }
            smtp.EnableSsl = Boolean.Parse(config.First(x => x.ParameterCode == Common.OrganizationParameterCode.MAIL_SSL).ParameterValue);

            return smtp;
        }

        public void SendUserRegistrationNotify(Decimal entryId)
        {
            var entry = _db.UserRegisterEntries.FirstOrDefault(x => x.EntryID == entryId);

            if (entry != null)
            {
                var email = entry.Email;
                var uri = Common.Constants.WEBSITE_URL + "/Register/ConfirmEmail?code=" + System.Net.WebUtility.UrlEncode(entry.ActivationCode) + "&entryId=" + entryId;
                var parameters = new Dictionary<String, String>()
                {
                    {"name", String.Format("{0} {1}", entry.Firstname, entry.Lastname)},
                    {"activation_link", uri}
                };
                SendMailWithTemplate("UserRegistrationNotify", parameters, email);
                var sms = new Business.SmsService();
                if (!string.IsNullOrEmpty(entry.Mobile))
                {
                    var mobile = new string[] { entry.Mobile };
                    sms.Send("ระบบการขอรับเงินสนับสนุนโครงการได้รับข้อมูลการสมัครสมาชิก", mobile);
                }
               
            }
        }

        public void SendOrganizationRegistrationNotify(Decimal entryId)
        {
            var entry = _db.OrganizationRegisterEntries.Include("UserRegisterEntry").FirstOrDefault(x => x.EntryID == entryId);

            if (entry != null)
            {
                var email = entry.Email;
                var email2 = entry.UserRegisterEntry.Email;
                var parameters = new Dictionary<String, String>()
                {
                    {"name", String.Format("{0} {1}", entry.UserRegisterEntry.Firstname, entry.UserRegisterEntry.Lastname)}
                };
                SendMailWithTemplate("OrganizationRegistrationNotify", parameters, email);
            }
        }

        public void SendForgetPasswordConfirmation(Decimal userId)
        {
            var entry = _db.SC_User.FirstOrDefault(x => x.UserID == userId);

            if (entry != null)
            {
                var email = entry.Email;
                var uri = Common.Constants.WEBSITE_URL + "/Account/ForgetPasswordConfirm?code=" + System.Net.WebUtility.UrlEncode(entry.ForgetPasswordToken) + "&username=" + System.Net.WebUtility.UrlEncode(entry.UserName);
                var parameters = new Dictionary<String, String>()
                {
                    {"name", String.Format("{0} {1}", entry.FirstName, entry.LastName)},
                    {"link", uri}
                };
                SendMailWithTemplate("ForgetPasswordConfirmation", parameters, email);
            }
        }

        public void SendRejectProject(Decimal projID)
        {
            var entry = _db.ProjectGeneralInfoes.Where(w => w.ProjectID == projID).FirstOrDefault();

            if (entry != null)
            {
                 
                var u = _db.SC_User.Where(w => w.UserID == entry.CreatedByID).FirstOrDefault();
                var email = u.Email;
               
                var parameters = new Dictionary<String, String>()
                {
                    {"fullname", String.Format("{0} {1}", u.FirstName, u.LastName)},
                    {"organizationname", entry.OrganizationNameTH }
                };
                SendMailWithTemplate("RejectProjectNotify", parameters, email);
            }
        }
        public void SendWarningContractDue(String folloupContact, ServiceModels.TemplateConfig.ContractDueWarning param,string email)
        {
            var diffM = 0;
            if (param.StartDate.HasValue && param.EndDate.HasValue)
            {
               diffM = (param.EndDate.Value.Year * 12 + param.EndDate.Value.Month) - (param.StartDate.Value.Year * 12 + param.StartDate.Value.Month);
            }
             
            var parameters = new Dictionary<String, String>()
            {
                {"dueNo", Common.Web.WebUtility.ToThaiNumber(param.DueNo, "##")},
                {"organizationNameTH", param.OrganizationTHName},
                {"fromDate", Common.Web.WebUtility.ToThaiDateDDMMMMYYYY(param.StartDate)},
                {"toDate", Common.Web.WebUtility.ToThaiDateDDMMMMYYYY(param.EndDate)},
                {"projectNameTH", param.ProjectTHName},
                {"nAmount", Common.Web.WebUtility.ToThaiNumber(param.Amount, "#,###.00")},
                {"strAmount", Common.Web.WebUtility.ToThaiBath(param.Amount)},
                {"months", Common.Web.WebUtility.ToThaiNumber(diffM, "##")},
                {"followupcontact", folloupContact}
            };

            SendMailWithTemplate("WarningContractDue", parameters, email);
        }
        public void SendWarningProjectReportResultToOrg(String folloupContact, String nepProjectDirectorPosition,  ServiceModels.TemplateConfig.OrgWaringReportParam param)
        {
            string email = param.Email;
            string telorgapprove1 = param.TELEPHONE1 != null ? param.TELEPHONE1 : "";
            string extenorgapprove1 = param.EXTENSION1 != null ? " ต่อ " + param.EXTENSION1 : "";
            string telorgapprove2 = param.TELEPHONE2 != null ? param.TELEPHONE2 : "";
            string extenorgapprove2 = param.EXTENSION2 != null ? " ต่อ " + param.EXTENSION2 : "";
            string telorg = "โทร. " + telorgapprove1 + extenorgapprove1;
            telorg += telorgapprove2 != "" ? "," + telorgapprove2 + extenorgapprove2 : "";
            string faxorgapprove1 = param.FAXNUMBER1 != null ? param.FAXNUMBER1 : "";
            string faxextenorgapprove1 = param.FAXEXTENSION1 != null ? " ต่อ " + param.FAXEXTENSION1 : "";
            string faxtelorgapprove2 = param.FAXNUMBER2 != null ? param.FAXNUMBER2 : "";
            string faxextenorgapprove2 = param.FAXEXTENSION2 != null ? " ต่อ " + param.FAXEXTENSION2 : "";
            string faxorg = "โทรสาร. " + faxorgapprove1 + faxextenorgapprove1;
            faxorg += faxtelorgapprove2 != "" ? "," + faxtelorgapprove2 + faxextenorgapprove2 : "";
            var parameters = new Dictionary<String, String>()
            {
                {"name", param.OrganizationNameTH},

                {"contractno", param.ContractNo}, 
                {"contractdate", Common.Web.WebUtility.ToThaiDateDDMMMMYYYY(param.ContractDate)},

                {"organizationname", param.OrganizationNameTH},

                {"projectname", param.ProjectNameTH},
                {"budgetamount", Common.Web.WebUtility.ToThaiNumber(param.BudgetReviseValue, "#,###.00")},
                {"budgetamounttext", Common.Web.WebUtility.ToThaiBath(param.BudgetReviseValue)},
                {"projectenddate", Common.Web.WebUtility.ToThaiDateDDMMMMYYYY(param.EndDate)},

                {"enddateprojectreport", Common.Web.WebUtility.ToThaiDateDDMMMMYYYY(param.EnddateProjectReport)},               

                {"followupcontact", folloupContact},
                {"orgapprove", param.ORGANIZATIONNAME!=null?param.ORGANIZATIONNAME:""},
                {"orgapprovetel", telorg},
                {"orgapprovefax", faxorg}
            };            

            SendMailWithTemplate("WarningReportProjectResult", parameters, email);
        }

        public void SendWarningProjectReportResultToPerson(String folloupContact, String nepProjectDirectorPosition, ServiceModels.TemplateConfig.OrgWaringReportParam param)
        {
            string email = param.Email1;
            string telorgapprove1 = param.TELEPHONE1 != null ? param.TELEPHONE1 : "";
            string extenorgapprove1 = param.EXTENSION1 != null ? " ต่อ " + param.EXTENSION1 : "";
            string telorgapprove2 = param.TELEPHONE2 != null ? param.TELEPHONE2 : "";
            string extenorgapprove2 = param.EXTENSION2 != null ? " ต่อ " + param.EXTENSION2 : "";
            string telorg = "โทร. " + telorgapprove1 + extenorgapprove1;
            telorg += telorgapprove2 != "" ? "," + telorgapprove2 + extenorgapprove2 : "";
            string faxorgapprove1 = param.FAXNUMBER1 != null ? param.FAXNUMBER1 : "";
            string faxextenorgapprove1 = param.FAXEXTENSION1 != null ? " ต่อ " + param.FAXEXTENSION1 : "";
            string faxtelorgapprove2 = param.FAXNUMBER2 != null ? param.FAXNUMBER2 : "";
            string faxextenorgapprove2 = param.FAXEXTENSION2 != null ? " ต่อ " + param.FAXEXTENSION2 : "";
            string faxorg = "โทรสาร. " + faxorgapprove1 + faxextenorgapprove1;
            faxorg +=  faxtelorgapprove2 != "" ? "," + faxtelorgapprove2 + faxextenorgapprove2 : "";
            var parameters = new Dictionary<String, String>()
            {
                {"name", param.PersonalName},

                {"contractno", param.ContractNo}, 
                {"contractdate", Common.Web.WebUtility.ToThaiDateDDMMMMYYYY(param.ContractDate)},

                {"organizationname", param.OrganizationNameTH},

                {"projectname", param.ProjectNameTH},
                {"budgetamount", Common.Web.WebUtility.ToThaiNumber(param.BudgetReviseValue , "#,###.00")},
                {"budgetamounttext", Common.Web.WebUtility.ToThaiBath(param.BudgetReviseValue)},
                {"projectenddate", Common.Web.WebUtility.ToThaiDateDDMMMMYYYY(param.EndDate)},

                {"enddateprojectreport", Common.Web.WebUtility.ToThaiDateDDMMMMYYYY(param.EnddateProjectReport)},                

                {"followupcontact", folloupContact},
               {"orgapprove", param.ORGANIZATIONNAME!=null?param.ORGANIZATIONNAME:""},
                {"orgapprovetel", telorg},
                {"orgapprovefax", faxorg}

            };

            SendMailWithTemplate("WarningReportProjectResult", parameters, email);
        }

        public void SendUserInternalRegistrationNotify(decimal entryId)
        {
            var entry = _db.UserRegisterEntries.FirstOrDefault(x => x.EntryID == entryId);

            if (entry != null)
            {
                var email = entry.Email;
                var uri = Common.Constants.WEBSITE_URL + "/Register/ConfirmEmail?code=" + System.Net.WebUtility.UrlEncode(entry.ActivationCode) + "&entryId=" + entryId;
                var parameters = new Dictionary<String, String>()
                {
                    {"name", String.Format("{0} {1}", entry.Firstname, entry.Lastname)},
                    {"activation_link", uri}
                };
                SendMailWithTemplate("UserInternalRegistryNotify", parameters, email);
            }           
        }

        public void SendProjectApprovalNotify(decimal projectID, decimal requestBudget, decimal approvedBudget)
        {
            DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
            if (genInfo != null)
            {
                DBModels.Model.MT_Organization org = _db.MT_Organization.Where(x => x.OrganizationID == genInfo.OrganizationID).FirstOrDefault();
                DBModels.Model.ProjectPersonel personal = _db.ProjectPersonels.Where(x => x.ProjectID == projectID).FirstOrDefault();

                string email = org.Email;
                string personalEmail = personal.Email1;

                decimal budget = (requestBudget == approvedBudget) ? requestBudget : approvedBudget;                
                string budgetText = Common.Web.WebUtility.ToThaiBath(budget);
                string approvalresult = (requestBudget == approvedBudget) ? String.Format("อนุมัติ ตามวงเงินที่โครงการขอสนับสนุน {0} บาท ({1}) ", budget.ToString("N2"), budgetText)
                    : String.Format("อนุมัติ ปรับลดวงเงินสนับสนุน {0} บาท ({1}) ", budget.ToString("N2"), budgetText);
                var parameters = new Dictionary<String, String>()
                {
                    {"name", genInfo.OrganizationNameTH},
                    {"projectname", genInfo.ProjectInformation.ProjectNameTH},
                    {"approvalresult", approvalresult}              
                };

                SendMailWithTemplate("ProjectApprovalResultNotify", parameters, email);
                if (email.ToLower() != personalEmail.ToLower())
                {
                    SendMailWithTemplate("ProjectApprovalResultNotify", parameters, personalEmail);
                }   

            }            
        }
        public void SendProjectConfirmNotify(decimal projectID)
        {
            DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
            if (genInfo != null)
            {
                List<string[]> mails = new List<string[]>();
                if (!string.IsNullOrEmpty(genInfo.RESPONSEEMAIL))
                {
                    mails.Add(new string[] {genInfo.RESPONSEFIRSTNAME + " " + genInfo.RESPONSELASTNAME, genInfo.RESPONSEEMAIL });

                }
                else
                {
                    var user = from u in _db.SC_User where u.GroupID == 1 && u.IsActive == "1" && u.IsDelete == "0"
                               && u.ProvinceID == genInfo.ProvinceID select u;
                    foreach (DBModels.Model.SC_User s in user)
                    {
                        mails.Add(new string[] { s.FirstName  + " " + s.LastName , s.Email });
                       
                    }
                   
                }
                DBModels.Model.MT_Organization org = _db.MT_Organization.Where(x => x.OrganizationID == genInfo.OrganizationID).FirstOrDefault();
               // DBModels.Model.ProjectPersonel personal = _db.ProjectPersonels.Where(x => x.ProjectID == projectID).FirstOrDefault();

              

                foreach (string[] tmp in mails)
                {
                    var parameters = new Dictionary<String, String>()
                    {
                        {"fullname", tmp[0]},
                        {"projectname", genInfo.ProjectInformation.ProjectNameTH},
                    
                    };

                    SendMailWithTemplate("SendDataToReview", parameters, tmp[1]);

                }

            

            }
        }

        public void SendProjectNotApprovalNotify(decimal projectID)
        {
            DBModels.Model.ProjectGeneralInfo genInfo = _db.ProjectGeneralInfoes.Where(x => x.ProjectID == projectID).FirstOrDefault();
            if(genInfo != null){
                DBModels.Model.MT_Organization org = _db.MT_Organization.Where(x => x.OrganizationID == genInfo.OrganizationID).FirstOrDefault();
                DBModels.Model.ProjectPersonel personal = _db.ProjectPersonels.Where(x => x.ProjectID == projectID).FirstOrDefault();

                string email = org.Email;
                string personalEmail = personal.Email1;

                var parameters = new Dictionary<String, String>()
                {
                    {"name", genInfo.OrganizationNameTH},
                    {"projectname", genInfo.ProjectInformation.ProjectNameTH}
                              
                };
                SendMailWithTemplate("ProjectNotApprovalResultNotify", parameters, email);
                if (email.ToLower() != personalEmail.ToLower())
                {
                    SendMailWithTemplate("ProjectNotApprovalResultNotify", parameters, personalEmail);
                }  
                                
            }            
        }
    }
}
