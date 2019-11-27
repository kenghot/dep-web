using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;

namespace Nep.Project.Web.UserControls
{
    [ToolboxData("<{0}:C2XFileUpload runat=server></{0}:FileUpload>")]
    [ValidationProperty("AllFiles")]
    public class C2XFileUpload : System.Web.UI.WebControls.CompositeControl/*, IPostBackEventHandler*/
    {
        public C2XFileUpload()
        {
            this.Attributes.Add("data-control-type", "C2XFileUpload");
        }

        private class KendoAttachmentComparer : IEqualityComparer<ServiceModels.KendoAttachment>
        {
            public bool Equals(ServiceModels.KendoAttachment x, ServiceModels.KendoAttachment y)
            {
                return ((x.id != null && x.id == y.id) || (x.tempId != null && x.tempId == y.tempId))
                    && x.name == y.name
                    && x.size == y.size
                    && x.extension == y.extension;
            }

            public int GetHashCode(ServiceModels.KendoAttachment obj)
            {
                Int32 hash = 0;
                if (obj.id != null)
                    hash = obj.id.GetHashCode();
                else if (obj.tempId != null)
                    hash = obj.tempId.GetHashCode();
                else if (obj.name != null)
                    hash = obj.name.GetHashCode();

                return hash;
            }
        }

        private String _viewAttachmentPrefix = String.Empty;
        public String ViewAttachmentPrefix{
            get
            {
                return _viewAttachmentPrefix;
            }
            set
            {
                if (value == null)
                {
                    _viewAttachmentPrefix = String.Empty;
                }
                else
                {
                    _viewAttachmentPrefix = value.Trim();
                }
            }
        }

        private String _fileExtensions = ".gif, .jpg, .jpeg, .png, .pdf, .doc, .docx, .xls, .xlsx, .txt";
        public String FileExtensions
        {
            get
            {
                return _fileExtensions;
            }
            set
            {
                _fileExtensions = value;
            }
        }

        private Boolean _multipleFileMode = false;
        public Boolean MultipleFileMode
        {
            get
            {
                return _multipleFileMode;
            }
            set
            {
                EnsureChildControls();
                _multipleFileMode = value;
                this.FileUploadControl.AllowMultiple = value;
            }
        }
        private Boolean _skipScript = false;
        public Boolean SkipScript
        {
            get
            {
                return _skipScript;
            }
            set
            {
                EnsureChildControls();
                _skipScript = value;
               // this.FileUploadControl.AllowMultiple = value;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (this.FileUploadControl.HasFile)
                {
                    var uploadedFiles = this.FileUploadControl.PostedFiles;
                    var kendoFiles = new List<ServiceModels.KendoAttachment>();
                    foreach (var file in uploadedFiles)
                    {
                        var kendoFile = new ServiceModels.KendoAttachment();
                        var tempFileName = Guid.NewGuid().ToString("N");

                        kendoFile.extension = Path.GetExtension(file.FileName);
                        kendoFile.name = Path.GetFileName(file.FileName);
                        kendoFile.size = file.ContentLength;
                        kendoFile.tempId = tempFileName;

                        file.SaveAs(Page.Server.MapPath("~/App_Data/UploadTemp/" + tempFileName));
                        kendoFiles.Add(kendoFile);
                    }
                }
            }
            
            base.OnLoad(e);
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            Object temp = null;

            temp = ViewState["MultipleFileMode"];
            if (temp != null)
            {
                _multipleFileMode = (Boolean)temp;
            }

            temp = ViewState["FileExtensions"];
            if (temp != null)
            {
                _fileExtensions = (String)temp;
            }

            temp = ViewState["ExistingFiles"];
            if (temp != null)
            {
                _existingFiles = (IEnumerable<ServiceModels.KendoAttachment>)temp;
            }

            temp = ViewState["ViewAttachmentPrefix"];
            if (temp != null)
            {
                _viewAttachmentPrefix = (String)temp;
            }
        }

        protected override object SaveViewState()
        
       {
            ViewState["MultipleFileMode"] = _multipleFileMode;
            ViewState["FileExtensions"] = _fileExtensions;
            ViewState["ExistingFiles"] = _existingFiles;
            ViewState["ViewAttachmentPrefix"] = _viewAttachmentPrefix;
            return base.SaveViewState();
        }

        private IEnumerable<ServiceModels.KendoAttachment> _existingFiles = new ServiceModels.KendoAttachment[0];

        [Bindable(true, BindingDirection.OneWay)]
        public IEnumerable<ServiceModels.KendoAttachment> ExistingFiles
        {
            get
            {
                return _existingFiles;
            }
            set
            {
                EnsureChildControls();

                if (value != null)
                {
                    var temp = value;
                    temp = temp.Where(x => x != null);
                    _existingFiles = temp.ToArray();
                }
                else
                {
                    _existingFiles = new ServiceModels.KendoAttachment[0];
                }

                IEnumerable<ServiceModels.KendoAttachment> result = new ServiceModels.KendoAttachment[0];
                EnsureChildControls();
                if (!String.IsNullOrWhiteSpace(RemovedHiddenField.Value))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ServiceModels.KendoAttachment>>(RemovedHiddenField.Value);
                }

                result = result.Where(x => _existingFiles.Contains(x, new KendoAttachmentComparer()));
                this.RemovedHiddenField.Value = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            }
        }

