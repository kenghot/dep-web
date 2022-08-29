using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nep.Project.ServiceModels;
using Nep.Project.Web.UserControls;
using System.IO;
using System.Web.UI.HtmlControls;


namespace Nep.Project.Web.ProjectInfo.Controls
{
    public partial class AttachmentProvideControl : Nep.Project.Web.Infra.BaseUserControl
    {
        public IServices.IProjectInfoService _service { get; set; }
        int attachmentNo;
        public String ViewAttachmentPrefix
        {
            get
            {
                if (ViewState["ViewAttachmentPrefix"] == null)
                {
                    string prefix = "Project/" + ProjectID;
                    ViewState["ViewAttachmentPrefix"] = prefix;
                }
                return ViewState["ViewAttachmentPrefix"].ToString() ;
            }

            set
            {
                ViewState["ViewAttachmentPrefix"] = value;
            }
        }

        public Decimal ProjectID
        {
            get
            {
                if (ViewState["ProjectID"] == null)
                {
                    string stID = Request.QueryString["id"];
                    decimal id = 0;
                    Decimal.TryParse(stID, out id);
                    ViewState["ProjectID"] = id;
                }


                return (decimal)ViewState["ProjectID"];
            }

            set
            {
                ViewState["ProjectID"] = value;
            }
        }
        public Boolean IsEditabled { 
            get {
                bool isEdit = true;
                if (ViewState["IsAttachmentProvideEditabled"] != null)
                {
                    isEdit = Convert.ToBoolean(ViewState["IsAttachmentProvideEditabled"]);
                }
                return isEdit;
            }
            set { ViewState["IsAttachmentProvideEditabled"] = value; }
        }

        public List<ServiceModels.ProjectInfo.AttachmentProvide> AttachmentProvides
        {
            get {
                List<ServiceModels.ProjectInfo.AttachmentProvide> list = new List<ServiceModels.ProjectInfo.AttachmentProvide>();
                if (ViewState["AttachmentProvides"] != null)
                {
                    list = (List<ServiceModels.ProjectInfo.AttachmentProvide>)(ViewState["AttachmentProvides"]);
                }

                return list;
            }
            set {
                ViewState["AttachmentProvides"] = value;
            }
        }


        public ServiceModels.ProjectInfo.ProjectDocument GetProjectDocument()
        {
            ServiceModels.ProjectInfo.ProjectDocument doc = new ServiceModels.ProjectInfo.ProjectDocument();            
            String no;
            C2XFileUpload fileUpload;
            //IEnumerable <ServiceModels.KendoAttachment> addedFiles;
            //IEnumerable <ServiceModels.KendoAttachment> removedFiles;
                  doc.AddedDocuments = new List<List<KendoAttachment>>();
                    doc.RemovedDocuments = new List<List<KendoAttachment>>();
            foreach (RepeaterItem item in RepeaterAttachemnt.Items){
                if(item.ItemType  == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item){
                    no  = ((HiddenField)item.FindControl("DocumentNo")).Value;
                    fileUpload = (C2XFileUpload)item.FindControl("C2XFileUploadProjectAttachment");
                    //kenghot
                    var add = new List<KendoAttachment>();
                    var rev = new List<KendoAttachment>();
                    doc.AddedDocuments.Add(add);
                    doc.RemovedDocuments.Add(rev);
                    if (fileUpload.AddedFiles.Count() > 0)
                    {
                        add.AddRange(fileUpload.AddedFiles.ToList());
                    }
                    if (fileUpload.RemovedFiles.Count() > 0)
                    { 
                       rev.AddRange(fileUpload.RemovedFiles.ToList());
                    }
                    //addedFiles = fileUpload.AddedFiles;
                    //removedFiles = fileUpload.RemovedFiles;


                    //switch (no)
                    //{
                    //    case "1":
                    //        {
                    //            doc.AddedDocument1 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //            doc.RemovedDocument1 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //            break;
                    //        }
                    //    case "2":
                    //    {
                    //        doc.AddedDocument2 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument2 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "3":
                    //    {
                    //        doc.AddedDocument3 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument3 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "4":
                    //    {
                    //        doc.AddedDocument4 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument4 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "5":
                    //    {
                    //        doc.AddedDocument5 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument5 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "6":
                    //    {
                    //        doc.AddedDocument6 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument6 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "7":
                    //    {
                    //        doc.AddedDocument7 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument7 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "8":
                    //    {
                    //        doc.AddedDocument8 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument8 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "9":
                    //    {
                    //        doc.AddedDocument9 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument9 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "10":
                    //    {
                    //        doc.AddedDocument10 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument10 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "11":
                    //    {
                    //        doc.AddedDocument11 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument11 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "12":
                    //    {
                    //        doc.AddedDocument12 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument12 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "13":
                    //    {
                    //        doc.AddedDocument13 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument13 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }
                    //    case "14":
                    //    {
                    //        doc.AddedDocument14 = (addedFiles.Count() > 0) ? addedFiles.ToList()[0] : null;
                    //        doc.RemovedDocument14 = (removedFiles.Count() > 0) ? removedFiles.ToList()[0] : null;
                    //        break;
                    //    }                        
                    //}
                }
            }

            return doc;
            //RepeaterAttachemnt.Items
            //String text = "Add: " + System.Web.HttpUtility.HtmlEncode(Common.Web.WebUtility.ToJSON(C2XFileUploadPersonalID.AddedFiles));
            //text += "<br />Remove: " + System.Web.HttpUtility.HtmlEncode(Common.Web.WebUtility.ToJSON(C2XFileUploadPersonalID.RemovedFiles));
        }

