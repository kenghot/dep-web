using System;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
namespace Nep.Project.Web.UserControls
{
    public class IDCardNumberValidator : CustomValidator
    {
        public IDCardNumberValidator()
        {
            this.ClientValidationFunction = "c2x.ClientValidateCitizenIdValidator";
            this.ServerValidate += new ServerValidateEventHandler(CitizenIdValidator_ServerValidate);
            this.Init += new EventHandler(CitizenIdValidator_Init);
        }

        void CitizenIdValidator_Init(object sender, EventArgs e)
        {
        }

        void CitizenIdValidator_ServerValidate(object source, ServerValidateEventArgs arguments)
        {
            String str = (!String.IsNullOrEmpty(arguments.Value))? arguments.Value.Trim() : "";
            str = str.Replace("-", "").Replace("_", "");

            if (str != "")
            {
                Regex validFormat = new Regex(@"^\d{13}$");
                if (!validFormat.IsMatch(str))
                {
                    arguments.IsValid = false;
                }
                else
                {
                    Int32 sum = 0;
                    Int32 checkDigit = str[12] - 48;
                    for (int i = 0; i <= 11; i++)
                    {
                        Int32 digit = str[i] - 48;
                        sum += digit * (13 - i);
                    }
                    Int32 calcDigit = 11 - (sum % 11);
                    if (calcDigit >= 10)
                        calcDigit -= 10;

                    arguments.IsValid = calcDigit == checkDigit;
                }
            }
            else
            {
                arguments.IsValid = true;
            }

            
        }
    }
}