        public IEnumerable<ServiceModels.KendoAttachment> AddedFiles
        {
            get
            {
                IEnumerable<ServiceModels.KendoAttachment> result = new ServiceModels.KendoAttachment[0];
                EnsureChildControls();
                if (!String.IsNullOrWhiteSpace(AddedHiddenField.Value))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ServiceModels.KendoAttachment>>(AddedHiddenField.Value);
                }
                return result;
            }
        }

        public IEnumerable<ServiceModels.KendoAttachment> RemovedFiles
        {
            get
            {
                IEnumerable<ServiceModels.KendoAttachment> result = new ServiceModels.KendoAttachment[0];
                EnsureChildControls();
                if (!String.IsNullOrWhiteSpace(RemovedHiddenField.Value))
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ServiceModels.KendoAttachment>>(RemovedHiddenField.Value);
                }

                result = result.Where(x => _existingFiles.Contains(x, new KendoAttachmentComparer()));
                return result;
            }
        }

        public IEnumerable<ServiceModels.KendoAttachment> AllFiles
        {
            get
            {
                var allFiles = _existingFiles.Except(RemovedFiles, new KendoAttachmentComparer()).Union(AddedFiles, new KendoAttachmentComparer());

                return allFiles.Count() == 0 ? null: allFiles;
            }
        }

        public void ClearChanges()
        {
            this.AddedHiddenField.Value = "";
            this.RemovedHiddenField.Value = "";
            //this._existingFiles = new ServiceModels.KendoAttachment[0];
        }

        #region Childs Control
        protected readonly global::System.Web.UI.WebControls.FileUpload FileUploadControl = new System.Web.UI.WebControls.FileUpload()
        {
            Enabled = true,
            ID = "FileUploadControl"
        };

        protected readonly global::System.Web.UI.WebControls.HiddenField AddedHiddenField = new System.Web.UI.WebControls.HiddenField()
        {
            EnableViewState = false,
            ID = "AddedFiles",
            ClientIDMode = ClientIDMode.AutoID
        };

        protected readonly global::System.Web.UI.WebControls.HiddenField RemovedHiddenField = new System.Web.UI.WebControls.HiddenField()
        {
            EnableViewState = false,
            ID = "Removed",
            ClientIDMode = ClientIDMode.AutoID
        };

        #endregion Childs Control

        protected override void CreateChildControls()
        {
            Controls.Clear();
            EnableViewState = true;
            this.FileUploadControl.Attributes.Clear();

            if (!String.IsNullOrEmpty(this._fileExtensions))
            {
                this.FileUploadControl.Attributes.Add("accept", this._fileExtensions);
            }

            this.Controls.Add(AddedHiddenField);
            this.Controls.Add(RemovedHiddenField);
            this.Controls.Add(FileUploadControl);
            base.CreateChildControls();
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {

            

            String cssFileContainer = (this._multipleFileMode) ? "input-file-container multi-file" : "input-file-container single-file";

            writer.AddAttribute(HtmlTextWriterAttribute.Class, cssFileContainer);  
            writer.RenderBeginTag("div");
                FileUploadControl.RenderControl(writer);
                AddedHiddenField.RenderControl(writer);
                RemovedHiddenField.RenderControl(writer);

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "error-text extension-validate");
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "visibility:hidden;");
                writer.RenderBeginTag("span");
                writer.RenderEndTag();
            writer.RenderEndTag();

            RegisterStartupScript();
               
        }

        public string SetupUploadScript { get; set; }
        private void RegisterStartupScript()
        {
            String fileUploadControlClientID = this.FileUploadControl.ClientID;
            String addedHiddenControlClientID = this.AddedHiddenField.ClientID;
            String removedHiddenControlClientID = this.RemovedHiddenField.ClientID;
            String multiple = (this._multipleFileMode) ? "true" : "false";
            String enabled = (this.Enabled) ? "true" : "false";
            var jsonData = new
            {
                controlID = this.ClientID,
                startValidate = Page.IsPostBack,
                fileUploadControlClientID = fileUploadControlClientID,
                addedHiddenControlClientID = addedHiddenControlClientID,
                removedHiddenControlClientID = removedHiddenControlClientID,
                enabled = this.Enabled,
                multiple = _multipleFileMode,
                existingFiles = _existingFiles,
                viewAttachmentPrefix = _viewAttachmentPrefix,
               
            };
            String scriptTag = @"jQuery(function () {c2x.uploadFileSetup(" + Newtonsoft.Json.JsonConvert.SerializeObject(jsonData) + ");});";
         
            SetupUploadScript = @"c2x.uploadFileSetup(" + Newtonsoft.Json.JsonConvert.SerializeObject(jsonData) + ");";
            
            if (_skipScript)
            {
                scriptTag = "var scriptupload_" + this.ClientID + " =  function () {" + SetupUploadScript + "}; ";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), fileUploadControlClientID, scriptTag, true);
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "Upload_FileTemplate" + this.ClientID, @"
            //                <script id='Upload_FileTemplate' type='text/x-kendo-template'>
            //                    <span class='k-progress'></span>
            //                    <div class='file-wrapper'>
            //                        <button type='button' class='k-upload-action'></button>   
            //                        <span class='file-name'>#=name#</span>
            //                        <a href='##' target='_blank' class='file-link' onclick='return c2x.openAttachment(this, """ + _viewAttachmentPrefix + @""");'>#=name#</a>
            //                    </div>
            //                </script>", false);

            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Upload_FileTemplate" + this.ClientID, @"
            //                <script id='Upload_FileTemplate' type='text/x-kendo-template'>
            //                    <span class='k-progress'></span>
            //                    <div class='file-wrapper'>
            //                        <button type='button' class='k-upload-action'></button>   
            //                        <span class='file-name'>#=name#</span>
            //                        <a href='##' target='_blank' class='file-link' onclick='return c2x.openAttachment(this, """ + _viewAttachmentPrefix + @""");'>#=name#</a>
            //                    </div>
            //                </script>", false);
        }
    }
}