        protected void RepeaterAttachemnt_DataBinding(object sender, EventArgs e)
        {
            List<ServiceModels.ProjectInfo.AttachmentProvide> list = AttachmentProvides;
            RepeaterAttachemnt.DataSource = list;
        }



        //private List<ServiceModels.ProjectInfo.AttachmentProvide> GetTempAttachProvideList()
        //{
            
        //    decimal projectID = ProjectID;
           
            
        //    List<ServiceModels.ProjectInfo.AttachmentProvide> list = new List<ServiceModels.ProjectInfo.AttachmentProvide>();
        //    var attachmentProvideResult = _service.GetProjectAttachmentProvide(projectID);
        //    if (attachmentProvideResult.IsCompleted)
        //    {
        //        list = attachmentProvideResult.Data;
        //    }else{
        //        ShowErrorMessage(attachmentProvideResult.Message);
        //    }
            
            
        //    return list;
        //}

        protected void RepeaterAttachemnt_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            C2XFileUpload fileUpload = (C2XFileUpload)e.Item.FindControl("C2XFileUploadProjectAttachment");
            CustomValidator fileUploadValidation = (CustomValidator)e.Item.FindControl("CustomValidatorProjectAttachment");
            HtmlGenericControl  span = (HtmlGenericControl)e.Item.FindControl("RequiredSign");

            ServiceModels.ProjectInfo.AttachmentProvide attach = (ServiceModels.ProjectInfo.AttachmentProvide)e.Item.DataItem;
            //kenghot
            //if(attach.AttachmentID.HasValue){
            //    List<ServiceModels.KendoAttachment> files = new List<ServiceModels.KendoAttachment>();
            //    ServiceModels.KendoAttachment file = new ServiceModels.KendoAttachment();
            //    file.id = attach.AttachmentID.ToString();               
            //    file.name = attach.AttachmentFileName;
            //    file.size = (int)attach.AttachmentFileSize;
            //    file.extension = Path.GetExtension(attach.AttachmentFileName);
            //    files.Add(file);

            //    fileUpload.ExistingFiles = files;
            //}

            fileUpload.ExistingFiles = attach.AttachFiles;
            
            // end kenghot
        }

        //protected void CustomValidatorProjectAttachment_ServerValidate(object source, ServerValidateEventArgs args)
        //{
        //    CustomValidator validator = (CustomValidator)source;
        //    String controlToValidate = validator.ControlToValidate;
        //    C2XFileUpload fileUpload;
        //    RepeaterItemCollection items = RepeaterAttachemnt.Items;
        //    bool isAttached = false;
          
