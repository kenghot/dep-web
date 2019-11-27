using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Nep.Project.DBModels;
namespace Nep.Project.Web
{
    public partial class TestSendMail : System.Web.UI.Page
    {
        public IServices.IProjectInfoService _projectService { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
          
            string displayName = "กองกองทุนและส่งเสริมความเสมอภาคคนพิการ";
            var mail = new System.Net.Mail.MailMessage();
            mail.HeadersEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.From = new MailAddress("no-reply@nep.go.th", displayName, System.Text.Encoding.UTF8);
            mail.To.Add(new MailAddress( txtEmail.Text));
            mail.Subject = "test";
            //var match = TitleRegex.Match(htmlMessage);
            //if (match.Success)
            //{
            //    var subject = match.Groups[1].Value;
            //    mail.Subject = subject;
            //}
            //else
            //{
            //    mail.Subject = Resources.UI.SystemName;
            //}

            mail.Body = "test send emal";
            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.UseDefaultCredentials = false;// !Boolean.Parse(config.First(x => x.ParameterCode == Common.OrganizationParameterCode.MAIL_AUTH).ParameterValue);

            if (!smtp.UseDefaultCredentials)
            {
                smtp.Credentials = new System.Net.NetworkCredential(
                   "nepfund.noreply@gmail.com",
                    "axbndarvqapjlrjx");
            }
            smtp.EnableSsl = true; // Boolean.Parse(config.First(x => x.ParameterCode == Common.OrganizationParameterCode.MAIL_SSL).ParameterValue);
            try
            {
                smtp.Send(mail);
                lblMsg.Text = "complete";
            } catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

            
            //foreach (var address in addresses)
            //{
            //    if (String.IsNullOrWhiteSpace(address))
            //        continue;
            //    mail.To.Add(new MailAddress(address));
            //}
            //mail.To.Add(txtEmail.text);

            //SendMail(mail);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
  Business.MailService _mailService = new Business.MailService(new DBModels.Model.NepProjectDBEntities());
                System.Net.Mail.MailMessage mail = new MailMessage();
                mail.Body = "test";
                mail.Sender = new MailAddress("dep_support@mgsolution.co.th");
                mail.From = new MailAddress("dep_support@mgsolution.co.th");
                mail.To.Add(new MailAddress(txtEmail.Text));
                // _mailService.SendUserRegistrationNotify( mail );
                _mailService.SendMailManual(mail);
                lblMsg.Text = "complete";
            } catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
          
        }

        protected void btnGenAct_Click(object sender, EventArgs e)
        {
            try
            {
                var db = _projectService.GetDB();
                var bg = db.ProjectBudgets.Where(w => w.BUDGETCODE == null && !w.ACTIVITYID.HasValue).ToList();
                var adddt = new DateTime(2000, 1, 1);
                foreach (var tmp in bg)
                {
                    DBModels.Model.PROJECTBUDGETACTIVITY act = new DBModels.Model.PROJECTBUDGETACTIVITY();
                    act.ACTIVITYDESC = "งบประมาณ";
                    act.ACTIVITYNAME = "รายละเอียด";
                    act.PROJECTID = tmp.ProjectID;
                    act.RUNNO = 1;
                    act.TOTALAMOUNT = tmp.BudgetValue;
                    act.CREATEDATE = adddt;
                    act.CREATEBYID = 1;
                    db.PROJECTBUDGETACTIVITies.Add(act);
                    db.SaveChanges();
                    tmp.ACTIVITYID = act.ACTIVITYID;
                    db.SaveChanges();

                }
                Label1.Text = "Gen activities completed.";
            } catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
          
        }
    }
}