using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using Nep.Project.Common.Web;


namespace Nep.Project.Web.UserControls
{
    public class ValidationSummary : System.Web.UI.WebControls.ValidationSummary
    {
        /// <summary>
        /// Allows the caller to place custom text messages inside the validation
        /// summary control
        /// </summary>
        /// lt;param name="msg">The message you want to appear in the summary</param>
        public void AddMessage(string Message)
        {
            DummyValidator cv = new DummyValidator();
            cv.ErrorMessage = Message;
            cv.ValidationGroup = this.ValidationGroup;
            this.Page.Validators.Add(cv);
        }

        public void AddMessage(string Message, bool isImportant)
        {
            if (isImportant)
            {
                ValidatorCollection vc = this.Page.Validators;
                for (int i = 0; i < vc.Count; i++)
                {
                    if (vc[i] is DummyValidator)
                    {
                        this.Page.Validators.Remove(vc[i]);
                    }
                }
            }
            this.AddMessage(Message);
        }

        public void AddMessage(List<String> MessageList)
        {
            this.AddMessage(WebUtility.DisplayResultMessageInHTML(MessageList.ToArray()));
        }

        public void AddMessage(string ValidationGroup, string Message)
        {
            this.ValidationGroup = ValidationGroup;
            this.AddMessage(Message);
        }

        public void AddMessage(string ValidationGroup, List<String> MessageList)
        {
            this.AddMessage(ValidationGroup, WebUtility.DisplayResultMessageInHTML(MessageList.ToArray()));
        }
    }

    /// <summary>
    /// The validation summary control works by iterating over the Page.Validators
    /// collection and displaying the ErrorMessage property of each validator
    /// that return false for the IsValid() property.  This class will act 
    /// like all the other validators except it always is invalid and thus the 
    /// ErrorMessage property will always be displayed.
    /// </summary>
    internal class DummyValidator : BaseValidator
    {
        public DummyValidator()
        {
            this.IsValid = false;
        }

        protected override bool EvaluateIsValid()
        {
            return false;
        }
    }
}