using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Nep.Project.Web.UserControls
{
    [DefaultProperty("SelectedDate")]
    [ValidationProperty("SelectedDate")]
    [ToolboxData("<{0}:DatePicker runat=server></{0}:DatePicker>")]
    public class DatePicker : System.Web.UI.WebControls.CompositeControl
    {       

        #region Childs Control
        /// <summary>
        /// TextBoxDate control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.TextBox TextBoxDate = new TextBox()
        {
            ID = "TextBoxDate",
            CssClass = "form-control form-control-datepicker"
        };

        /// <summary>
        /// ImageCalendar control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::System.Web.UI.WebControls.ImageButton ImageCalendar = new ImageButton()
        {
            ID = "ImageCalendar",
            ImageUrl = "~/Images/calendar.gif",
            AlternateText = "เลือก",
            CausesValidation = false,
        };

        /// <summary>
        /// RegularExpressionValidatorCalendar control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        //  ValidationExpression = "^([0-3][0-9])[/]((0[1-9])|(1[0-2]))[/](\\d{4})$"
        protected global::System.Web.UI.WebControls.RegularExpressionValidator RegularExpressionValidatorCalendar = new RegularExpressionValidator()
        {
            ID = "RegularExpressionValidatorCalendar",
            Text = String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LebelDate),
            CssClass = "error-text",
            Display = ValidatorDisplay.Dynamic,
            ControlToValidate = "TextBoxDate",
            ValidationExpression = "^([0-3][0-9])[/]((0[1-9])|(1[0-2]))[/](\\d{4})$"
        };

        /// <summary>
        /// CalendarExtenderDate control.
        /// </summary>
        /// <remarks>
        /// Auto-generated field.
        /// To modify move field declaration from designer file to code-behind file.
        /// </remarks>
        protected global::AjaxControlToolkit.CalendarExtender CalendarExtenderDate = new AjaxControlToolkit.CalendarExtender()
        {
            ID = "CalendarExtenderDate",
            PopupButtonID = "ImageCalendar",
            TargetControlID = "TextBoxDate",
            CssClass = "nep-calendar",
            Format = "dd/MM/yyyy",
            FirstDayOfWeek = FirstDayOfWeek.Sunday ,
            PopupPosition = AjaxControlToolkit.CalendarPosition.TopLeft,
            
            
        };

        #endregion Childs Control

        public event EventHandler TextChanged;

        protected override void OnInit(EventArgs e)
        {
            Page.RegisterRequiresControlState(this);
            //Move Create and Register Control step from CreateChildControl to OnInit to fix bug that the value will be lost when postback and the control is disabled.
            TextBoxDate.TextChanged += TextBoxDate_TextChanged;
            ImageCalendar.Attributes.Add("alt", "เลือก");
            this.Controls.Add(TextBoxDate);
            this.Controls.Add(ImageCalendar);
            this.Controls.Add(RegularExpressionValidatorCalendar);
            this.Controls.Add(CalendarExtenderDate);
            base.OnInit(e);
        }

        protected override void RenderChildren(HtmlTextWriter writer)
        {
            writer.WriteLine(@"<span class=""datepicker-span""> ");
            TextBoxDate.RenderControl(writer);
            writer.WriteLine(@"");
            ImageCalendar.RenderControl(writer);
            writer.WriteLine(@"");
            RegularExpressionValidatorCalendar.RenderControl(writer);
            writer.WriteLine(@"");
            writer.WriteLine(@"</span>");
            CalendarExtenderDate.RenderControl(writer);
        }

        #region Public Properties

        public Boolean ClearTime
        {
            get
            {
                return this.CalendarExtenderDate.ClearTime;
            }
            set
            {
                this.CalendarExtenderDate.ClearTime = value;
            }
        }

        public Boolean AutoPostBack
        {
            get
            {
                return TextBoxDate.AutoPostBack;
            }
            set
            {
                TextBoxDate.AutoPostBack = value;
            }
        }      

        public DateTime? SelectedDate
        {
            get
            {
                if (string.IsNullOrEmpty(TextBoxDate.Text))
                    return null;
                else
                {
                    CultureInfo ci = new CultureInfo(Common.Constants.UI_CULTURE);
                    DateTime uiDate = DateTime.ParseExact(TextBoxDate.Text, CalendarExtenderDate.Format, ci.DateTimeFormat);
                    //string format = this.CalendarExtenderDate.Format;
                    //format = (string.IsNullOrEmpty(format))? "dd/MM/yyyy" : format;
                    //string en = uiDate.ToString(new CultureInfo(Common.Constants.CULTURE));
                    return DateTime.ParseExact(TextBoxDate.Text, CalendarExtenderDate.Format, ci.DateTimeFormat);
                }
            }
            set
            {
                if (value.HasValue)
                {
                    DateTime selectectdDate =  (DateTime)value;
                    CultureInfo culture = CultureInfo.CurrentCulture;
                    CalendarExtenderDate.SelectedDate = value;
                    TextBoxDate.Text = value.Value.ToString(CalendarExtenderDate.Format);
                }
                else
                {
                    CalendarExtenderDate.SelectedDate = null;
                    TextBoxDate.Text = string.Empty;
                }

            }
        }

        //public DateTime? SelectedUIDate
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(TextBoxDate.Text))
        //            return null;
        //        else
        //        {
        //            CultureInfo ci = new CultureInfo(Common.Constants.UI_CULTURE);
        //            return DateTime.ParseExact(TextBoxDate.Text, CalendarExtenderDate.Format, ci.DateTimeFormat);
        //        }
        //    }
        //    set
        //    {
        //        if (value.HasValue)
        //        {
        //            CalendarExtenderDate.SelectedDate = value;
        //            TextBoxDate.Text = value.Value.ToString(CalendarExtenderDate.Format);
        //        }
        //        else
        //        {
        //            CalendarExtenderDate.SelectedDate = null;
        //            TextBoxDate.Text = string.Empty;
        //        }

        //    }
        //}

        //public override bool Enabled
        //{
        //    get
        //    {
        //        return base.Enabled;
        //    }
        //    set
        //    {
        //        base.Enabled = value;
        //        ImageCalendar.Enabled = value;
        //        TextBoxDate.Enabled = true;
        //    }
        //}

        public System.Web.UI.WebControls.TextBox RefTextBox
        {
            get
            {
                return (TextBoxDate);
            }
        }

        private bool _enableTextBox = false;
        public bool EnabledTextBox
        {
            get
            {
                return _enableTextBox;
            }
            set
            {
                _enableTextBox = value;
            }
        }

        public String TextBoxCssClass
        {
            set
            {
                TextBoxDate.CssClass = value;
            }
        }

        public virtual string ValidationGroup
        {
            get
            {
                return TextBoxDate.ValidationGroup;
            }
            set
            {
                TextBoxDate.ValidationGroup = value;
                RegularExpressionValidatorCalendar.ValidationGroup = value;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return this.RegularExpressionValidatorCalendar.ErrorMessage;
            }
            set
            {
                this.RegularExpressionValidatorCalendar.ErrorMessage = value;
                this.RegularExpressionValidatorCalendar.Text = value;
            }
        }        

        private string _format;
        public string Format
        {
            get
            {
                return _format;
            }

            set
            {
                _format = value;
            }
        }

        private string _onClientDateSelectionChanged;
        public String OnClientDateSelectionChanged
        {
            get
            {
                return _onClientDateSelectionChanged;
            }

            set
            {
                _onClientDateSelectionChanged = value;
            }
        }  
      
        private string _onClientDateTextChanged;
        public String OnClientDateTextChanged
        {
            get
            {
                return _onClientDateTextChanged;
            }

            set
            {
                _onClientDateTextChanged = value;
            }
        }  
        #endregion

        #region Public Function
        public void Clear()
        {
            SelectedDate = null;
        }
        #endregion

        #region Events
        protected void TextBoxDate_TextChanged(Object sender, EventArgs e)
        {
            this.CalendarExtenderDate.SelectedDate = this.SelectedDate;

            if (TextChanged != null)
            {
                TextChanged(this, e);
            }
            else
            {
                this.CalendarExtenderDate.SelectedDate = (DateTime?)null;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (_enableTextBox)
            {
                TextBoxDate.Attributes.Remove("readonly");
            }
            else
            {
                TextBoxDate.Attributes.Add("readonly", "readonly");
            }

            if (!String.IsNullOrEmpty(_format))
            {
                CalendarExtenderDate.Format = _format;
                if (_format == "yyyy")
                {
                    CalendarExtenderDate.OnClientShown = "c2x.OnDatePickerYearShown";
                    CalendarExtenderDate.OnClientHidden = "c2x.OnDatePickerYearHide";



                    RegularExpressionValidatorCalendar.ValidationExpression = "^([1-9])(\\d{3})$";
                    RegularExpressionValidatorCalendar.ErrorMessage = String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LabelYear);
                    RegularExpressionValidatorCalendar.Text = String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LabelYear);
                }
                else if (_format == "MMM yyyy")
                {
                    CalendarExtenderDate.OnClientShown = "c2x.OnDatePickerMonthShown";
                    CalendarExtenderDate.OnClientHidden = "c2x.OnDatePickerMonthHide";

                    RegularExpressionValidatorCalendar.ValidationExpression = "^((ม.ค.)||(ก.พ.)||(มี.ค.)||(เม.ย.)||(พ.ค.)||(มิ.ย.)||(ก.ค.)||(ส.ค.)||(ก.ย.)||(ต.ค.)||(พ.ย.)||(ธ.ค.))(\\s)(([1-9])(\\d{3}))$";
                    RegularExpressionValidatorCalendar.ErrorMessage = String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LabelMonth);
                    RegularExpressionValidatorCalendar.Text = String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LabelMonth);
                }
                else if ((_format == "MM/yyyy") || (_format == "MM yyyy") || (_format == "M/yyyy") || (_format == "M yyyy"))
                {
                    int mIndex = _format.LastIndexOf("M");
                    int delimitIndex = _format.IndexOf("/");
                    String delimit = (delimitIndex > 0) ? "\\/" : "\\s";
                    String regex = (mIndex == 0) ? ("^([1-9]|1[0-2])(" + delimit + ")(([1-9])(\\d{3}))$") : ("^(0[1-9]|1[0-2])(" + delimit + ")(([1-9])(\\d{3}))$");
                    CalendarExtenderDate.OnClientShown = "c2x.OnDatePickerMonthShown";
                    CalendarExtenderDate.OnClientHidden = "c2x.OnDatePickerMonthHide";

                    RegularExpressionValidatorCalendar.ValidationExpression = regex;
                    RegularExpressionValidatorCalendar.ErrorMessage = String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LabelMonth);
                    RegularExpressionValidatorCalendar.Text = String.Format(Nep.Project.Resources.Error.ValidationWrongFormat, Nep.Project.Resources.UI.LabelMonth);
                }

                String placeHolder = GetPlaceHolder(_format);
                TextBoxDate.Attributes.Add("placeholder", placeHolder);
            }
            else
            {
                string f = CalendarExtenderDate.Format;
                String placeHolder = GetPlaceHolder(f);
                TextBoxDate.Attributes.Add("placeholder", placeHolder);
            }

            if(!String.IsNullOrEmpty(this.ID)){
                CalendarExtenderDate.BehaviorID = this.ID;
            }

            if(!String.IsNullOrEmpty(_onClientDateSelectionChanged)){
                CalendarExtenderDate.OnClientDateSelectionChanged = _onClientDateSelectionChanged;
                        
            }

            if(!String.IsNullOrEmpty(_onClientDateTextChanged)){
                TextBoxDate.Attributes.Add("onchange", _onClientDateTextChanged);        
            }
            
            base.OnPreRender(e);
        }
        #endregion

        protected override object SaveControlState()
        {
            object obj = base.SaveControlState();

            return new Object[] { obj, _enableTextBox, _format, _onClientDateSelectionChanged, _onClientDateTextChanged };
        }

        protected override void LoadControlState(object state)
        {
            if (state != null)
            {
                Object[] p = state as Object[];
                if (p[0] != null)
                {
                    base.LoadControlState(p[0]);
                }
                if (p[1] != null)
                {
                    _enableTextBox = (Boolean)p[1];
                }               
                if (p[2] != null)
                {
                    _format = (String)p[2];
                }   
                if(p[3] != null){
                    _onClientDateSelectionChanged = (String)p[3];
                }
                if (p[4] != null)
                {
                    _onClientDateTextChanged = (String)p[4];
                }
                
            }
        }

        private String GetPlaceHolder(String format)
        {
            StringBuilder text = new StringBuilder();
            if (!String.IsNullOrEmpty(format))
            {
                char[] temp = format.ToCharArray();
                char f;
                for (int i = 0; i < temp.Length; i++)
                {
                    f = temp[i];
                    if(f == 'd'){
                        text.Append("ว");
                    }
                    else if (f == 'M')
                    {
                        text.Append("ด");
                    }
                    else if (f == 'y')
                    {
                        text.Append("ป");
                    }
                    else
                    {
                        text.Append(f.ToString());
                    }
                }
            }
           
            return text.ToString();
        }
    }
}