using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.Business;
namespace Nep.Project.Web
{
    public partial class Test : System.Web.UI.Page
    {
        IServices.ISetFollowupStatusJobService _follow;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var ws = new NepServiceWS.NepService();
          
            var cred = new System.Net.NetworkCredential("nepws", "nepws1234");
            Uri uri = new Uri(ws.Url);
        
           
            var s = ws.GetPaymentSlip("nepws", "nepws1234","3303");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            var ws = new DesPersonWS.WebService();
            var p = new DesPersonWS.getDesPersonRequest();
            p.username = "jobdepgoth";
            p.password = "]y[l6fpvf";
            p.person_code = "1102002038471";
            var res = ws.getDesPerson(p);

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            var sms = new Business.SmsService();
            sms.Send(TextBox1.Text, new string[] { TextBox2.Text });
        }
        protected void Button4_Click(object sender, EventArgs e)
        {

            var f = _follow;

        }
    }
}