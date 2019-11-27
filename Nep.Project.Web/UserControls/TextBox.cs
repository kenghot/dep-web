using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Nep.Project.Web.UserControls
{
    public class TextBox : System.Web.UI.WebControls.TextBox
    {
      
        private const string NUMERIC_TEXTBOX_DATA_ROLE = "numerictextbox";

        public string PlaceHolder { get; set; }
        /// <summary>
        /// NumberFormat is "N"
        /// Example: 123 -> N2 -> 123.00
        /// </summary>
        public string NumberFormat { get; set; }

        public decimal? Min { get; set; }

        public decimal? Max { get; set; }

        public bool IsNumeric { get; set; }

        //public int? Decimal { get; set; }

        protected override void OnInit(EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PlaceHolder))
                this.Attributes.Add("placeholder", PlaceHolder);


            if ((this.TextMode == System.Web.UI.WebControls.TextBoxMode.MultiLine) && (this.MaxLength > 0))
            {
                this.Attributes.Add("maxlength", this.MaxLength.ToString());
                this.Attributes.Add("onkeypress", "c2x.onTextAreaKeypress(event)");
            }
            else if (this.TextMode == System.Web.UI.WebControls.TextBoxMode.Number)
            {
                this.IsNumeric = true;

                if (this.Min.HasValue)
                {
                    this.Attributes.Add("min", this.Min.ToString());
                }

                if (this.Max.HasValue)
                {
                    this.Attributes.Add("max", this.Max.ToString());
                }

                if (String.IsNullOrEmpty(this.NumberFormat))
                {
                    this.NumberFormat = "N2";

                }
                this.Attributes.Add("numberformat", this.NumberFormat);

                this.Attributes.Add("onkeypress", "c2x.onNumberTextBoxKeyPress(event)");
                this.Attributes.Add("onkeyup", "c2x.onNumberTextBoxKeyUp(event)");
                this.Attributes.Add("onfocus", "c2x.onNumberTextBoxFocus(this)");
                this.Attributes.Add("onblur", "c2x.onNumberTextBoxBlur(this)");

                if(!String.IsNullOrEmpty(this.OnClientTextChanged)){
                    this.Attributes.Add("onchange", this.OnClientTextChanged);
                }

                ///override textmode form 'number' to 'text' because the input type number don't support number format and force key number to input tag 
                this.TextMode = System.Web.UI.WebControls.TextBoxMode.SingleLine;
            }

            base.OnInit(e);
        }
        
        protected string _text = "";
        public override string Text
        {
            get
            {
                string text = _text;

                if ((!String.IsNullOrEmpty(text)) && (this.TextMode == System.Web.UI.WebControls.TextBoxMode.Number))
                {
                    text.Replace(",", "");
                }
                return text;
            }
            set
            {
                _text = (!String.IsNullOrEmpty(value))? value.Trim() : "";

                if ((!String.IsNullOrEmpty(_text)) && (this.IsNumeric))
                {
                    Decimal num = 0;
                    Decimal.TryParse(_text, out num);
                    _text = num.ToString(NumberFormat);
                }                
            }
        }

        public string OnClientTextChanged { get; set; }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {

            base.AddAttributesToRender(writer);
        }
        
        protected override void RenderChildren(HtmlTextWriter writer)
        {
                base.RenderChildren(writer);
        }
        
    }
}