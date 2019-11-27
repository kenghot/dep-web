using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                var folder = Server.MapPath("~" + txbFolder.Text);
                //if (FileUpload1.HasFile)
                //{
                    FileUpload1.PostedFile.SaveAs(folder + "\\" + FileUpload1.FileName);
                    lblMessage.Text = "upload completed";
                //}  else
                //{
                //    lblMessage.Text = "file is not found.";
                //}    
                     
            } catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
                
        }
    }
}