        //    for (int i = 0; i < items.Count; i++)
        //    {
        //        fileUpload = (C2XFileUpload)items[i].FindControl("C2XFileUploadProjectAttachment");
        //        if (fileUpload.AllFiles != null)
        //        {
        //            isAttached = true;
        //            break;
        //        }
        //    }            

        //    args.IsValid = isAttached;         
                      
        //}
        
        protected void CustomValidatorProjectAttachment_ServerValidate_New(object source, ServerValidateEventArgs args)
        {
            //Beer06082021 ปรับปรุงหน้าจอเอกสารแนบให้บังคับแนบเอกสารในหัวข้อที่ 1 2 3 5 8
            CustomValidator validator = (CustomValidator)source;
            String controlToValidate = validator.ControlToValidate;
            C2XFileUpload fileUpload;
            RepeaterItemCollection items = RepeaterAttachemnt.Items;
            bool isAttached = false;
            fileUpload = (C2XFileUpload)items[attachmentNo].FindControl("C2XFileUploadProjectAttachment");
            if (fileUpload.AllFiles == null &&(attachmentNo == 0 || attachmentNo == 1|| attachmentNo == 2 || attachmentNo == 4 || attachmentNo == 7) )
            {
                isAttached = false;
            }
            else
            {
                isAttached = true;
            }
            attachmentNo = attachmentNo + 1;
            args.IsValid = isAttached;
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            UpdatePanelAttachment.RenderControl(writer);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HandleProjectAttachmentCheckbox" + this.ClientID, @"
                $(function () {
                        if (typeof (bindEventToProjectDocumentAttachment) != 'undefined') {
                            bindEventToProjectDocumentAttachment();
                            rechecheckAttachProjectDocument();
                        }                  
                    });", true);

            RegisterClientScript();
        }

        private void RegisterClientScript()
        {
            String searchInputFile = "input[type=\"file\"]";
            String searchSenderName = "input[name=\"' + e.sender.name + '\"]";
            String searchTextbox = "input[type=\"checkbox\"]";

            String script = @"
                function bindEventToProjectDocumentAttachment() {
                    var attachmentControls = $('#attachment-form').find('" + searchInputFile + @"');
                    var control;
                    var controlID;
                    $.each(attachmentControls, function () {
                        controlID = $(this).attr('id');
                        control = $('#' + controlID).data('kendoUpload'); 
                        control.bind('success', checkAttachProjectDocument);                  
                    });
                }

                function checkAttachProjectDocument(e) {
                    var operation = e.operation; /* upload, remove*/
                    var attachmentRow = $('" + searchSenderName + @"').closest('div[attachment-row]');
                    var checkbox = $(attachmentRow).find('"+ searchTextbox +@"');
                
                    if (operation == 'upload') {
                        $(checkbox).get(0).checked = true;                   
                    } else if (operation == 'remove') {
                        $(checkbox).get(0).checked = false;                   
                    }               
                }

                function rechecheckAttachProjectDocument() {
                    var attachmentControls = $('#attachment-form').find('"+searchInputFile+@"');
                    var control;
                    var controlID;
                    $.each(attachmentControls, function () {
                        controlID = $(this).attr('id');
                        fileUpload = $('#' + controlID).data('kendoUpload');  
                        var attachmentRow = $('#' + controlID).closest('div[attachment-row]');
                        var checkbox = $(attachmentRow).find('"+ searchTextbox +@"');

                        if ((fileUpload != null) && (fileUpload.options.files.length > 0)) {
                            $(checkbox).get(0).checked = true;
                        } else {
                            $(checkbox).get(0).checked = false;                       
                        }
                    });

                }

                function validatorProjectAttachmentServerValidate(oSrc, args) {                
                    var isValid = false;
                    var control = $('#' + oSrc.controltovalidate).find('"+ searchInputFile +@"');                    
                    var fileUpload = $('#' + control.attr('id')).data('kendoUpload');                    
                    if ((fileUpload != null) && (fileUpload.options.files != null) && (fileUpload.options.files.length > 0)) {
                        isValid = true;
                    }

                    args.IsValid = isValid;
                }
            ";
            ScriptManager.RegisterClientScriptBlock(
                       UpdatePanelAttachment,
                       this.GetType(),
                       "ManageProjectAttachmentScript",
                       script,
                       true);

        }
    }
}